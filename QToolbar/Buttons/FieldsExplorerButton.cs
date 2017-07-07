using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class FieldsExplorerButton : ButtonBase
   {
      public FieldsExplorerButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.FieldsExplorerFolder, barManager, menu, FieldsExplorer_ItemClick, ShouldAddItem)
      {
         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void FieldsExplorer_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = GetItemPath(e.Item.Caption);
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private static new bool ShouldAddItem(BarItem item)
      {
         return File.Exists(GetItemPath(item.Caption));
      }

      private static string GetItemPath(string itemCaption)
      {
         return Path.Combine(OptionsInstance.FieldsExplorerFolder, itemCaption, "SCToolkit.Utilities.FieldExplorer.application");
      }
   }
}
