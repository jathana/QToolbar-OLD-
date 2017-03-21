using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace QToolbar
{
   public partial class uc_SQL : DevExpress.XtraEditors.XtraUserControl
   {
      private StringBuilder _Messages;
      public string QueryName { get; set; }
      public string Server { get; set; }
      public string Database { get; set; }
      public string Query { get; set; }
      public bool QueryRunImmediate { get; set; }
      public string ConnectionString
      {
         get
         {
            return $"Server={Server};Database={Database};Integrated Security=SSPI;";
         }
      }

      public uc_SQL()
      {
         InitializeComponent();
         backgroundWorker1.WorkerSupportsCancellation = true;
         backgroundWorker1.WorkerReportsProgress = true;
         _Messages = new StringBuilder();
         
      }

      public void Initialize()
      {
         txtSQL.Text = Query;
      }

      public void Run()
      {
         try
         {
            btnRun.Enabled = false;
            progressPanel1.Description = $"Executing {QueryName}...";
            //progressPanel1.Visible = true;
            lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            backgroundWorker1.RunWorkerAsync();
         }
         catch(Exception ex)
         {
            //progressPanel1.Visible = false;
            lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            btnRun.Enabled = true;
         }

      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
               _Messages.Clear();
               con.FireInfoMessageEventOnUserErrors = true;
               con.InfoMessage += Con_InfoMessage;
               SqlDataAdapter adapter = new SqlDataAdapter(txtSQL.Text, con);
               
               DataSet dataset = new DataSet();
               adapter.Fill(dataset);
               e.Result = dataset;
            }
         }
         catch (Exception ex)
         {
           backgroundWorker1.ReportProgress(100,$"Server:{Server}\r\nDatabase:{Database}\r\n{Query}\r\nFailed, {ex.Message}.");
            e.Result = ex;
         }
      }

      private void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
      {
         _Messages.AppendLine(e.Message);

      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result != null)
         {
            if (e.Result is DataSet)
            {
               DataSet ds = (DataSet)e.Result;
               if (ds.Tables.Count > 0)
               {
                  // splitting is needed

                  gridResults.DataSource = ds.Tables[0];
                  gridView1.BestFitColumns();
               }
            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
         memMessages.Text = _Messages.ToString();
         btnRun.Enabled = true;
      }


      private void AddGrid()
      {
         SplitterControl splitter = new SplitterControl();
         this.xtraTabPage1.Controls.Add(splitter);
         splitter.Dock = DockStyle.Top;
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
        
      }

      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Run();
         
      }
   }
}
