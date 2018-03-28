using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class EnvironmentsConfigurationButton : ButtonBase
   {
      public EnvironmentsConfigurationButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.EnvironmentsConfigurationFolder, barManager, menu)
      {
         
      }

      /// <summary>
      /// Retrieves the most recent EnvironmentsConfiguration.xml File. 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         string EnvDir = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, e.Item.Caption);
         
         DataRow[] ch = OptionsInstance.Checkouts.Data.Select($"Name='{e.Item.Caption}'");
         if (ch.Length==1)
         {
            string localPath= Path.Combine(ch[0]["Path"].ToString(), "BuildConfiguration");
            if (Directory.Exists(localPath))
            {
               string localFile = Path.Combine(localPath, "EnvironmentsConfiguration.xml");
               DateTime lastLocalWr=File.GetLastWriteTimeUtc(localFile);
               string remFile = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
               DateTime lastRemWr = File.GetLastWriteTimeUtc(remFile);
               if (lastLocalWr > lastRemWr) 
               {
                  EnvDir = localPath;
               }
            }
         }
         if (Directory.Exists(EnvDir))
         {
            string file = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
            try
            {
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
