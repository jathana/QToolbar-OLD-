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
using DevExpress.XtraGrid.Columns;
using System.IO;
using QToolbar.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace QToolbar.Plugins.Environments
{
   public partial class Frm_UpdateCFs : DevExpress.XtraEditors.XtraForm
   {

      public class CfInfo : QEnvironment.CfInfo
      {
         private string _PreviewPath;
         private string _BackupPath;
         private string _Result;

         public string BackupPath
         {
            get
            {
               return _BackupPath;
            }

            set
            {
               _BackupPath = value;
            }
         }

         public string Result
         {
            get
            {
               return _Result;
            }

            set
            {
               _Result = value;
            }
         }

         public string PreviewPath
         {
            get
            {
               return _PreviewPath;
            }

            set
            {
               _PreviewPath = value;
            }
         }
      }

      private const string DATABASES_SECTION = "DatabaseName";
      private const string SERVERS_SECTION   = "Servers";
      private const string PASSWORDS_SECTION = "Passwords";

      private QEnvironment _QEnv;
      private List<CfInfo> _CFsInfo;
      private Frm_TxtDiff _FrmTxtDiff;
      private string _BackupDir;
      private string _PreviewDir;

      public Frm_UpdateCFs()
      {
         InitializeComponent();
         
      }


      public void Show(QEnvironment qenv)
      {
         _QEnv = qenv;
         Show();
      }

      private void Frm_UpdateCFs_Load(object sender, EventArgs e)
      {
         InitGrid();
         ShowCFsData();
         PrepareChangesForPreview();
      }

      private void InitGrid()
      {
         // select criteria grid
         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.ColumnAutoWidth = false;
         UXGridView.OptionsSelection.MultiSelect = true;
         UXGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
         UXGridView.OptionsView.ShowGroupPanel = false;
         //barManager1.AllowQuickCustomization = false;
         //barManager1.AllowCustomization = false;
         //barManager1.AllowMoveBarOnToolbar = false;
      }

      private void ShowCFsData()
      {
         // fill input fields
         txtKey.Text = _QEnv.Name;
         txtServer.Text = _QEnv.DBCollectionPlusServer;
         txtDatabase.Text = _QEnv.DBCollectionPlusName;

         // get cf data
         _CFsInfo = _QEnv.CFs.Select(a => new CfInfo()
            {
               Name = a.Name,
               Path = a.Path,
               Repository = a.Repository
            }).ToList();

         // add columns in specific order
         UXGridView.Columns.AddField("Name").Visible = true; 
         UXGridView.Columns.AddField("Path").Visible = true; 
         UXGridView.Columns.AddField("Repository").Visible = true; 
         //UXGridView.Columns.AddField("BackupPath").Visible = false; 
         UXGridView.Columns.AddField("Result").Visible = true; 
         
         // set datasource
         UXGrid.DataSource = _CFsInfo;
         UXGridView.BestFitColumns();
      }

      

      private void PrepareChangesForPreview()
      {
         _PreviewDir = Path.Combine(AppInstance.CacheDirectory, DateTime.Now.TimeStamp());
         if (Utils.EnsureFolder(_PreviewDir))
         {
            foreach (CfInfo info in _CFsInfo)
            {
               string prefix = info.Path.Replace("\\", "_").Replace("$", "_").Replace(":","_");
               info.PreviewPath = Path.Combine(_PreviewDir, prefix);

               File.Copy(info.Path, info.PreviewPath);

               IniFile2.WriteValue(SERVERS_SECTION, txtKey.Text, txtServer.Text,info.PreviewPath);
               IniFile2.WriteValue(DATABASES_SECTION, txtKey.Text, txtDatabase.Text, info.PreviewPath);
               IniFile2.WriteValue(PASSWORDS_SECTION, txtKey.Text, txtPassword.Text, info.PreviewPath);
            }
            UXGridView.BestFitColumns();
         }

      }

      private void btnApply_Click(object sender, EventArgs e)
      {

         if (XtraMessageBox.Show($"Are you sure to proceed with cf updates?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
         {

            StringBuilder b = new StringBuilder();
            var selRows = UXGridView.GetSelectedRows();
            foreach (int rowHandle in selRows)
            {
               CfInfo info = (CfInfo)UXGridView.GetRow(rowHandle);
               b.AppendLine($"file:{info.Path}");


            }
         }   
      }

      private string UpdateCf(string cfFile, string section, string key, string server, string database, string password)
      {
         StringBuilder retval = new StringBuilder();
         retval.AppendLine(UpdateCfKey(cfFile, SERVERS_SECTION, key, server));
         retval.AppendLine(UpdateCfKey(cfFile, DATABASES_SECTION, key, database ));
         retval.AppendLine(UpdateCfKey(cfFile, PASSWORDS_SECTION, key, password));

         return retval.ToString();
      }


      private string UpdateCfKey(string cfFile, string section, string key, string value)
      {
         string message = string.Empty;

         try
         {
            string[] keys = IniFile2.ReadKeys(section, cfFile);

            if (!keys.Contains(key))
               message = $"Key '{key}={value}' in '{section}' section was added.";
            else
            {
               string serverVal = IniFile2.ReadValue(section, key, cfFile, null);
               if (!serverVal.Equals(value))
                  message = $"Key '{key}={value}' in '{section}' section was updated (old value: {serverVal})";
               else
                  message = $"Key '{key}={value}' in '{section}' section was not changed. ";
            }
            //IniFile2.WriteValue(section, key, value, cfFile);
         }
         catch(Exception ex)
         {
            message = $"Failed to update cf's {cfFile} key {key}={value} in section {section} ({ex.Message}).";
         }
         return message;
      }


      private void UXGrid_DoubleClick(object sender, EventArgs e)
      {
         string sourceFile = ((CfInfo)UXGridView.GetFocusedRow()).Path;
         string destFile = ((CfInfo)UXGridView.GetFocusedRow()).PreviewPath;
         if (_FrmTxtDiff == null || _FrmTxtDiff.IsDisposed)
         {
            _FrmTxtDiff = new Frm_TxtDiff();
         }
         _FrmTxtDiff.CompareFiles(sourceFile, destFile, $"Original {sourceFile}", $"Preview changes {sourceFile}",FastColoredTextBoxNS.Language.CSharp);
      }

      private void CloseForm()
      {
         if (Directory.Exists(_PreviewDir))
         {
            Directory.Delete(_PreviewDir,true);
         }
         if (_FrmTxtDiff != null && !_FrmTxtDiff.IsDisposed)
         {
            _FrmTxtDiff.Close();
         }
         Close();
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
         CloseForm();
      }

      private void Frm_UpdateCFs_FormClosing(object sender, FormClosingEventArgs e)
      {
         CloseForm();
      }
   }
}