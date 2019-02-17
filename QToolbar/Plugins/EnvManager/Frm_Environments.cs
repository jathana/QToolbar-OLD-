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
using System.IO;
using QToolbar.Options;
using DevExpress.XtraBars;
using QToolbar.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using System.Threading;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.Xml.Serialization;
using System.Xml;
using QToolbar.Plugins.EnvManager;

namespace QToolbar.Plugins.EnvManager
{
   public partial class Frm_Environments : DevExpress.XtraEditors.XtraForm
   {
      private CfFile _QbcAdminCF;

      SynchronizationContext _SyncContext;

      private QEnvLoader _EnvLoader;

      public Frm_Environments()
      {
         InitializeComponent();

         //UXGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
         //UXGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
         //UXGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
         // test
         _EnvLoader = new QEnvLoader();
            _EnvLoader.InfoCollected += _EnvLoader_InfoCollected;
            _EnvLoader.AllInfoCollected += _EnvLoader_AllInfoCollected;

         UXGridView.OptionsView.ColumnAutoWidth = false;
         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         UXGrid.ViewRegistered += UXGrid_ViewRegistered;

         UXGridView.OptionsPrint.PrintDetails = true;
         UXGridView.OptionsPrint.ExpandAllDetails = true;
         UXGridView.OptionsPrint.ExpandAllGroups = true;

         UXGridView.RowStyle += UXGridView_RowStyle;
         _SyncContext = SynchronizationContext.Current;


      }

        private void _EnvLoader_AllInfoCollected(object sender, EventArgs e)
        {
            EnableButtons(true);
        }

        private void _EnvLoader_InfoCollected(object sender, EnvManager.EnvInfoEventArgs e)
        {
            _SyncContext.Post((input) =>
            {

                QEnv env = (input as EnvManager.EnvInfoEventArgs).Environment;
                if (env != null)
                {
                    int rowHandle = UXGridView.LocateByValue("Name", env.Name);
                    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    {
                        UXGridView.SetRowCellValue(rowHandle, "Status", $"Ready({env.Errors.GetStrongestDesc()})");
                    }
                }
                UXGridView.BestFitColumns();
            }, e);
        }

        private void _Envs_AllInfoCollected(object sender, EventArgs e)
      {
         EnableButtons(true);
      }

      private void UXGridView_RowStyle(object sender, RowStyleEventArgs e)
      {
         //if (_ColorRows.Contains(e.RowHandle))
         //{
         //   e.Appearance.ForeColor = Color.Green;
         //}
      }

      private void UXGrid_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
      {
         e.View.DoubleClick += View_DoubleClick;
         ((GridView)e.View).BestFitColumns();
      }

      private void View_DoubleClick(object sender, EventArgs e)
      {
         GridView view = (GridView)sender;
         Point pt = view.GridControl.PointToClient(Control.MousePosition);
         DoRowDoubleClick(view, pt);
      }

      #region Environments Menu

      private void Frm_Environments_Load(object sender, EventArgs e)
      {
        CreateEnvironmentsMenuItems();

         UXGrid.DataSource = _EnvLoader.Data;

      }


      public void CreateEnvironmentsMenuItems()
      {
         mnuEnvironments.ClearLinks();
         BarButtonItem mniClearAll = new BarButtonItem(null, "Clear List", 1);
         mniClearAll.ItemClick += MniClearAll_ItemClick;
         mnuEnvironments.AddItem(mniClearAll);
         // load custom folders
         try
         {
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               AddEnvironmentItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void MniClearAll_ItemClick(object sender, ItemClickEventArgs e)
      {
         RemoveAllSelectedEnvironments();
      }

      private void RemoveAllSelectedEnvironments()
      {
         foreach (var mnuItem in mnuEnvironments.ItemLinks)
         {
            if (mnuItem.GetType().Equals(typeof(BarSubItemLink)))
            {

               foreach (var itemLink in ((BarSubItemLink)mnuItem).Item.ItemLinks)
               {
                  (itemLink as BarEditItemLink).EditValue = false;
               }
            }
         }
      }

      private void AddEnvironmentItem(DataRow row)
      {
         if (Directory.Exists(row["Path"].ToString()))
         {
            BarSubItem envMenu = new BarSubItem(null, row["Name"].ToString(), 0);
            mnuEnvironments.AddItem(envMenu);
            // get qbc_admin
            string cfFile=Path.Combine(row["Path"].ToString(), @"VS Projects\QCS\QCSClient\QBC_Admin.cf");
            _QbcAdminCF = new CfFile(cfFile);
            List<string> keys = _QbcAdminCF.GetKeys();
            foreach(string key in keys)
            {
               repositoryItemCheckEdit1.Caption = "";
               BarEditItem envItem = new BarEditItem(null, repositoryItemCheckEdit1);
               envItem.CaptionAlignment = DevExpress.Utils.HorzAlignment.Center;
               envItem.ContentHorizontalAlignment = BarItemContentAlignment.Center;
               envItem.Caption = key;              
               envItem.EditValueChanged += EnvItem_EditValueChanged;
               envItem.Tag = new Tuple<string, string, string>(cfFile, row["Path"].ToString(), row["ProteusPath"].ToString());
               envMenu.AddItem(envItem);
               
            }
         }
      }

      private void EnvItem_EditValueChanged(object sender, EventArgs e)
      {
         try
         {
            BarEditItem item = (BarEditItem)sender;
            Tuple<string, string, string> tag = (Tuple<string, string, string>)item.Tag;
            CfFile cf = new CfFile(tag.Item1);
            if ((bool)item.EditValue)
            {

               EnableButtons(false);
               _EnvLoader.AddOrUpdate(item.Caption, cf, tag.Item2, tag.Item3);
            }
            else
            {
               _EnvLoader.Remove(item.Caption);
            }
            
            UXGridView.BestFitColumns();
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }

      }

      #endregion

      private void UXGridView_DoubleClick(object sender, EventArgs e)
      {
         GridView view = (GridView)sender;
         Point pt = view.GridControl.PointToClient(Control.MousePosition);
         DoRowDoubleClick(view, pt);
      }

      private void DoRowDoubleClick(GridView view, Point pt)
      {
         GridHitInfo info = view.CalcHitInfo(pt);
         if (info.InRow || info.InRowCell)
         {
            string cellText = view.FocusedValue.ToString(); 
            switch (info.Column.GetCaption())
            {
               case "Path":
               case "QBC Admin Cf Path":

                  if (File.Exists(cellText))
                  {
                     viewFile(cellText);
                  }
                  else
                  {
                     OpenDir(cellText);
                  }

                  break;
               case "Checkout Path":
               case "Proteus Checkout Path":
               case "Batch Executor Win Service UNC":
               case "Eod Executor Win Service UNC":
               case "Win Services Dir":
               case "GLM Dir":
               case "GLM Log Dir":
               case "UNC":

                  OpenDir(cellText);
                  break;
            }
         }
      }

      private void OpenDir(string path)
      {
         if (Directory.Exists(path))
         {
            Process.Start("explorer.exe", "/select, " + path);
         }
         else
         {
            XtraMessageBox.Show($"Dir {path} does not exist!");
         }
      }

      private void viewFile(string file)
      {
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            Frm_FileViewer f1 = new Frm_FileViewer();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }

      private void btnRemoveEnvsFromCFs_ItemClick(object sender, ItemClickEventArgs e)
      {
         //_Env.RemoveEnvsFromCFs();
      }

      private void Frm_Environments_FormClosing(object sender, FormClosingEventArgs e)
      {
         RemoveAllSelectedEnvironments();
      }

      private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
      {
         if (_EnvLoader.Data.Count > 0)
         {
            EnableButtons(false);
            UXGridView.CollapseAllDetails();
            _EnvLoader.Refresh();
            UXGridView.RefreshData();
         }
      }

      private void UXGrid_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Control && e.KeyCode == Keys.C)
         {
            GridView view = UXGrid.FocusedView as GridView;
            Clipboard.SetText(view.GetFocusedDisplayText());
            e.Handled = true;
         }
      }

      private void btnUpdateCFs_ItemClick(object sender, ItemClickEventArgs e)
      {

         //int[] selectedRows = UXGridView.GetSelectedRows();
         //if (selectedRows.Length > 0)
         //{
         //   QEnvironment qenv = ((QEnvironment)UXGridView.GetRow(selectedRows[0]));
         //   Frm_UpdateCFs f = new Frm_UpdateCFs();
         //   f.Show(qenv);
         //}
         //else
         //   XtraMessageBox.Show($"You have to select an environment first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }


      private void EnableButtons(bool enabled)
      {
         btnRefresh.Enabled = enabled;
         btnUpdateCFs.Enabled = enabled;
         btnExcelExport.Enabled = enabled;
      }

      private void btnExcelExport_ItemClick(object sender, ItemClickEventArgs e)
      {
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
         saveFileDialog1.Filter = "Excel Files|*.xls";
         saveFileDialog1.Title = "Save as Excel File";
         saveFileDialog1.ShowDialog();

         // If the file name is not an empty string open it for saving.  
         if (saveFileDialog1.FileName != "")
         {
            UXGrid.ExportToXls(saveFileDialog1.FileName, new XlsExportOptionsEx()
            {
               ExportType = DevExpress.Export.ExportType.WYSIWYG
            });
            Process.Start(saveFileDialog1.FileName);

            //Customize export options 

            //(UXGrid.MainView as GridView).OptionsPrint.PrintHeader = true;
            //XlsxExportOptionsEx advOptions = new XlsxExportOptionsEx();
            //advOptions.AllowGrouping = DevExpress.Utils.DefaultBoolean.True;
            //advOptions.ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.True;
            //advOptions.SheetName = "Exported from Data Grid";

            //UXGrid.ExportToPdf(saveFileDialog1.FileName);
            //// Open the created XLSX file with the default application. 
            //Process.Start(saveFileDialog1.FileName);



            //XmlSerializer xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(QEnvironments));

            //var xml = "";

            //using (var sww = new StringWriter())
            //{
            //   using (XmlWriter writer = XmlWriter.Create(sww))
            //   {
            //      xsSubmit.Serialize(writer, _Envs);
            //      xml = sww.ToString(); // Your XML
            //   }
            //}

         }
      }
   }
}