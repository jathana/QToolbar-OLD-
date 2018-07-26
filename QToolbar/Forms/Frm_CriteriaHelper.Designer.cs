namespace QToolbar.Forms
{
   partial class Frm_CriteriaHelper
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CriteriaHelper));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtSelectSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.grdSelectCriteria = new DevExpress.XtraGrid.GridControl();
         this.grdviewSelectCriteria = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.btnLoadCriteria = new DevExpress.XtraBars.BarButtonItem();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.grdviewCreateCriteria = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.grdCreateCriteria = new DevExpress.XtraGrid.GridControl();
         this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
         this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
         this.txtCreateSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
         this.btnCreateSQL = new DevExpress.XtraBars.BarButtonItem();
         this.txtGeneratedSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
         this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtSelectSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdSelectCriteria)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdviewSelectCriteria)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdviewCreateCriteria)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdCreateCriteria)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtCreateSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtGeneratedSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.txtGeneratedSQL);
         this.layoutControl1.Controls.Add(this.txtCreateSQL);
         this.layoutControl1.Controls.Add(this.grdCreateCriteria);
         this.layoutControl1.Controls.Add(this.splitterControl1);
         this.layoutControl1.Controls.Add(this.txtSelectSQL);
         this.layoutControl1.Controls.Add(this.grdSelectCriteria);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 31);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1423, 338, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(1017, 660);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtSelectSQL
         // 
         this.txtSelectSQL.AutoCompleteBracketsList = new char[] {
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
         this.txtSelectSQL.AutoIndentCharsPatterns = "";
         this.txtSelectSQL.AutoIndentExistingLines = false;
         this.txtSelectSQL.AutoScrollMinSize = new System.Drawing.Size(543, 15);
         this.txtSelectSQL.BackBrush = null;
         this.txtSelectSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtSelectSQL.CharHeight = 15;
         this.txtSelectSQL.CharWidth = 7;
         this.txtSelectSQL.CommentPrefix = "--";
         this.txtSelectSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtSelectSQL.DelayedEventsInterval = 200;
         this.txtSelectSQL.DelayedTextChangedInterval = 500;
         this.txtSelectSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtSelectSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtSelectSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtSelectSQL.IsReplaceMode = false;
         this.txtSelectSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtSelectSQL.LeftBracket = '(';
         this.txtSelectSQL.Location = new System.Drawing.Point(12, 12);
         this.txtSelectSQL.Name = "txtSelectSQL";
         this.txtSelectSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtSelectSQL.ReservedCountOfLineNumberChars = 2;
         this.txtSelectSQL.RightBracket = ')';
         this.txtSelectSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtSelectSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSelectSQL.ServiceColors")));
         this.txtSelectSQL.Size = new System.Drawing.Size(993, 79);
         this.txtSelectSQL.TabIndex = 6;
         this.txtSelectSQL.Text = "SELECT CRI_UNIQUE_ID, CRI_DESC FROM AT_CRITERIA ORDER BY CRI_CREATED DESC";
         this.txtSelectSQL.Zoom = 100;
         // 
         // grdSelectCriteria
         // 
         this.grdSelectCriteria.Location = new System.Drawing.Point(12, 100);
         this.grdSelectCriteria.MainView = this.grdviewSelectCriteria;
         this.grdSelectCriteria.MenuManager = this.barManager1;
         this.grdSelectCriteria.Name = "grdSelectCriteria";
         this.grdSelectCriteria.Size = new System.Drawing.Size(993, 160);
         this.grdSelectCriteria.TabIndex = 5;
         this.grdSelectCriteria.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdviewSelectCriteria});
         // 
         // grdviewSelectCriteria
         // 
         this.grdviewSelectCriteria.GridControl = this.grdSelectCriteria;
         this.grdviewSelectCriteria.Name = "grdviewSelectCriteria";
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnLoadCriteria,
            this.btnCreateSQL});
         this.barManager1.MaxItemId = 2;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLoadCriteria, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCreateSQL)});
         this.bar1.Text = "Tools";
         // 
         // btnLoadCriteria
         // 
         this.btnLoadCriteria.Caption = "Run";
         this.btnLoadCriteria.Id = 0;
         this.btnLoadCriteria.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadCriteria.ImageOptions.Image")));
         this.btnLoadCriteria.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLoadCriteria.ImageOptions.LargeImage")));
         this.btnLoadCriteria.Name = "btnLoadCriteria";
         toolTipTitleItem4.Text = "Loads Criteria";
         superToolTip4.Items.Add(toolTipTitleItem4);
         this.btnLoadCriteria.SuperTip = superToolTip4;
         this.btnLoadCriteria.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadCriteria_ItemClick);
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(1017, 31);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 691);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(1017, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 660);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(1017, 31);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 660);
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.splitterItem1,
            this.splitterItem2,
            this.layoutControlItem4,
            this.splitterItem3,
            this.layoutControlItem5,
            this.splitterItem4});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(1017, 660);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.grdSelectCriteria;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 88);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(997, 164);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.txtSelectSQL;
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(997, 83);
         this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem3.TextVisible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // splitterControl1
         // 
         this.splitterControl1.Location = new System.Drawing.Point(316, 263);
         this.splitterControl1.Name = "splitterControl1";
         this.splitterControl1.Size = new System.Drawing.Size(5, 10);
         this.splitterControl1.TabIndex = 7;
         this.splitterControl1.TabStop = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.grdCreateCriteria;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 352);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(997, 160);
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // grdviewCreateCriteria
         // 
         this.grdviewCreateCriteria.GridControl = this.grdCreateCriteria;
         this.grdviewCreateCriteria.Name = "grdviewCreateCriteria";
         // 
         // grdCreateCriteria
         // 
         this.grdCreateCriteria.Location = new System.Drawing.Point(12, 364);
         this.grdCreateCriteria.MainView = this.grdviewCreateCriteria;
         this.grdCreateCriteria.MenuManager = this.barManager1;
         this.grdCreateCriteria.Name = "grdCreateCriteria";
         this.grdCreateCriteria.Size = new System.Drawing.Size(993, 156);
         this.grdCreateCriteria.TabIndex = 4;
         this.grdCreateCriteria.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdviewCreateCriteria});
         // 
         // splitterItem1
         // 
         this.splitterItem1.AllowHotTrack = true;
         this.splitterItem1.Location = new System.Drawing.Point(0, 347);
         this.splitterItem1.Name = "splitterItem1";
         this.splitterItem1.Size = new System.Drawing.Size(997, 5);
         // 
         // splitterItem2
         // 
         this.splitterItem2.AllowHotTrack = true;
         this.splitterItem2.Location = new System.Drawing.Point(0, 83);
         this.splitterItem2.Name = "splitterItem2";
         this.splitterItem2.Size = new System.Drawing.Size(997, 5);
         // 
         // txtCreateSQL
         // 
         this.txtCreateSQL.AutoCompleteBracketsList = new char[] {
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
         this.txtCreateSQL.AutoIndentCharsPatterns = "";
         this.txtCreateSQL.AutoIndentExistingLines = false;
         this.txtCreateSQL.AutoScrollMinSize = new System.Drawing.Size(543, 15);
         this.txtCreateSQL.BackBrush = null;
         this.txtCreateSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtCreateSQL.CharHeight = 15;
         this.txtCreateSQL.CharWidth = 7;
         this.txtCreateSQL.CommentPrefix = "--";
         this.txtCreateSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtCreateSQL.DelayedEventsInterval = 200;
         this.txtCreateSQL.DelayedTextChangedInterval = 500;
         this.txtCreateSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtCreateSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtCreateSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtCreateSQL.IsReplaceMode = false;
         this.txtCreateSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtCreateSQL.LeftBracket = '(';
         this.txtCreateSQL.Location = new System.Drawing.Point(12, 269);
         this.txtCreateSQL.Name = "txtCreateSQL";
         this.txtCreateSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtCreateSQL.ReservedCountOfLineNumberChars = 2;
         this.txtCreateSQL.RightBracket = ')';
         this.txtCreateSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtCreateSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCreateSQL.ServiceColors")));
         this.txtCreateSQL.Size = new System.Drawing.Size(993, 86);
         this.txtCreateSQL.TabIndex = 8;
         this.txtCreateSQL.Text = "SELECT CRI_UNIQUE_ID, CRI_DESC FROM AT_CRITERIA ORDER BY CRI_CREATED DESC";
         this.txtCreateSQL.Zoom = 100;
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.txtCreateSQL;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 257);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(997, 90);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // splitterItem3
         // 
         this.splitterItem3.AllowHotTrack = true;
         this.splitterItem3.Location = new System.Drawing.Point(0, 252);
         this.splitterItem3.Name = "splitterItem3";
         this.splitterItem3.Size = new System.Drawing.Size(997, 5);
         // 
         // btnCreateSQL
         // 
         this.btnCreateSQL.Caption = "Create SQL";
         this.btnCreateSQL.Id = 1;
         this.btnCreateSQL.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
         this.btnCreateSQL.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
         this.btnCreateSQL.Name = "btnCreateSQL";
         this.btnCreateSQL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCreateSQL_ItemClick);
         // 
         // txtGeneratedSQL
         // 
         this.txtGeneratedSQL.AutoCompleteBracketsList = new char[] {
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
         this.txtGeneratedSQL.AutoIndentCharsPatterns = "";
         this.txtGeneratedSQL.AutoIndentExistingLines = false;
         this.txtGeneratedSQL.AutoScrollMinSize = new System.Drawing.Size(543, 15);
         this.txtGeneratedSQL.BackBrush = null;
         this.txtGeneratedSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtGeneratedSQL.CharHeight = 15;
         this.txtGeneratedSQL.CharWidth = 7;
         this.txtGeneratedSQL.CommentPrefix = "--";
         this.txtGeneratedSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtGeneratedSQL.DelayedEventsInterval = 200;
         this.txtGeneratedSQL.DelayedTextChangedInterval = 500;
         this.txtGeneratedSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtGeneratedSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtGeneratedSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtGeneratedSQL.IsReplaceMode = false;
         this.txtGeneratedSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtGeneratedSQL.LeftBracket = '(';
         this.txtGeneratedSQL.Location = new System.Drawing.Point(12, 529);
         this.txtGeneratedSQL.Name = "txtGeneratedSQL";
         this.txtGeneratedSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtGeneratedSQL.ReservedCountOfLineNumberChars = 2;
         this.txtGeneratedSQL.RightBracket = ')';
         this.txtGeneratedSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtGeneratedSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtGeneratedSQL.ServiceColors")));
         this.txtGeneratedSQL.Size = new System.Drawing.Size(993, 119);
         this.txtGeneratedSQL.TabIndex = 9;
         this.txtGeneratedSQL.Text = "SELECT CRI_UNIQUE_ID, CRI_DESC FROM AT_CRITERIA ORDER BY CRI_CREATED DESC";
         this.txtGeneratedSQL.Zoom = 100;
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.txtGeneratedSQL;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 517);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(997, 123);
         this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem5.TextVisible = false;
         // 
         // splitterItem4
         // 
         this.splitterItem4.AllowHotTrack = true;
         this.splitterItem4.Location = new System.Drawing.Point(0, 512);
         this.splitterItem4.Name = "splitterItem4";
         this.splitterItem4.Size = new System.Drawing.Size(997, 5);
         // 
         // backgroundWorker2
         // 
         this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
         this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
         // 
         // Frm_CriteriaHelper
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1017, 691);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_CriteriaHelper";
         this.Text = "Criteria Helper";
         this.Load += new System.EventHandler(this.Frm_CriteriaHelper_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtSelectSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdSelectCriteria)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdviewSelectCriteria)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdviewCreateCriteria)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.grdCreateCriteria)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtCreateSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtGeneratedSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraGrid.GridControl grdSelectCriteria;
      private DevExpress.XtraGrid.Views.Grid.GridView grdviewSelectCriteria;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private FastColoredTextBoxNS.FastColoredTextBox txtSelectSQL;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraBars.BarButtonItem btnLoadCriteria;
      private DevExpress.XtraEditors.SplitterControl splitterControl1;
      private DevExpress.XtraGrid.GridControl grdCreateCriteria;
      private DevExpress.XtraGrid.Views.Grid.GridView grdviewCreateCriteria;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.SplitterItem splitterItem1;
      private DevExpress.XtraLayout.SplitterItem splitterItem2;
      private FastColoredTextBoxNS.FastColoredTextBox txtCreateSQL;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraLayout.SplitterItem splitterItem3;
      private DevExpress.XtraBars.BarButtonItem btnCreateSQL;
      private FastColoredTextBoxNS.FastColoredTextBox txtGeneratedSQL;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.SplitterItem splitterItem4;
      private System.ComponentModel.BackgroundWorker backgroundWorker2;
   }
}