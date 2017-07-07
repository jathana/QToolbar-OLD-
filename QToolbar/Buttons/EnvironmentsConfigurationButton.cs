using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class EnvironmentsConfigurationButton : ButtonBase
   {
      public EnvironmentsConfigurationButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.EnvironmentsConfigurationFolder, barManager, menu, EnvironmentsConfiguration_ItemClick, ShouldAddItem)
      {
         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void EnvironmentsConfiguration_ItemClick(object sender, ItemClickEventArgs e)
      {
         string EnvDir = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, e.Item.Caption);
         if (Directory.Exists(EnvDir))
         {
            string file = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
            try
            {
               //Frm_FileViewer f = new Frm_FileViewer();
               //f.ViewFile(file);
               Frm_FileViewer f = new Frm_FileViewer();
               f.Size = new Size(1200, 800);
               f.ViewFile(file, FastColoredTextBoxNS.Language.XML);

            }
            catch (Exception ex)
            {
               XtraMessageBox.Show(ex.Message);
            }
         }
      }
   }
}
