namespace QToolbar.Tools
{
   partial class Frm_ScriptCriteria
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ScriptCriteria));
         DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.cboInstallations = new DevExpress.XtraEditors.PopupContainerEdit();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.btnRun = new DevExpress.XtraBars.BarButtonItem();
         this.btnScript = new DevExpress.XtraBars.BarButtonItem();
         this.btnSelectFromFile = new DevExpress.XtraBars.BarButtonItem();
         this.bar3 = new DevExpress.XtraBars.Bar();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
         this.grdInstallations = new DevExpress.XtraGrid.GridControl();
         this.gviewInstallations = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.txtScriptDirectory = new DevExpress.XtraEditors.ButtonEdit();
         this.gridControl1 = new DevExpress.XtraGrid.GridControl();
         this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.txtSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.cboInstallations.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
         this.popupContainerControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.grdInstallations)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gviewInstallations)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtScriptDirectory.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
         this.splitContainerControl1.SuspendLayout();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.cboInstallations);
         this.layoutControl1.Controls.Add(this.txtScriptDirectory);
         this.layoutControl1.Controls.Add(this.gridControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1040, 197, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(1049, 410);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // cboInstallations
         // 
         this.cboInstallations.Location = new System.Drawing.Point(79, 364);
         this.cboInstallations.MenuManager = this.barManager1;
         this.cboInstallations.Name = "cboInstallations";
         this.cboInstallations.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.cboInstallations.Properties.PopupControl = this.popupContainerControl1;
         this.cboInstallations.Size = new System.Drawing.Size(968, 20);
         this.cboInstallations.StyleController = this.layoutControl1;
         this.cboInstallations.TabIndex = 3;
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnRun,
            this.btnScript,
            this.btnSelectFromFile});
         this.barManager1.MaxItemId = 3;
         this.barManager1.StatusBar = this.bar3;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnScript),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectFromFile)});
         this.bar1.Text = "Tools";
         // 
         // btnRun
         // 
         this.btnRun.Caption = "Run";
         this.btnRun.Id = 0;
         this.btnRun.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.ImageOptions.Image")));
         this.btnRun.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRun.ImageOptions.LargeImage")));
         this.btnRun.Name = "btnRun";
         this.btnRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRun_ItemClick);
         // 
         // btnScript
         // 
         this.btnScript.Caption = "Script";
         this.btnScript.Id = 1;
         this.btnScript.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnScript.ImageOptions.Image")));
         this.btnScript.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnScript.ImageOptions.LargeImage")));
         this.btnScript.Name = "btnScript";
         this.btnScript.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnScript_ItemClick);
         // 
         // btnSelectFromFile
         // 
         this.btnSelectFromFile.Caption = "Select Criteria From File";
         this.btnSelectFromFile.Id = 2;
         this.btnSelectFromFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFromFile.ImageOptions.Image")));
         this.btnSelectFromFile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFromFile.ImageOptions.LargeImage")));
         this.btnSelectFromFile.Name = "btnSelectFromFile";
         toolTipTitleItem2.Text = "Select Criteria From File";
         superToolTip2.Items.Add(toolTipTitleItem2);
         this.btnSelectFromFile.SuperTip = superToolTip2;
         this.btnSelectFromFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectFromFile_ItemClick);
         // 
         // bar3
         // 
         this.bar3.BarName = "Status bar";
         this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
         this.bar3.DockCol = 0;
         this.bar3.DockRow = 0;
         this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
         this.bar3.OptionsBar.AllowQuickCustomization = false;
         this.bar3.OptionsBar.DrawDragBorder = false;
         this.bar3.OptionsBar.UseWholeRow = true;
         this.bar3.Text = "Status bar";
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(1049, 31);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 546);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(1049, 23);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 515);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(1049, 31);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 515);
         // 
         // popupContainerControl1
         // 
         this.popupContainerControl1.Controls.Add(this.grdInstallations);
         this.popupContainerControl1.Location = new System.Drawing.Point(232, 12);
         this.popupContainerControl1.Name = "popupContainerControl1";
         this.popupContainerControl1.Size = new System.Drawing.Size(526, 263);
         this.popupContainerControl1.TabIndex = 9;
         // 
         // grdInstallations
         // 
         this.grdInstallations.Dock = System.Windows.Forms.DockStyle.Fill;
         this.grdInstallations.Location = new System.Drawing.Point(0, 0);
         this.grdInstallations.MainView = this.gviewInstallations;
         this.grdInstallations.MenuManager = this.barManager1;
         this.grdInstallations.Name = "grdInstallations";
         this.grdInstallations.Size = new System.Drawing.Size(526, 263);
         this.grdInstallations.TabIndex = 5;
         this.grdInstallations.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewInstallations});
         // 
         // gviewInstallations
         // 
         this.gviewInstallations.GridControl = this.grdInstallations;
         this.gviewInstallations.Name = "gviewInstallations";
         this.gviewInstallations.OptionsSelection.MultiSelect = true;
         this.gviewInstallations.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
         // 
         // txtScriptDirectory
         // 
         this.txtScriptDirectory.Location = new System.Drawing.Point(79, 388);
         this.txtScriptDirectory.Name = "txtScriptDirectory";
         this.txtScriptDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
         this.txtScriptDirectory.Size = new System.Drawing.Size(968, 20);
         this.txtScriptDirectory.StyleController = this.layoutControl1;
         this.txtScriptDirectory.TabIndex = 4;
         this.txtScriptDirectory.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtScriptDirectory_ButtonClick);
         // 
         // gridControl1
         // 
         this.gridControl1.Location = new System.Drawing.Point(2, 2);
         this.gridControl1.MainView = this.gridView1;
         this.gridControl1.Name = "gridControl1";
         this.gridControl1.Size = new System.Drawing.Size(1045, 358);
         this.gridControl1.TabIndex = 2;
         this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
         // 
         // gridView1
         // 
         this.gridView1.GridControl = this.gridControl1;
         this.gridView1.Name = "gridView1";
         this.gridView1.OptionsSelection.MultiSelect = true;
         this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.layoutControlGroup1.Size = new System.Drawing.Size(1049, 410);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.gridControl1;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(1049, 362);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.txtScriptDirectory;
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 386);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(1049, 24);
         this.layoutControlItem3.Text = "Script Directory";
         this.layoutControlItem3.TextSize = new System.Drawing.Size(74, 13);
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.cboInstallations;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 362);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(1049, 24);
         this.layoutControlItem4.Text = "Installations";
         this.layoutControlItem4.TextSize = new System.Drawing.Size(74, 13);
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
         this.txtSQL.AutoScrollMinSize = new System.Drawing.Size(513, 15);
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
         this.txtSQL.Size = new System.Drawing.Size(1049, 100);
         this.txtSQL.TabIndex = 0;
         this.txtSQL.Text = "SELECT CRI_UNIQUE_ID, CRI_DESC FROM AT_CRITERIA ORDER BY CRI_CREATED DESC";
         this.txtSQL.Zoom = 100;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // splitContainerControl1
         // 
         this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainerControl1.Horizontal = false;
         this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
         this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(0);
         this.splitContainerControl1.Name = "splitContainerControl1";
         this.splitContainerControl1.Panel1.Controls.Add(this.txtSQL);
         this.splitContainerControl1.Panel1.Text = "Panel1";
         this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
         this.splitContainerControl1.Panel2.Text = "Panel2";
         this.splitContainerControl1.Size = new System.Drawing.Size(1049, 515);
         this.splitContainerControl1.TabIndex = 14;
         this.splitContainerControl1.Text = "splitContainerControl1";
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.FileName = "openFileDialog1";
         // 
         // Frm_ScriptCriteria
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1049, 569);
         this.Controls.Add(this.splitContainerControl1);
         this.Controls.Add(this.popupContainerControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_ScriptCriteria";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Script Criteria";
         this.Load += new System.EventHandler(this.Frm_ScriptCriteria_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.cboInstallations.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
         this.popupContainerControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.grdInstallations)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gviewInstallations)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtScriptDirectory.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
         this.splitContainerControl1.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private FastColoredTextBoxNS.FastColoredTextBox txtSQL;
      private DevExpress.XtraEditors.ButtonEdit txtScriptDirectory;
      private DevExpress.XtraGrid.GridControl gridControl1;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.BarButtonItem btnRun;
      private DevExpress.XtraBars.BarButtonItem btnScript;
      private DevExpress.XtraBars.Bar bar3;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraEditors.PopupContainerEdit cboInstallations;
      private DevExpress.XtraGrid.GridControl grdInstallations;
      private DevExpress.XtraGrid.Views.Grid.GridView gviewInstallations;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
      private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
      private DevExpress.XtraBars.BarButtonItem btnSelectFromFile;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
   }
}