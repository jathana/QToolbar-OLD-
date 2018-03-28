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

namespace QToolbar.Plugins.Environments
{
   public partial class Frm_Environments : DevExpress.XtraEditors.XtraForm
   {
      private CfFile _QbcAdminCF;

      private QEnvironments _Envs;
      SynchronizationContext _SyncContext;


      public Frm_Environments()
      {
         InitializeComponent();

         _Envs = new QEnvironments();
         _Envs.InfoCollected += _Envs_InfoCollected;
         UXGridView.OptionsView.ColumnAutoWidth = false;
         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         UXGrid.ViewRegistered += UXGrid_ViewRegistered;

         UXGridView.RowStyle += UXGridView_RowStyle;
         _SyncContext = SynchronizationContext.Current;
      }

      private void UXGridView_RowStyle(object sender, RowStyleEventArgs e)
      {
         //if (_ColorRows.Contains(e.RowHandle))
         //{
         //   e.Appearance.ForeColor = Color.Green;
         //}
      }

      private void _Envs_InfoCollected(object sender, EnvInfoEventArgs e)
      {
         _SyncContext.Post((input) =>
         {

            QEnvironment env = (input as EnvInfoEventArgs).Environment;
            if (env != null)
            {
               int rowHandle = UXGridView.LocateByValue("DBCollectionPlusName", env.DBCollectionPlusName);
               if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
               {
                  UXGridView.SetRowCellValue(rowHandle,"Status","Ready");
               }
            }
            UXGridView.BestFitColumns();
         }, e);
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

         UXGrid.DataSource = _Envs.Data;

      }


      public void CreateEnvironmentsMenuItems()
      {
         mnuEnvironments.ClearLinks();

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
               _Envs.AddOrUpdate(item.Caption, cf, tag.Item2, tag.Item3);
            }
            else
            {
               _Envs.Remove(item.Caption);
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
            string file = "";
            switch (info.Column.GetCaption())
            {
               case "Path":
                  file = view.FocusedValue.ToString();
                  viewFile(file);

                  break;
               case "QBC Admin Cf Path":
                  file = view.FocusedValue.ToString();
                  viewFile(file);
                  break;
            }
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
         _Envs.RemoveEnvsFromCFs();
      }
   }
}