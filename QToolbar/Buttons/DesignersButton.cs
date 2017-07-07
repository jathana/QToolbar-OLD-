using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QToolbar.Buttons
{
   public class DesignersButton: ButtonBase
   {
      public DesignersButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.DesignersFolder, barManager, menu, DesignerMenuItem_ItemClick, ShouldAddItem)
      {
         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void DesignerMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            XtraMessageBox.Show($"Before use designer {e.Item.Caption} ensure that \"Relative Code Path\" & \"Proteus Relative Code Path\" are set correctly! \r\nTo configure click \"Qualco\" menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string designer = GetItemPath(e.Item.Caption);
            System.Diagnostics.Process.Start(designer);
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
         return Path.Combine(OptionsInstance.DesignersFolder, itemCaption, "SCToolkit.Designers.Client.application");
      }


   }
}
