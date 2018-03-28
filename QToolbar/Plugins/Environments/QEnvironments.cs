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
         if (adding)
         {
            _Data.Add(envObj);
         }

         // collect rest info
         CollectEnvInfo(envObj.DBCollectionPlusServer, envObj.DBCollectionPlusName, envObj);


      }
      #endregion

      #region private methods
      private async void CollectEnvInfo(string server, string db, QEnvironment env)
      {

         Task<QEnvironment> rs = Task<QEnvironment>.Factory.StartNew(() =>
         {
            QEnvironment retval = new QEnvironment();

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
                     retval.DBCollectionPlusVersion = ex.Message;
                  }

                  //The location of the GLM folder can be found from the following query:
                  //select inst_root from bi_glm_installation
                  try
                  {
                     com.CommandText = "select inst_root from bi_glm_installation";
                     string glmDir= com.ExecuteScalar().ToString();
                     retval.GLMDir = glmDir;
                     retval.GLMLocalDir = new Utils().GetPath(glmDir);
                  }
                  catch (Exception ex)
                  {
                     retval.GLMDir = ex.Message;
                  }

                  //Also the location of the GLM logs folder can be found with:
                  //select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'
                  try
                  {
                     com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'";
                     string glmLogDir= com.ExecuteScalar().ToString();
                     retval.GLMLogDir = glmLogDir;
                     retval.GLMLocalLogDir = new Utils().GetPath(glmLogDir);
                  }
                  catch (Exception ex)
                  {
                     retval.GLMLogDir = ex.Message;
                  }

                  try
                  {
                     com.CommandText = @"select SPR_VALUE from AT_SYSTEM_PREF
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
                        string localPath = new Utils().GetPath(pathrow["SPR_VALUE"].ToString());
                        retval.QCSystemSharedDirs.Add(new QEnvironment.SharedDir()
                        {
                           UNC = pathrow["SPR_VALUE"].ToString(),
                           LocalPath = new Utils().GetPath(pathrow["SPR_VALUE"].ToString())
                        });
                     }
                  }
                  catch (Exception ex)
                  {                     
                  }
               }
               catch (Exception ex)
               {
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
               catch(Exception ex)
               {

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
                        if (s.Name.StartsWith(envNameInWeb))
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

               }

               // Add cfs from batch & eod services
               string ver = Path.GetFileName(env.CheckoutPath);
               string envConfFile = GetEnvironmentsConfigurationFile(ver);
               QEnvironmentsConfiguration ec = new QEnvironmentsConfiguration(ver, envConfFile);
               ec.Load();
               var envFound=ec.FirstOrDefault(e => e.Database == env.DBCollectionPlusName && e.Server == env.DBCollectionPlusServer);
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
                  retval.EodExecutorWinServicePath= envFound.EODServiceUNCPath;
                  retval.WinServicesDir = Directory.GetParent(envFound.EODServiceUNCPath).FullName;
               }


               retval.CheckoutPath = env.CheckoutPath;
               retval.ProteusCheckoutPath = env.ProteusCheckoutPath;
               retval.DBCollectionPlusServer = env.DBCollectionPlusServer;
               retval.DBCollectionPlusName = env.DBCollectionPlusName;
               retval.ToolkitWSUrl = env.ToolkitWSUrl;
               retval.AppWSUrl = env.AppWSUrl;
               retval.QBCAdminCfPath = env.QBCAdminCfPath;




            };
            return retval;
         });
         await Task.WhenAll(rs).ContinueWith((t) =>
         {
            Dispatcher.CurrentDispatcher.Invoke(() => {

               QEnvironment val = rs.Result;

               env.DBCollectionPlusVersion = val.DBCollectionPlusVersion;
               env.GLMDir = val.GLMDir;
               env.GLMLocalDir = val.GLMLocalDir;
               env.GLMLogDir = val.GLMLogDir;
               env.GLMLocalLogDir = val.GLMLocalLogDir;

               env.QCSystemSharedDirs.AddRange(val.QCSystemSharedDirs);
               env.CFs.AddRange(val.CFs);
               env.BatchExecutorWinServicePath = val.BatchExecutorWinServicePath;
               env.EodExecutorWinServicePath = val.EodExecutorWinServicePath;
               env.WinServicesDir = val.WinServicesDir;



               return rs; })

            .ContinueWith((t1) => {
               


               InfoCollected(this, new EnvInfoEventArgs(rs.Result));
            });

         });

         //QEnvironment val = rs.Result;

         //env.DBCollectionPlusVersion = val.DBCollectionPlusVersion;
         //env.GLMDir = val.GLMDir;
         //env.GLMLocalDir = val.GLMLocalDir;
         //env.GLMLogDir = val.GLMLogDir;
         //env.GLMLocalLogDir = val.GLMLocalLogDir;         

         //env.QCSystemSharedDirs.AddRange(val.QCSystemSharedDirs);
         //env.CFs.AddRange(val.CFs);
         //env.BatchExecutorWinServicePath = val.BatchExecutorWinServicePath;
         //env.EodExecutorWinServicePath = val.EodExecutorWinServicePath;
         //env.WinServicesDir = val.WinServicesDir;
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
