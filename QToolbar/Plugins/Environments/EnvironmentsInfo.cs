using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QToolbar.Plugins.Environments
{
   public class EnvironmentsInfo
   {

      public event EventHandler PathsAdded;

      private DataTable _Table;

      public EnvironmentsInfo()
      {
         _Table = new DataTable();
         _Table.Columns.Add("ENV_NAME", typeof(string));
         _Table.Columns.Add("QBC_ADMIN_CF", typeof(string));
         _Table.Columns.Add("ENV_VERSION", typeof(string));
         _Table.Columns.Add("DB_SERVER", typeof(string));
         _Table.Columns.Add("DB_NAME", typeof(string));
         _Table.Columns.Add("TOOLKIT_WS_URL", typeof(string));
         _Table.Columns.Add("APP_WS_URL", typeof(string));
         _Table.Columns.Add("ENV_BATCHEXEC_SERVICE_PATH", typeof(string));
         _Table.Columns.Add("ENV_EOD_SERVICE_PATH", typeof(string));
         _Table.Columns.Add("ENV_SERVICES_PATH", typeof(string));
         _Table.Columns.Add("ENV_GLM_FOLDER", typeof(string));
         _Table.Columns.Add("ENV_GLM_LOCAL_FOLDER", typeof(string));
         _Table.Columns.Add("ENV_GLM_LOG_FOLDER", typeof(string));
         _Table.Columns.Add("ENV_GLM_LOG_LOCAL_FOLDER", typeof(string));
         _Table.Columns.Add("ENV_QC_SYSTEM_FOLDER", typeof(string));
         _Table.Columns.Add("ENV_QC_LOCAL_SYSTEM_FOLDER", typeof(string));
      }

      public DataTable Table
      {
         get
         {
            return _Table;
         }

         set
         {
            _Table = value;
         }
      }

      public void AddOrUpdate(string envName, CfFile cf)
      {
         bool adding = false;
         DataRow[] rows = _Table.Select($"ENV_NAME='{envName}'");
         DataRow rowToHandle = null;

         if (rows.Length == 0)
         {
            //add
            rowToHandle = _Table.NewRow();
            adding = true;
         }
         else if (rows.Length == 1)
         {
            rowToHandle = rows[0];
         }
         else if (rows.Length > 1)
         {
            throw new Exception($"Environment {envName} exists.");
         }

         if (rowToHandle != null)
         {
            rowToHandle["ENV_NAME"] = envName;
            //newRow["ENV_VERSION"], typeof(string));
            rowToHandle["DB_SERVER"] = cf.GetServer(envName);
            rowToHandle["DB_NAME"] = cf.GetDatabase(envName);
            rowToHandle["TOOLKIT_WS_URL"] = cf.GetValue("General", "ToolkitWSURL");
            rowToHandle["APP_WS_URL"] = cf.GetValue("General", "ApplicationWSURL");
            rowToHandle["QBC_ADMIN_CF"] = cf.File;
         }

         if (adding)
         {
            _Table.Rows.Add(rowToHandle);

         }
         AddPaths(rowToHandle["DB_SERVER"].ToString(), rowToHandle["DB_NAME"].ToString(), rowToHandle);

      }

      private async void AddPaths(string server, string db, DataRow row)
      {


         Task<Dictionary<string, string>> rs = Task<Dictionary<string, string>>.Factory.StartNew(() =>
          {
             Dictionary<string, string> vals = new Dictionary<string, string>();
             vals.Add("ENV_VERSION", "");
             vals.Add("ENV_GLM_FOLDER", "");
             vals.Add("ENV_GLM_LOCAL_FOLDER", "");
             vals.Add("ENV_GLM_LOG_FOLDER", "");
             vals.Add("ENV_GLM_LOG_LOCAL_FOLDER", "");
             vals.Add("ENV_QC_SYSTEM_FOLDER", "");
             vals.Add("ENV_QC_LOCAL_SYSTEM_FOLDER", "");

             using (SqlConnection con = new SqlConnection())
             {
                con.ConnectionString = Utils.GetConnectionString(server, db);
                try
                {
                   con.Open();
                   SqlCommand com = new SqlCommand();
                   com.Connection = con;
                   string glmDir = "", glmLogDir = "";
                   string glmLocalDir = "", glmLogLocalDir = "";

                   try
                   {
                      com.CommandText = "SELECT TOP(1) CONVERT(NVARCHAR,MAJOR)+'.' + CONVERT(NVARCHAR,MINOR) FROM TLK_DATABASE_VERSIONS ORDER BY MAJOR DESC,MINOR DESC";
                      vals["ENV_VERSION"] = com.ExecuteScalar().ToString();
                   }
                   catch (Exception ex)
                   {
                      vals["ENV_VERSION"] = ex.Message;
                   }

                  //The location of the GLM folder can be found from the following query:
                  //select inst_root from bi_glm_installation
                  try
                   {
                      com.CommandText = "select inst_root from bi_glm_installation";
                      glmDir = com.ExecuteScalar().ToString();
                      glmLocalDir = Utils.GetPath(glmDir);
                      vals["ENV_GLM_FOLDER"] = glmDir;
                      vals["ENV_GLM_LOCAL_FOLDER"] = glmLocalDir;

                   }
                   catch (Exception ex)
                   {
                      vals["ENV_GLM_FOLDER"] = ex.Message;
                   }

                  //Also the location of the GLM logs folder can be found with:
                  //select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'
                  try
                   {
                      com.CommandText = "select SPRA_VALUE from AT_SYSTEM_PARAMS where SPRA_TYPE = 'EOD_LOGS_PATH'";
                      glmLogDir = com.ExecuteScalar().ToString();
                      glmLogLocalDir = Utils.GetPath(glmLogDir);
                      vals["ENV_GLM_LOG_FOLDER"] = glmLogDir;
                      vals["ENV_GLM_LOG_LOCAL_FOLDER"] = glmLogLocalDir;

                   }
                   catch (Exception ex)
                   {
                      vals["ENV_GLM_LOG_FOLDER"] = ex.Message;
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
                         string localPath = Utils.GetPath(pathrow["SPR_VALUE"].ToString());
                         if (!locbuilder.ToString().Contains(localPath))
                         {
                            locbuilder.AppendLine(localPath);
                         }
                         builder.AppendLine(pathrow["SPR_VALUE"].ToString());
                      }
                      vals["ENV_QC_SYSTEM_FOLDER"] = builder.ToString();
                      vals["ENV_QC_LOCAL_SYSTEM_FOLDER"] = locbuilder.ToString();

                   }
                   catch (Exception ex)
                   {
                      vals["ENV_QC_SYSTEM_FOLDER"] = ex.Message;
                   }
                }
                catch (Exception ex)
                {
                   vals["ENV_GLM_FOLDER"] = ex.Message;
                   vals["ENV_GLM_LOG_FOLDER"] = ex.Message;
                }
                finally
                {
                   if (con.State == ConnectionState.Open)
                   {
                      con.Close();
                   }
                }

             };
             return vals;
          });
         await Task.WhenAll(rs).ContinueWith((t) =>
         {
            Dispatcher.CurrentDispatcher.Invoke(() => 
               {
               PathsAdded(this, new EventArgs());
               return rs;
            });
         });
         

         Dictionary<string, string> val = rs.Result;
         row["ENV_VERSION"] = val["ENV_VERSION"];
         row["ENV_GLM_FOLDER"] = val["ENV_GLM_FOLDER"];
         row["ENV_GLM_LOCAL_FOLDER"] = val["ENV_GLM_LOCAL_FOLDER"];
         row["ENV_GLM_LOG_FOLDER"] = val["ENV_GLM_LOG_FOLDER"];
         row["ENV_GLM_LOG_LOCAL_FOLDER"] = val["ENV_GLM_LOG_LOCAL_FOLDER"];
         row["ENV_QC_SYSTEM_FOLDER"] = val["ENV_QC_SYSTEM_FOLDER"];
         row["ENV_QC_LOCAL_SYSTEM_FOLDER"] = val["ENV_QC_LOCAL_SYSTEM_FOLDER"];
         

      }



      public void Remove(string envName)
      {
         DataRow[] rows = _Table.Select($"ENV_NAME='{envName}'");
         if (rows.Length == 1)
         {
            _Table.Rows.Remove(rows[0]);
         }
         else if (rows.Length > 1)
         {
            throw new Exception($"Cannot delete.Multiple environments {envName} found.");
         }
      }
   }
}
