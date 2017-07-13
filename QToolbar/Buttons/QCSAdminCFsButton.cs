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
   public class QCSAdminCFsButton:ButtonBase
   {
      public QCSAdminCFsButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.QCSAdminFolder, barManager, menu)
      {
      }


      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         string qcsAdminCFDir = Path.Combine(OptionsInstance.QCSAdminFolder, e.Item.Caption, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            string destDir = "";
            Version maxVersion = new Version(0, 0, 0, 0);
            foreach (string dir in subdirs)
            {
               Version curVersion = Utils.GetVersion(dir, "_", 1);

               if (curVersion != null)
               {
                  if (maxVersion < curVersion)
                  {
                     maxVersion = curVersion;
                     destDir = dir;
                  }
               }
               else
               {
                  XtraMessageBox.Show("Cannot parse directory name.");
               }
            }
            string file = Path.Combine(destDir, "QBC_Admin.cf.deploy");
            try
            {
               Frm_FileViewer f = new Frm_FileViewer();
               f.Size = new Size(800, 800);

               f.ViewFile(file, FastColoredTextBoxNS.Language.CSharp);
            }
            catch (Exception ex)
            {
               XtraMessageBox.Show($"file: {file}, msg:{ex.Message}, ");
            }
         }
      }
   }
}
