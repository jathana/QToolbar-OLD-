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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace QToolbar
{
   public partial class uc_SQL : DevExpress.XtraEditors.XtraUserControl
   {

      private List<GridControl> _Grids;
      private List<SplitterControl> _Splitters;
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
            return Utils.GetConnectionString(Server, Database);
         }
      }

      public uc_SQL()
      {
         InitializeComponent();
         backgroundWorker1.WorkerSupportsCancellation = true;
         backgroundWorker1.WorkerReportsProgress = true;
         _Messages = new StringBuilder();
         _Grids = new List<GridControl>();
         _Splitters = new List<SplitterControl>();
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
            ClearGrids();
            ClearSplitters();
            
            backgroundWorker1.RunWorkerAsync(txtSQL.Text);
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
               SqlDataAdapter adapter = new SqlDataAdapter((string)e.Argument, con);
               
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
               LayoutGrids(ds);
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
      

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
        
      }

      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Run();
         
      }

      private void ClearGrids()
      {
         for(int i=0;i<_Grids.Count;i++)
         {
            pageResults.Controls.Remove(_Grids[i]);
            _Grids[i] = null;
         }
         _Grids.Clear();
      }

      private void ClearSplitters()
      {
         for (int i = 0; i < _Splitters.Count; i++)
         {
            pageResults.Controls.Remove(_Splitters[i]);
            _Splitters[i] = null;
         }
         _Splitters.Clear();
      }

      private void LayoutGrids(DataSet ds)
      {
         if(ds!=null)
         {
            if(ds.Tables.Count > 0)
            {
               // find layout info
               int gridHeight = (int)pageResults.Height / ds.Tables.Count;
               for (int i=0; i < ds.Tables.Count; i++)
               {
                  // add grid
                  GridControl grid = CreateGrid();
                  _Grids.Add(grid);
                  pageResults.Controls.Add(grid);
                  grid.Top = gridHeight * i;
                  grid.Height = gridHeight;
                  grid.Dock = i == 0 ? grid.Dock = DockStyle.Fill : grid.Dock = DockStyle.Top;
                  
                  grid.DataSource = ds.Tables[ds.Tables.Count - 1 - i];
                  ((GridView)grid.DefaultView).BestFitColumns();

                  // add splitter
                  if (i < ds.Tables.Count - 1)
                  {
                     SplitterControl splitter = new SplitterControl();
                     pageResults.Controls.Add(splitter);
                     splitter.Dock = DockStyle.Top;
                     _Splitters.Add(splitter);
                  }

               }
            }
         }
      }

      private void ResizeGrids()
      {
         if (_Grids.Count > 0)
         {
            // find layout info
            int gridHeight = (int)pageResults.Height / _Grids.Count;
            for (int i = 0; i < _Grids.Count; i++)
            {
               // resize grid
               GridControl grid = _Grids[i];
               grid.Height = gridHeight;
            }
         }
      }

      private GridControl CreateGrid()
      {
         GridControl retVal = new GridControl();
         GridView gridView = new GridView();
         retVal.MainView = gridView;
         retVal.Name = "retVal";
         retVal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {gridView});
         retVal.KeyDown += grid_KeyDown;
         
         // 
         // gridView1
         // 
         gridView.GridControl = retVal;
         gridView.Name = "gridView1";
         gridView.OptionsBehavior.Editable = false;
         gridView.OptionsView.ColumnAutoWidth = false;
         gridView.OptionsView.ShowGroupPanel = false;
         gridView.IndicatorWidth = 40;
         
         gridView.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

         return retVal;
      }

      private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
      {
         if (e.RowHandle >= 0)
            e.Info.DisplayText = e.RowHandle.ToString();
      }

      private void grid_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Control && e.KeyCode == Keys.C)
         {
            GridControl grid = (GridControl)sender;
            GridView view = grid.FocusedView as GridView;
            Clipboard.SetText(view.GetFocusedDisplayText());
            e.Handled = true;
         }
      }

      private void pageResults_SizeChanged(object sender, EventArgs e)
      {
         ResizeGrids();
      }
   }
}
