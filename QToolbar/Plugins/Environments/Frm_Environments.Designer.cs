namespace QToolbar.Plugins.Environments
{
   partial class Frm_Environments
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Environments));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.UXGrid = new DevExpress.XtraGrid.GridControl();
            this.UXGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.mnuEnvironments = new DevExpress.XtraBars.BarSubItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdateCFs = new DevExpress.XtraBars.BarButtonItem();
            this.btnExcelExport = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UXGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.UXGrid);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 116);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(932, 437);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // UXGrid
            // 
            gridLevelNode1.RelationName = "Level1";
            this.UXGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.UXGrid.Location = new System.Drawing.Point(12, 12);
            this.UXGrid.MainView = this.UXGridView;
            this.UXGrid.Name = "UXGrid";
            this.UXGrid.Size = new System.Drawing.Size(908, 413);
            this.UXGrid.TabIndex = 4;
            this.UXGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXGridView});
            this.UXGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UXGrid_KeyDown);
            // 
            // UXGridView
            // 
            this.UXGridView.GridControl = this.UXGrid;
            this.UXGridView.Name = "UXGridView";
            this.UXGridView.DoubleClick += new System.EventHandler(this.UXGridView_DoubleClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(932, 437);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.UXGrid;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(912, 417);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Images = this.imageCollection1;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.mnuEnvironments,
            this.btnRefresh,
            this.btnUpdateCFs,
            this.btnExcelExport});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 17;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(932, 116);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("env.png", "images/actions/right_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/right_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "env.png");
            this.imageCollection1.InsertGalleryImage("delete.png", "images/edit/delete_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/delete_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "delete.png");
            // 
            // mnuEnvironments
            // 
            this.mnuEnvironments.Caption = "Environments (Local)";
            this.mnuEnvironments.Id = 2;
            this.mnuEnvironments.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuEnvironments.ImageOptions.Image")));
            this.mnuEnvironments.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuEnvironments.ImageOptions.LargeImage")));
            this.mnuEnvironments.Name = "mnuEnvironments";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Refresh";
            this.btnRefresh.Id = 13;
            this.btnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.Image")));
            this.btnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.LargeImage")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnUpdateCFs
            // 
            this.btnUpdateCFs.Caption = "Update CFs";
            this.btnUpdateCFs.Id = 15;
            this.btnUpdateCFs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateCFs.ImageOptions.Image")));
            this.btnUpdateCFs.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateCFs.ImageOptions.LargeImage")));
            this.btnUpdateCFs.Name = "btnUpdateCFs";
            this.btnUpdateCFs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdateCFs_ItemClick);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Caption = "Export (Excel)";
            this.btnExcelExport.Id = 16;
            this.btnExcelExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.ImageOptions.Image")));
            this.btnExcelExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.ImageOptions.LargeImage")));
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExcelExport_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.mnuEnvironments);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnRefresh);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUpdateCFs);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExcelExport);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Environments";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // popupMenu2
            // 
            this.popupMenu2.Name = "popupMenu2";
            this.popupMenu2.Ribbon = this.ribbonControl1;
            // 
            // Frm_Environments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 553);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "Frm_Environments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environments";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Environments_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Environments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UXGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraGrid.GridControl UXGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXGridView;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
      private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
      private DevExpress.XtraBars.BarSubItem mnuEnvironments;
      private DevExpress.Utils.ImageCollection imageCollection1;
      private DevExpress.XtraBars.PopupMenu popupMenu1;
      private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
      private DevExpress.XtraBars.PopupMenu popupMenu2;
      private DevExpress.XtraBars.BarButtonItem btnRefresh;
      private DevExpress.XtraBars.BarButtonItem btnUpdateCFs;
      private DevExpress.XtraBars.BarButtonItem btnExcelExport;
   }
}