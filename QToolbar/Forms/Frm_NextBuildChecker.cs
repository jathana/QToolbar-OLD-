using Microsoft.SqlServer.Management.SqlParser.Parser;
using QToolbar.Builds;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QToolbar
{
   public partial class Frm_NextBuildChecker : Form
   {
      private const string LOG_FILE = "log.txt";

      //private string _CheckoutName = "";
      //private string _CheckoutPath = "";
      //private string _NextBuildPath = "";
      //private string _AnalyticsNextBuildPath = "";
      //private bool _Errors = false;

      //private DataTable _Table = new DataTable();
      //private DataTable _AnalyticsTable = new DataTable();

      //
      private BuildChecker _BuildChecker = null;
      private AnalyticsBuildChecker _AnalyticsBuildChecker = null;

      public Frm_NextBuildChecker()
      {
         InitializeComponent();
      }

      private void InitUI()
      {
         //_Table.Columns.Clear();
         //_Table.Columns.Add("File", typeof(string));
         //_Table.Columns.Add("Message", typeof(string));
         //_Table.Columns.Add("Tag", typeof(string));
         //_Table.Columns.Add("Result", typeof(string));

         //_AnalyticsTable.Columns.Clear();
         //_AnalyticsTable.Columns.Add("File", typeof(string));
         //_AnalyticsTable.Columns.Add("Message", typeof(string));
         //_AnalyticsTable.Columns.Add("Tag", typeof(string));
         //_AnalyticsTable.Columns.Add("Result", typeof(string));


         Text = $"{_BuildChecker.CheckoutName} - {_BuildChecker.NextBuildPath} & {_AnalyticsBuildChecker.NextBuildPath}";
         gridView1.OptionsBehavior.Editable = false;
         gridView2.OptionsBehavior.Editable = false;
      }

      public void Show(string checkoutName, string checkoutPath)
      {
        
         //_CheckoutName = checkoutName;
         //_CheckoutPath = checkoutPath;

         _BuildChecker = new BuildChecker();
         _BuildChecker.FileChecked += _BuildChecker_FileChecked;
         _BuildChecker.CheckoutName = checkoutName;
         _BuildChecker.CheckoutPath = checkoutPath;
         _BuildChecker.NextBuildPath = Path.Combine(checkoutPath, @"Builds\Next Build");

         _AnalyticsBuildChecker = new AnalyticsBuildChecker(); 
         _AnalyticsBuildChecker.CheckoutName = checkoutName;
         _AnalyticsBuildChecker.CheckoutPath = checkoutPath;
         _AnalyticsBuildChecker.NextBuildPath = Path.Combine(checkoutPath, @"AnalyticsBuilds\Next Build");
        
         Show();
      }

      private void _BuildChecker_FileChecked(object sender, BuildCheckerEventArgs e)
      {
         backgroundWorker1.ReportProgress(1, e.Message);
      }

      private void BeforeCheck()
      {
         Cursor.Current = Cursors.WaitCursor;
         gridResults.DataSource = null;
         grdAnalyticsResults.DataSource = null;
      }


      private void CheckAsync()
      {
         backgroundWorker1.RunWorkerAsync();
      }

      private void AfterCheck()
      {
         gridResults.DataSource = _BuildChecker.Table;
         grdAnalyticsResults.DataSource = _AnalyticsBuildChecker.Table;
         ribbonStatusBar1.ItemLinks[0].Caption = "Finished.";
         Cursor.Current = Cursors.Default;
      }

      private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
      {
      }

      private void gridView1_DoubleClick(object sender, EventArgs e)
      {
         string file = gridView1.GetFocusedDataRow()["File"].ToString();
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            Frm_FileViewer f1 = new Frm_FileViewer();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }

      private void RunAsync()
      {
         try
         {
            if (!backgroundWorker1.IsBusy)
            {
               BeforeCheck();
               CheckAsync();
            }
         }
         catch (Exception ex)
         {

         }
         finally
         {

         }
      }

      private void btnCheck_Click(object sender, EventArgs e)
      {
          RunAsync();
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         _BuildChecker.InitState();
         _AnalyticsBuildChecker.InitState();

         _BuildChecker.Check();
         _AnalyticsBuildChecker.Check();

        
      }

      private void Frm_NextBuildChecker_Load(object sender, EventArgs e)
      {
         InitUI();

         RunAsync();
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         AfterCheck();
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         ribbonStatusBar1.ItemLinks[0].Caption = e.UserState.ToString();
      }

      private void btnCheckBuild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         RunAsync();
      }

      private void gridView2_DoubleClick(object sender, EventArgs e)
      {
         string file = gridView2.GetFocusedDataRow()["File"].ToString();
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            Frm_FileViewer f1 = new Frm_FileViewer();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }

      private void barStaticItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {

      }
   }
}
