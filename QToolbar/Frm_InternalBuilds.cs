using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.IO;

namespace QToolbar
{
   public partial class Frm_InternalBuilds : DevExpress.XtraEditors.XtraForm
   {
      private string _InternalBuildPath = "";
      private string _InternalBuildName = "";

      public Frm_InternalBuilds()
      {
         InitializeComponent();
      }

      public void Show(string internalBuildPath)
      {
         _InternalBuildPath = internalBuildPath;
         Text = $"InternalBuild - {_InternalBuildPath}";
         Show();
      }

      

      private void btnConnectorWarningsLog_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if (!string.IsNullOrEmpty(_InternalBuildPath) && Directory.Exists(_InternalBuildPath))
         {
            string folder = Uri.EscapeDataString(_InternalBuildPath);
            string file = "ConnectorWarningsLog.txt size:> 0 MB";
            string uri = "search:query=" + file + "&crumb=location:" + folder;
            Process.Start(new ProcessStartInfo(uri));
         }
         else
         {
            XtraMessageBox.Show($"Folder {_InternalBuildPath} does not exist!");
         }
      }
   }
}