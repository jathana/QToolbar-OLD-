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
      public FieldsExplorerButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.FieldsExplorerFolder, barManager, menu, FieldsExplorer_ItemClick)
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
            string app = Path.Combine(OptionsInstance.FieldsExplorerFolder, e.Item.Caption, "SCToolkit.Utilities.FieldExplorer.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
