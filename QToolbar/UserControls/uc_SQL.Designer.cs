using DevExpress.Utils.Filtering;

namespace QToolbar
{
   partial class uc_SQL
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_SQL));
         this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
         this.txtSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
         this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
         this.pageResults = new DevExpress.XtraTab.XtraTabPage();
         this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
         this.memMessages = new DevExpress.XtraEditors.MemoEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.lgrProgress = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layProgress = new DevExpress.XtraLayout.LayoutControlItem();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.btnRun = new DevExpress.XtraBars.BarButtonItem();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.filteringUIContext1 = new DevExpress.Utils.Filtering.FilteringUIContext(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
         this.splitContainerControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
         this.xtraTabControl1.SuspendLayout();
         this.xtraTabPage2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.memMessages.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.lgrProgress)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layProgress)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.filteringUIContext1)).BeginInit();
         this.SuspendLayout();
         // 
         // splitContainerControl1
         // 
         this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainerControl1.Horizontal = false;
         this.splitContainerControl1.Location = new System.Drawing.Point(35, 0);
         this.splitContainerControl1.Name = "splitContainerControl1";
         this.splitContainerControl1.Panel1.Controls.Add(this.txtSQL);
         this.splitContainerControl1.Panel1.Text = "Panel1";
         this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
         this.splitContainerControl1.Panel2.Text = "Panel2";
         this.splitContainerControl1.Size = new System.Drawing.Size(629, 547);
         this.splitContainerControl1.SplitterPosition = 154;
         this.splitContainerControl1.TabIndex = 0;
         this.splitContainerControl1.Text = "splitContainerControl1";
         // 
         // txtSQL
         // 
         this.txtSQL.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
         this.txtSQL.AutoIndentCharsPatterns = "";
         this.txtSQL.AutoIndentExistingLines = false;
         this.txtSQL.AutoScrollMinSize = new System.Drawing.Size(2, 15);
         this.txtSQL.BackBrush = null;
         this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtSQL.CharHeight = 15;
         this.txtSQL.CharWidth = 7;
         this.txtSQL.CommentPrefix = "--";
         this.txtSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtSQL.DelayedEventsInterval = 200;
         this.txtSQL.DelayedTextChangedInterval = 500;
         this.txtSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
         this.txtSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtSQL.IsReplaceMode = false;
         this.txtSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtSQL.LeftBracket = '(';
         this.txtSQL.Location = new System.Drawing.Point(0, 0);
         this.txtSQL.Name = "txtSQL";
         this.txtSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtSQL.ReservedCountOfLineNumberChars = 2;
         this.txtSQL.RightBracket = ')';
         this.txtSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSQL.ServiceColors")));
         this.txtSQL.Size = new System.Drawing.Size(629, 154);
         this.txtSQL.TabIndex = 4;
         this.txtSQL.Zoom = 100;
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.progressPanel1);
         this.layoutControl1.Controls.Add(this.xtraTabControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1256, 174, 450, 397);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(629, 388);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // progressPanel1
         // 
         this.progressPanel1.AnimationElementCount = 6;
         this.progressPanel1.AnimationToTextDistance = 0;
         this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
         this.progressPanel1.Appearance.Options.UseBackColor = true;
         this.progressPanel1.BarAnimationElementThickness = 2;
         this.progressPanel1.Caption = "";
         this.progressPanel1.Description = "";
         this.progressPanel1.LineAnimationElementHeight = 6;
         this.progressPanel1.Location = new System.Drawing.Point(5, 364);
         this.progressPanel1.Name = "progressPanel1";
         this.progressPanel1.ShowCaption = false;
         this.progressPanel1.Size = new System.Drawing.Size(619, 19);
         this.progressPanel1.StyleController = this.layoutControl1;
         this.progressPanel1.TabIndex = 4;
         this.progressPanel1.Text = "progressPanel1";
         // 
         // xtraTabControl1
         // 
         this.xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
         this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
         this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(0);
         this.xtraTabControl1.Name = "xtraTabControl1";
         this.xtraTabControl1.SelectedTabPage = this.pageResults;
         this.xtraTabControl1.Size = new System.Drawing.Size(625, 355);
         this.xtraTabControl1.TabIndex = 0;
         this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageResults,
            this.xtraTabPage2});
         // 
         // pageResults
         // 
         this.pageResults.Name = "pageResults";
         this.pageResults.Size = new System.Drawing.Size(617, 325);
         this.pageResults.Text = "Results";
         this.pageResults.SizeChanged += new System.EventHandler(this.pageResults_SizeChanged);
         // 
         // xtraTabPage2
         // 
         this.xtraTabPage2.Controls.Add(this.memMessages);
         this.xtraTabPage2.Name = "xtraTabPage2";
         this.xtraTabPage2.Size = new System.Drawing.Size(617, 325);
         this.xtraTabPage2.Text = "Messages";
         // 
         // memMessages
         // 
         this.memMessages.Dock = System.Windows.Forms.DockStyle.Fill;
         this.memMessages.Location = new System.Drawing.Point(0, 0);
         this.memMessages.Name = "memMessages";
         this.memMessages.Size = new System.Drawing.Size(617, 325);
         this.memMessages.TabIndex = 0;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lgrProgress});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.layoutControlGroup1.Size = new System.Drawing.Size(629, 388);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.xtraTabControl1;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(629, 359);
         this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // lgrProgress
         // 
         this.lgrProgress.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layProgress});
         this.lgrProgress.Location = new System.Drawing.Point(0, 359);
         this.lgrProgress.Name = "lgrProgress";
         this.lgrProgress.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.lgrProgress.Size = new System.Drawing.Size(629, 29);
         this.lgrProgress.TextVisible = false;
         this.lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
         // 
         // layProgress
         // 
         this.layProgress.Control = this.progressPanel1;
         this.layProgress.Location = new System.Drawing.Point(0, 0);
         this.layProgress.MaxSize = new System.Drawing.Size(0, 23);
         this.layProgress.MinSize = new System.Drawing.Size(54, 23);
         this.layProgress.Name = "layProgress";
         this.layProgress.Size = new System.Drawing.Size(623, 23);
         this.layProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layProgress.TextSize = new System.Drawing.Size(0, 0);
         this.layProgress.TextVisible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // barManager1
         // 
         this.barManager1.AllowCustomization = false;
         this.barManager1.AllowMoveBarOnToolbar = false;
         this.barManager1.AllowQuickCustomization = false;
         this.barManager1.AllowShowToolbarsPopup = false;
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnRun});
         this.barManager1.MaxItemId = 2;
         // 
         // bar1
         // 
         this.bar1.BarName = "Main";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Left;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRun)});
         this.bar1.OptionsBar.AllowRename = true;
         this.bar1.Text = "Main";
         // 
         // btnRun
         // 
         this.btnRun.Caption = "Run";
         this.btnRun.Id = 1;
         this.btnRun.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.ImageOptions.Image")));
         this.btnRun.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRun.ImageOptions.LargeImage")));
         this.btnRun.Name = "btnRun";
         this.btnRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRun_ItemClick);
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(664, 0);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 547);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(664, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(35, 547);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(664, 0);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 547);
         // 
         // uc_SQL
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.splitContainerControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "uc_SQL";
         this.Size = new System.Drawing.Size(664, 547);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
         this.splitContainerControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
         this.xtraTabControl1.ResumeLayout(false);
         this.xtraTabPage2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.memMessages.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.lgrProgress)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layProgress)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.filteringUIContext1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
      private FastColoredTextBoxNS.FastColoredTextBox txtSQL;
      private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
      private DevExpress.XtraTab.XtraTabPage pageResults;
      private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layProgress;
      private DevExpress.XtraLayout.LayoutControlGroup lgrProgress;
      private DevExpress.XtraEditors.MemoEdit memMessages;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.BarButtonItem btnRun;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private FilteringUIContext filteringUIContext1;
   }
}
