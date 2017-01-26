namespace QToolbar
{
   partial class Frm_NextBuildChecker
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NextBuildChecker));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.gridResults = new DevExpress.XtraGrid.GridControl();
         this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colResult = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colFile = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colTag = new DevExpress.XtraGrid.Columns.GridColumn();
         this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
         this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
         this.grpActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.btnCheckBuild = new DevExpress.XtraBars.BarButtonItem();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.gridResults);
         this.layoutControl1.Controls.Add(this.labelControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 95);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2308, 203, 424, 592);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(999, 366);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // gridResults
         // 
         this.gridResults.Location = new System.Drawing.Point(12, 29);
         this.gridResults.MainView = this.gridView1;
         this.gridResults.Name = "gridResults";
         this.gridResults.Size = new System.Drawing.Size(975, 325);
         this.gridResults.TabIndex = 0;
         this.gridResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
         // 
         // gridView1
         // 
         this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMessage,
            this.colResult,
            this.colFile,
            this.colTag});
         this.gridView1.GridControl = this.gridResults;
         this.gridView1.Name = "gridView1";
         this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
         this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
         // 
         // colMessage
         // 
         this.colMessage.Caption = "Message";
         this.colMessage.FieldName = "Message";
         this.colMessage.Name = "colMessage";
         this.colMessage.Visible = true;
         this.colMessage.VisibleIndex = 0;
         this.colMessage.Width = 555;
         // 
         // colResult
         // 
         this.colResult.Caption = "Result";
         this.colResult.FieldName = "Result";
         this.colResult.Name = "colResult";
         this.colResult.Visible = true;
         this.colResult.VisibleIndex = 1;
         this.colResult.Width = 62;
         // 
         // colFile
         // 
         this.colFile.Caption = "File";
         this.colFile.FieldName = "File";
         this.colFile.Name = "colFile";
         this.colFile.Visible = true;
         this.colFile.VisibleIndex = 2;
         this.colFile.Width = 371;
         // 
         // colTag
         // 
         this.colTag.Caption = "Tag";
         this.colTag.FieldName = "Tag";
         this.colTag.Name = "colTag";
         // 
         // labelControl1
         // 
         this.labelControl1.Location = new System.Drawing.Point(12, 12);
         this.labelControl1.Name = "labelControl1";
         this.labelControl1.Size = new System.Drawing.Size(207, 13);
         this.labelControl1.StyleController = this.layoutControl1;
         this.labelControl1.TabIndex = 1;
         this.labelControl1.Text = "Ensure that you have the latest checkout!!";
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(999, 366);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.labelControl1;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(979, 17);
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.gridResults;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 17);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(979, 329);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // ribbonControl1
         // 
         this.ribbonControl1.AllowMinimizeRibbon = false;
         this.ribbonControl1.ExpandCollapseItem.Id = 0;
         this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnCheckBuild});
         this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
         this.ribbonControl1.MaxItemId = 2;
         this.ribbonControl1.Name = "ribbonControl1";
         this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
         this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
         this.ribbonControl1.Size = new System.Drawing.Size(999, 95);
         this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
         // 
         // ribbonPage1
         // 
         this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpActions});
         this.ribbonPage1.Name = "ribbonPage1";
         this.ribbonPage1.Text = "ribbonPage1";
         // 
         // grpActions
         // 
         this.grpActions.ItemLinks.Add(this.btnCheckBuild);
         this.grpActions.Name = "grpActions";
         this.grpActions.Text = "Actions";
         // 
         // btnCheckBuild
         // 
         this.btnCheckBuild.Caption = "Check Build";
         this.btnCheckBuild.Glyph = ((System.Drawing.Image)(resources.GetObject("btnCheckBuild.Glyph")));
         this.btnCheckBuild.Id = 1;
         this.btnCheckBuild.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnCheckBuild.LargeGlyph")));
         this.btnCheckBuild.Name = "btnCheckBuild";
         this.btnCheckBuild.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCheckBuild_ItemClick);
         // 
         // Frm_NextBuildChecker
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(999, 461);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.ribbonControl1);
         this.Name = "Frm_NextBuildChecker";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Next Build Checker";
         this.Load += new System.EventHandler(this.Frm_NextBuildChecker_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
      private DevExpress.XtraEditors.LabelControl labelControl1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraGrid.GridControl gridResults;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraGrid.Columns.GridColumn colMessage;
      private DevExpress.XtraGrid.Columns.GridColumn colFile;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraGrid.Columns.GridColumn colTag;
      private DevExpress.XtraGrid.Columns.GridColumn colResult;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
      private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpActions;
      private DevExpress.XtraBars.BarButtonItem btnCheckBuild;
   }
}

