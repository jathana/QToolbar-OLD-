﻿using System;
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

      private void btnBrowseInternalBuildFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         if (!string.IsNullOrEmpty(_InternalBuildPath) && Directory.Exists(_InternalBuildPath))
         {
            Process.Start(_InternalBuildPath);
         }
         else
         {
            XtraMessageBox.Show($"Folder {_InternalBuildPath} does not exist!");
         }
      }

      private void ShowInfo()
      {
         txtVersion.Text = Path.GetFileName(_InternalBuildPath);
         txtStarTeamLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
      }

      private void Frm_InternalBuilds_Load(object sender, EventArgs e)
      {
         ShowInfo();
      }
   }
}