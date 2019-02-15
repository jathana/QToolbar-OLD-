using Microsoft.Web.Administration;
using QToolbar.Helpers;
using QToolbar.Options;
using QToolbar.Plugins.Environments;
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

namespace QToolbar.Plugins.EnvManager
{
   public class QEnvLoader
   {
      #region fields
      private List<QEnv> _Data = new List<QEnv>();
      private List<Task> _Tasks = new List<Task>();
      private Dictionary<string, CancellationTokenSource> _CancellationTokenSourceList = new Dictionary<string, CancellationTokenSource>();
      #endregion


      public void AddOrUpdate(string envName, CfFile qbcAdminCf, string checkoutPath, string proteusCheckoutPath)
      {
         QEnv envObj = null;
         bool adding = false;
         List<QEnv> envsFound = _Data.FindAll(e => e.Name == envName);
         if (envsFound.Count == 0)
         {
            // add 
            envObj = new QEnv();
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
         if(envObj != null)
         {
            envObj.Status = "Loading...";
            envObj.Name = envName;
            envObj.CheckoutPath = checkoutPath;
            envObj.ProteusCheckoutPath = proteusCheckoutPath;
            envObj.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value = qbcAdminCf.GetServer(envName);
            envObj.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value = qbcAdminCf.GetDatabase(envName);
            envObj.QCS_CLIENT.QBC_ADMIN.TOOLKIT_WS_URL.Value = qbcAdminCf.GetValue("General", "ToolkitWSURL");
            envObj.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.Value = qbcAdminCf.GetValue("General", "ApplicationWSURL");

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

      private async void CollectEnvInfoAsync(QEnv env)
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

         Task<QEnv> rs = Task<QEnv>.Factory.StartNew(state =>
         {
            QEnv objEnv = (QEnv)state;

            CancellationToken cancelToken = tokenSource.Token;

            List<string> eodFlows = new List<string>();

            CfFile adminCf = new CfFile(env.QBCAdminCfPath);

            if (!cancelToken.IsCancellationRequested)
            {
               // load parameters from databases
               QEnvDatabasesLoader dbLoader = new QEnvDatabasesLoader();
               dbLoader.Load(objEnv, cancelToken);


               // Check environment's files & folders
               //CheckEnvFilesAndFolders(objEnv, adminCf, eodFlows, cancelToken);

            };
            return objEnv;
         }, env, tokenSource.Token);

         _Tasks.Add(rs);

         await Task.WhenAll(rs).ContinueWith((t) =>
         {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
               //OnInfoCollected(new EnvInfoEventArgs(env));
            });
         });

         // when all tasks are completed raise AllInfoCollected event
         await Task.Factory.ContinueWhenAll(_Tasks.ToArray(),
            (z) =>
            {
              // AllInfoCollected(this, new EventArgs());
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


      private void SetWSUrlsFromRemote(QEnv objEnv, CancellationToken cancelToken)
      {
         if (cancelToken.IsCancellationRequested) return;

         // fix AppWSUrl,ToolkitWSUrl for current
         // read ApplicationWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (objEnv.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.EmptyValue)
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.VERSION.Value));
            objEnv.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.Value = IniFile2.ReadValue("General", "ApplicationWSURL", remoteQBCAdminCf);
            objEnv.Errors.AddWarning($"ApplicationWSURL was not found in local file ({objEnv.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", objEnv.QBCAdminCfPath);
         }
         // read ToolkitWSURL from OptionsInstance.QCSAdminFolder Folder if it is not in local qbc_admin.cf
         if (objEnv.QCS_CLIENT.QBC_ADMIN.TOOLKIT_WS_URL.EmptyValue)
         {
            string remoteQBCAdminCf = GetRemoteQBCAdminFile(Path.GetFileName(objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.VERSION.Value));
            objEnv.QCS_CLIENT.QBC_ADMIN.TOOLKIT_WS_URL.Value = IniFile2.ReadValue("General", "ToolkitWSURL", remoteQBCAdminCf);
            objEnv.Errors.AddWarning($"ToolkitWSURL was not found in local file ({objEnv.QBCAdminCfPath}).Read from remote ({remoteQBCAdminCf}).", objEnv.QBCAdminCfPath);
         }
      }

      #region Check environment's files & folders
      private void CheckEnvFilesAndFolders(QEnv objEnv, CfFile adminCf, List<string> eodFlows, CancellationToken cancelToken)
      {
         // add cfs from local checkout
         AddCFsFromLocalCheckout(objEnv, cancelToken);

         // add cfs from qc web server
         AddCFsFromQCWebServer(objEnv, cancelToken);

         // add cfs from legal web server
         AddCFsFromLegalWebServer(objEnv, cancelToken);

         // add cfs from batch & eod services, check glm inst stem name
         AddCFsFromBatchAndEODServices(objEnv, cancelToken);

         //// check BatchExecutorService.exe.config
         //CheckBatchExecutorServiceExeConfig(objEnv, cancelToken);

         //// check EODExecutorService.exe.config
         //CheckEODExecutorServiceExeConfig(objEnv, cancelToken);

         //// check cmd_commands.txt
         //Check_cmd_commands_txt(objEnv, cancelToken);

         //// check existence of Install_Service.bat & Uninstall_Service.bat
         //CheckExistenceOfWinServicesBatFiles(objEnv, cancelToken);

         //// check subfolders of system folder and eod.ini files
         //CheckSystemFolderSubfoldersAndEodIniFiles(objEnv, eodFlows, cancelToken);

         //// Validate cfs
         //ValidateCFs(objEnv, adminCf, cancelToken);
      }

      private void AddCFsFromLocalCheckout(QEnv objEnv, CancellationToken cancelToken)
      {
         if (!cancelToken.IsCancellationRequested)
         {

            try
            {
               // add cfs from local checkout
               //objEnv.CFs.Clear();
               // add cfs from local checkout
               foreach (DataRow cfRow in OptionsInstance.EnvCFs.Data.Rows)
               {
                  QEnvFileProperty prop = new QEnvFileProperty()
                  {
                     Category = QEnvPropCategory.Files,
                     SubCategory = QEnvPropSubCategory.CFFile,
                     Origin = Path.GetFileName(cfRow["Repository"].ToString()),
                  };
                  if (prop.Origin == "QC")
                     prop.Value = Path.Combine(objEnv.CheckoutPath, cfRow["Path"].ToString());
                  else if (prop.Origin == "PROTEUS")
                     prop.Value = Path.Combine(objEnv.ProteusCheckoutPath, cfRow["Path"].ToString());

                  objEnv.Properties.Add(prop);
               }
            }
            catch (Exception ex)
            {
               objEnv.Errors.AddError($"Error getting local cfs information ({ex.Message})", "");
            }
         }
      }

      private void AddCFsFromQCWebServer(QEnv objEnv, CancellationToken cancelToken)
      {
         if (!cancelToken.IsCancellationRequested)
         {

            try
            {
               string envNameInWeb = $"QCS_{objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.MAJOR.Value}_{objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.MINOR.Value}";

               // add cfs from web server

               Uri webServer = new Uri(objEnv.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.Value);
               using (ServerManager mgr = ServerManager.OpenRemote(webServer.Host))
               {
                  foreach (var s in mgr.Sites)
                  {
                     try
                     {
                        if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null && (s.Bindings[0]).EndPoint.Port.ToString().Equals(objEnv.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.Port) && s.Name.StartsWith(envNameInWeb))
                        {
                           string qcwsPhPath = null;
                           string toolkitPhPath = null;
                           foreach (var a in s.Applications)
                           {
                              var qcwsVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\QCSWS"));
                              if (qcwsVDir != null)
                              {
                                 qcwsPhPath = qcwsVDir.PhysicalPath;
                                 objEnv.Properties.Add(new QEnvFileProperty()
                                 {
                                    Name = "qbc.cf",
                                    Origin = "QC",
                                    Value = $"\\\\{webServer.Host}\\{qcwsPhPath.Replace(":", "$")}\\qbc.cf",
                                 });
                              }


                              var toolkitVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\SCToolkitWS"));
                              if (toolkitVDir != null)
                              {
                                 toolkitPhPath = toolkitVDir.PhysicalPath;
                                 objEnv.Properties.Add(new QEnvFileProperty()
                                 {
                                    Name = "qbc.cf",
                                    Origin = "QC",
                                    Value = $"\\\\{webServer.Host}\\{toolkitPhPath.Replace(":", "$")}\\qbc.cf",
                                 });

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

      private void AddCFsFromLegalWebServer(QEnv objEnv, CancellationToken cancelToken)
      {
         if (!cancelToken.IsCancellationRequested)
         {
            if (!string.IsNullOrEmpty(objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Host))
            {
               try
               {
                  string envNameInWeb = $"LegalApp_{objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.MAJOR.Value}_{objEnv.QBC.PSet_TLK_DATABASE_VERSIONS.MINOR.Value}";

                  // add cfs from legal web server
                  using (ServerManager mgr = ServerManager.OpenRemote(objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Host))
                  {
                     foreach (var s in mgr.Sites)
                     {
                        try
                        {
                           if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null && (s.Bindings[0]).EndPoint.Port.ToString().Equals(objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Port) && s.Name.StartsWith(envNameInWeb))
                           {
                              string wsPhysicalPath = null;
                              foreach (var a in s.Applications)
                              {
                                 var qcBackOfficeVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\QCSBackOfficeWS"));
                                 if (qcBackOfficeVDir != null)
                                 {
                                    wsPhysicalPath = qcBackOfficeVDir.PhysicalPath;
                                    objEnv.Properties.Add(new QEnvFileProperty()
                                    {
                                       Name = "qbc.cf",
                                       Origin = "PROTEUS",
                                       Value = $"\\\\{objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Host}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf",
                                    });

                                 }

                                 var qcWebCollectionWSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\QCWebCollectionWS"));
                                 if (qcWebCollectionWSVDir != null)
                                 {
                                    wsPhysicalPath = qcWebCollectionWSVDir.PhysicalPath;
                                    objEnv.Properties.Add(new QEnvFileProperty()
                                    {
                                       Name = "qbc.cf",
                                       Origin = "PROTEUS",
                                       Value = $"\\\\{objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Host}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf",
                                    });
                                 }

                                 var SCToolkit2WSVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\SCToolkit2WS"));
                                 if (SCToolkit2WSVDir != null)
                                 {
                                    wsPhysicalPath = SCToolkit2WSVDir.PhysicalPath;
                                    objEnv.Properties.Add(new QEnvFileProperty()
                                    {
                                       Name = "qbc.cf",
                                       Origin = "PROTEUS",
                                       Value = $"\\\\{objEnv.QBC.PSet_AT_SYSTEM_PREF.LEGAL_APP_PROCESS_MAPPING_WS_URL.Host}\\{wsPhysicalPath.Replace(":", "$")}\\qbc.cf",
                                    });
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

      private void AddEnvironmentsConfiguration(QEnvironmentConfiguration qenvConf, QEnv objEnv)
      {
         if (qenvConf!=null)
         {
            objEnv.ENV_CONF.ENV.Name.Value = qenvConf.Name;
            objEnv.ENV_CONF.ENV.Server.Value = qenvConf.Server;
            objEnv.ENV_CONF.ENV.Database.Value = qenvConf.Database;
            objEnv.ENV_CONF.ENV.Username.Value = qenvConf.Username;
            objEnv.ENV_CONF.ENV.Password.Value = qenvConf.Password;
            objEnv.ENV_CONF.ENV.IsWinAuth.Value = qenvConf.IsWinAuth;
            objEnv.ENV_CONF.ENV.GLMPrefix.Value = qenvConf.GLMPrefix;
            objEnv.ENV_CONF.ENV.ConnectorRepositoryRoot.Value = qenvConf.ConnectorRepositoryRoot;
            objEnv.ENV_CONF.ENV.ConnectorBranch.Value = qenvConf.ConnectorBranch;
            objEnv.ENV_CONF.ENV.EoDMonitorAlerts.Value = qenvConf.EoDMonitorAlerts;
            objEnv.ENV_CONF.ENV.BatchServiceName.Value = qenvConf.BatchServiceName;
            objEnv.ENV_CONF.ENV.BatchServicePath.Value = qenvConf.BatchServicePath;
            objEnv.ENV_CONF.ENV.BatchServiceServer.Value = qenvConf.BatchServiceServer;
            objEnv.ENV_CONF.ENV.BatchServiceUNCPath.Value = qenvConf.BatchServiceUNCPath;
            objEnv.ENV_CONF.ENV.EODServiceName.Value = qenvConf.EODServiceName;
            objEnv.ENV_CONF.ENV.EODServicePath.Value = qenvConf.EODServicePath;
            objEnv.ENV_CONF.ENV.EODServiceServer.Value = qenvConf.EODServiceServer;
            objEnv.ENV_CONF.ENV.EODServiceUNCPath.Value = qenvConf.EODServiceUNCPath;
            objEnv.ENV_CONF.ENV.EnvironmentFlavor.Value = qenvConf.EnvironmentFlavor;
            objEnv.ENV_CONF.ENV.AnalyticsServer.Value = qenvConf.AnalyticsServer;
            objEnv.ENV_CONF.ENV.AnalyticsDatabase.Value = qenvConf.AnalyticsDatabase;
            objEnv.ENV_CONF.ENV.OLAPServer.Value = qenvConf.OLAPServer;
            objEnv.ENV_CONF.ENV.STDConnector.Value = qenvConf.STDConnector;
            objEnv.ENV_CONF.ENV.SynchConnectors.Value = qenvConf.SynchConnectors;
            objEnv.ENV_CONF.ENV.CloneConnectors.Value = qenvConf.CloneConnectors;

         }
      }

      private void AddCFsFromBatchAndEODServices(QEnv objEnv, CancellationToken cancelToken)
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
               var envFound = ec.FirstOrDefault(e => e.Database.ToLower() == objEnv.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value.ToLower() && e.Server.ToLower() == objEnv.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value.ToLower());
               if (envFound != null)
               {
                  // validate environment configuration
                  objEnv.Errors.AddRange(envFound.Validate());

                  // get enviroment data
                  AddEnvironmentsConfiguration(envFound, objEnv);

                  // batch executor qbc.cf
                  objEnv.Properties.Add(new QEnvFileProperty()
                  {
                     Name = "qbc.cf",
                     Origin = "QC",
                     Value = $"{envFound.BatchServiceUNCPath}\\qbc.cf",

                  });

                  // EOD executor
                  objEnv.Properties.Add(new QEnvFileProperty()
                  {
                     Name = "qbc.cf",
                     Origin = "QC",
                     Value = $"{envFound.EODServiceUNCPath}\\qbc.cf"
                  });

                  objEnv.ENV_CONF.ENV.WinServicesUNC.Value = Directory.GetParent(envFound.EODServiceUNCPath).FullName;
                  objEnv.ENV_CONF.ENV.WinServicesPath.Value = Directory.GetParent(Directory.GetParent(envFound.EODServicePath).FullName).FullName;
                                    
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

      //private void CheckBatchExecutorServiceExeConfig(QEnv objEnv, CancellationToken cancelToken)
      //{
      //   string batchServiceConfig = $"{objEnv.BatchWinServiceUNC}\\BatchExecutorService.exe.config";
      //   try
      //   {
      //      if (File.Exists(batchServiceConfig))
      //      {
      //         XmlDocument doc = new XmlDocument();
      //         doc.Load(batchServiceConfig);
      //         XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='ServiceName']");
      //         if (node != null)
      //         {
      //            string value = node.ReadString("value");
      //            string expected = $"QCS Batch Executor {objEnv.Name}";
      //            if (!value.Equals(expected))
      //               objEnv.Errors.AddWarning($"QCS Batch Executor service name expected '{expected}' but found '{value}'", batchServiceConfig);
      //         }
      //         else
      //            objEnv.Errors.AddError($"Service Name was not found in batch executor's config file ({batchServiceConfig})", batchServiceConfig);

      //         // add file to Other Files
      //         objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(batchServiceConfig), Path = batchServiceConfig });
      //      }
      //      else
      //         objEnv.Errors.AddError($"Batch executor config file not found ({batchServiceConfig})", batchServiceConfig);
      //   }
      //   catch (Exception ex)
      //   {
      //      objEnv.Errors.AddError($"Error while accessing Batch Executor's config file ({ex.Message})", "");
      //   }
      //}

      //private void CheckEODExecutorServiceExeConfig(QEnv objEnv, CancellationToken cancelToken)
      //{
      //   string eodServiceConfig = $"{objEnv.EodWinServiceUNC}\\EODExecutorService.exe.config";
      //   try
      //   {
      //      if (File.Exists(eodServiceConfig))
      //      {
      //         XmlDocument doc = new XmlDocument();
      //         doc.Load(eodServiceConfig);
      //         XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='ServiceName']");
      //         if (node != null)
      //         {
      //            string value = node.ReadString("value");
      //            string expected = $"QCS EOD Executor {objEnv.Name}";
      //            if (!value.Equals(expected))
      //               objEnv.Errors.AddWarning($"QCS EOD Executor service name expected '{expected}' but found '{value}'", eodServiceConfig);
      //         }
      //         else
      //            objEnv.Errors.AddError($"Service Name was not found in EOD executor's config file ({eodServiceConfig})", eodServiceConfig);

      //         // add file to Other Files
      //         objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(eodServiceConfig), Path = eodServiceConfig });
      //      }
      //      else
      //         objEnv.Errors.AddError($"EOD executor config file not found ({eodServiceConfig})", eodServiceConfig);


      //   }
      //   catch (Exception ex)
      //   {
      //      objEnv.Errors.AddError($"Error while accessing EOD Executor's config file ({ex.Message})", "");
      //   }
      //}

      //private void Check_cmd_commands_txt(QEnvironment objEnv, CancellationToken cancelToken)
      //{
      //   string cmdFile = Path.Combine(objEnv.WinServicesUNC, "cmd_commands.txt");
      //   if (File.Exists(cmdFile))
      //   {

      //      string content = File.ReadAllText(cmdFile).ToLower();
      //      string uninstallBatch = $"\"{objEnv.WinServicesPath}\\Uninstall_Service.bat\" \"{objEnv.BatchWinServicePath}\"";
      //      string uninstallEod = $"\"{objEnv.WinServicesPath}\\Uninstall_Service.bat\" \"{objEnv.EodWinServicePath}\"";

      //      string installBatch = $"\"{objEnv.WinServicesPath}\\Install_Service.bat\" \"{objEnv.BatchWinServicePath}\"";
      //      string installEod = $"\"{objEnv.WinServicesPath}\\Install_Service.bat\" \"{objEnv.EodWinServicePath}\"";

      //      if (!content.Contains(uninstallBatch.ToLower())) objEnv.Errors.AddError($"Wrong uninstall Batch Service command. Expected:{uninstallBatch}", cmdFile);
      //      if (!content.Contains(uninstallEod.ToLower())) objEnv.Errors.AddError($"Wrong uninstall EOD Service command. Expected:{uninstallEod}", cmdFile);
      //      if (!content.Contains(installBatch.ToLower())) objEnv.Errors.AddError($"Wrong install Batch Service command. Expected:{installBatch}", cmdFile);
      //      if (!content.Contains(installEod.ToLower())) objEnv.Errors.AddError($"Wrong install EOD Service command. Expected:{installEod}", cmdFile);


      //      // add file to Other Files
      //      objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = Path.GetFileName(cmdFile), Path = cmdFile });
      //   }
      //   else
      //   {
      //      objEnv.Errors.AddError($"File not found: {cmdFile}", cmdFile);
      //   }
      //}

      //private void CheckExistenceOfWinServicesBatFiles(QEnvironment objEnv, CancellationToken cancelToken)
      //{
      //   string installServiceFile = Path.Combine(objEnv.WinServicesUNC, "Install_Service.bat");
      //   if (!File.Exists(installServiceFile)) objEnv.Errors.AddError($"File not found \"{installServiceFile}\"", installServiceFile);
      //   string uninstallServiceFile = Path.Combine(objEnv.WinServicesUNC, "Uninstall_Service.bat");
      //   if (!File.Exists(uninstallServiceFile)) objEnv.Errors.AddError($"File not found \"{uninstallServiceFile}\"", uninstallServiceFile);
      //}

      //private void CheckSystemFolderSubfoldersAndEodIniFiles(QEnvironment objEnv, List<string> eodFlows, CancellationToken cancelToken)
      //{
      //   string eodExecutorEodIniFile = string.Empty;
      //   string applicationUpdateEodIniFile = string.Empty;
      //   if (!string.IsNullOrEmpty(objEnv.SystemFolder) && Directory.Exists(objEnv.SystemFolder))
      //   {
      //      string[] eodExecutorDirs = Directory.GetDirectories(objEnv.SystemFolder, "EOD*Executor");
      //      string eodExecutorDir = string.Empty;

      //      // EODExecutor dir
      //      if (eodExecutorDirs.Length == 1)
      //      {
      //         eodExecutorDir = eodExecutorDirs[0];
      //         eodExecutorEodIniFile = Path.Combine(eodExecutorDir, "eod.ini");
      //         if (File.Exists(eodExecutorEodIniFile))
      //         {
      //            objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = "EOD Executor eod.ini", Path = eodExecutorEodIniFile });
      //         }
      //         else
      //            objEnv.Errors.AddError($"File not found \"{eodExecutorEodIniFile}\"", eodExecutorEodIniFile);

      //         // add ...QCS_SystemFolders\INST_X_Y\EODExecutor\qbc.cf
      //         if (!string.IsNullOrEmpty(eodExecutorDir))
      //         {
      //            QEnvironment.CfInfo eodExecutorUpdateCfInfo = new QEnvironment.CfInfo();
      //            eodExecutorUpdateCfInfo.Name = "qbc.cf";
      //            eodExecutorUpdateCfInfo.Repository = "QC";
      //            eodExecutorUpdateCfInfo.Path = $"{eodExecutorDir}\\qbc.cf";
      //            objEnv.CFs.Add(eodExecutorUpdateCfInfo);
      //         }
      //      }
      //      else
      //         objEnv.Errors.AddError($"Dir not found \"{objEnv.SystemFolder}\\EOD*Executor\"", objEnv.SystemFolder);

      //      // ApplicationUpdate dir
      //      string applicationUpdateDir = Path.Combine(objEnv.SystemFolder, "ApplicationUpdate");
      //      applicationUpdateEodIniFile = Path.Combine(applicationUpdateDir, "eod.ini");
      //      if (File.Exists(applicationUpdateEodIniFile))
      //      {
      //         objEnv.OtherFiles.Add(new QEnvironment.OtherFile() { Name = "Application Update eod.ini", Path = applicationUpdateEodIniFile });
      //      }
      //      else
      //      {
      //         objEnv.Errors.AddError($"File not found \"{applicationUpdateEodIniFile}\"", applicationUpdateEodIniFile);
      //      }

      //      // add ...QCS_SystemFolders\INST_X_Y\ApplicationUpdate\qbc.cf
      //      if (!string.IsNullOrEmpty(applicationUpdateDir))
      //      {
      //         QEnvironment.CfInfo applicationUpdateCfInfo = new QEnvironment.CfInfo();
      //         applicationUpdateCfInfo.Name = "qbc.cf";
      //         applicationUpdateCfInfo.Repository = "QC";
      //         applicationUpdateCfInfo.Path = $"{applicationUpdateDir}\\qbc.cf";
      //         objEnv.CFs.Add(applicationUpdateCfInfo);
      //      }



      //      CheckFolderExistence(applicationUpdateDir, objEnv);
      //      CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Attachments"), objEnv);
      //      CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Exports"), objEnv);
      //      CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "ExternalAgencies"), objEnv);
      //      CheckFolderExistence(Path.Combine(objEnv.SystemFolder, "Templates"), objEnv);
      //   }
      //   else
      //   {
      //      objEnv.Errors.AddError($"System Folder is empty or does not exist ({objEnv.SystemFolder}).", objEnv.SystemFolder);
      //   }

      //   #region check eod.ini if out of date
      //   try
      //   {
      //      string eodIniFileText = File.ReadAllText(eodExecutorEodIniFile);
      //      foreach (string name in eodFlows)
      //      {
      //         if (!eodIniFileText.Contains(name))
      //         {
      //            objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{eodExecutorEodIniFile}\"))", eodExecutorEodIniFile);
      //         }
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      objEnv.Errors.AddError($"Error while reading \"{eodExecutorEodIniFile}\" ({ex.Message})", eodExecutorEodIniFile);
      //   }
      //   try
      //   {
      //      string applicationUpdateEodIniFileText = File.ReadAllText(applicationUpdateEodIniFile);
      //      foreach (string name in eodFlows)
      //      {
      //         if (!applicationUpdateEodIniFileText.Contains(name))
      //         {
      //            objEnv.Errors.AddError($"eod.ini out of date, flow \"{name}\" is missing (\"{applicationUpdateEodIniFile}\"))", applicationUpdateEodIniFile);
      //         }
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      objEnv.Errors.AddError($"Error while reading \"{applicationUpdateEodIniFile}\" ({ex.Message})", applicationUpdateEodIniFile);
      //   }

      //   #endregion
      //}

      //private void ValidateCFs(QEnvironment objEnv, CfFile adminCf, CancellationToken cancelToken)
      //{
      //   CfValidator cfValidator = new CfValidator();

      //   // validate against qbc_admin.cf
      //   List<string> keys = adminCf.GetKeys(objEnv.DBCollectionPlusServer, objEnv.DBCollectionPlusName);
      //   if (keys.Count == 0)
      //   {
      //      objEnv.Errors.AddError($"Info not found {objEnv.DBCollectionPlusServer}.{objEnv.DBCollectionPlusName}", "");
      //   }
      //   foreach (QEnvironment.CfInfo cfInfo in objEnv.CFs)
      //   {
      //      objEnv.Errors.AddRange(cfValidator.Validate(cfInfo.Path, keys));
      //   }
      //   // validate against agent qbc.cf
      //   // get agent client cf
      //   string path = objEnv.QBCAdminCfPath.Replace(@"QCS\QCSClient\QBC_Admin.cf", @"CollectionAgentSystem\CollectionAgentSystemClient\QBC.cf");
      //   CfFile agentCf = new CfFile(path);
      //   keys = agentCf.GetKeys(objEnv.DBCollectionPlusServer, objEnv.DBCollectionPlusName);
      //   if (keys.Count == 0)
      //   {
      //      objEnv.Errors.AddError($"Info not found {objEnv.DBCollectionPlusServer}.{objEnv.DBCollectionPlusName}", "");
      //   }
      //   foreach (QEnvironment.CfInfo cfInfo in objEnv.CFs)
      //   {
      //      objEnv.Errors.AddRange(cfValidator.Validate(cfInfo.Path, keys));
      //   }


      //}
      #endregion

      #region private
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

   }

}
