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

namespace QToolbar.Plugins.Environments
{
   public partial class Frm_Environments : DevExpress.XtraEditors.XtraForm
   {
      private CfFile _QbcAdminCF;
      private EnvironmentsInfo _EnvsInfo;
      SynchronizationContext _SyncContext;


      public Frm_Environments()
      {
         InitializeComponent();
         _EnvsInfo = new EnvironmentsInfo();
         _EnvsInfo.PathsAdded += _EnvsInfo_PathsAdded;
         UXGridView.OptionsView.ColumnAutoWidth = false;
         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         _SyncContext = SynchronizationContext.Current;
      }

      private void _EnvsInfo_PathsAdded(object sender, EventArgs e)
      {
         _SyncContext.Post((a) => { UXGridView.BestFitColumns(); },null);
         
      }


      #region Environments Menu

      private void Frm_Environments_Load(object sender, EventArgs e)
      {
        CreateEnvironmentsMenuItems();

         UXGrid.DataSource = _EnvsInfo.Table;
         UXGridView.Columns["ENV_QC_SYSTEM_FOLDER"].ColumnEdit = new RepositoryItemMemoEdit();
         UXGridView.Columns["ENV_QC_LOCAL_SYSTEM_FOLDER"].ColumnEdit = new RepositoryItemMemoEdit();
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
               envItem.Tag = cfFile;
               envMenu.AddItem(envItem);
               
            }
         }
      }

      private void EnvItem_EditValueChanged(object sender, EventArgs e)
      {
         try
         {
            BarEditItem item = (BarEditItem)sender;
            CfFile cf = new CfFile(item.Tag.ToString());
            if ((bool)item.EditValue)
            {
               _EnvsInfo.AddOrUpdate(item.Caption, cf);
               UXGrid.DataSource = _EnvsInfo.Table;
            }
            else
            {
               _EnvsInfo.Remove(item.Caption);
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

      private static void DoRowDoubleClick(GridView view, Point pt)
      {
         GridHitInfo info = view.CalcHitInfo(pt);
         if (info.InRow || info.InRowCell)
         {
            
            switch (info.Column.GetCaption())
            {
               case "QBC_ADMIN_CF":
                  string file = view.GetFocusedDataRow()["QBC_ADMIN_CF"].ToString();
                  if (!string.IsNullOrEmpty(file) && File.Exists(file))
                  {
                     Frm_FileViewer f1 = new Frm_FileViewer();
                     f1.Size = new Size(800, 800);

                     f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
                  }
                  break;
            }
         }
      }
   }
}