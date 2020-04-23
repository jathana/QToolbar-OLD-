using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Collections.Specialized;
using DiffPlex.DiffBuilder;
using DiffPlex;
using DiffPlex.DiffBuilder.Model;

namespace QToolbar.Forms
{
   public partial class Frm_FieldsHelper : DevExpress.XtraEditors.XtraForm
   {
      private ConnectionInfo _Info;
      private List<ConnectionInfo> _DBs;
      private Dictionary<string, Tuple<DataTable, string>> _Data = new Dictionary<string, Tuple<DataTable, string>>();

      #region public
      public Frm_FieldsHelper()
      {
         InitializeComponent();
      }

      public void Show(ConnectionInfo info, List<ConnectionInfo> dbs)
      {
         _Info = info;
         _DBs = dbs;
         Text = $"Fields Helper - {_Info.Server} . {_Info.Database}";
         Show();

      }
      #endregion

      #region properties


      private ConnectionInfo SelectedConInfo
      {
         get
         {
            var row = lkpDatabase.GetSelectedDataRow();
            ConnectionInfo ret = null;
            if(row != null)
            {
               ret=(ConnectionInfo)row.GetType().GetProperty("CON_INFO").GetValue(row);
            }
            return ret;
         }
      }

      private string SelectedTable
      {
         get
         {
            var rowView = (DataRowView)lkpTable.GetSelectedDataRow();
            string ret = string.Empty;
            if (rowView != null)
            {
               DataRow row = (DataRow)rowView.Row;
               ret = (string)row["TABLE_NAME"];
            }
            return ret;
         }
      }

      private string Field
      {
         get
         {
            return lkpField.SelectedText;
         }
      }

      #endregion

      #region event handlers
      private void Frm_FieldsHelper_Load(object sender, EventArgs e)
      {
         lkpTable.Properties.DisplayMember = "TABLE_NAME";
         lkpTable.Properties.ValueMember = "TABLE_NAME";

         lkpDatabase.Properties.DisplayMember = "DATABASE";
         lkpDatabase.Properties.ValueMember = "DATABASE";

         lkpField.Properties.DisplayMember = "FIELD_NAME";
         lkpField.Properties.ValueMember = "FIELD_NAME";
         
         lkpDatabase.Properties.Columns["CON_INFO"].Visible = false;
         lkpDatabase.EditValueChanged += LkpDatabase_EditValueChanged;

         layProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

         btnCompare.Enabled = false;

         LoadDevDBs();

         workerFetchTables.RunWorkerAsync(_Info);


         

      }

      private void LkpDatabase_EditValueChanged(object sender, EventArgs e)
      {
         
      }

      private void lkpTable_EditValueChanged(object sender, EventArgs e)
      {
         Tuple<string, ConnectionInfo> arg = new Tuple<string, ConnectionInfo>(SelectedTable, _Info);
         workerFetchFields.RunWorkerAsync(arg);
      }

      private void btnGenerateSQL_Click(object sender, EventArgs e)
      {
         Tuple<string, string, ConnectionInfo, ConnectionInfo> arg = new Tuple<string, string, ConnectionInfo, ConnectionInfo>(Field, SelectedTable, _Info, SelectedConInfo);
         layProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
         txtGenerateSQL.Text = string.Empty;
         btnCompare.Enabled = false;
         workerGenerateSQL.RunWorkerAsync(arg);
      }

      #endregion

      #region workerFetchTables
      private void workerFetchTables_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            ConnectionInfo currentdDB = (ConnectionInfo)e.Argument;
            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(currentdDB.Server, currentdDB.Database)))
            {
               con.FireInfoMessageEventOnUserErrors = true;
               //con.InfoMessage += Con_InfoMessage;
               SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT TABLE_NAME
                                                         FROM INFORMATION_SCHEMA.TABLES
                                                         WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%_dbAudits' AND TABLE_NAME LIKE 'AT_%'
                                                         ORDER BY TABLE_NAME", con);

               // get information of target database
               DataSet ds = new DataSet();
               adapter.Fill(ds);
               e.Result = new Tuple<DataSet, ConnectionInfo>(ds, currentdDB);
            }
         }
         catch (Exception ex)
         {
            e.Result = ex;
         }

      }

      private void workerFetchTables_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result is Exception)
         {
            MessageBox.Show(((Exception)e.Result).Message);
         }
         else
         {
            Tuple<DataSet, ConnectionInfo> result = (Tuple<DataSet, ConnectionInfo>)e.Result;
            lkpTable.Properties.DataSource = result.Item1.Tables[0];
         }
      }
      #endregion

      #region workerFetchFields
      private void workerFetchFields_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            Tuple<string, ConnectionInfo> arg = (Tuple<string, ConnectionInfo>)e.Argument;
            ConnectionInfo currentdDB = arg.Item2;
            string tableName = arg.Item1;
            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(currentdDB.Server, currentdDB.Database)))
            {
               con.FireInfoMessageEventOnUserErrors = true;
               
               //con.InfoMessage += Con_InfoMessage;
               SqlDataAdapter adapter = new SqlDataAdapter(GetTableFieldsSQL(tableName), con);

               // get information of target database
               DataSet ds = new DataSet();
               adapter.Fill(ds);
               e.Result = new Tuple<string, DataSet, ConnectionInfo>(tableName, ds, currentdDB);
            }
         }
         catch (Exception ex)
         {
            e.Result = ex;
         }
      }

      private void workerFetchFields_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result is Exception)
         {
            MessageBox.Show(((Exception)e.Result).Message);
         }
         else
         {
            Tuple<string, DataSet, ConnectionInfo> result = (Tuple<string, DataSet, ConnectionInfo>)e.Result;
            lkpField.Properties.DataSource = result.Item2.Tables[0];
         }
      }

      #endregion

      #region workerGenerateSQL
      private string GetTableScript(ConnectionInfo info, string table)
      {
         string ret = string.Empty;
         ServerConnection svrCon = new ServerConnection();
         svrCon.LoginSecure = true;
         svrCon.ServerInstance = info.Server;
         svrCon.DatabaseName = info.Database;
         Server svr = new Server(svrCon);
         Scripter scripter = new Scripter(svr);
         Database db = svr.Databases[info.Database];
         ScriptingOptions options = new ScriptingOptions()
         {
            DriAllConstraints = true,
            DriChecks = true,
            DriClustered = true,
            DriForeignKeys = true,
            DriWithNoCheck = true,
            DriDefaults = true,
            IncludeIfNotExists = false,
            Indexes = true,
            NonClusteredIndexes = true,
            ScriptDrops = false,
            AppendToFile = true,
            AllowSystemObjects = false,
            WithDependencies = false,
            AnsiPadding = false
         };
         scripter.Options = options;
         return ScriptObject(new Urn[] { db.Tables[table].Urn }, scripter);
      }

      private void workerGenerateSQL_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            // input
            Tuple<string, string, ConnectionInfo, ConnectionInfo> arg = (Tuple<string, string, ConnectionInfo, ConnectionInfo>)e.Argument;
            ConnectionInfo currentDB = arg.Item3;
            ConnectionInfo selectedDB = arg.Item4;
            string table = arg.Item2;
            string field = arg.Item1;
            StringBuilder b = new StringBuilder();

            // dev dbs, including current
            var devdbs = _DBs.Where(d => OptionsInstance.DevSQLInstances.Contains(d.Server) &&
                                      d.Database.ToLower().StartsWith("qbcollection_plus_")).OrderByDescending(d=>d.DatabaseSortName).ToList();

            Dictionary<string, Tuple<DataTable, string>> data = new Dictionary<string, Tuple<DataTable, string>>();

            // collect table information across databases
            foreach (var info in devdbs)
            {
               workerGenerateSQL.ReportProgress(0, $"Fetching information ({info.Server}.{info.Database})");
               try
               {
                  using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(info.Server, info.Database)))
                  {
                     con.FireInfoMessageEventOnUserErrors = true;

                     //con.InfoMessage += Con_InfoMessage;
                     SqlDataAdapter adapter = new SqlDataAdapter(GetTableFieldsDefSQL(info.Database, table), con);

                     // get information of target database
                     DataSet ds = new DataSet();
                     adapter.Fill(ds);

                     string tableScript = GetTableScript(info, table);
                     data.Add(info.Database, new Tuple<DataTable, string>(ds.Tables[0], tableScript));
                  }
               }
               catch(Exception ex)
               {
                  data.Add(info.Database, new Tuple<DataTable, string>(null, $"/* {ex.Message} */"));                  
               }
               if(info.Database.ToLower().Equals(selectedDB.Database.ToLower()))
               {
                  break;
               }
            }
            // find table differences against current's version
            DataTable currentTable = data[currentDB.Database].Item1;
            string currentScript = data[currentDB.Database].Item2;

            foreach (KeyValuePair<string, Tuple<DataTable, string>> pair in data.Where(c=>c.Key!=currentDB.Database))
            {
               workerGenerateSQL.ReportProgress(0, $"Processing ({pair.Key})");
               b.AppendLine();
               b.AppendLine($"-- db:{pair.Key}, table:{table}");

               if (pair.Value.Item1 != null)
               {
                  string tableScript = pair.Value.Item2;

                  var differ = new Differ();
                  var charDiffs=differ.CreateCharacterDiffs(tableScript, currentScript, true);
                  if (charDiffs.DiffBlocks.Count > 0)
                  {
                     b.AppendLine();
                     b.AppendLine($"USE [{pair.Key}]");
                     b.AppendLine("GO");
                     b.AppendLine();
                     b.AppendLine($"ALTER TABLE {table} ADD ");
                  }
                  string blockScript = string.Empty;
                  StringBuilder fb = new StringBuilder(); 
                  foreach (var db in charDiffs.DiffBlocks)
                  {
                     blockScript = currentScript.Substring(db.InsertStartB, db.InsertCountB);                     
                     fb.Append(blockScript);
                  }

                  string[] lines = fb.ToString().Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
                  // compare current with version
                  string fieldToScript = string.Empty;
                  string line = string.Empty;
                  string separator = " ";
                  foreach (DataRow rowCurrent in currentTable.Rows)
                  {
                     fieldToScript = rowCurrent["name"].ToString();
                     DataRow[] foundRows = pair.Value.Item1.Select($"name='{fieldToScript}'");
                     if (foundRows.Length == 0)
                     {
                        //b.AppendLine($"-- ALTER TABLE {table} ADD {rowCurrent["name"].ToString()} ");
                        //b.AppendLine($"--  {rowCurrent["name"].ToString()} ");
                        var linesFound = lines.Where(l => l.Contains(fieldToScript)).ToList();
                        if (linesFound.Count > 0)
                        {
                           line = linesFound[0].ToString().Trim();
                           line = line.RemoveAtEnd(",");
                           b.AppendLine($"  {separator}{line}");
                           separator = ",";
                        }
                     }
                     if (fieldToScript.ToLower().Equals(lkpField.EditValue.ToString().ToLower()))
                     {
                        break;
                     }
                  }

                  if (charDiffs.DiffBlocks.Count > 0)
                  {
                     b.AppendLine();
                     b.AppendLine("GO");
                     b.AppendLine();
                  }
               }
               else
               {
                  if (!string.IsNullOrEmpty(pair.Value.Item2))
                  {
                     b.AppendLine(pair.Value.Item2);
                  }
               }

            }

            e.Result = new Tuple<string,Dictionary<string, Tuple<DataTable, string>>>( b.ToString(),data);

         }
         catch (Exception ex)
         {
            e.Result = ex;
         }
      }

      private void workerGenerateSQL_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         lblProgress.Text = e.UserState.ToString();
      }

      private void workerGenerateSQL_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         Tuple<string, Dictionary<string, Tuple<DataTable, string>>> res = (Tuple<string, Dictionary<string, Tuple<DataTable, string>>>)e.Result;

         txtGenerateSQL.Text = res.Item1;
         _Data = res.Item2;
         btnCompare.Enabled = true;
         layProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 
         lblProgress.Text = string.Empty;
      }

      private string ScriptObject(Urn[] urns, Scripter scripter)
      {
         StringCollection sc = scripter.Script(urns);
         StringBuilder sb = new StringBuilder();
         
         foreach (string str in sc)
         {
            sb.Append(str + Environment.NewLine + "GO" +
              Environment.NewLine + Environment.NewLine);
         }

         return sb.ToString();
      }
      #endregion

      #region private
      private void LoadDevDBs()
      {

         var devdbs = _DBs.Where(d => OptionsInstance.DevSQLInstances.Contains(d.Server) &&
                                      d.Database.ToLower().StartsWith("qbcollection_plus_") &&
                                      d.Database != _Info.Database).Select(c => new { DATABASE = c.Database, CON_INFO = c }).ToList();

         lkpDatabase.Properties.DataSource = devdbs;
      }

      private string GetTableFieldsSQL(string table)
      {
         return $@"select name AS FIELD_NAME, column_id
                  from sys.columns
                  where object_id = object_id('{table}')
                  order by column_id";
      }

      private string GetTableFieldsDefSQL(string db, string table)
      {
         return $@"IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{db}')
                     BEGIN
                        select * 
                        from sys.columns
                        where object_id = object_id('{table}')
                        order by column_id
                     END";
      }




      #endregion

      private void btnCompare_Click(object sender, EventArgs e)
      {
         try
         {
            string textRight = _Data[_Info.Database].Item2;
            string textLeft = _Data[lkpDatabase.EditValue.ToString()].Item2;
            Forms.Frm_TxtDiff f = new Frm_TxtDiff();
            f.CompareText(textLeft, textRight);
         }
         catch(Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
   }
}