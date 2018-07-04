using Microsoft.Web.Administration;
using QToolbar.Helpers;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironments
   {
      #region fields
      private List<QEnvironment> _Data = new List<QEnvironment>();

      private List<Task> _Tasks = new List<Task>();
      private Dictionary<string, CancellationTokenSource> _CancellationTokenSourceList = new Dictionary<string, CancellationTokenSource>();

      private QEnvironmentParameters _EnvParams = new QEnvironmentParameters();
      #endregion

      #region events
      public delegate void InfoCollectedEventHandler(object sender, EnvInfoEventArgs args);
      public event InfoCollectedEventHandler InfoCollected;
      public event EventHandler AllInfoCollected;
      #endregion

      #region properties
      public List<QEnvironment> Data
      {
         get
         {
            return _Data;
         }
      }


      #endregion


      public QEnvironments()
      {
      }

      #region methods

      public void AddOrUpdate(string envName, CfFile qbcAdminCf, string checkoutPath, string proteusCheckoutPath)
      {
         QEnvironment envObj = null;
         bool adding = false;
         List<QEnvironment> envsFound = _Data.FindAll(e => e.Name == envName);

         if (envsFound.Count == 0)
         {
            // add 
            envObj = new QEnvironment();
            adding = true;
         }
         else if (envsFound.Count == 1)
         {
            // update
            envObj = envsFound[0];
         }
         else if (envsFound.Count > 1)
         {
            throw new Exception($"Found environment {envName} multiple times.");
         }
         if (envObj != null)
         {
            envObj.Status = "Loading...";
            envObj.Name = envName;
            envObj.CheckoutPath = checkoutPath;
            envObj.ProteusCheckoutPath = proteusCheckoutPath;
            envObj.DBCollectionPlusServer = qbcAdminCf.GetServer(envName);
            envObj.DBCollectionPlusName = qbcAdminCf.GetDatabase(envName);
            envObj.ToolkitWSUrl = qbcAdminCf.GetValue("General", "ToolkitWSURL");
            envObj.AppWSUrl = qbcAdminCf.GetValue("General", "ApplicationWSURL");
            if (!string.IsNullOrEmpty(envObj.AppWSUrl))
            {
               Uri AppWSUri = new Uri(envObj.AppWSUrl);
               envObj.AppWSUrlPort = AppWSUri.Port.ToString();
            }
            envObj.QBCAdminCfPath = qbcAdminCf.File;
         }

         if (string.IsNullOrEmpty(checkoutPath))
         {
            envObj.Errors.AddError($"Checkout path is empty.","");
         }
         if (string.IsNullOrEmpty(proteusCheckoutPath))
         {
            envObj.Errors.AddWarning($"Proteus checkout path is empty.","");
         }
         // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (string.IsNullOrEmpty(envObj.AppWSUrl))
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(envObj.CheckoutPath));
            envObj.AppWSUrl = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
            if (!string.IsNullOrEmpty(envObj.AppWSUrl))
               envObj.Errors.AddWarning($"ApplicationWSURL was not found in local file ({envObj.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", envObj.QBCAdminCfPath);
         }
         // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (string.IsNullOrEmpty(envObj.ToolkitWSUrl))
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(envObj.CheckoutPath));
            envObj.ToolkitWSUrl = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
            if (!string.IsNullOrEmpty(envObj.ToolkitWSUrl))
               envObj.Errors.AddWarning($"ToolkitWSURL was not found in local file ({envObj.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", envObj.QBCAdminCfPath);
         }
         if (adding)
         {
            _Data.Add(envObj);
         }

         // collect rest info async
         CollectEnvInfoAsync(envObj.DBCollectionPlusServer, envObj.DBCollectionPlusName, envObj);
      }
      #endregion

      #region private methods
      private async void CollectEnvInfoAsync(string server, string db, QEnvironment env)
      {
         CancellationTokenSource tokenSource = null;

         if (!_CancellationTokenSourceList.ContainsKey(env.Name))
         {
            tokenSource = new CancellationTokenSource();
            _CancellationTokenSourceList.Add(env.Name, tokenSource);
         }
         else
         {
            tokenSource = _CancellationTokenSourceList[env.Name];
         }

         Task<QEnvironment> rs = Task<QEnvironment>.Factory.StartNew(state =>
         {
            QEnvironment objEnv = (QEnvironment)state;

            CancellationToken cancelToken = tokenSource.Token;

            CfFile adminCf = new CfFile(env.QBCAdminCfPath);

            List<string> eodFlows = new List<string>();

            if (!cancelToken.IsCancellationRequested)
            {
               #region Check QBCollection Plus DB

               using (SqlConnection con = new SqlConnection())
               {
                  con.ConnectionString = Utils.GetConnectionString(server, db);
                  
                  try
                  {
                     con.Open();
                     SqlCommand com = new SqlCommand();
                     com.Connection = con;                     

                     #region get version from database 
                     if (!cancelToken.IsCancellationRequested)
                     {
                        // version
                        try
                        {
                           com.CommandText = "SELECT TOP(1) CONVERT(NVARCHAR,MAJOR)+'.' + CONVERT(NVARCHAR,MINOR) FROM TLK_DATABASE_VERSIONS ORDER BY MAJOR DESC,MINOR DESC";
                           objEnv.DBCollectionPlusVersion = com.ExecuteScalar().ToString();
                           objEnv.DBCollectionPlusMajorVersion = objEnv.DBCollectionPlusVersion.Split('.')[0];
                           objEnv.DBCollectionPlusMinorVersion = objEnv.DBCollectionPlusVersion.Split('.')[1];
                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Error while fetching version information ({ex.Message})", "");
                        }
                        // fix AppWSUrl,ToolkitWSUrl for current
                        // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
                        if (string.IsNullOrEmpty(env.AppWSUrl))
                        {
                           string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.DBCollectionPlusVersion));
                           env.AppWSUrl = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
                           env.Errors.AddWarning($"ApplicationWSURL was not found in local file ({env.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", env.QBCAdminCfPath);
                        }
                        // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
                        if (string.IsNullOrEmpty(env.ToolkitWSUrl))
                        {
                           string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.DBCollectionPlusVersion));
                           env.ToolkitWSUrl = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
                           env.Errors.AddWarning($"ToolkitWSURL was not found in local file ({env.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", env.QBCAdminCfPath);
                        }
                     }
                     #endregion

                     #region get system params
                     try
                     {
                        com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'DIALER_DB_NAME'";
                        objEnv.DialerDBName= com.ExecuteScalar().ToString();
                        if (!string.IsNullOrEmpty(objEnv.DialerDBName))
                        {
                           objEnv.DialerServer = objEnv.DBCollectionPlusServer;
                        }
                     }
                     catch(Exception ex)
                     {
                        env.Errors.AddError($"Error retrieving 'DIALER_DB_NAME' from AT_SYSTEM_PARAMS ({ex.Message})", "");
                     }
                     #endregion

                     #region get location of the GLM folder
                     if (!cancelToken.IsCancellationRequested)
                     {
                        //The location of the GLM folder can be found from the following query:
                        //select inst_root from bi_glm_installation
                        try
                        {
                           com.CommandText = @"select *  from bi_glm_installation glm";
                           SqlDataAdapter adapter = new SqlDataAdapter(com);
                           DataTable glmTable = new DataTable();
                           adapter.Fill(glmTable);
                           if (glmTable.Rows.Count == 1)
                           {
                              DataRow glmRow = glmTable.Rows[0];
                              #region check GLM folder
                              objEnv.GLMInstStemName = glmRow["inst_stem_name"].ToString();
                              objEnv.AnalyticsServer = glmRow["qba_server"].ToString();
                              objEnv.AnalyticsDBName = glmRow["qba_db_name"].ToString();
                              objEnv.D3FServer = glmRow["qd3f_server"].ToString();
                              objEnv.D3FDBName = glmRow["qd3f_db_name"].ToString();

                              if (string.IsNullOrEmpty(objEnv.AnalyticsServer))
                              {
                                 objEnv.Errors.AddWarning($"Analytics Server not set (bi_glm_installation table)", "");
                              }
                              if (string.IsNullOrEmpty(objEnv.AnalyticsDBName))
                              {
                                 objEnv.Errors.AddWarning($"Analytics Database not set (bi_glm_installation table)", "");
                              }

                              string glmDir = glmRow["inst_root"].ToString();
                              objEnv.GLMDir = glmDir;
                              int permissions = -1;
                              bool unresolved = false;
                              string glmLocalDir = Utils.GetPath(glmDir, out permissions, out unresolved);
                              objEnv.GLMLocalDir = glmLocalDir;
                              objEnv.GLMDirPermissions = Utils.GetPermissionsDesc(permissions);
                              if (!string.IsNullOrEmpty(glmDir) && permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                              {
                                 objEnv.Errors.AddError($"Full Access permission is required for GLM dir {glmDir}", glmDir);
                              }
                              if (unresolved)
                              {
                                 objEnv.Errors.AddError($"Unresolved GLM dir.", "");
                              }
                              if (string.IsNullOrEmpty(glmDir))
                              {
                                 objEnv.Errors.AddError($"GLM dir is empty.", "");
                              }

                              QEnvironment.SharedDir objGlmDir = new QEnvironment.SharedDir()
                              {
                                 UNC = glmDir,
                                 LocalPath = glmLocalDir,
                                 Permissions = permissions,
                                 Description = "GLM DIR"
                              };
                              objEnv.QCSystemSharedDirs.Add(objGlmDir);
                              #endregion

                              #region check db information of bi_glm_installation table                             
                              // check bi_glm_installation.INST_SERVER
                              if (!glmRow["INST_SERVER"].ToString().ToLower().Equals(objEnv.DBCollectionPlusServer.ToLower()))
                              {
                                 objEnv.Errors.AddError($"bi_glm_installation table : found INST_SERVER={glmRow["INST_SERVER"].ToString()} while expecting {objEnv.DBCollectionPlusServer}.", "");
                              }
                              // check bi_glm_installation.INST_DB_NAME
                              if (!glmRow["INST_DB_NAME"].ToString().ToLower().Equals(objEnv.DBCollectionPlusName.ToLower()))
                              {
                                 objEnv.Errors.AddError($"bi_glm_installation table : found INST_DB_NAME={glmRow["INST_DB_NAME"].ToString()} while expecting {objEnv.DBCollectionPlusName}.", "");
                              }

                              #endregion


                           }
                           else if (glmTable.Rows.Count == 0)
                           {
                              objEnv.Errors.AddError($"bi_glm_installation table is empty.", "");
                           }
                           else
                           {
                              objEnv.Errors.AddError($"More than one rows found in bi_glm_installation table.", "");
                           }
                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Error while fetching glm information ({ex.Message})", "");
                        }
                     }
                     #endregion

                     #region get the location of GLM log folder
                     if (!cancelToken.IsCancellationRequested)
                     {
                        //Also the location of the GLM logs folder can be found with:
                        //select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'
                        try
                        {
                           com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'";
                           string glmLogDir = com.ExecuteScalar().ToString();
                           objEnv.GLMLogDir = glmLogDir;
                           int permissions = -1;
                           bool unresolved = false;
                           string glmLocalLogDir = Utils.GetPath(glmLogDir, out permissions, out unresolved);
                           objEnv.GLMLocalLogDir = glmLocalLogDir;
                           objEnv.GLMLogDirPermissions = Utils.GetPermissionsDesc(permissions);
                           if (!string.IsNullOrEmpty(glmLogDir) && permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                           {
                              objEnv.Errors.AddError($"Full Access permission is required for GLM Log dir {glmLogDir}", glmLogDir);
                           }
                           if (unresolved)
                           {
                              objEnv.Errors.AddError($"Unresolved GLM Log dir.", "");
                           }
                           if (string.IsNullOrEmpty(glmLogDir))
                           {
                              objEnv.Errors.AddError($"Empty GLM Log dir", "");
                           }

                           QEnvironment.SharedDir objGlmLogDir = new QEnvironment.SharedDir()
                           {
                              UNC = glmLogDir,
                              LocalPath = glmLocalLogDir,
                              Permissions = permissions,
                              Description = "GLM LOG DIR"
                           };
                           objEnv.QCSystemSharedDirs.Add(objGlmLogDir);
                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Error while fetching glm log information ({ex.Message})", "");
                        }
                     }
                     #endregion

                     #region check system prefs
                     if (!cancelToken.IsCancellationRequested)
                     {

                        try
                        {
                           com.CommandText = @"select SPR_TYPE, SPR_VALUE from AT_SYSTEM_PREF
                                       where SPR_TYPE in ('WORDTEMPLATESFOLDER',
                                                         'ATTACHMENTS_DIRECTORY',
                                                         'BULK_OUTPUT_EXPORT_DIRECTORY', 
                                                         'CRITERIA_PUBLISHED_PATH',
                                                         'FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL',
                                                         'LEGAL_APP_PROCESS_MAPPING_WS_URL')";
                           SqlDataAdapter adapter = new SqlDataAdapter(com);
                           DataTable pathsTable = new DataTable();
                           adapter.Fill(pathsTable);
                           StringBuilder builder = new StringBuilder();
                           StringBuilder locbuilder = new StringBuilder();
                           foreach (DataRow pathrow in pathsTable.Rows)
                           {


                              #region check shared folders
                              try
                              {
                                 // LEGAL_APP_PROCESS_MAPPING_WS_URL
                                 if (pathrow["SPR_TYPE"].ToString().Equals("LEGAL_APP_PROCESS_MAPPING_WS_URL"))
                                 {
                                    objEnv.Errors.AddInfo($"Check AT_SYSTEM_PREF.SPR_TYPE = LEGAL_APP_PROCESS_MAPPING_WS_URL  ({pathrow["SPR_VALUE"].ToString()}) ", "");

                                    // set LegalAppProcessMappingWSUrl & LegalAppProcessMappingWSHost
                                    if(!string.IsNullOrEmpty(pathrow["SPR_VALUE"].ToString()))
                                    {
                                       Uri uri = new Uri(pathrow["SPR_VALUE"].ToString());
                                       objEnv.LegalAppProcessMappingWSUrl = uri.AbsoluteUri;
                                       if (!string.IsNullOrEmpty(objEnv.LegalAppProcessMappingWSUrl))
                                       {
                                          Uri legalAppWSUri = new Uri(objEnv.LegalAppProcessMappingWSUrl);
                                          objEnv.LegalAppWSUrlPort = legalAppWSUri.Port.ToString();
                                       }
                                       objEnv.LegalAppProcessMappingWSHost = uri.Host;
                                    }
                                 }
                                 // FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL
                                 else if (pathrow["SPR_TYPE"].ToString().Equals("FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL"))
                                 {
                                    if(!objEnv.AppWSUrl.ToLower().Equals(pathrow["SPR_VALUE"].ToString().ToLower()))
                                    {
                                       objEnv.Errors.AddError($"AT_SYSTEM_PREF.SPR_TYPE = FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL  [{pathrow["SPR_VALUE"].ToString()}] not equals to  objEnv.AppWSUrl", "");
                                    }
                                 }
                                 else // shared folders
                                 {
                                    int permissions = -1;
                                    bool unresolved = false;

                                    QEnvironment.SharedDir sharedDir = new QEnvironment.SharedDir()
                                    {
                                       UNC = pathrow["SPR_VALUE"].ToString(),
                                       LocalPath = Utils.GetPath(pathrow["SPR_VALUE"].ToString(), out permissions, out unresolved),
                                       Permissions = permissions,
                                       Description = pathrow["SPR_TYPE"].ToString()
                                    };
                                    objEnv.QCSystemSharedDirs.Add(sharedDir);

                                    if (permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                                    {
                                       objEnv.Errors.AddError($"Full Access permission is required {sharedDir.UNC}", sharedDir.UNC);
                                    }
                                    if (unresolved)
                                    {
                                       objEnv.Errors.AddError($"Unresolved dir {sharedDir.Description} : {sharedDir.UNC}.", sharedDir.UNC);
                                    }
                                    else
                                    {
                                       // take a system sub folder that exists and get system folder
                                       Uri uri = new Uri(sharedDir.UNC);

                                       objEnv.SystemFolder = Directory.GetParent($"\\\\{uri.Host}\\{sharedDir.LocalPath.Replace(":", "$")}").FullName;
                                    }
                                    if (string.IsNullOrEmpty(sharedDir.UNC))
                                    {
                                       objEnv.Errors.AddError($"Empty dir {sharedDir.Description} ", "");
                                    }
                                    // check shared folders names
                                    if (!string.IsNullOrEmpty(sharedDir.UNC) && !string.IsNullOrEmpty(objEnv.DBCollectionPlusVersion))
                                    {
                                       string versionWithUnderscore = env.DBCollectionPlusVersion.Replace('.', '_');
                                       if (!sharedDir.UNC.Contains(versionWithUnderscore))
                                       {
                                          objEnv.Errors.AddWarning($"Shared dir \"{sharedDir.UNC}\" naming convention warning. {versionWithUnderscore} is not contained in dir's name.", "");
                                       }
                                    }
                                 }
                              }
                              catch (Exception ex)
                              {
                                 objEnv.Errors.AddError($"Error while fetching shared dir \"{pathrow["SPR_VALUE"].ToString()}\" information ({ex.Message})", "");
                              }
                              #endregion
                           }
                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Error while fetching shared dirs information ({ex.Message})", "");
                        }
                     }
                     #endregion

                     #region get flows for eod.ini

                     if (!cancelToken.IsCancellationRequested)
                     {

                        try
                        {
                           com.CommandText = @"declare @prefix nvarchar(10)
                                                select @prefix=INST.INST_PREFIX 
                                                from Enterprises ENT 
                                                INNER JOIN AT_INSTALLATIONS INST ON ENT.INST_CODE=INST.INST_CODE

                                                SELECT EOFC_FLOW_NAME
                                                FROM Man_ClientFlows(@prefix)
                                                WHERE EOFC_ACTIVE = 1 ORDER BY FLOW_ORDERING 
                                                ";
                           SqlDataAdapter adapter = new SqlDataAdapter(com);
                           DataTable table = new DataTable();
                           adapter.Fill(table);
                           StringBuilder builder = new StringBuilder();
                           StringBuilder locbuilder = new StringBuilder();
                           foreach (DataRow row in table.Rows)
                           {
                              eodFlows.Add(row["EOFC_FLOW_NAME"].ToString());
                           }
                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Error while fetching flows for eod.ini ({ex.Message})", "");
                        }
                     }
                     #endregion

                     #region check AT_EXTERNAL_COMPANIES.EXTC_IO_DIR field with the appropriate path per external company                      
                     if (!cancelToken.IsCancellationRequested)
                     {
                        StringBuilder b = new StringBuilder();
                        // missing agencies bat file
                        string missingExtfile = Path.Combine(AppInstance.CacheDirectory, $"{objEnv.Name}_ext_agencies_missing.bat");
                        if (File.Exists(missingExtfile))
                        {
                           try
                           {
                              File.Delete(missingExtfile);
                           }
                           catch(Exception ex)
                           {
                              objEnv.Errors.AddWarning($"Cannot delete {missingExtfile}", missingExtfile);
                           }
                        }

                        try
                        {
                           com.CommandText = @"SELECT EXTC_IO_DIR FROM AT_EXTERNAL_COMPANIES";
                           SqlDataAdapter adapter = new SqlDataAdapter(com);
                           DataTable table = new DataTable();
                           adapter.Fill(table);
                           StringBuilder builder = new StringBuilder();
                           foreach (DataRow row in table.Rows)
                           {
                              string extcIODir = string.Empty;
                              try
                              {
                                 extcIODir= row["EXTC_IO_DIR"].ToString();
                                 if (!string.IsNullOrEmpty(extcIODir))
                                 {
                                    if (!Directory.Exists(extcIODir))
                                    {
                                       objEnv.Errors.AddError($"AT_EXTERNAL_COMPANIES.EXTC_IO_DIR \"{extcIODir}\" not found", extcIODir);
                                       b.AppendLine($"mkdir {extcIODir}");
                                    }
                                    if (!Directory.Exists($"{extcIODir}\\HISTORY")) b.AppendLine($"mkdir {extcIODir}\\HISTORY");
                                    if (!Directory.Exists($"{extcIODir}\\IN")) b.AppendLine($"mkdir {extcIODir}\\IN");
                                    if (!Directory.Exists($"{extcIODir}\\OUT")) b.AppendLine($"mkdir {extcIODir}\\OUT");
                                    if ((objEnv.Name.ToLower().Contains("etbn") || objEnv.Name.ToLower().Contains("nbg")) && !Directory.Exists($"{extcIODir}\\REJECT")) b.AppendLine($"mkdir {extcIODir}\\REJECT");
                                 }
                              }
                              catch (Exception ex)
                              {
                                 objEnv.Errors.AddError($"Error while fetching AT_EXTERNAL_COMPANIES.EXTC_IO_DIR \"{extcIODir}\"", extcIODir);
                              }
                           }
                        }
                        catch (Exception ex)
                        {

                        }
                        if(b.Length>0)
                        {
                           
                           File.AppendAllText(missingExtfile, b.ToString());
                           objEnv.Errors.AddInfo($"External Companies Folders are missing, bat file created {missingExtfile}", missingExtfile);
                        }
                     }
                     #endregion
                  }
                  catch (Exception ex)
                  {
                     objEnv.Errors.AddError($"QBCollection Plus Error ({ex.Message})", "");
                  }
                  finally
                  {
                     if (con.State == ConnectionState.Open)
                     {
                        con.Close();
                     }
                  }
                 

                  if (cancelToken.IsCancellationRequested)
                  {
                     Debug.WriteLine($"Task Cancelled {env.Name}");
                  }
               }
               #endregion

               #region Check environment's files & folders

               #region add cfs from local checkout
               if (!cancelToken.IsCancellationRequested)
               {

                  try
                  {
                     // add cfs from local checkout
                     objEnv.CFs.Clear();
                     // add cfs from local checkout
                     foreach (DataRow cfRow in OptionsInstance.EnvCFs.Data.Rows)
                     {
                        QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                        cfInfo.Name = Path.GetFileName(cfRow["Path"].ToString());
                        cfInfo.Repository = Path.GetFileName(cfRow["Repository"].ToString());
                        if (cfInfo.Repository == "QC")
                           cfInfo.Path = Path.Combine(env.CheckoutPath, cfRow["Path"].ToString());
                        else if (cfInfo.Repository == "PROTEUS")
                           cfInfo.Path = Path.Combine(env.ProteusCheckoutPath, cfRow["Path"].ToString());

                        objEnv.CFs.Add(cfInfo);
                     }
                  }
                  catch (Exception ex)
                  {
                     objEnv.Errors.AddError($"Error getting local cfs information ({ex.Message})", "");
                  }
               }
               #endregion

               #region add cfs from qc web server
               if (!cancelToken.IsCancellationRequested)
               {

                  try
                  {                     
                     string envNameInWeb = $"QCS_{objEnv.DBCollectionPlusMajorVersion}_{objEnv.DBCollectionPlusMinorVersion}";

                     // add cfs from web server

                     Uri webServer = new Uri(env.AppWSUrl);
                     using (ServerManager mgr = ServerManager.OpenRemote(webServer.Host))
                     {
                        foreach (var s in mgr.Sites)
                        {
                           try
                           {
                              if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null && (s.Bindings[0]).EndPoint.Port.ToString().Equals(objEnv.AppWSUrlPort) && s.Name.StartsWith(envNameInWeb))
                              {
                                 string qcwsPhPath = null;
                                 string toolkitPhPath = null;
                                 foreach (var a in s.Applications)
                                 {
                                    var qcwsVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\QCSWS"));
                                    if (qcwsVDir != null)
                                    {
                                       qcwsPhPath = qcwsVDir.PhysicalPath;
                                       QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                       cfInfo.Name = "qbc.cf";
                                       cfInfo.Repository = "QC";
                                       cfInfo.Path = $"\\\\{webServer.Host}\\{qcwsPhPath.Replace(":", "$")}\\qbc.cf";
                                       objEnv.CFs.Add(cfInfo);
                                    }


                                    var toolkitVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\SCToolkitWS"));
                                    if (toolkitVDir != null)
                                    {
                                       toolkitPhPath = toolkitVDir.PhysicalPath;
                                       QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                       cfInfo.Name = "qbc.cf";
                                       cfInfo.Repository = "QC";
                                       cfInfo.Path = $"\\\\{webServer.Host}\\{toolkitPhPath.Replace(":", "$")}\\qbc.cf";
                                       objEnv.CFs.Add(cfInfo);

                                    }
                                 }
                              }
                           }
                           catch(Exception ex)
                           {

                           }
                        }

                     }
                  }
                  catch (Exception ex)
                  {
                     objEnv.Errors.AddError($"Error while fetching web server's cf information. IIS Management Console is needed to install. ({ex.Message})", "");
                  }
               }
               #endregion

               #region add cfs from legal web server
               if (!cancelToken.IsCancellationRequested)
               {
                  if (!string.IsNullOrEmpty(objEnv.LegalAppProcessMappingWSHost))
                  {
                     try
                     {
                        string envNameInWeb = $"LegalApp_{objEnv.DBCollectionPlusMajorVersion}_{objEnv.DBCollectionPlusMinorVersion}";

                        // add cfs from legal web server
                        using (ServerManager mgr = ServerManager.OpenRemote(env.LegalAppProcessMappingWSHost))
                        {
                           foreach (var s in mgr.Sites)
                           {
                              try
                              {
                                 if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null && (s.Bindings[0]).EndPoint.Port.ToString().Equals(objEnv.LegalAppWSUrlPort) && s.Name.StartsWith(envNameInWeb))
                                 {
                                    string wsPhysicalPath = null;
                                    foreach (var a in s.Applications)
                                    {
                                       var qcBackOfficeVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\QCSBackOfficeWS"));
                                       if (qcBackOfficeVDir != null)
                                       {
                                          wsPhysicalPath = qcBackOfficeVDir.PhysicalPath;
                                          QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                          cfInfo.Name = "qbc.cf";
                                          cfInfo.Repository = "PROTEUS";
                                          cfInfo.Path = $"\\\\{env.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                          objEnv.CFs.Add(cfInfo);
                                       }

                                       var qcWebCollectionWSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\QCWebCollectionWS"));
                                       if (qcWebCollectionWSVDir != null)
                                       {
                                          wsPhysicalPath = qcWebCollectionWSVDir.PhysicalPath;
                                          QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                          cfInfo.Name = "qbc.cf";
                                          cfInfo.Repository = "PROTEUS";
                                          cfInfo.Path = $"\\\\{env.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                          objEnv.CFs.Add(cfInfo);
                                       }

                                       var SCToolkit2WSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\SCToolkit2WS"));
                                       if (SCToolkit2WSVDir != null)
                                       {
                                          wsPhysicalPath = SCToolkit2WSVDir.PhysicalPath;
                                          QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                          cfInfo.Name = "qbc.cf";
                                          cfInfo.Repository = "PROTEUS";
                                          cfInfo.Path = $"\\\\{env.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                          objEnv.CFs.Add(cfInfo);
                                       }


                                    }
                                 }
                              }
                              catch(Exception ex)
                              {

                              }
                           }
                        }
                     }
                     catch (Exception ex)
                     {
                        objEnv.Errors.AddError($"Error while fetching web server's cf information. IIS Management Console is needed to install. ({ex.Message})", "");
                     }
                  }
               }
               #endregion

               #region add cfs from batch & eod services, check glm inst stem name
               if (!cancelToken.IsCancellationRequested)
               {
                  try
                  {
                     // Add cfs from batch & eod services
                     string ver = Path.GetFileName(env.CheckoutPath);
                     string envConfFile = GetEnvironmentsConfigurationFile(ver);
                     QEnvironmentsConfiguration ec = new QEnvironmentsConfiguration(ver, envConfFile);
                     ec.Load();
                     var envFound = ec.FirstOrDefault(e => e.Database.ToLower() == env.DBCollectionPlusName.ToLower() && e.Server.ToLower() == env.DBCollectionPlusServer.ToLower());
                     if (envFound != null)
                     {
                        // validate environment configuration
                        objEnv.Errors.AddRange(envFound.Validate());
                        // check bi_glm_installetion.inst_stem_name
                        if (!string.IsNullOrEmpty(envFound.GLMPrefix))
                        {
                           if (!envFound.GLMPrefix.ToLower().Equals(objEnv.GLMInstStemName.ToLower()))
                           {
                              objEnv.Errors.AddError($"BI_GLM_INSTALLATION.INST_STEM_NAME({objEnv.GLMInstStemName}) must equals to <GLMPrefix>({envFound.GLMPrefix}) of EnvironmentsConfiguration.xml", envConfFile);
                           }
                        }

                        // batch executor
                        QEnvironment.CfInfo cfInfoBatch = new QEnvironment.CfInfo();
                        cfInfoBatch.Name = "qbc.cf";
                        cfInfoBatch.Repository = "QC";
                        cfInfoBatch.Path = $"{envFound.BatchServiceUNCPath}\\qbc.cf";
                        objEnv.CFs.Add(cfInfoBatch);
                        objEnv.BatchWinServiceUNC = envFound.BatchServiceUNCPath;
                        objEnv.BatchWinServicePath = envFound.BatchServicePath;

                        QEnvironment.SharedDir objBatchServiceDir = new QEnvironment.SharedDir()
                        {
                           UNC = envFound.BatchServiceUNCPath,
                           LocalPath = envFound.BatchServicePath,
                           Permissions = -1,
                           Description = "BATCH SERVICE"
                        };
                        objEnv.QCSystemSharedDirs.Add(objBatchServiceDir);

                        // EOD executor
                        QEnvironment.CfInfo cfInfoEOD = new QEnvironment.CfInfo();
                        cfInfoEOD.Name = "qbc.cf";
                        cfInfoEOD.Repository = "QC";
                        cfInfoEOD.Path = $"{envFound.EODServiceUNCPath}\\qbc.cf";
                        objEnv.CFs.Add(cfInfoEOD);
                        objEnv.EodWinServiceUNC = envFound.EODServiceUNCPath;
                        objEnv.EodWinServicePath = envFound.EODServicePath;

                        objEnv.WinServicesUNC = Directory.GetParent(envFound.EODServiceUNCPath).FullName;
                        objEnv.WinServicesPath = Directory.GetParent(Directory.GetParent(envFound.EODServicePath).FullName).FullName;


                        QEnvironment.SharedDir objEODServiceDir = new QEnvironment.SharedDir()
                        {
                           UNC = envFound.EODServiceUNCPath,
                           LocalPath = envFound.EODServicePath,
                           Permissions = -1,
                           Description = "EOD SERVICE"
                        };
                        objEnv.QCSystemSharedDirs.Add(objEODServiceDir);
                     }
                     else
                     {
                        objEnv.Errors.AddError($"Environment not found (file:{envConfFile})", envConfFile);
                     }
                  }
                  catch (Exception ex)
                  {
                     objEnv.Errors.AddError($"Error while fetching Batch Executor's & EOD Executor's  cf information ({ex.Message})", "");
                  }
               }
               #endregion

               #region check BatchExecutorService.exe.config
               string batchServiceConfig = $"{objEnv.BatchWinServiceUNC}\\BatchExecutorService.exe.config";
               try
               {
                  if (File.Exists(batchServiceConfig))
                  {
                     XmlDocument doc = new XmlDocument();
                     doc.Load(batchServiceConfig);
                     XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='ServiceName']");
                     if (node != null)
                     {
                        string value = node.ReadString("value");
                        string expected = $"QCS Batch Executor {env.Name}";
                        if (!value.Equals(expected))
                           objEnv.Errors.AddWarning($"QCS Batch Executor service name expected '{expected}' but found '{value}'", batchServiceConfig);
                     }
                     else
                        objEnv.Errors.AddError($"Service Name was not found in batch executor's config file ({batchServiceConfig})", batchServiceConfig);

                     // add file to Other Files
                     objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(batchServiceConfig), Path = batchServiceConfig });
                  }
                  else
                     objEnv.Errors.AddError($"Batch executor config file not found ({batchServiceConfig})", batchServiceConfig);
               }
               catch (Exception ex)
               {
                  objEnv.Errors.AddError($"Error while accessing Batch Executor's config file ({ex.Message})", "");
               }
               #endregion

               #region check EODExecutorService.exe.config
               string eodServiceConfig = $"{objEnv.EodWinServiceUNC}\\EODExecutorService.exe.config";
               try
               {
                  if (File.Exists(batchServiceConfig))
                  {
                     XmlDocument doc = new XmlDocument();
                     doc.Load(eodServiceConfig);
                     XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='ServiceName']");
                     if (node != null)
                     {
                        string value = node.ReadString("value");
                        string expected = $"QCS EOD Executor {env.Name}";
                        if (!value.Equals(expected))
                           objEnv.Errors.AddWarning($"QCS EOD Executor service name expected '{expected}' but found '{value}'", eodServiceConfig);
                     }
                     else
                        objEnv.Errors.AddError($"Service Name was not found in EOD executor's config file ({eodServiceConfig})", eodServiceConfig);

                     // add file to Other Files
                     objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(eodServiceConfig), Path = eodServiceConfig });
                  }
                  else
                     objEnv.Errors.AddError($"EOD executor config file not found ({eodServiceConfig})", eodServiceConfig);


               }
               catch (Exception ex)
               {
                  objEnv.Errors.AddError($"Error while accessing EOD Executor's config file ({ex.Message})", "");
               }
               #endregion

               #region check cmd_commands.txt
               string cmdFile = Path.Combine(objEnv.WinServicesUNC, "cmd_commands.txt");
               if (File.Exists(cmdFile))
               {

                  string content = File.ReadAllText(cmdFile).ToLower();
                  string uninstallBatch = $"\"{objEnv.WinServicesPath}\\Uninstall_Service.bat\" \"{objEnv.BatchWinServicePath}\"";
                  string uninstallEod = $"\"{objEnv.WinServicesPath}\\Uninstall_Service.bat\" \"{objEnv.EodWinServicePath}\"";

                  string installBatch = $"\"{objEnv.WinServicesPath}\\Install_Service.bat\" \"{objEnv.BatchWinServicePath}\"";
                  string installEod = $"\"{objEnv.WinServicesPath}\\Install_Service.bat\" \"{objEnv.EodWinServicePath}\"";

                  if (!content.Contains(uninstallBatch.ToLower())) objEnv.Errors.AddError($"Wrong uninstall Batch Service command. Expected:{uninstallBatch}", cmdFile);
                  if (!content.Contains(uninstallEod.ToLower())) objEnv.Errors.AddError($"Wrong uninstall EOD Service command. Expected:{uninstallEod}", cmdFile);
                  if (!content.Contains(installBatch.ToLower())) objEnv.Errors.AddError($"Wrong install Batch Service command. Expected:{installBatch}", cmdFile);
                  if (!content.Contains(installEod.ToLower())) objEnv.Errors.AddError($"Wrong install EOD Service command. Expected:{installEod}", cmdFile);


                  // add file to Other Files
                  objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(cmdFile), Path = cmdFile });
               }
               else
               {
                  objEnv.Errors.AddError($"File not found: {cmdFile}", cmdFile);
               }
               #endregion

               #region check existence of Install_Service.bat & Uninstall_Service.bat
               string installServiceFile = Path.Combine(objEnv.WinServicesUNC, "Install_Service.bat");
               if (!File.Exists(installServiceFile)) objEnv.Errors.AddError($"File not found \"{installServiceFile}\"", installServiceFile);
               string uninstallServiceFile = Path.Combine(objEnv.WinServicesUNC, "Uninstall_Service.bat");
               if (!File.Exists(uninstallServiceFile)) objEnv.Errors.AddError($"File not found \"{uninstallServiceFile}\"", uninstallServiceFile);

               #endregion

               #region check subfolders of system folder and eod.ini files
               
               string eodExecutorEodIniFile = string.Empty;
               string applicationUpdateEodIniFile = string.Empty;
               if (!string.IsNullOrEmpty(objEnv.SystemFolder) && Directory.Exists(objEnv.SystemFolder))
               {
                  string[] eodExecutorDirs = Directory.GetDirectories(objEnv.SystemFolder, "EOD*Executor");
                  string eodExecutorDir = string.Empty;
                  
                  // EODExecutor dir
                  if (eodExecutorDirs.Length == 1)
                  {
                     eodExecutorDir = eodExecutorDirs[0];
                     eodExecutorEodIniFile = Path.Combine(eodExecutorDir, "eod.ini");
                     if (File.Exists(eodExecutorEodIniFile))
                     {
                        objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = "EOD Executor eod.ini", Path = eodExecutorEodIniFile });
                     }
                     else
                        objEnv.Errors.AddError($"File not found \"{eodExecutorEodIniFile}\"", eodExecutorEodIniFile);

                     // add ...QCS_SystemFolders\INST_X_Y\EODExecutor\qbc.cf
                     if (!string.IsNullOrEmpty(eodExecutorDir))
                     {
                        QEnvironment.CfInfo eodExecutorUpdateCfInfo = new QEnvironment.CfInfo();
                        eodExecutorUpdateCfInfo.Name = "qbc.cf";
                        eodExecutorUpdateCfInfo.Repository = "QC";
                        eodExecutorUpdateCfInfo.Path = $"{eodExecutorDir}\\qbc.cf";
                        objEnv.CFs.Add(eodExecutorUpdateCfInfo);
                     }
                  }
                  else
                     objEnv.Errors.AddError($"Dir not found \"{objEnv.SystemFolder}\\EOD*Executor\"", objEnv.SystemFolder);

                  // ApplicationUpdate dir
                  string applicationUpdateDir = Path.Combine(objEnv.SystemFolder, "ApplicationUpdate");
                  applicationUpdateEodIniFile = Path.Combine(applicationUpdateDir, "eod.ini");
                  if (File.Exists(applicationUpdateEodIniFile))
                  {
                     objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = "Application Update eod.ini", Path = applicationUpdateEodIniFile });
                  }
                  else
                  {
                     objEnv.Errors.AddError($"File not found \"{applicationUpdateEodIniFile}\"", applicationUpdateEodIniFile);
                  }

                  // add ...QCS_SystemFolders\INST_X_Y\ApplicationUpdate\qbc.cf
                  if (!string.IsNullOrEmpty(applicationUpdateDir))
                  {
                     QEnvironment.CfInfo applicationUpdateCfInfo = new QEnvironment.CfInfo();
                     applicationUpdateCfInfo.Name = "qbc.cf";
                     applicationUpdateCfInfo.Repository = "QC";
                     applicationUpdateCfInfo.Path = $"{applicationUpdateDir}\\qbc.cf";
                     objEnv.CFs.Add(applicationUpdateCfInfo);
                  }
                  


                  CheckFolderExistence(applicationUpdateDir, objEnv);
                  CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Attachments"), objEnv);
                  CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Exports"), objEnv);
                  CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "ExternalAgencies"), objEnv);
                  CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Templates"), objEnv);
               }
               else
               {
                  objEnv.Errors.AddError($"System Folder is empty or does not exist ({objEnv.SystemFolder}).", objEnv.SystemFolder);
               }

               #endregion

               #region check eod.ini if out of date
               try
               {
                  string eodIniFileText = File.ReadAllText(eodExecutorEodIniFile);
                  foreach (string name in eodFlows)
                  {
                     if (!eodIniFileText.Contains(name))
                     {
                        objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{eodExecutorEodIniFile}\"))", eodExecutorEodIniFile);
                     }
                  }
               }
               catch (Exception ex)
               {
                  objEnv.Errors.AddError($"Error while reading \"{eodExecutorEodIniFile}\" ({ex.Message})", eodExecutorEodIniFile);
               }
               try
               {
                  string applicationUpdateEodIniFileText = File.ReadAllText(applicationUpdateEodIniFile);
                  foreach (string name in eodFlows)
                  {
                     if (!applicationUpdateEodIniFileText.Contains(name))
                     {
                        objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{applicationUpdateEodIniFile}\"))", applicationUpdateEodIniFile);
                     }
                  }
               }
               catch (Exception ex)
               {
                  objEnv.Errors.AddError($"Error while reading \"{applicationUpdateEodIniFile}\" ({ex.Message})", applicationUpdateEodIniFile);
               }

               #endregion

               #region Validate cfs
               CfValidator cfValidator = new CfValidator();

               List<string> keys = adminCf.GetKeys(objEnv.DBCollectionPlusServer, objEnv.DBCollectionPlusName);
               if (keys.Count == 0)
               {
                  objEnv.Errors.AddError($"Info not found {objEnv.DBCollectionPlusServer}.{objEnv.DBCollectionPlusName}", "");
               }
               foreach (QEnvironment.CfInfo cfInfo in objEnv.CFs)
               {
                  objEnv.Errors.AddRange(cfValidator.Validate(cfInfo.Path, keys));
               }
               #endregion

               #endregion

               #region Check Analytics DB
               if (!cancelToken.IsCancellationRequested)
               {
                  if (!string.IsNullOrEmpty(objEnv.AnalyticsServer) && !string.IsNullOrEmpty(objEnv.AnalyticsServer))
                  {
                     using (SqlConnection con = new SqlConnection())
                     {
                        con.ConnectionString = Utils.GetConnectionString(objEnv.AnalyticsServer, objEnv.AnalyticsDBName);

                        try
                        {
                           con.Open();
                           SqlCommand com = new SqlCommand();
                           com.Connection = con;

                           #region  check ADMIN_WRAPPER_SETTINGS. Fields containing server names must always have brackets
                           if (!cancelToken.IsCancellationRequested)
                           {
                              //The location of the GLM folder can be found from the following query:
                              //select inst_root from bi_glm_installation
                              try
                              {
                                 com.CommandText = @"SELECT * FROM ADMIN_WRAPPER_SETTINGS";
                                 SqlDataAdapter adapter = new SqlDataAdapter(com);
                                 DataTable glmTable = new DataTable();
                                 adapter.Fill(glmTable);
                                 if (glmTable.Rows.Count == 1)
                                 {
                                    #region check brackets at fields storing server name    
                                    CheckBrackets("QBC_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName);
                                    CheckBrackets("DIALER_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName);
                                    CheckBrackets("QBA_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName);
                                    #endregion

                                    #region check fields are not empty and correct                                 
                                    CheckDBValue("QBC_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.DBCollectionPlusServer);
                                    CheckDBValue("QBC_DB_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.DBCollectionPlusName);

                                    CheckDBValue("DIALER_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.DialerServer);
                                    CheckDBValue("DIALER_DB_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.DialerDBName);

                                    CheckDBValue("QBA_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.AnalyticsServer);
                                    CheckDBValue("QBA_LINKED_DB_NAME", glmTable.Rows[0], objEnv, objEnv.AnalyticsServer, objEnv.AnalyticsDBName, objEnv.AnalyticsDBName);

                                    #endregion

                                 }
                                 else if (glmTable.Rows.Count == 0)
                                 {
                                    objEnv.Errors.AddError($"bi_glm_installation table is empty.", "");
                                 }
                                 else
                                 {
                                    objEnv.Errors.AddError($"More than one rows found in bi_glm_installation table.", "");
                                 }
                              }
                              catch (Exception ex)
                              {
                                 objEnv.Errors.AddError($"Error while fetching glm information ({ex.Message})", "");
                              }
                           }
                           #endregion


                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Analytics Error ({ex.Message})", "");
                        }
                        finally
                        {
                           if (con.State == ConnectionState.Open)
                           {
                              con.Close();
                           }
                        }
                     }
                  }
               }
               #endregion

               #region Check D3F DB
               if (!cancelToken.IsCancellationRequested)
               {
                  if (!string.IsNullOrEmpty(objEnv.D3FServer) && !string.IsNullOrEmpty(objEnv.D3FDBName))
                  {
                     using (SqlConnection con = new SqlConnection())
                     {
                        con.ConnectionString = Utils.GetConnectionString(objEnv.D3FServer, objEnv.D3FDBName);

                        try
                        {
                           con.Open();
                           SqlCommand com = new SqlCommand();
                           com.Connection = con;

                           #region  check ADMIN_WRAPPER_SETTINGS. Fields containing server names must always have brackets
                           if (!cancelToken.IsCancellationRequested)
                           {
                              //The location of the GLM folder can be found from the following query:
                              //select inst_root from bi_glm_installation
                              try
                              {
                                 com.CommandText = @"SELECT * FROM ADMIN_WRAPPER_SETTINGS";
                                 SqlDataAdapter adapter = new SqlDataAdapter(com);
                                 DataTable glmTable = new DataTable();
                                 adapter.Fill(glmTable);
                                 if (glmTable.Rows.Count == 1)
                                 {
                                    #region check brackets at fields storing server name    
                                    CheckBrackets("QBC_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName);
                                    CheckBrackets("D3F_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName);
                                    CheckBrackets("DIALER_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName);
                                    CheckBrackets("QBA_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName);
                                    #endregion

                                    #region check fields are not empty and correct                                 
                                    CheckDBValue("QBC_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.DBCollectionPlusServer);
                                    CheckDBValue("QBC_DB_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.DBCollectionPlusName);
                                    CheckDBValue("QBA_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.AnalyticsServer);
                                    CheckDBValue("QBA_LINKED_DB_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.AnalyticsDBName);
                                    CheckDBValue("DIALER_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.DialerServer);
                                    CheckDBValue("DIALER_DB_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.DialerDBName);
                                    CheckDBValue("D3F_LINKED_SERVER_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.D3FServer);
                                    CheckDBValue("D3F_LINKED_DB_NAME", glmTable.Rows[0], objEnv, objEnv.D3FServer, objEnv.D3FDBName, objEnv.D3FDBName);

                                    #endregion

                                 }
                                 else if (glmTable.Rows.Count == 0)
                                 {
                                    objEnv.Errors.AddError($"bi_glm_installation table is empty.", "");
                                 }
                                 else
                                 {
                                    objEnv.Errors.AddError($"More than one rows found in bi_glm_installation table.", "");
                                 }
                              }
                              catch (Exception ex)
                              {
                                 objEnv.Errors.AddError($"Error while fetching glm information ({ex.Message})", "");
                              }
                           }
                           #endregion


                        }
                        catch (Exception ex)
                        {
                           objEnv.Errors.AddError($"Analytics Error ({ex.Message})", "");
                        }
                        finally
                        {
                           if (con.State == ConnectionState.Open)
                           {
                              con.Close();
                           }
                        }
                     }
                  }
               }
               #endregion


            };
            return objEnv;
         }, env, tokenSource.Token);

         _Tasks.Add(rs);

         await Task.WhenAll(rs).ContinueWith((t) =>
         {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
               OnInfoCollected(new EnvInfoEventArgs(env));
            });
         });

         // when all tasks are completed raise AllInfoCollected event
         await Task.Factory.ContinueWhenAll(_Tasks.ToArray(),
            (z) =>
            {
               AllInfoCollected(this, new EventArgs());
            });
      }

      #region validation
      private void CheckBrackets(string fieldName, DataRow glmRow, QEnvironment env, string server, string db)
      {
         if (!string.IsNullOrEmpty(glmRow[fieldName].ToString()))
         {
            if (!glmRow[fieldName].ToString().StartsWith("[") || !glmRow[fieldName].ToString().EndsWith("]"))
            {
               env.Errors.AddError($"{server}.{server} field \"{fieldName}\" should be enclosed in brackets", "");
            }
         }
      }

      private void CheckDBValue(string fieldName, DataRow glmRow, QEnvironment env, string server, string db, string comparissonValue)
      {
         if (!string.IsNullOrEmpty(glmRow[fieldName].ToString()))
         {
            
            if (!glmRow[fieldName].ToString().ToLower().Replace("[", "").Replace("]", "").Equals(comparissonValue.ToLower()))
            {               
               env.Errors.AddError($"{server}.{db} field \"{fieldName}\" ({glmRow[fieldName].ToString().Replace("[", "").Replace("]","")}) should be {comparissonValue}", "");
            }
         }
         else
         {
            if (!string.IsNullOrEmpty(comparissonValue))
            {
               env.Errors.AddError($"{server}.{db} field \"{fieldName}\" is empty", "");
            }
         }

      }

#endregion

      private void CheckFolderExistence(string path, QEnvironment env)
      {
         if(!Directory.Exists(path))
         {
            env.Errors.AddError($"Folder not found \"{path}\"", path);
         }
      }

      private void CheckFolderExistence(string path, string pattern, QEnvironment env)
      {
         string[] subDirs = Directory.GetDirectories(path, pattern);

         if (subDirs.Length == 0)
         {
            env.Errors.AddError($"Folder not found \"{path}\\{pattern}\"", path);
         }
      }


      private void OnInfoCollected(EnvInfoEventArgs args)
      {
         if (InfoCollected != null)
         {
            InfoCollected(this, args);
         }
      }

      private void OnAllInfoCollected(EventArgs args)
      {
         if (AllInfoCollected != null)
         {
            AllInfoCollected(this, args);
         }
      }


      public void Refresh()
      {
         string envName = string.Empty;
         CfFile cf = new CfFile(string.Empty);
         string checkoutPath = string.Empty;
         string proteusCheckoutPath = string.Empty;
         QEnvironment item = null;
         for (int i = 0; i < _Data.Count; i++)
         {
            item = _Data[i];
            envName = item.Name;
            cf.File = item.QBCAdminCfPath;
            checkoutPath = item.CheckoutPath;
            proteusCheckoutPath = item.ProteusCheckoutPath;

            item.Clear();
            item.Name = envName;

            AddOrUpdate(envName, cf, checkoutPath, proteusCheckoutPath);
            Debug.WriteLine(envName);
         }

      }

      private string GetRemoteQBCAdminFile(string version)
      {
         string retVal = string.Empty;
         string qcsAdminCFDir = Path.Combine(OptionsInstance.QCSAdminFolder, version, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            string destDir = "";
            Version maxVersion = new Version(0, 0, 0, 0);
            foreach (string dir in subdirs)
            {
               Version curVersion = Utils.GetVersion(dir, "_", 1);

               if (curVersion != null)
               {
                  if (maxVersion < curVersion)
                  {
                     maxVersion = curVersion;
                     destDir = dir;
                  }
               }
               else
               {

               }
            }
            retVal = Path.Combine(destDir, "QBC_Admin.cf.deploy");
            if (!File.Exists(retVal))
               retVal = string.Empty;
         }
         return retVal;
      }

      public void Remove(string envName)
      {
         List<QEnvironment> rs = _Data.FindAll(e => e.Name == envName);
         if (rs.Count == 1)
         {
            CancellationTokenSource cts = _CancellationTokenSourceList.FirstOrDefault(ct => ct.Key == envName).Value;
            if (cts != null)
               cts.Cancel();
            _CancellationTokenSourceList.Remove(envName);
            _Data.Remove(rs[0]);
         }
         else if (rs.Count > 1)
         {
            throw new Exception($"Cannot delete.Multiple environments {envName} found.");
         }

         Debug.WriteLine(_CancellationTokenSourceList.Count);
      }


      public void RemoveEnvsFromCFs()
      {
         for (int i = 0; i < _Data.Count; i++)
         {
            // delete already added cfs
            _Data[i].CFs.Clear();
            // add cfs
            foreach (DataRow cfRow in OptionsInstance.EnvCFs.Data.Rows)
            {
               QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
               cfInfo.Name = Path.GetFileName(cfRow["Path"].ToString());
               cfInfo.Repository = Path.GetFileName(cfRow["Repository"].ToString());
               if (cfInfo.Repository == "QC")
                  cfInfo.Path = Path.Combine(_Data[i].CheckoutPath, cfRow["Path"].ToString());
               else if (cfInfo.Repository == "PROTEUS")
                  cfInfo.Path = Path.Combine(_Data[i].ProteusCheckoutPath, cfRow["Path"].ToString());

               _Data[i].CFs.Add(cfInfo);
            }

         }
      }


      private string GetEnvironmentsConfigurationFile(string version)
      {
         string retval = "";
         string EnvDir = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, version);

         DataRow[] ch = OptionsInstance.Checkouts.Data.Select($"Name='{version}'");
         if (ch.Length == 1)
         {
            string localPath = Path.Combine(ch[0]["Path"].ToString(), "BuildConfiguration");
            if (Directory.Exists(localPath))
            {
               string localFile = Path.Combine(localPath, "EnvironmentsConfiguration.xml");
               DateTime lastLocalWr = File.GetLastWriteTimeUtc(localFile);
               string remFile = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
               DateTime lastRemWr = File.GetLastWriteTimeUtc(remFile);
               if (lastLocalWr > lastRemWr)
               {
                  EnvDir = localPath;
               }
            }
         }
         if (Directory.Exists(EnvDir))
         {
            retval = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
         }
         return retval;
      }
      #endregion

      #region events
      #endregion
   }
}
