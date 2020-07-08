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
using QToolbar.Options;
using DevExpress.XtraBars;

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
      private List<ConnectionInfo> _DBs;
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
      private bool _CreateDataChanged = true;


      private bool CreateDataChanged
      {
         get { return _CreateDataChanged; }
         set
         {
            _CreateDataChanged = value;
            CreateDataChangedUI();
         }
      }

      public Frm_CriteriaHelper()
      {
         InitializeComponent();
      }

      public void Show(ConnectionInfo info, List<ConnectionInfo> dbs)
      {
         _Info = info;
         _DBs = dbs;
         Text = $"Criteria Helper - {_Info.Server} . {_Info.Database}";
         CreateDataChanged = true;
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
         repoLookupCriterioTable.DataSource = _SelectData.Tables[CRITERIA_WHERE_TABLES];
         repoLookupCriterioTable.DisplayMember = "TABLE_NAME";
         repoLookupCriterioTable.Name = "repoLookupCriterioTable";
         repoLookupCriterioTable.ValueMember = "TABLE_NAME";
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
         repoLookupSQLTypes.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
         repoLookupSQLTypes.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.True;


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

               
               SqlDataAdapter adapter = new SqlDataAdapter(GetBaseSQL(txtSelectSQL.Text), con);

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
         if (e.Result is Exception)
         {
            MessageBox.Show(((Exception)e.Result).Message);
         }
         else
         {
            try
            {
               if (e.Result != null)
               {
                  if (e.Result is DataSet)
                  {
                     _SelectData = (DataSet)e.Result;
                     grdviewSelectCriteria.Columns.Clear();
                     grdSelectCriteria.DataSource = _SelectData.Tables[SELECT_CRITERIA];

                     if (_CreateData.Tables.Count == 0)
                     {
                        _CreateData.Tables.Add(_SelectData.Tables[CREATE_CRITERIA].Clone());
                        // change CRI_CODE datatype to string in order to allow multi copies of the same criterio.
                        _CreateData.Tables[0].PrimaryKey = null; 
                        _CreateData.Tables[0].Columns["CRI_CODE"].DataType = typeof(string);
                        _CreateData.Tables[0].Columns["CRI_CODE"].ReadOnly = false;
                        _CreateData.Tables[0].ColumnChanged += Frm_CriteriaHelper_ColumnChanged;
                        _CreateData.Tables[0].TableNewRow += Frm_CriteriaHelper_TableNewRow;
                        _CreateData.Tables[0].RowChanged += Frm_CriteriaHelper_RowChanged;
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
               
               grdviewSelectCriteria.BestFitColumns();
               grdviewCreateCriteria.BestFitColumns();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
         }
         btnLoadCriteria.Enabled = true;         
      }

      private void Frm_CriteriaHelper_RowChanged(object sender, DataRowChangeEventArgs e)
      {
         CreateDataChanged = true;         
      }

      private void Frm_CriteriaHelper_TableNewRow(object sender, DataTableNewRowEventArgs e)
      {
         CreateDataChanged = true;
      }

      private void Frm_CriteriaHelper_ColumnChanged(object sender, DataColumnChangeEventArgs e)
      {
         CreateDataChanged = true;
      }


      private void CreateDataChangedUI()
      {
         mnuDevDBs.Enabled = !_CreateDataChanged;               
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
         grdviewCreateCriteria.ValidateRow += GrdviewCreateCriteria_ValidateRow;

         barManager1.AllowQuickCustomization = false;
         barManager1.AllowCustomization = false;
         barManager1.AllowMoveBarOnToolbar = false;
         LoadDevDBsMenu();

      }

      private void GrdviewCreateCriteria_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
      {
         StringBuilder b = new StringBuilder();

         DataRow row = (DataRow)((DataRowView)e.Row).Row;
         if (DBNull.Value.Equals(row["CRI_CATEGORY"]))
         {
            b.AppendLine("CRI_CATEGORY is Empty");
         }

         if (DBNull.Value.Equals(row["CRJ_CODE"]))
         {
            b.AppendLine("CRJ_CODE is Empty");
         }

         if (DBNull.Value.Equals(row["CRI_WHERE_TABLE"]))
         {
            b.AppendLine("CRI_WHERE_TABLE is Empty");
         }

         if (DBNull.Value.Equals(row["CRI_WHERE_FIELD"]))
         {
            b.AppendLine("CRI_WHERE_FIELD is Empty");
         }

         if (DBNull.Value.Equals(row["CRI_WHERE_FIELD_SQL_TYPE"]))
         {
            b.AppendLine("CRI_WHERE_FIELD_SQL_TYPE is Empty");
         }

         if (DBNull.Value.Equals(row["CRI_TYPE"]))
         {
            b.AppendLine("CRI_TYPE is Empty");
         }

         if (DBNull.Value.Equals(row["CRI_DESC"]))
         {
            b.AppendLine("CRI_DESC is Empty");
         }

         ValidateData(row, b);

         if (b.Length>0)
         {
            b.AppendLine();
            e.ErrorText = b.ToString();
            e.Valid = false;            
         }

      }


      private void ValidateData(DataRow row, StringBuilder errorBuilder)
      {
         using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(_Info.Server, _Info.Database)))
         {
            con.Open();

            // validate criteria join
            ValidateCriterioJoin(con, row, errorBuilder);
            
            // validate CRI_WHERE_FIELD_SQL_TYPE
            Validate_CRI_WHERE_FIELD_SQL_TYPE(con, row, errorBuilder);

            // Validate_CRI_WHERE_FIELD
            Validate_CRI_WHERE_FIELD(con, row, errorBuilder);

            con.Close();
         }

      }

      private void ValidateCriterioJoin(SqlConnection con, DataRow row, StringBuilder errorBuilder)
      {
         try
         {
            if(!DBNull.Value.Equals(row["CRJ_CODE"]) && !DBNull.Value.Equals(row["CRI_WHERE_TABLE"]))
            {
               int crjCode = (int)row["CRJ_CODE"];
               string criWhereTable = row["CRI_WHERE_TABLE"].ToString();
               SqlCommand com = new SqlCommand($"IF EXISTS(SELECT 1 FROM AT_CRITERIA_JOINS WHERE CRJ_JOIN LIKE '%{criWhereTable}%'  AND CRJ_CODE={crjCode}) SELECT 1 ELSE SELECT  0", con);
               
               int result = (int)com.ExecuteScalar();
               if(result==0)
               {
                  errorBuilder.AppendLine("Not found CRI_WHERE_TABLE in selected criterio join.");
               }               
            }
         }
         catch (Exception ex)
         {
            errorBuilder.AppendLine($"Error while validating CRJ_CODE ({ex.Message})");
         }
      }

      private void Validate_CRI_WHERE_FIELD_SQL_TYPE(SqlConnection con, DataRow row, StringBuilder errorBuilder)
      {
         try
         {
            if (!DBNull.Value.Equals(row["CRI_WHERE_FIELD_SQL_TYPE"]))
            {
               string criSqlType = row["CRI_WHERE_FIELD_SQL_TYPE"].ToString();               
               SqlCommand com = new SqlCommand($"if exists(select 1 from sys.types where name = '{criSqlType.Split('(')[0]}') select 1 else select 0", con);
               int result = (int)com.ExecuteScalar();
               if (result == 0)
               {
                  errorBuilder.AppendLine($"CRI_WHERE_FIELD_SQL_TYPE:{criSqlType} is not valid since was not found in sql data types.");
               }
               con.Close();
            }
         }
         catch (Exception ex)
         {
            errorBuilder.AppendLine($"Error while validating CRI_WHERE_FIELD_SQL_TYPE ({ex.Message})");
         }
      }

      private void Validate_CRI_WHERE_FIELD(SqlConnection con, DataRow row, StringBuilder errorBuilder)
      {
         try
         {
            if (!DBNull.Value.Equals(row["CRI_WHERE_FIELD"]) && !DBNull.Value.Equals(row["CRI_WHERE_TABLE"]))
            {
               string criWhereField = row["CRI_WHERE_FIELD"].ToString();
               string criWhereTable = row["CRI_WHERE_TABLE"].ToString();
               SqlCommand com = new SqlCommand($"SELECT {criWhereField} FROM {criWhereTable} WHERE 1=2", con);
               SqlDataAdapter adapter = new SqlDataAdapter(com);
               DataTable table = new DataTable();
               try
               {
                  adapter.Fill(table);
               }
               catch(Exception ex)
               {
                  errorBuilder.AppendLine($"Error while validating CRI_WHERE_FIELD ({ex.Message})");
               }
               finally
               {
                  con.Close();
               }
            }
         }
         catch (Exception ex)
         {
            errorBuilder.AppendLine($"Error while validating CRI_WHERE_FIELD ({ex.Message})");
         }
      }

      private string GetNewCriterioCriCode()
      {
                  
         string ret = "1".PadLeft(3,'0');

         // copy row but change CRI_CODE to allow copy an existing criterio multiple times
         // CRI_CODE contains the original cri_code and a number denoting the clone number of the same criterio.
         // E.g. The original CRI_CODE 8859 becomes 8859-1 for the first clone, 8859-2 for the second etc.
         int criCodeOrdinal = _CreateData.Tables[0].Columns["CRI_CODE"].Ordinal;
         int val;
         DataRow[] rows = _CreateData.Tables[0].Rows.Cast<DataRow>().Where(r=> int.TryParse(r["CRI_CODE"].ToString(),out val)).ToArray();
            
         if (rows.Length > 0)
         {
            // get the max number of 
            var max = rows.OrderByDescending(r => r[criCodeOrdinal].ToString()).FirstOrDefault().Field<string>("CRI_CODE");
            ret = Convert.ToString(Convert.ToInt32(max.Trim()) + 1);
         }
         return ret.PadLeft(3, '0');
      }
      private void GrdviewCreateCriteria_InitNewRow(object sender, InitNewRowEventArgs e)
      {
         // DEFAULT VALUES
         GridView view = sender as GridView;
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CODE"], GetNewCriterioCriCode());
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CREATED"], DateTime.Today);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_LAST_UPD"], DateTime.Today);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_CREATED_BY"], "system");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_LAST_UPD_BY"], "system");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_MODIFICATION_NUM"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_INTERNAL"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["ENT_CODE"], 1);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_TABLE"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_FIELDS"], "");
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_WHERE"], "");
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
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_SCORING"], 0);
         view.SetRowCellValue(e.RowHandle, view.Columns["CRI_RECOMPILE_FLAG"], 0); 

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

      private void CreateSQL(ConnectionInfo info, DataTable createTable, DataSet ds, Dictionary<string,List<string>> unresolvedColumns)
      {
         StringBuilder b = new StringBuilder();
         StringBuilder b2 = new StringBuilder();
         bool first = true;
         bool last = false;
         Dictionary<string, int> lens = GetLengths();
         b2.Clear();
         b.AppendLine($"--     Database :: {info.Server}.{info.Database}");
         b.AppendLine("--");
         b.AppendLine($"-- New Criteria :: ");
         b.AppendLine("--");
         foreach (DataRow row in createTable.Rows)
         {
            b.AppendLine($"--    {CriterioToString(row, lens, ds)}");
         }
         b.AppendLine();
         
         b.Append("-- New Criteria         ");

         foreach (DataColumn col in createTable.Columns)
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

         foreach (DataRow row in createTable.Rows)
         {
            first = true;
            last = false;
            b2.Clear();
            b.Append("INSERT INTO AT_CRITERIA(");
            int index = 0;
            foreach (DataColumn col in createTable.Columns)
            {
               if (!col.ColumnName.Equals("CRI_CODE"))
               {
                  last = (index == createTable.Columns.Count - 2);  // crj_code is excluded
                  if (!first) b.Append(", ");
                  b.Append(col.ColumnName);

                  string val = GetValue(col, row);
                  if (unresolvedColumns != null && unresolvedColumns[row["CRI_UNIQUE_ID"].ToString()].Contains(col.ColumnName.ToLower()))
                  {
                     val = new string('?', val.Length);
                  }
                  
                  b2.Append(Pad(lens, col.ColumnName, val + (last ? "  " : ", ")));

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
         if (col.DataType == typeof(string))
         {
            ret = DBNull.Value.Equals(row[col]) ? "" : ret=$"'{row[col].ToString().Replace("'","''")}'";
            
            if (DBNull.Value.Equals(row[col]))
            {
               // set null when cri_dependendent_unique_id, we store null in db
               if (col.ColumnName.ToUpper().Equals("CRI_DEPENDENT_UNIQUE_ID"))
               {
                  ret = "NULL";
               }
            }
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
         if (e.Result is Exception)
         {
            MessageBox.Show(((Exception)e.Result).Message);
         }
         else
         {
            if (e.Result != null)
            {
               if (e.Result is string)
               {
                  int newCriUniqueId = Convert.ToInt32(e.Result);
                  for (int i = 0; i < _CreateData.Tables[0].Rows.Count; i++)
                  {
                     _CreateData.Tables[0].Rows[i]["CRI_UNIQUE_ID"] = $"CRITERIO_{newCriUniqueId}";
                     _CreateData.Tables[0].Rows[i]["CRI_SYNC_GUID"] = $"CRITERIO_{newCriUniqueId}";
                     _CreateData.Tables[0].Rows[i]["CRI_CREATED"] = DateTime.Now;
                     _CreateData.Tables[0].Rows[i]["CRI_LAST_UPD"] = DateTime.Now;
                     newCriUniqueId++;
                  }
                  _CreateData.Tables[0].AcceptChanges();
                  CreateSQL(_Info, _CreateData.Tables[0], _SelectData, null);
                  CreateDataChanged = false;                  
               }
               else if (e.Result is Exception)
               {
                  this.Focus();
                  Exception ex = (Exception)e.Result;
                  XtraMessageBox.Show(ex.Message);
               }
            }
         }
         btnCreateSQL.Enabled = true;
         EnableScriptMenu(true);
      }

      private void btnCloneCriterio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {
            
            // copy row but change CRI_CODE to allow copy an existing criterio multiple times
            // CRI_CODE contains the original cri_code and a number denoting the clone number of the same criterio.
            // E.g. The original CRI_CODE 8859 becomes 8859-1 for the first clone, 8859-2 for the second etc.
            int criCodeOrdinal = _CreateData.Tables[0].Columns["CRI_CODE"].Ordinal;
            object[] copyRow = grdviewSelectCriteria.GetFocusedDataRow().ItemArray;
            string currentCriCode = copyRow[criCodeOrdinal].ToString();

            DataRow[] rows = _CreateData.Tables[0].Select($"CRI_CODE LIKE '{currentCriCode}-%'");
            int index = 1;
            if (rows.Length > 0)
            {
               // get the max number of 
               var max = rows.OrderByDescending(r => r[criCodeOrdinal]).FirstOrDefault().Field<string>("CRI_CODE").Split('-')[1];
               index = Convert.ToInt32(max) + 1;
            }
            // set CRI_CODE
            copyRow[criCodeOrdinal] = $"{currentCriCode}-{index.ToString().PadLeft(2,'0')}";
            DataRow newRow = _CreateData.Tables[0].Rows.Add(copyRow);

         }
         catch (ConstraintException exp)
         {
            MessageBox.Show("This row already exists");
         }
      }

      private void btnDeleteCriterio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if (MessageBox.Show("Delete Criterio?", "Confirmation", MessageBoxButtons.YesNo) !=
              DialogResult.Yes)
            return;
         grdviewCreateCriteria.DeleteRow(grdviewCreateCriteria.FocusedRowHandle);
         _CreateData.AcceptChanges();
      }


      private string CriterioToString(DataRow row, Dictionary<string,int> lens, DataSet ds)
      {
         StringBuilder b = new StringBuilder();
         if(row != null)
         {
            b.Append($"{ row["CRI_UNIQUE_ID"]} : {Pad(lens, "CRI_DESC", row["CRI_DESC"].ToString())} ");

            string categoryDesc = ds.Tables[CRITERIA_CATEGORIES].Select($"LOV_CODE={row["CRI_CATEGORY"]}")[0]["LOV_DESC"].ToString();
            b.Append($"[Category:{categoryDesc}");

            b.Append($"{((bool)row["CRI_STRATEGY"] ? ", Strategies":"")}");
            b.Append($"{((bool)row["CRI_QUEUE"] ? ", Queues" : "")}");
            b.Append($"{((bool)row["CRI_DYNAMIC_QUEUE"] ? ", Dynamic Queues" : "")}");
            b.Append($"{((bool)row["CRI_WORKLIST"] ? ", Worklists" : "")}");
            b.Append($"{((bool)row["CRI_REVOCATION"] ? ", Revocation" : "")}");
            b.Append($"{((bool)row["CRI_DECISION_TREE"] ? ", Decision Trees" : "")}");
            b.Append($"{((bool)row["CRI_DECISION_TREE_ENTRY"] ? ", Decision Trees Entries" : "")}");

            b.Append("]");
         }
         return b.ToString();
      }

      private void LoadDevDBsMenu()
      {
         
         // clear links
         mnuDevDBs.ClearLinks();

         var devdbs = _DBs.Where(d => OptionsInstance.DevSQLInstances.Contains(d.Server) && 
                                      d.Database.ToLower().StartsWith("qbcollection_plus_") &&
                                      d.Database!=_Info.Database).ToList();

         // add items
         if (devdbs != null)
         {
            try
            {
               foreach (ConnectionInfo info in devdbs)
               {
                  BarButtonItem menuItem = new BarButtonItem(barManager1, info.Database);
                  {
                     menuItem.ItemClick += MenuItem_ItemClick;
                     menuItem.Tag = info;
                     mnuDevDBs.AddItem(menuItem);
                  }
               }
            }
            catch (Exception ex)
            {
               XtraMessageBox.Show("Failed to load development databases information");
            }
         }
      }

      private void MenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         ConnectionInfo destDBInfo = (ConnectionInfo)((BarButtonItem)e.Item).Tag;
         if(destDBInfo != null)
         {
            EnableScriptMenu(false);
            workerScriptForOtherDB.RunWorkerAsync(new Tuple<DataSet,ConnectionInfo>(_CreateData.Clone(), destDBInfo));
         }
      }

      private void workerScriptForOtherDB_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {

            Tuple<DataSet, ConnectionInfo> arg = (Tuple<DataSet, ConnectionInfo>)e.Argument;
            DataSet createdDS = arg.Item1;

            ConnectionInfo info = (ConnectionInfo)arg.Item2;

            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(info.Server, info.Database)))
            {
               con.FireInfoMessageEventOnUserErrors = true;
               //con.InfoMessage += Con_InfoMessage;
               SqlDataAdapter adapter = new SqlDataAdapter(GetBaseSQL("SELECT 1"), con);

               // get information of target database
               DataSet otherDBDs = new DataSet();
               adapter.FillSchema(otherDBDs, SchemaType.Source);
               adapter.Fill(otherDBDs);
               otherDBDs.Tables[CREATE_CRITERIA].Columns["CRI_CODE"].DataType = typeof(string);
               e.Result = new Tuple<DataSet, ConnectionInfo>(otherDBDs, info);
            }
         }
         catch (Exception ex)
         {
            e.Result = ex;
         }
      }


      private string GetBaseSQL(string selectCriteriaSQL)
      {
         StringBuilder builder = new StringBuilder();
         builder.Append(selectCriteriaSQL);
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

         // SQL Types from criteria sql types
         builder.Append(';');
         builder.Append(@"SELECT CRI_WHERE_FIELD_SQL_TYPE  FROM AT_CRITERIA GROUP BY CRI_WHERE_FIELD_SQL_TYPE ORDER BY CRI_WHERE_FIELD_SQL_TYPE");

         

         return builder.ToString();
      }

      private void EnableScriptMenu(bool enable)
      {
         mnuDevDBs.Enabled = enable;
      }

      private void workerScriptForOtherDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result is Exception)
         {
            MessageBox.Show(((Exception)e.Result).Message);
         }
         else
         {

            StringBuilder b = new StringBuilder();

            Tuple<DataSet, ConnectionInfo> result = (Tuple<DataSet, ConnectionInfo>)e.Result;
            ConnectionInfo otherDBInfo = result.Item2;
            DataSet otherDBDs = (DataSet)result.Item1;

            // get table copy
            DataTable otherCreateTable = otherDBDs.Tables[CREATE_CRITERIA];
            DataRow[] lookupRows;
            String lookupDesc;

            // fix criterio category

            Dictionary<String, List<string>> unresolvedColumns = new Dictionary<string, List<string>>();

            foreach (DataRow row in _CreateData.Tables[0].Rows)
            {

               unresolvedColumns.Add(row["CRI_UNIQUE_ID"].ToString(), new List<string>());
               // add criterio row
               DataRow newRow = otherCreateTable.NewRow();
               foreach (DataColumn col in otherCreateTable.Columns)
               {
                  newRow[col.ColumnName] = row[col.ColumnName];
               }

               // get criterio category desc of original
               lookupDesc = _SelectData.Tables[CRITERIA_CATEGORIES].Select($"LOV_CODE={row["CRI_CATEGORY"]}")[0]["LOV_DESC"].ToString();
               // select category code of other db
               lookupRows = otherDBDs.Tables[CRITERIA_CATEGORIES].Select($"LOV_DESC='{lookupDesc}'");
               if (lookupRows.Length > 0)
               {
                  newRow["CRI_CATEGORY"] = (int)lookupRows[0]["LOV_CODE"];
               }
               else
               {
                  unresolvedColumns[row["CRI_UNIQUE_ID"].ToString()].Add("cri_category");
                  b.AppendLine($"Failed to get criterio category code for criterio {row["CRI_UNIQUE_ID"]}");
               }

               // get criterio data type
               lookupDesc = _SelectData.Tables[CRITERIA_TYPES].Select($"LOV_CODE={row["CRI_TYPE"]}")[0]["LOV_DESC"].ToString();
               // select DATA TYPE code of other db
               lookupRows = otherDBDs.Tables[CRITERIA_TYPES].Select($"LOV_DESC='{lookupDesc}'");
               if (lookupRows.Length > 0)
               {
                  newRow["CRI_TYPE"] = (int)lookupRows[0]["LOV_CODE"];
               }
               else
               {
                  unresolvedColumns[row["CRI_UNIQUE_ID"].ToString()].Add("cri_type");
                  b.AppendLine($"Failed to get criterio type code for criterio {row["CRI_UNIQUE_ID"]}");
               }

              

               // get criterio join
               lookupDesc = _SelectData.Tables[CRITERIA_JOINS].Select($"CRJ_CODE={row["CRJ_CODE"]}")[0]["CRJ_JOIN"].ToString();
               // select CRITERIO JOIN code of other db
               lookupRows = otherDBDs.Tables[CRITERIA_JOINS].Select($"CRJ_JOIN='{lookupDesc}'");
               if (lookupRows.Length > 0)
               {
                  newRow["CRJ_CODE"] = (int)lookupRows[0]["CRJ_CODE"];
               }
               else
               {
                  unresolvedColumns[row["CRI_UNIQUE_ID"].ToString()].Add("crj_code");
                  b.AppendLine($"Failed to get criterio join code for criterio {row["CRI_UNIQUE_ID"]}");
               }

               // add to table
               otherCreateTable.Rows.Add(newRow);

            }
            otherCreateTable.AcceptChanges();
            CreateSQL(otherDBInfo, otherCreateTable, otherDBDs, unresolvedColumns);
            if (b.Length > 0)
            {
               txtGeneratedSQL.Text += "\r\n" + b.ToString();
            }
         }
         EnableScriptMenu(true);

      }

      private void grdviewCreateCriteria_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
      {
      }
   }
}