using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class NextBuildButton:ButtonBase
   {
      public NextBuildButton(BarManager barManager, BarSubItem menu):base("",barManager, menu, NextBuild_ItemClick, ShouldAddItem)
      {
      }

      public void CreateItems()
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
         BarButtonItem nextBuildItem = new BarButtonItem(_BarManager, row["Name"].ToString(), 0);
         nextBuildItem.ItemClick += NextBuild_ItemClick;
         nextBuildItem.Tag = row;
         _Menu.AddItem(nextBuildItem);
      }

      private static void NextBuild_ItemClick(object sender, ItemClickEventArgs e)
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
