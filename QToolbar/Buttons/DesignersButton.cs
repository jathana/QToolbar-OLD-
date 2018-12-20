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
      public DesignersButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.DesignersFolder, barManager, menu)
      {
         
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            //XtraMessageBox.Show($"Before use designer {e.Item.Caption} ensure that \"Relative Code Path\" & \"Proteus Relative Code Path\" are set correctly! \r\nTo configure click \"Qualco\" menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string designer = GetItemPath(e.Item.Caption);
            System.Diagnostics.Process.Start(designer);
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

      private string GetItemPath(string itemCaption)
      {
         return Path.Combine(OptionsInstance.DesignersFolder, itemCaption, "SCToolkit.Designers.Client.application");
      }


   }
}
