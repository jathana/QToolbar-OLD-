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
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace QToolbar.Tools
{
   public partial class Frm_ScriptCriteria : DevExpress.XtraEditors.XtraForm
   {
      private ConnectionInfo _Info;
      private StringBuilder connectionMsg = new StringBuilder();

      public Frm_ScriptCriteria()
      {
         InitializeComponent();
      }


      public void Show(ConnectionInfo info)
      {
         _Info = info;
         SetSQL();
         Text = $"Criteria Scripter - {_Info.Server} . {_Info.Database}";
         Show();
         btnRun_ItemClick(null,null);
      }

      private void SetSQL()
      {
         txtSQL.Text = "SELECT CRI_UNIQUE_ID, CRI_DESC FROM AT_CRITERIA WHERE CRI_USER_DEFINED_FLAG=0 ORDER BY CRI_CREATED DESC";
      }

      private void txtScriptDirectory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
          if(folderBrowserDialog1.ShowDialog()==DialogResult.OK)
         {
            txtScriptDirectory.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {

            btnRun.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
         }
         catch (Exception ex)
         {
            //progressPanel1.Visible = false;
            btnRun.Enabled = true;
         }
      }


      private bool IsUIInputValid()
      {
         bool retVal = true;
         StringBuilder builder = new StringBuilder();
         if(string.IsNullOrEmpty(txtScriptDirectory.Text))
         {
            retVal = false;
            builder.AppendLine("Script Directory is missing!");
         }

         int[] selRows = gridView1.GetSelectedRows();
         if (selRows.Length== 0)
         {
            retVal = false;
            builder.AppendLine("There aren't any criteria selected!");
         }
         else
         {
            // check first row
            string criUniqueId = (string)gridView1.GetRowCellValue(selRows[0], "CRI_UNIQUE_ID");
            if(criUniqueId==null)
            {
               retVal = false;
               builder.AppendLine("Column CRI_UNIQUE_ID is missing from sql statement!");
            }
         }

         int[] instRows = gviewInstallations.GetSelectedRows();
         if (instRows.Length == 0)
         {
            retVal = false;
            builder.AppendLine("Installation not selected!");
         }

         if (!retVal)
         {
            XtraMessageBox.Show(builder.ToString(), "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }



         return retVal;
      }


      private void btnScript_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if(!IsUIInputValid())  return;

         if (!Directory.Exists(txtScriptDirectory.Text))
            Directory.CreateDirectory(txtScriptDirectory.Text);

         using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(_Info.Server, _Info.Database)))
         {
            try
            {
               con.Open();
               con.InfoMessage += Con_InfoMessage;
               

               // get installation codes
               var selInsts = gviewInstallations.GetSelectedRows();
               StringBuilder instBuilder = new StringBuilder();

               for(int i=0;i<selInsts.Length;i++)
               {
                  if (i > 0) instBuilder.Append(",");
                  int instHandle = selInsts[i];
                  instBuilder.Append((int)gviewInstallations.GetRowCellValue(instHandle, "INST_CODE"));
               }
               var selRows = gridView1.GetSelectedRows();
               int criteriaScripted = 0;
               int criteriaTried = 0;
               foreach (int rowHandle in selRows)
               {
                  criteriaTried++;
                  // clear messages buffer before criterio scripting
                  connectionMsg.Clear();

                  string criUniqueId = (string)gridView1.GetRowCellValue(rowHandle, "CRI_UNIQUE_ID");
                  
                  SqlCommand command = new SqlCommand("Man_ScriptCriterion_PerInstallation", con);

                  command.CommandType = CommandType.StoredProcedure;
                  command.Parameters.Add("@cri_unique_id", SqlDbType.NVarChar, 255).Value = criUniqueId;
                  command.Parameters.Add("@inst_codes", SqlDbType.NVarChar, 1000).Value = instBuilder.ToString();
                  //connectionMsg.Clear();
                  string ret = (string)command.ExecuteScalar();

                  File.WriteAllText(Path.Combine(txtScriptDirectory.Text, criUniqueId + ".sql"), connectionMsg.ToString(), Encoding.Unicode);
                  criteriaScripted++;
               }

               XtraMessageBox.Show($"Criteria were scripted succesfully (tried:{criteriaTried},scripted:{criteriaScripted})!","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            finally
            {
               con.InfoMessage -= Con_InfoMessage;
               con.Close();
            }

         }
      }

      private void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
      {
         connectionMsg.AppendLine(e.Message);
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
               builder.Append(txtSQL.Text);
               builder.Append(';');
               builder.Append("SELECT INST_CODE,INST_PREFIX,INST_DESC FROM AT_INSTALLATIONS ORDER BY INST_DESC");
               SqlDataAdapter adapter = new SqlDataAdapter(builder.ToString() , con);

               DataSet dataset = new DataSet();
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
               DataSet ds = (DataSet)e.Result;
               gridView1.Columns.Clear();
               gridControl1.DataSource = ds.Tables[0];
               grdInstallations.DataSource = ds.Tables[1];
            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         btnRun.Enabled = true;
      }

      private void Frm_ScriptCriteria_Load(object sender, EventArgs e)
      {
         gridView1.OptionsSelection.MultiSelect = true;
         gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
         cboInstallations.QueryResultValue += popupContainerEdit1_QueryResultValue;
         cboInstallations.QueryPopUp += popupContainerEdit1_QueryPopUp;


         gviewInstallations.OptionsBehavior.Editable = false;
         gviewInstallations.OptionsView.ShowGroupPanel = false;


         gridView1.OptionsBehavior.Editable = false;
         gridView1.OptionsView.ShowGroupPanel = false;

      }

      void popupContainerEdit1_QueryResultValue(object sender, QueryResultValueEventArgs e)
      {
         int[] selectedRows = gviewInstallations.GetSelectedRows();
         StringBuilder sb = new StringBuilder();
         foreach (int selectionRow in selectedRows)
         {
            DataRow row = ((DataRowView)gviewInstallations.GetRow(selectionRow)).Row;
            if (sb.ToString().Length > 0) { sb.Append(", "); }
            sb.Append($"{row["INST_PREFIX"]}");
         }
         e.Value = sb.ToString();
      }

      void popupContainerEdit1_QueryPopUp(object sender, CancelEventArgs e)
      {
         object val = cboInstallations.EditValue;
         if (val == null)
            gviewInstallations.ClearSelection();
         else
         {
            string[] texts = val.ToString().Split(',');
            foreach (string text in texts)
            {
               int rowHandle = gridView1.LocateByValue("INST_PREFIX", text.Trim());
               gviewInstallations.SelectRow(rowHandle);
            }

         }
      }

      private void btnSelectFromFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if(openFileDialog1.ShowDialog()==DialogResult.OK)
         {
            List<string> lines = File.ReadLines(openFileDialog1.FileName).ToList();
            int linesMatched = 0;
            StringBuilder notMatched = new StringBuilder();
            foreach(var criuniqueid in lines)
            {
               if(!string.IsNullOrEmpty(criuniqueid))
               {
                  int rowHandle = gridView1.LocateByValue("CRI_UNIQUE_ID", criuniqueid.Trim());
                  if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
                  {
                     gridView1.SelectRow(rowHandle);                     
                     linesMatched++;
                  }
                  else
                  {
                     notMatched.Append(criuniqueid);
                     notMatched.Append(" ");
                  }
               }
            }

            // validate
            var selRows = gridView1.GetSelectedRows();
            int criteriaNotSelected = 0;
            int criteriaChecked = 0;
            StringBuilder notSelected = new StringBuilder();

            foreach (var criuniqueid in lines)
            {
               if(!string.IsNullOrEmpty(criuniqueid))
               {
                  int rowHandle = gridView1.LocateByValue("CRI_UNIQUE_ID", criuniqueid);
                  if (!gridView1.IsRowSelected(rowHandle))
                  {
                     notSelected.Append(criuniqueid);
                     notSelected.Append(" ");
                     criteriaNotSelected++;
                  }
                  criteriaChecked++;
               }
            }
            XtraMessageBox.Show($"Lines read:{lines.Count}, criteria selected: {linesMatched}, checked:{criteriaChecked}, not selected:{criteriaNotSelected} ({notSelected.ToString()}), not matched:{notMatched.ToString()}");
            
         }
      }
   }
}