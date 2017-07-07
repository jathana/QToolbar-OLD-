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
      public DatabaseScripterButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.DatabaseScripterFolder, barManager, menu)
      {
         
      }

      private string GetItemPath(string itemCaption)
      {
         return Path.Combine(OptionsInstance.DatabaseScripterFolder, itemCaption, "QCSScript.application");
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
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

      protected override bool ShouldAddMenuItem(BarButtonItem menuItem)
      {
         return File.Exists(GetItemPath(menuItem.Caption));
      }
   }
}
