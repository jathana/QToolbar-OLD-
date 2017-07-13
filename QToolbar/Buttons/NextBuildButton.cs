using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class NextBuildButton:ButtonBase
   {
      public NextBuildButton(BarManager barManager, BarSubItem menu):base("",barManager, menu)
      {
      }

      public override void CreateMenuItems()
      {
         _Menu.ClearLinks();

         // load custom folders
         try
         {
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               AddNextBuildItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void AddNextBuildItem(DataRow row)
      {
         if (Directory.Exists(row["Path"].ToString()))
         {
            BarButtonItem nextBuildItem = new BarButtonItem(_BarManager, row["Name"].ToString(), 0);
            nextBuildItem.ItemClick += MenuItemClick;
            nextBuildItem.Tag = row;
            _Menu.AddItem(nextBuildItem);
         }
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            DataRow row = (DataRow)e.Item.Tag;
            Frm_NextBuildChecker f = new Frm_NextBuildChecker();
            f.Show(row["Name"].ToString(), row["Path"].ToString());
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
