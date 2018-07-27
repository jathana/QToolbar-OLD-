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
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace QToolbar.Forms
{
   public partial class Frm_CriteriaHelper : DevExpress.XtraEditors.XtraForm
   {
      private const int SELECT_CRITERIA = 0;
      private const int CREATE_CRITERIA = 1;
      private const int CRITERIA_CATEGORIES = 2;
      private const int CRITERIA_TYPES = 3;
      private const int CRITERIA_JOINS = 4;
      private const int CRITERIA_UNIQUE_IDS = 5;
      private const int CRITERIA_WHERE_TABLES = 6;
      private const int CRITERIA_TABLES = 7;
      private const int CRITERIA_SQL_TYPES = 8;


      private ConnectionInfo _Info;
      private StringBuilder connectionMsg = new StringBuilder();
      RepositoryItemGridLookUpEdit repoLookupCriterioCategory = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioType = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioUniqueId = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioWhereTable = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioDependentUniqueId = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioTable = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupCriterioJoin = new RepositoryItemGridLookUpEdit();
      RepositoryItemGridLookUpEdit repoLookupSQLTypes = new RepositoryItemGridLookUpEdit();

      DataSet _SelectData = new DataSet();
      DataSet _CreateData = new DataSet();

      public Frm_CriteriaHelper()
      {
         InitializeComponent();
      }

      public void Show(ConnectionInfo info)
      {
         _Info = info;
         Text = $"Criteria Helper - {_Info.Server} . {_Info.Database}";
         SetSQL();
         Show();
         btnLoadCriteria_ItemClick(null, null);
         
      }

      private void SetSQL()
      {
         txtSelectSQL.Text = "SELECT * FROM AT_CRITERIA WHERE CRI_USER_DEFINED_FLAG=0 ORDER BY CRI_CREATED DESC";
      }
      

      void InitGrid()
      {
         //repoLookupCriterioCategory.BeginInit();
         repoLookupCriterioCategory.DataSource = _SelectData.Tables[CRITERIA_CATEGORIES];
         repoLookupCriterioCategory.DisplayMember = "LOV_DESC";
         repoLookupCriterioCategory.Name = "repoLookupCriterioCategory";
         repoLookupCriterioCategory.ValueMember = "LOV_CODE";
         repoLookupCriterioCategory.PopupFormSize = new Size(1000, 500);
         //repoLookupCriterioCategory.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioCategory);
         grdviewCreateCriteria.Columns["CRI_CATEGORY"].ColumnEdit = repoLookupCriterioCategory;
         //repoLookupCriterioCategory.EndInit();

         //repoLookupCriterioType.BeginInit();
         repoLookupCriterioType.DataSource = _SelectData.Tables[CRITERIA_TYPES];
         repoLookupCriterioType.DisplayMember = "LOV_DESC";
         repoLookupCriterioType.Name = "repoLookupCriterioType";
         repoLookupCriterioType.ValueMember = "LOV_CODE";
         repoLookupCriterioType.PopupFormSize = new Size(500, 250);
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioType);
         grdviewCreateCriteria.Columns["CRI_TYPE"].ColumnEdit = repoLookupCriterioType;
         //repoLookupCriterioType.EndInit();

         //repoLookupCriterioUniqueId.BeginInit();
         repoLookupCriterioUniqueId.DataSource = _SelectData.Tables[CRITERIA_UNIQUE_IDS];
         repoLookupCriterioUniqueId.DisplayMember = "CRI_UNIQUE_ID";
         repoLookupCriterioUniqueId.Name = "repoLookupCriterioNewId";
         repoLookupCriterioUniqueId.ValueMember = "CRI_UNIQUE_ID";
         repoLookupCriterioUniqueId.PopupFormSize = new Size(500, 500);
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioUniqueId);
         grdviewCreateCriteria.Columns["CRI_UNIQUE_ID"].ColumnEdit = repoLookupCriterioUniqueId;
         repoLookupCriterioUniqueId.EndInit();


         //repoLookupCriterioWhereTable
         //repoLookupCriterioWhereTable.BeginInit();
         repoLookupCriterioWhereTable.DataSource = _SelectData.Tables[CRITERIA_WHERE_TABLES];
         repoLookupCriterioWhereTable.DisplayMember = "TABLE_NAME";
         repoLookupCriterioWhereTable.Name = "repoLookupCriterioWhereTable";
         repoLookupCriterioWhereTable.ValueMember = "TABLE_NAME";
         repoLookupCriterioWhereTable.PopupFormSize = new Size(1000, 500);
         repoLookupCriterioWhereTable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioWhereTable);
         grdviewCreateCriteria.Columns["CRI_WHERE_TABLE"].ColumnEdit = repoLookupCriterioWhereTable;
         //repoLookupCriterioWhereTable.EndInit();

         //repoLookupCriterioDependentUniqueId
         //repoLookupCriterioDependentUniqueId.BeginInit();
         repoLookupCriterioDependentUniqueId.DataSource = _SelectData.Tables[CRITERIA_UNIQUE_IDS];
         repoLookupCriterioDependentUniqueId.DisplayMember = "CRI_UNIQUE_ID";
         repoLookupCriterioDependentUniqueId.Name = "repoLookupCriterioDependentUniqueId";
         repoLookupCriterioDependentUniqueId.ValueMember = "CRI_UNIQUE_ID";
         repoLookupCriterioDependentUniqueId.PopupFormSize = new Size(500, 500);
         repoLookupCriterioDependentUniqueId.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioDependentUniqueId);
         grdviewCreateCriteria.Columns["CRI_DEPENDENT_UNIQUE_ID"].ColumnEdit = repoLookupCriterioDependentUniqueId;
         //repoLookupCriterioDependentUniqueId.EndInit();

         //repoLookupCriterioTable
         //repoLookupCriterioTable.BeginInit();
         repoLookupCriterioTable.DataSource = _SelectData.Tables[CRITERIA_TABLES];
         repoLookupCriterioTable.DisplayMember = "CRI_TABLE";
         repoLookupCriterioTable.Name = "repoLookupCriterioTable";
         repoLookupCriterioTable.ValueMember = "CRI_TABLE";
         repoLookupCriterioTable.PopupFormSize = new Size(1000, 500);
         repoLookupCriterioTable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioTable);
         grdviewCreateCriteria.Columns["CRI_TABLE"].ColumnEdit = repoLookupCriterioTable;
         //repoLookupCriterioTable.EndInit();

         //repoLookupCriterioJoin
         //repoLookupCriterioJoin.BeginInit();
         repoLookupCriterioJoin.DataSource = _SelectData.Tables[CRITERIA_JOINS];
         repoLookupCriterioJoin.DisplayMember = "CRJ_CODE";
         repoLookupCriterioJoin.Name = "repoLookupCriterioJoin";
         repoLookupCriterioJoin.ValueMember = "CRJ_CODE";         
         repoLookupCriterioJoin.PopupFormSize = new Size(1000, 500);         
         grdCreateCriteria.RepositoryItems.Add(repoLookupCriterioJoin);
         grdviewCreateCriteria.Columns["CRJ_CODE"].ColumnEdit = repoLookupCriterioJoin;
         //repoLookupCriterioJoin.EndInit();

         //repoLookupSQLTypes
         //repoLookupSQLTypes.BeginInit();
         repoLookupSQLTypes.DataSource = _SelectData.Tables[CRITERIA_SQL_TYPES];
         repoLookupSQLTypes.DisplayMember = "CRI_WHERE_FIELD_SQL_TYPE";
         repoLookupSQLTypes.Name = "repoLookupSQLTypes";
         repoLookupSQLTypes.ValueMember = "CRI_WHERE_FIELD_SQL_TYPE";
         repoLookupSQLTypes.PopupFormSize = new Size(1000, 500);
         grdCreateCriteria.RepositoryItems.Add(repoLookupSQLTypes);
         grdviewCreateCriteria.Columns["CRI_WHERE_FIELD_SQL_TYPE"].ColumnEdit = repoLookupSQLTypes;
         //repoLookupSQLTypes.EndInit();


      }


      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(_Info.Server, _Info.Database)))
            {
               con.FireInfoMessageEventOnUserErrors = true;
               //con.InfoMessage += Con_InfoMessage;
               StringBuilder builder = new StringBuilder();
               builder.Append(txtSelectSQL.Text);
               builder.Append(';');
               builder.Append("SELECT * FROM AT_CRITERIA WHERE 1 = 2");
               // criteria categories
               builder.Append(';');
               builder.Append("SELECT LOV_CODE, LOV_DESC, LOV_INTERNAL_DESC, LOV_ACTIVE FROM VW_AT_LST_OF_VAL WHERE LOV_TYPE='CRITERIA_CATEGORIES' ORDER BY LOV_DESC");

               // criteria types
               builder.Append(';');
               builder.Append("SELECT LOV_CODE,LOV_DESC, LOV_INTERNAL_DESC, LOV_ACTIVE FROM VW_AT_LST_OF_VAL WHERE LOV_TYPE='CRITERIA_TYPES' ORDER BY LOV_DESC");

               // criteria joins
               builder.Append(';');
               builder.Append("SELECT CRJ_CODE, CRJ_JOIN, CRJ_DESCRIPTION, CRJ_NAME, CRJ_SOURCE_TABLE, CRJ_INTERNAL FROM AT_CRITERIA_JOINS");

               // NEW CRI_UNIQUE_ID
               builder.Append(';');
               builder.Append(@"DECLARE @NEW_CRI_UNIQUE_ID NVARCHAR(50)
                              SELECT TOP(1) @NEW_CRI_UNIQUE_ID = 'CRITERIO_' + CONVERT(NVARCHAR(50), SUBSTRING(CRI_UNIQUE_ID, CHARINDEX('_', CRI_UNIQUE_ID) + 1, 100) + 1)
                              FROM AT_CRITERIA
                              WHERE CRI_UNIQUE_ID LIKE '%CRITERIO_%'
                              AND CRI_UNIQUE_ID NOT LIKE '%CRITERIO_NEMO_%'
                              ORDER BY CRI_CODE DESC
                              
                              SELECT @NEW_CRI_UNIQUE_ID AS CRI_UNIQUE_ID, 'NEW CRITERIO' AS CRI_DESC
                              UNION
                              SELECT CRI_UNIQUE_ID, CRI_DESC
                              FROM AT_CRITERIA
                              WHERE CRI_UNIQUE_ID LIKE '%CRITERIO_%'
                              AND CRI_UNIQUE_ID NOT LIKE '%CRITERIO_NEMO_%'
                              ORDER BY 1 DESC");

               // tables/views
               builder.Append(';');
               builder.Append(@"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME");

               // custom cri table
               builder.Append(';');
               builder.Append(@"SELECT 'VW_AT_LST_OF_VAL' AS CRI_TABLE");

               // SQL Types
               builder.Append(';');
               builder.Append(@"SELECT CRI_WHERE_FIELD_SQL_TYPE  FROM AT_CRITERIA GROUP BY CRI_WHERE_FIELD_SQL_TYPE ORDER BY CRI_WHERE_FIELD_SQL_TYPE");

               SqlDataAdapter adapter = new SqlDataAdapter(builder.ToString(), con);

               DataSet dataset = new DataSet();
               adapter.FillSchema(dataset, SchemaType.Source);
               adapter.Fill(dataset);
               e.Result = dataset;
            }
         }
         catch (Exception ex)
         {
            e.Result = ex;
         }
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result != null)
         {
            if (e.Result is DataSet)
            {
               _SelectData = (DataSet)e.Result;
               grdviewSelectCriteria.Columns.Clear();
               grdSelectCriteria.DataSource = _SelectData.Tables[SELECT_CRITERIA];

               if(_CreateData.Tables.Count==0)
               {
                  _CreateData.Tables.Add(_SelectData.Tables[CREATE_CRITERIA].Clone());
                  grdCreateCriteria.DataSource = _CreateData.Tables[0];
               }
            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         InitGrid();
         btnLoadCriteria.Enabled = true;
         grdviewSelectCriteria.BestFitColumns();
         grdviewCreateCriteria.BestFitColumns();
         //btnRun.Enabled = true;
      }

      private void btnLoadCriteria_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {
            btnLoadCriteria.Enabled = false;
            bool loadCriteriaSchema = grdviewCreateCriteria.DataSource == null;
            backgroundWorker1.RunWorkerAsync(loadCriteriaSchema);
         }
         catch (Exception ex)
         {
            btnLoadCriteria.Enabled = true;

         }
         finally
         {
            
         }
      }

      private void Frm_CriteriaHelper_Load(object sender, EventArgs e)
      {
         // select criteria grid
         grdviewSelectCriteria.OptionsBehavior.Editable = false;
         grdviewSelectCriteria.OptionsView.ColumnAutoWidth = false;

         // create criteria grid
         grdviewCreateCriteria.OptionsView.ColumnAutoWidth = false;
         grdviewCreateCriteria.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
         grdviewCreateCriteria.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
         grdviewCreateCriteria.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
         grdviewCreateCriteria.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
         grdviewCreateCriteria.InitNewRow += GrdviewCreateCriteria_InitNewRow;

      }

      private void GrdviewCreateCriteria_InitNewRow(object sender, InitNewRowEventArgs e)
      {
         // DEFAULT VALUES
         GridView view = sender as GridView;
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CREATED"], DateTime.Today);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_LAST_UPD"], DateTime.Today);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CREATED_BY"], "system");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_LAST_UPD_BY"], "system");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_MODIFICATION_NUM"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_INTERNAL"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["ENT_CODE"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_TABLE"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_FIELDS"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_WHERE_SHOW"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["WLT_INTDESC"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_IS_CUSTOMER_LEVEL"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_STRATEGY"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_QUEUE"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_DYNAMIC_QUEUE"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_WORKLIST"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_REVOCATION"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_ORDER"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_RETURN_DUPLICATE_FLAG"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_IS_TERRITORIAL"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_USER_DEFINED_FLAG"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_DEPLOYED_FLAG"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CLOSED_CASES"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_SETTLEMENT_FLAG"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_SINGLE_VALUE_SELECTION_FLAG"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_PROCESS_INSTANCES_FLAG"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_DECISION_TREE_ENTRY"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_DECISION_TREE"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_SCORING"], 1);



         //////////////////////////
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_DESC"], "Test");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_TYPE"], 430);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CATEGORY"], 244);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRJ_CODE"], 1);
         
         
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_WHERE_TABLE"], "AT_CASE_EXTRA");





      }

      private void SetCriteriaUniqueIds()
      {

      }


      private void btnCreateSQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {

            btnCreateSQL.Enabled = false;
            grdviewCreateCriteria.CloseEditor();
            grdviewCreateCriteria.UpdateCurrentRow();

            backgroundWorker2.RunWorkerAsync();
         }
         catch (Exception ex)
         {
            btnCreateSQL.Enabled = true;
         }
         finally
         {

         }
      }

      private Dictionary<string,int> GetLengths()
      {
         Dictionary<string, int> ret = new Dictionary<string, int>();
         
         foreach (DataColumn col in _CreateData.Tables[0].Columns)
         {
            int len = col.ColumnName.Length+2;
            foreach (DataRow row in _CreateData.Tables[0].Rows)
            {
               len = Math.Max(GetValue(col, row).Length+2, len);
            }
            ret.Add(col.ColumnName, len);
         }
         return ret;
      }

      private string Pad(Dictionary<string, int> lens, string columnName, string value)
      {
         int len = lens[columnName];
         return value.PadRight(len);
      }

      private void CreateSQL()
      {
         StringBuilder b = new StringBuilder();
         StringBuilder b2 = new StringBuilder();
         bool first = true;
         bool last = false;
         Dictionary<string, int> lens = GetLengths();
         b2.Clear();
         b.AppendLine($"--     Database :: {_Info.Server}.{_Info.Database}");
         b.AppendLine("--");
         b.AppendLine($"-- New Criteria :: ");
         b.AppendLine("--");
         foreach (DataRow row in _CreateData.Tables[0].Rows)
         {
            b.AppendLine($"--    {CriterioToString(row, lens)}");
         }
         b.AppendLine();
         
         b.Append("-- New Criteria         ");

         foreach (DataColumn col in _CreateData.Tables[0].Columns)
         {
            if (!col.ColumnName.Equals("CRI_CODE"))
            {
               if (!first) b.Append(", ");
               b.Append(col.ColumnName);

               b2.Append(Pad(lens, col.ColumnName, col.ColumnName+"  "));
               first = false;
            }
            
         }
         b.Append(" ".PadLeft(10));
         b.AppendLine(b2.ToString());
         b.AppendLine(new string('-', b.ToString().Length));

         foreach (DataRow row in _CreateData.Tables[0].Rows)
         {
            first = true;
            last = false;
            b2.Clear();
            b.Append("INSERT INTO AT_CRITERIA(");
            int index = 0;
            foreach (DataColumn col in _CreateData.Tables[0].Columns)
            {
               if (!col.ColumnName.Equals("CRI_CODE"))
               {
                  last = (index == _CreateData.Tables[0].Columns.Count - 2);  // crj_code is excluded
                  if (!first) b.Append(", ");
                  b.Append(col.ColumnName);

                  b2.Append(Pad(lens, col.ColumnName, GetValue(col, row) + (last ? "  " : ", ")));

                  first = false;
                  index++;
               }
            }
            b.Append(") VALUES (");
            b.Append(b2.ToString());
            b.AppendLine(")");

         }

         txtGeneratedSQL.Text = b.ToString();
      }

      private string GetValue(DataColumn col, DataRow row)
      {
         string ret = string.Empty;
         if(DBNull.Value.Equals(row[col]))
         {            
            ret = "''";
         }
         else if (col.DataType == typeof(string))
         {
            ret = DBNull.Value.Equals(row[col]) ? "" : ret=$"'{row[col].ToString().Replace("'","''")}'";
         }
         else if (col.DataType == typeof(bool))
         {
            ret = Convert.ToInt16(row[col]).ToString();
         }
         else if (col.DataType == typeof(DateTime))
         {
            ret = $"'{Convert.ToDateTime(row[col]).ToString("yyyy-MM-dd")}'";
         }
         else
         {
            ret = row[col].ToString();
         }

         return ret;
      }

      private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(_Info.Server, _Info.Database)))
            {
               con.FireInfoMessageEventOnUserErrors = true;
               //con.InfoMessage += Con_InfoMessage;
               StringBuilder builder = new StringBuilder();
               builder.Append(@"DECLARE @NEW_CRI_UNIQUE_ID NVARCHAR(50)
                              SELECT TOP(1) @NEW_CRI_UNIQUE_ID = CONVERT(NVARCHAR(50), SUBSTRING(CRI_UNIQUE_ID, CHARINDEX('_', CRI_UNIQUE_ID) + 1, 100)+1)
                              FROM AT_CRITERIA
                              WHERE CRI_UNIQUE_ID LIKE '%CRITERIO_%'
                              AND CRI_UNIQUE_ID NOT LIKE '%CRITERIO_NEMO_%'
                              ORDER BY CRI_CODE DESC
                              
                              SELECT @NEW_CRI_UNIQUE_ID AS CRI_UNIQUE_ID");
              
               SqlCommand com = new SqlCommand(builder.ToString(),con);
               con.Open();
               e.Result = com.ExecuteScalar();
               con.Close();
            }
         }
         catch (Exception ex)
         {
            e.Result = ex;
         }
      }

      private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result != null)
         {
            if (e.Result is string)
            {
               int newCriUniqueId = Convert.ToInt32(e.Result);
               for(int i=0;i< _CreateData.Tables[0].Rows.Count;i++)
               {
                  _CreateData.Tables[0].Rows[i]["CRI_UNIQUE_ID"] = $"CRITERIO_{newCriUniqueId}";
                  _CreateData.Tables[0].Rows[i]["CRI_SYNC_GUID"] = $"CRITERIO_{newCriUniqueId}";
                  newCriUniqueId++;
               }
               _CreateData.Tables[0].AcceptChanges();
               CreateSQL();
               btnCreateSQL.Enabled = true;

            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         
      }

      private void btnCloneCriterio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {
            _CreateData.Tables[0].Rows.Add(grdviewSelectCriteria.GetFocusedDataRow().ItemArray);
         }
         catch (ConstraintException exp)
         {
            MessageBox.Show("This row is already exists");
         }
      }

      private void btnDeleteCriterio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if (MessageBox.Show("Delete Criterio?", "Confirmation", MessageBoxButtons.YesNo) !=
              DialogResult.Yes)
            return;
         grdviewCreateCriteria.DeleteRow(grdviewCreateCriteria.FocusedRowHandle);
      }


      private string CriterioToString(DataRow row, Dictionary<string,int> lens)
      {
         StringBuilder b = new StringBuilder();
         if(row != null)
         {
            b.Append($"{ row["CRI_UNIQUE_ID"]} : {Pad(lens, "CRI_DESC", row["CRI_DESC"].ToString())} ");

            string categoryDesc = _SelectData.Tables[CRITERIA_CATEGORIES].Select($"LOV_CODE={row["CRI_CATEGORY"]}")[0]["LOV_DESC"].ToString();
            b.Append($"[Category:{categoryDesc} ");

            b.Append($"{((bool)row["CRI_STRATEGY"] ? ",Strategies":"")}");
            b.Append($"{((bool)row["CRI_QUEUE"] ? ",Queues" : "")}");
            b.Append($"{((bool)row["CRI_DYNAMIC_QUEUE"] ? ",Dynamic Queues" : "")}");
            b.Append($"{((bool)row["CRI_WORKLIST"] ? ",Worklists" : "")}");
            b.Append($"{((bool)row["CRI_REVOCATION"] ? ",Revocation" : "")}");
            b.Append($"{((bool)row["CRI_DECISION_TREE"] ? ",Decision Trees" : "")}");
            b.Append($"{((bool)row["CRI_DECISION_TREE_ENTRY"] ? ",Decision Trees Entries" : "")}");

            b.Append("]");
         }
         return b.ToString();
      }
   }
}