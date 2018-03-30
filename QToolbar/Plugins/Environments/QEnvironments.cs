using Microsoft.Web.Administration;
using QToolbar.Helpers;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironments
   {
      #region fields
      private List<QEnvironment> _Data = new List<QEnvironment>();
      #endregion

      #region events
      public delegate void InfoCollectedEventHandler(object sender, EnvInfoEventArgs args);
      public event InfoCollectedEventHandler InfoCollected;
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
            envObj.Name = envName;
            envObj.CheckoutPath = checkoutPath;
            envObj.ProteusCheckoutPath = proteusCheckoutPath;
            envObj.DBCollectionPlusServer = qbcAdminCf.GetServer(envName);
            envObj.DBCollectionPlusName = qbcAdminCf.GetDatabase(envName);
            envObj.ToolkitWSUrl = qbcAdminCf.GetValue("General", "ToolkitWSURL");
            envObj.AppWSUrl = qbcAdminCf.GetValue("General", "ApplicationWSURL");
            envObj.QBCAdminCfPath = qbcAdminCf.File;
         }

         if (string.IsNullOrEmpty(checkoutPath))
         {
            envObj.Errors.AddError($"Checkout path is empty.");
         }
         if (string.IsNullOrEmpty(proteusCheckoutPath))
         {
            envObj.Errors.AddWarning($"Proteus checkout path is empty.");
         }
         // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (string.IsNullOrEmpty(envObj.AppWSUrl))
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(envObj.CheckoutPath));
            envObj.AppWSUrl = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
            if(!string.IsNullOrEmpty(envObj.AppWSUrl))
               envObj.Errors.AddWarning($"ApplicationWSURL was not found in local file ({envObj.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).");
         }
         // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (string.IsNullOrEmpty(envObj.ToolkitWSUrl))
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(envObj.CheckoutPath));
            envObj.ToolkitWSUrl = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
            if (!string.IsNullOrEmpty(envObj.ToolkitWSUrl))
               envObj.Errors.AddWarning($"ToolkitWSURL was not found in local file ({envObj.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).");
         }
         if (adding)
         {
            _Data.Add(envObj);
         }

         // collect rest info
         CollectEnvInfoAsync(envObj.DBCollectionPlusServer, envObj.DBCollectionPlusName, envObj);
      }
      #endregion

      #region private methods
      private async void CollectEnvInfoAsync(string server, string db, QEnvironment env)
      {

         Task<QEnvironment> rs = Task<QEnvironment>.Factory.StartNew(() =>
         {
            QEnvironment retval = new QEnvironment();

            CfFile adminCf = new CfFile(env.QBCAdminCfPath);

            using (SqlConnection con = new SqlConnection())
            {
               con.ConnectionString = Utils.GetConnectionString(server, db);
               try
               {
                  con.Open();
                  SqlCommand com = new SqlCommand();
                  com.Connection = con;
                  // version
                  try
                  {
                     com.CommandText = "SELECT TOP(1) CONVERT(NVARCHAR,MAJOR)+'.' + CONVERT(NVARCHAR,MINOR) FROM TLK_DATABASE_VERSIONS ORDER BY MAJOR DESC,MINOR DESC";
                     retval.DBCollectionPlusVersion = com.ExecuteScalar().ToString();
                  }
                  catch (Exception ex)
                  {
                     retval.Errors.AddError($"Error while fetching version information ({ex.Message})");
                  }
                  // fix AppWSUrl,ToolkitWSUrl for current
                  // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
                  if (string.IsNullOrEmpty(env.AppWSUrl))
                  {
                     string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(retval.DBCollectionPlusVersion));
                     env.AppWSUrl = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
                     env.Errors.AddWarning($"ApplicationWSURL was not found in local file ({env.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).");
                  }
                  // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
                  if (string.IsNullOrEmpty(env.ToolkitWSUrl))
                  {
                     string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(retval.DBCollectionPlusVersion));
                     env.ToolkitWSUrl = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
                     env.Errors.AddWarning($"ToolkitWSURL was not found in local file ({env.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).");
                  }



                  //The location of the GLM folder can be found from the following query:
                  //select inst_root from bi_glm_installation
                  try
                  {
                     com.CommandText = "select inst_root from bi_glm_installation";
                     string glmDir = com.ExecuteScalar().ToString();
                     retval.GLMDir = glmDir;
                     int permissions = -1;
                     bool unresolved = false;
                     retval.GLMLocalDir = Utils.GetPath(glmDir, out permissions, out unresolved);
                     retval.GLMDirPermissions = Utils.GetPermissionsDesc(permissions);
                     if (!string.IsNullOrEmpty(glmDir) && permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                     {
                        retval.Errors.AddError($"Full Access permission is required for GLM dir {glmDir}");
                     }
                     if(unresolved)
                     {
                        retval.Errors.AddError($"Unresolved GLM dir.");
                     }
                     if(string.IsNullOrEmpty(glmDir))
                     {
                        retval.Errors.AddError($"GLM dir is empty.");
                     }


                  }
                  catch (Exception ex)
                  {
                     retval.Errors.AddError($"Error while fetching glm information ({ex.Message})");
                  }

                  //Also the location of the GLM logs folder can be found with:
                  //select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'
                  try
                  {
                     com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'";
                     string glmLogDir = com.ExecuteScalar().ToString();
                     retval.GLMLogDir = glmLogDir;
                     int permissions = -1;
                     bool unresolved = false;
                     retval.GLMLocalLogDir = Utils.GetPath(glmLogDir, out permissions, out unresolved);
                     retval.GLMLogDirPermissions = Utils.GetPermissionsDesc(permissions);
                     if (!string.IsNullOrEmpty(glmLogDir) && permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                     {
                        retval.Errors.AddError($"Full Access permission is required for GLM Log dir {glmLogDir}");
                     }
                     if (unresolved)
                     {
                        retval.Errors.AddError($"Unresolved GLM Log dir.");
                     }
                     if (string.IsNullOrEmpty(glmLogDir))
                     {
                        retval.Errors.AddError($"Empty GLM Log dir");
                     }

                  }
                  catch (Exception ex)
                  {
                     retval.Errors.AddError($"Error while fetching glm log information ({ex.Message})");
                  }

                  try
                  {
                     com.CommandText = @"select SPR_TYPE, SPR_VALUE from AT_SYSTEM_PREF
                                       where SPR_TYPE in ('WORDTEMPLATESFOLDER',
                                                         'ATTACHMENTS_DIRECTORY',
                                                         'BULK_OUTPUT_EXPORT_DIRECTORY', 
                                                         'CRITERIA_PUBLISHED_PATH')";
                     SqlDataAdapter adapter = new SqlDataAdapter(com);
                     DataTable pathsTable = new DataTable();
                     adapter.Fill(pathsTable);
                     StringBuilder builder = new StringBuilder();
                     StringBuilder locbuilder = new StringBuilder();
                     foreach (DataRow pathrow in pathsTable.Rows)
                     {

                        int permissions = -1;
                        bool unresolved = false;
                        //retval.QCSystemSharedDirs.Add(


                        QEnvironment.SharedDir sharedDdir = new QEnvironment.SharedDir()
                        {
                           UNC = pathrow["SPR_VALUE"].ToString(),
                           LocalPath = Utils.GetPath(pathrow["SPR_VALUE"].ToString(), out permissions, out unresolved),
                           Permissions = permissions,
                           Description = pathrow["SPR_TYPE"].ToString()
                        };
                        retval.QCSystemSharedDirs.Add(sharedDdir);

                        if (permissions!=Utils.FILE_PERMISSION_FULL_ACCESS)
                        {
                           retval.Errors.AddError($"Full Access permission is required {sharedDdir.UNC}");
                        }
                        if (unresolved)
                        {
                           retval.Errors.AddError($"Unresolved dir {sharedDdir.Description} : {sharedDdir.UNC}.");
                        }
                        if (string.IsNullOrEmpty(sharedDdir.UNC))
                        {
                           retval.Errors.AddError($"Empty dir {sharedDdir.Description} ");
                        }
                     }
                  }
                  catch (Exception ex)
                  {
                     retval.Errors.AddError($"Error while fetching shared dirs information ({ex.Message})");
                  }
               }
               catch (Exception ex)
               {
                  retval.Errors.AddError($"Generic Error ({ex.Message})");
               }
               finally
               {
                  if (con.State == ConnectionState.Open)
                  {
                     con.Close();
                  }
               }

               try
               {
                  // add cfs from local checkout
                  retval.CFs.Clear();
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

                     retval.CFs.Add(cfInfo);
                  }
               }
               catch (Exception ex)
               {
                  retval.Errors.AddError($"Error getting local cfs information ({ex.Message})");
               }

               try
               {
                  string envNameInWeb = $"QCS_{string.Join("_", Path.GetFileName(env.CheckoutPath).Split('.'))}";

                  // add cfs from web server
                  
                  Uri webServer = new Uri(env.AppWSUrl);
                  using (ServerManager mgr = ServerManager.OpenRemote(webServer.Host))
                  {
                     foreach (var s in mgr.Sites)
                     {
                        if (s.Name.Equals(envNameInWeb))
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
                                 retval.CFs.Add(cfInfo);
                              }


                              var toolkitVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\SCToolkitWS"));
                              if (toolkitVDir != null)
                              {
                                 toolkitPhPath = toolkitVDir.PhysicalPath;
                                 QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                                 cfInfo.Name = "qbc.cf";
                                 cfInfo.Repository = "QC";
                                 cfInfo.Path = $"\\\\{webServer.Host}\\{toolkitPhPath.Replace(":", "$")}\\qbc.cf";
                                 retval.CFs.Add(cfInfo);

                              }
                           }
                        }
                     }
                  }
               }
               catch (Exception ex)
               {
                  retval.Errors.AddError($"Error while fetching web server's cf information ({ex.Message})");
               }

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

                     QEnvironment.CfInfo cfInfoBatch = new QEnvironment.CfInfo();
                     cfInfoBatch.Name = "qbc.cf";
                     cfInfoBatch.Repository = "QC";
                     cfInfoBatch.Path = $"{envFound.BatchServiceUNCPath}\\qbc.cf";
                     retval.CFs.Add(cfInfoBatch);
                     retval.BatchExecutorWinServicePath = envFound.BatchServiceUNCPath;

                     QEnvironment.CfInfo cfInfoEOD = new QEnvironment.CfInfo();
                     cfInfoEOD.Name = "qbc.cf";
                     cfInfoEOD.Repository = "QC";
                     cfInfoEOD.Path = $"{envFound.EODServiceUNCPath}\\qbc.cf";
                     retval.CFs.Add(cfInfoEOD);
                     retval.EodExecutorWinServicePath = envFound.EODServiceUNCPath;
                     retval.WinServicesDir = Directory.GetParent(envFound.EODServiceUNCPath).FullName;
                  }
               }
               catch (Exception ex)
               {
                  retval.Errors.AddError($"Error while fetching Batch Executor's & EOD Executor's  cf information ({ex.Message})");
               }

               retval.CheckoutPath = env.CheckoutPath;
               retval.ProteusCheckoutPath = env.ProteusCheckoutPath;
               retval.DBCollectionPlusServer = env.DBCollectionPlusServer;
               retval.DBCollectionPlusName = env.DBCollectionPlusName;
               retval.ToolkitWSUrl = env.ToolkitWSUrl;
               retval.AppWSUrl = env.AppWSUrl;
               retval.QBCAdminCfPath = env.QBCAdminCfPath;

               // validate cfs
               CfValidator cfValidator = new CfValidator();

               List<string> keys = adminCf.GetKeys(retval.DBCollectionPlusServer, retval.DBCollectionPlusName);
               if(keys.Count != 1)
               {
                  retval.Errors.AddError($"Info not found {retval.DBCollectionPlusServer}.{retval.DBCollectionPlusName}");
               }
               foreach (QEnvironment.CfInfo cfInfo in retval.CFs)
               {
                  retval.Errors.AddRange(cfValidator.Validate(cfInfo.Path,keys));
               }

            };
            return retval;
         });
         await Task.WhenAll(rs).ContinueWith((t) =>
         {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {

               QEnvironment val = rs.Result;

               env.DBCollectionPlusVersion = val.DBCollectionPlusVersion;
               env.GLMDir = val.GLMDir;
               env.GLMLocalDir = val.GLMLocalDir;
               env.GLMDirPermissions = val.GLMDirPermissions;
               env.GLMLogDir = val.GLMLogDir;
               env.GLMLocalLogDir = val.GLMLocalLogDir;
               env.GLMLogDirPermissions = val.GLMLogDirPermissions;

               env.QCSystemSharedDirs.AddRange(val.QCSystemSharedDirs);
               env.CFs.AddRange(val.CFs);
               env.BatchExecutorWinServicePath = val.BatchExecutorWinServicePath;
               env.EodExecutorWinServicePath = val.EodExecutorWinServicePath;
               env.WinServicesDir = val.WinServicesDir;
               env.Errors.AddRange(val.Errors);

               //return val;
               InfoCollected(this, new EnvInfoEventArgs(env));
            });

         });
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
            _Data.Remove(rs[0]);
         }
         else if (rs.Count > 1)
         {
            throw new Exception($"Cannot delete.Multiple environments {envName} found.");
         }
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
