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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace QToolbar.Plugins.Environments
{
    public class QEnvironments
    {
        #region consts
        const string ACTIVE_FLOWS_SQL = @"declare @prefix nvarchar(10)
                                        select @prefix=INST.INST_PREFIX 
                                        from Enterprises ENT 
                                        INNER JOIN AT_INSTALLATIONS INST ON ENT.INST_CODE=INST.INST_CODE

                                        SELECT EOFC_FLOW_NAME
                                        FROM Man_ClientFlows(@prefix)
                                        WHERE EOFC_ACTIVE = 1 ORDER BY FLOW_ORDERING 
                                        ";
        #endregion

        #region fields
        private List<QEnvironment> _Data = new List<QEnvironment>();

        private List<Task> _Tasks = new List<Task>();
        private Dictionary<string, CancellationTokenSource> _CancellationTokenSourceList = new Dictionary<string, CancellationTokenSource>();

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

        #region contructors
        public QEnvironments()
        {
        }
        #endregion

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

                envObj.QBCAdminCfPath = qbcAdminCf.File;


                if (string.IsNullOrEmpty(checkoutPath))
                {
                    envObj.Errors.AddError($"Checkout path is empty.", "");
                }
                if (string.IsNullOrEmpty(proteusCheckoutPath))
                {
                    envObj.Errors.AddWarning($"Proteus checkout path is empty.", "");
                }

            }
            if (adding)
            {
                _Data.Add(envObj);
            }

            // collect rest info async
            CollectEnvInfoAsync(envObj);
        }
        #endregion

        #region private methods
        private async void CollectEnvInfoAsync(QEnvironment env)
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

                List<string> eodFlows = new List<string>();

                CfFile adminCf = new CfFile(env.QBCAdminCfPath);

                if (!cancelToken.IsCancellationRequested)
                {
                    // read ProductName from QCSClient project
                    CheckQCSClientProductName(objEnv, cancelToken);

                    // Check QBCollection Plus DB
                    CheckQBCollectionPlusDB(objEnv, eodFlows, cancelToken);

                    // Check environment's files & folders
                    CheckEnvFilesAndFolders(objEnv, adminCf, eodFlows, cancelToken);

                    // Check Analytics DB
                    CheckAnalyticsDB(objEnv, cancelToken);

                    // Check D3F DB
                    CheckD3FDB(objEnv, cancelToken);

                    // CheckVersions
                    CheckVersions(objEnv, cancelToken);

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
                    env.Errors.AddError($"{server}.{db} field \"{fieldName}\" ({glmRow[fieldName].ToString().Replace("[", "").Replace("]", "")}) should be {comparissonValue}", "");
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
            if (!Directory.Exists(path))
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

        #region misc

        private void CheckVersions(QEnvironment objEnv, CancellationToken cancelToken)
        {

            if (!objEnv.BranchVersion.Contains(objEnv.DBCollectionPlusVersion))
            {
                objEnv.Errors.AddError($"Database version {objEnv.DBCollectionPlusVersion} and branch version found in QCSClient ProductName are mismatched.", "");
            }
        }
        private void CheckQCSClientProductName(QEnvironment objEnv, CancellationToken cancelToken)
        {
            // get ProductVersion element value from QCSClient.vbproj
            string prjFile = Path.Combine(objEnv.CheckoutPath, @"VS Projects\QCS\QCSClient\QCSClient.vbproj");
            if (File.Exists(prjFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    XDocument doc = XDocument.Load(prjFile);
                    var res = doc.Descendants()
                        .Where(node => node.Name.LocalName == "PropertyGroup")
                       .Descendants()
                       .Where(node => node.Name.LocalName == "ProductName");
                    objEnv.QCSClientProductName = res.ToList()[0].Value;

                    if (!string.IsNullOrEmpty(objEnv.QCSClientProductName))
                    {
                        Regex reg = new Regex("[0-9]+[.][0-9]+[.]*[0-9]*");
                        Match match = reg.Match(objEnv.QCSClientProductName);
                        if (match.Success)
                        {
                            objEnv.BranchVersion = match.Value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objEnv.Errors.AddError($"Error while getting ProductName tag of \"{prjFile}\".", prjFile);
                }
            }
            else
            {
                objEnv.Errors.AddError($"Could not find \"{prjFile}\"", "");
            }
        }
        #endregion

        #region Check QBCollection Plus DB

        

        private void CheckQBCollectionPlusDB(QEnvironment objEnv, List<string> eodFlows, CancellationToken cancelToken)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Utils.GetConnectionString(objEnv.DBCollectionPlusServer, objEnv.DBCollectionPlusName);

                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;

                    // DBCollectionPlusVersion
                    SetDBCollectionPlusVersion(objEnv, com, cancelToken);


                    // fix AppWSUrl,ToolkitWSUrl for current
                    // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
                    SetWSUrlsFromRemote(objEnv, cancelToken);

                    // olap info
                    GetOlapInfo(objEnv, com, cancelToken);

                    // SetDialerDBNameFromSysPref
                    SetDialerDBNameFromSysPref(objEnv, com, cancelToken);

                    // SetGLMDirInfo
                    SetGLMDirInfo(objEnv, com, cancelToken);

                    // SetGLMLogDirInfo
                    SetGLMLogDirInfo(objEnv, com, cancelToken);

                    // CheckSystemPrefs
                    CheckSystemPrefs(objEnv, com, cancelToken);

                    // CheckSystemParams
                    CheckSystemParams(objEnv, com, cancelToken);

                    // GetFlowsForEodIni
                    GetFlowsForEodIni(objEnv, com, eodFlows, cancelToken);

                    // check AT_EXTERNAL_COMPANIES.EXTC_IO_DIR field with the appropriate path per external company                      
                    CheckExternalCompaniesEXTC_IO_DIR(objEnv, com, cancelToken);
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
                    Debug.WriteLine($"Task Cancelled {objEnv.Name}");
                }
            }
        }

        private void SetDBCollectionPlusVersion(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;
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
        }

        private void SetWSUrlsFromRemote(QEnvironment objEnv, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

            // fix AppWSUrl,ToolkitWSUrl for current
            // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
            if (string.IsNullOrEmpty(objEnv.AppWSUrl))
            {
                string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.BranchVersion));
                objEnv.AppWSUrl = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
                objEnv.Errors.AddWarning($"ApplicationWSURL was not found in local file ({objEnv.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", objEnv.QBCAdminCfPath);
            }
            if (!string.IsNullOrEmpty(objEnv.AppWSUrl))
            {
                Uri AppWSUri = new Uri(objEnv.AppWSUrl);
                objEnv.AppWSUrlPort = AppWSUri.Port.ToString();
            }
            // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
            if (string.IsNullOrEmpty(objEnv.ToolkitWSUrl))
            {
                string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.BranchVersion));
                objEnv.ToolkitWSUrl = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
                objEnv.Errors.AddWarning($"ToolkitWSURL was not found in local file ({objEnv.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", objEnv.QBCAdminCfPath);
            }


        }

        private void SetDialerDBNameFromSysPref(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;
            try
            {
                com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'DIALER_DB_NAME'";
                objEnv.DialerDBName = com.ExecuteScalar().ToString();
                if (!string.IsNullOrEmpty(objEnv.DialerDBName))
                {
                    objEnv.DialerServer = objEnv.DBCollectionPlusServer;
                }
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"Error retrieving 'DIALER_DB_NAME' from AT_SYSTEM_PARAMS ({ex.Message})", "");
            }
        }

        private void SetGLMDirInfo(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

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

        private void SetGLMLogDirInfo(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {

            if (cancelToken.IsCancellationRequested) return;
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


        private void GetOlapInfo(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {

            if (cancelToken.IsCancellationRequested) return;
            try
            {
                com.CommandText = @"-- OLAP DATABASE AND SERVER
                              SELECT SP.SPAR_VALUE, SLPARAM.SLPAR_DESC
                              FROM DBO.BI_GLM_FLOW F 
                              INNER JOIN DBO.BI_GLM_CONTAINER C ON F.FLOW_ID = C.FLOW_ID 
                              INNER JOIN DBO.BI_GLM_PACKAGE P ON C.CNT_ID = P.CNT_ID 
                              INNER JOIN DBO.BI_GLM_STEP S ON P.PACK_ID = S.PACK_ID 
                              INNER JOIN DBO.BI_GLM_STEP_LIB_PARAMS SLPARAM ON SLPARAM.SLIB_ID = S.SLIB_ID
                              INNER JOIN DBO.BI_GLM_STEP_PARAMETERS SP ON S.STEP_ID = SP.STEP_ID AND SP.SPAR_ORDER = SLPARAM.SLPAR_ORDER
                              WHERE SLPAR_DESC IN ('@SERVER_ANL', '@DATABASE_ANL')
                              GROUP BY SP.SPAR_VALUE, SLPARAM.SLPAR_DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable pathsTable = new DataTable();
                adapter.Fill(pathsTable);
                StringBuilder builder = new StringBuilder();
                StringBuilder locbuilder = new StringBuilder();
                string spraValue;
                foreach (DataRow row in pathsTable.Rows)
                {
                    spraValue = row["SPAR_VALUE"].ToString();
                    switch (row["SLPAR_DESC"].ToString())
                    {
                        case "@DATABASE_ANL":
                            objEnv.OlapDatabase = spraValue;
                            break;
                        case "@SERVER_ANL":
                            objEnv.OlapServer = spraValue;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"Error while fetching olap db information ({ex.Message})", "");
            }
        }


        private void CheckSystemParams(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {

            if (cancelToken.IsCancellationRequested) return;
            try
            {
                com.CommandText = @"SELECT * FROM AT_SYSTEM_PARAMS 
                                 WHERE SPRA_TYPE IN ('DIALER_DB_NAME', 'QBC_NAME', 'QBC_SERVER', 'QBC_SERVER', 'FILE_SERVER_NAME', 'SPRA_INSTALLATION_CRITERIA_NAME', 'QBA_NAME', 'QBA_SERVER', 'DB_NAME_ANALYTICS', 'PATH_DATA_TRANSFORMATION_EXECUTABLE','DB_OLAP_DB_NAME')";
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable pathsTable = new DataTable();
                adapter.Fill(pathsTable);
                StringBuilder builder = new StringBuilder();
                StringBuilder locbuilder = new StringBuilder();
                string spraValue;
                foreach (DataRow row in pathsTable.Rows)
                {
                    spraValue = row["SPRA_VALUE"].ToString();
                    switch (row["SPRA_TYPE"].ToString())
                    {
                        case "DB_NAME_ANALYTICS":
                            if (spraValue != objEnv.AnalyticsDBName)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.DB_NAME_ANALYTICS {spraValue} not equals to BI_GLM_INSTALLATION.QBA_db_NAME {objEnv.AnalyticsDBName}", "");
                            }
                            break;
                        case "FILE_SERVER_NAME":
                            break;
                        case "QBA_NAME":
                            if (spraValue != objEnv.AnalyticsDBName)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.QBA_NAME {spraValue} not equals to BI_GLM_INSTALLATION.QBA_db_NAME {objEnv.AnalyticsDBName}", "");
                            }
                            break;
                        case "QBA_SERVER":
                            if (spraValue != objEnv.AnalyticsServer)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.QBA_SERVER {spraValue} not equals to BI_GLM_INSTALLATION.QBA_SERVER {objEnv.AnalyticsServer}", "");
                            }
                            break;
                        case "QBC_NAME":
                            if (spraValue != objEnv.DBCollectionPlusName)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.QBC_NAME {spraValue} not equals to BI_GLM_INSTALLATION.INST_db_NAME {objEnv.DBCollectionPlusName}", "");
                            }
                            break;
                        case "QBC_SERVER":
                            if (spraValue != objEnv.DBCollectionPlusServer)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.QBC_SERVER {spraValue} not equals to BI_GLM_INSTALLATION.INST_SERVER {objEnv.DBCollectionPlusName}", "");
                            }
                            break;
                        case "PATH_DATA_TRANSFORMATION_EXECUTABLE":
                            break;
                        case "SPRA_INSTALLATION_CRITERIA_NAME":
                            break;
                        case "DIALER_DB_NAME":
                            break;
                        case "DB_OLAP_DB_NAME":
                            if (spraValue != objEnv.OlapDatabase)
                            {
                                objEnv.Errors.AddError($"AT_SYSTEM_PARAMS.DB_OLAP_DB_NAME {spraValue} not equals to BI_GLM_STEP_LIB_PARAMS.@DATABASE_ANL {objEnv.OlapDatabase}", "");
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"Error while fetching information from AT_SYSTEM_PARAMS ({ex.Message})", "");
            }
        }


        private void CheckSystemPrefs(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {

            if (cancelToken.IsCancellationRequested) return;
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
                            objEnv.Errors.AddInfo($"Check AT_SYSTEM_PREF.SPR_TYPE = LEGAL_APP_PROCESS_MAPPING_WS_URL  ({pathrow["SPR_VALUE"].ToString()}) ", "",
                               "SELECT * FROM AT_SYSTEM_PREF WHERE SPR_TYPE='LEGAL_APP_PROCESS_MAPPING_WS_URL'");

                            // set LegalAppProcessMappingWSUrl & LegalAppProcessMappingWSHost
                            if (!string.IsNullOrEmpty(pathrow["SPR_VALUE"].ToString()))
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
                            objEnv.AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_URL = pathrow["SPR_VALUE"].ToString();
                            if (!objEnv.AppWSUrl.RemoveAtEnd("/").ToLower().Equals(pathrow["SPR_VALUE"].ToString().RemoveAtEnd("/").ToLower()))
                            {

                                objEnv.Errors.AddError($"AT_SYSTEM_PREF.SPR_TYPE = FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL ({pathrow["SPR_VALUE"].ToString()}) not equals to  objEnv.AppWSUrl ({objEnv.AppWSUrl})", "",
                                   $"SELECT * FROM AT_SYSTEM_PREF WHERE SPR_TYPE='FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL'\r\n--UPDATE AT_SYSTEM_PREF SET SPR_VALUE='{objEnv.AppWSUrl.RemoveAtEnd("/")}' WHERE SPR_TYPE='FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL'");
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
                            if (!string.IsNullOrEmpty(sharedDir.UNC) && !string.IsNullOrEmpty(objEnv.BranchVersion))
                            {
                                string versionWithUnderscore = objEnv.BranchVersion.Replace('.', '_');
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
                objEnv.Errors.AddError($"Error while fetching information FROM AT_SYSTEM_PREF ({ex.Message})", "");
            }
        }

        private void GetFlowsForEodIni(QEnvironment objEnv, SqlCommand com, List<string> eodFlows, CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {

                try
                {
                    com.CommandText = ACTIVE_FLOWS_SQL;
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
        }

        private void CheckExternalCompaniesEXTC_IO_DIR(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
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
                    catch (Exception ex)
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
                            extcIODir = row["EXTC_IO_DIR"].ToString();
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
                if (b.Length > 0)
                {

                    File.AppendAllText(missingExtfile, b.ToString());
                    objEnv.Errors.AddInfo($"External Companies Folders are missing, bat file created {missingExtfile}", missingExtfile);
                }
            }
        }
        #endregion

        #region Check environment's files & folders
        private void CheckEnvFilesAndFolders(QEnvironment objEnv, CfFile adminCf, List<string> eodFlows, CancellationToken cancelToken)
        {
            // add cfs from local checkout
            AddCFsFromLocalCheckout(objEnv, cancelToken);

            // add cfs from qc web server
            AddCFsFromQCWebServer(objEnv, cancelToken);

            // add cfs from legal web server
            AddCFsFromLegalWebServer(objEnv, cancelToken);

            // add cfs from batch & eod services, check glm inst stem name
            AddCFsFromBatchAndEODServices(objEnv, cancelToken);

            // check BatchExecutorService.exe.config
            CheckBatchExecutorServiceExeConfig(objEnv, cancelToken);

            // check EODExecutorService.exe.config
            CheckEODExecutorServiceExeConfig(objEnv, cancelToken);

            // check cmd_commands.txt
            Check_cmd_commands_txt(objEnv, cancelToken);

            // check existence of Install_Service.bat & Uninstall_Service.bat
            CheckExistenceOfWinServicesBatFiles(objEnv, cancelToken);

            // check subfolders of system folder and eod.ini files
            CheckSystemFolderSubfoldersAndEodIniFiles(objEnv, eodFlows, cancelToken);

            // Validate cfs
            ValidateCFs(objEnv, adminCf, cancelToken);
        }

        private void AddCFsFromLocalCheckout(QEnvironment objEnv, CancellationToken cancelToken)
        {
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
                            cfInfo.Path = Path.Combine(objEnv.CheckoutPath, cfRow["Path"].ToString());
                        else if (cfInfo.Repository == "PROTEUS")
                            cfInfo.Path = Path.Combine(objEnv.ProteusCheckoutPath, cfRow["Path"].ToString());

                        objEnv.CFs.Add(cfInfo);
                    }
                }
                catch (Exception ex)
                {
                    objEnv.Errors.AddError($"Error getting local cfs information ({ex.Message})", "");
                }
            }
        }

        private void AddCFsFromQCWebServer(QEnvironment objEnv, CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {

                try
                {
                    string envNameInWeb = $"QCS_{objEnv.DBCollectionPlusMajorVersion}_{objEnv.DBCollectionPlusMinorVersion}";

                    // add cfs from web server

                    Uri webServer = new Uri(objEnv.AppWSUrl);
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
                            catch (Exception ex)
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

        private void AddCFsFromLegalWebServer(QEnvironment objEnv, CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {
                if (!string.IsNullOrEmpty(objEnv.LegalAppProcessMappingWSHost))
                {
                    try
                    {
                        string envNameInWeb = $"LegalApp_{objEnv.DBCollectionPlusMajorVersion}_{objEnv.DBCollectionPlusMinorVersion}";

                        // add cfs from legal web server
                        using (ServerManager mgr = ServerManager.OpenRemote(objEnv.LegalAppProcessMappingWSHost))
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
                                                cfInfo.Path = $"\\\\{objEnv.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                                objEnv.CFs.Add(cfInfo);
                                            }

                                            var qcWebCollectionWSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\QCWebCollectionWS"));
                                            if (qcWebCollectionWSVDir != null)
                                            {
                                                wsPhysicalPath = qcWebCollectionWSVDir.PhysicalPath;
                                                QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                                cfInfo.Name = "qbc.cf";
                                                cfInfo.Repository = "PROTEUS";
                                                cfInfo.Path = $"\\\\{objEnv.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                                objEnv.CFs.Add(cfInfo);
                                            }

                                            var SCToolkit2WSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\SCToolkit2WS"));
                                            if (SCToolkit2WSVDir != null)
                                            {
                                                wsPhysicalPath = SCToolkit2WSVDir.PhysicalPath;
                                                QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                                cfInfo.Name = "qbc.cf";
                                                cfInfo.Repository = "PROTEUS";
                                                cfInfo.Path = $"\\\\{objEnv.LegalAppProcessMappingWSHost}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf";
                                                objEnv.CFs.Add(cfInfo);
                                            }


                                        }
                                    }
                                }
                                catch (Exception ex)
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
        }

        private void AddCFsFromBatchAndEODServices(QEnvironment objEnv, CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    // Add cfs from batch & eod services
                    string ver = Path.GetFileName(objEnv.CheckoutPath);
                    string envConfFile = GetEnvironmentsConfigurationFile(ver);
                    QEnvironmentsConfiguration ec = new QEnvironmentsConfiguration(ver, envConfFile);
                    ec.Load();
                    var envFound = ec.FirstOrDefault(e => e.Database.ToLower() == objEnv.DBCollectionPlusName.ToLower() && e.Server.ToLower() == objEnv.DBCollectionPlusServer.ToLower());
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
        }

        private void CheckBatchExecutorServiceExeConfig(QEnvironment objEnv, CancellationToken cancelToken)
        {
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
                        string expected = $"QCS Batch Executor {objEnv.Name}";
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
        }

        private void CheckEODExecutorServiceExeConfig(QEnvironment objEnv, CancellationToken cancelToken)
        {
            string eodServiceConfig = $"{objEnv.EodWinServiceUNC}\\EODExecutorService.exe.config";
            try
            {
                if (File.Exists(eodServiceConfig))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(eodServiceConfig);
                    XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='ServiceName']");
                    if (node != null)
                    {
                        string value = node.ReadString("value");
                        string expected = $"QCS EOD Executor {objEnv.Name}";
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
        }

        private void Check_cmd_commands_txt(QEnvironment objEnv, CancellationToken cancelToken)
        {
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
        }

        private void CheckExistenceOfWinServicesBatFiles(QEnvironment objEnv, CancellationToken cancelToken)
        {
            string installServiceFile = Path.Combine(objEnv.WinServicesUNC, "Install_Service.bat");
            if (!File.Exists(installServiceFile)) objEnv.Errors.AddError($"File not found \"{installServiceFile}\"", installServiceFile);
            string uninstallServiceFile = Path.Combine(objEnv.WinServicesUNC, "Uninstall_Service.bat");
            if (!File.Exists(uninstallServiceFile)) objEnv.Errors.AddError($"File not found \"{uninstallServiceFile}\"", uninstallServiceFile);
        }

        private void CheckSystemFolderSubfoldersAndEodIniFiles(QEnvironment objEnv, List<string> eodFlows, CancellationToken cancelToken)
        {
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

            #region check eod.ini if out of date
            try
            {
                string eodIniFileText = File.ReadAllText(eodExecutorEodIniFile);
                foreach (string name in eodFlows)
                {
                    if (!eodIniFileText.Contains(name))
                    {
                        objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{eodExecutorEodIniFile}\"))", eodExecutorEodIniFile, ACTIVE_FLOWS_SQL);
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
                        objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{applicationUpdateEodIniFile}\"))", applicationUpdateEodIniFile, ACTIVE_FLOWS_SQL);
                    }
                }
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"Error while reading \"{applicationUpdateEodIniFile}\" ({ex.Message})", applicationUpdateEodIniFile);
            }

            #endregion
        }

        private void ValidateCFs(QEnvironment objEnv, CfFile adminCf, CancellationToken cancelToken)
        {
            CfValidator cfValidator = new CfValidator();
            List<string> keys = adminCf.GetKeys(objEnv.DBCollectionPlusServer, objEnv.DBCollectionPlusName);

            if (keys.Count == 0)
            {
                objEnv.Errors.AddError($"Info not found {objEnv.DBCollectionPlusServer}.{objEnv.DBCollectionPlusName}", "");
            }
            // validate against qbc_admin.cf
            foreach (QEnvironment.CfInfo cfInfo in objEnv.CFs)
            {
                // exclude archive.cf
                if (!cfInfo.Path.Contains("DataArchive"))
                {
                    objEnv.Errors.AddRange(cfValidator.Validate(cfInfo.Path, keys));
                }
            }
        }
        #endregion

        #region Check Analytics DB
        private void CheckAnalyticsDB(QEnvironment objEnv, CancellationToken cancelToken)
        {
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

                            // check ADMIN_WRAPPER_SETTINGS. Fields containing server names must always have brackets
                            CheckAnalyticsAdminWrapperSettings(objEnv, com, cancelToken);
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
        }

        private void CheckAnalyticsAdminWrapperSettings(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
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
        }

        #endregion

        #region Check D3F

        private void CheckD3FDB(QEnvironment objEnv, CancellationToken cancelToken)
        {
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

                            // check ADMIN_WRAPPER_SETTINGS. Fields containing server names must always have brackets
                            CheckD3FAdminWrapperSettings(objEnv, com, cancelToken);
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
        }
        private void CheckD3FAdminWrapperSettings(QEnvironment objEnv, SqlCommand com, CancellationToken cancelToken)
        {
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
        }
        #endregion

        #region event handlers
        #endregion
    }
}
