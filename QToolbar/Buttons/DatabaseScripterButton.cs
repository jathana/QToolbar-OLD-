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
   public class DatabaseScripterButton : ButtonBase
   {
      public DatabaseScripterButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.DatabaseScripterFolder, barManager, menu, DatabaseScripter_ItemClick)
      {
         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void DatabaseScripter_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = Path.Combine(OptionsInstance.DatabaseScripterFolder, e.Item.Caption, "QCSScript.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
