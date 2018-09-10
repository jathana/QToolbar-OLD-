namespace QToolbar
{
   partial class Frm_SQLQueries
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SQLQueries));
         DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
         this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
         this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
         this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
         this.uc_SQL1 = new QToolbar.uc_SQL();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
         this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtFilter = new DevExpress.XtraEditors.TextEdit();
         this.treeDatabases = new DevExpress.XtraTreeList.TreeList();
         this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
         this.colDatabase = new DevExpress.XtraTreeList.Columns.TreeListColumn();
         this.colServer = new DevExpress.XtraTreeList.Columns.TreeListColumn();
         this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
         this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
         this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
         this.dockPanel2.SuspendLayout();
         this.dockPanel2_Container.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         this.dockPanel1.SuspendLayout();
         this.dockPanel1_Container.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.treeDatabases)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
         this.SuspendLayout();
         // 
         // dockManager1
         // 
         this.dockManager1.Form = this;
         this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel2});
         this.dockManager1.MenuManager = this.barManager1;
         this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
         this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
         // 
         // dockPanel2
         // 
         this.dockPanel2.Controls.Add(this.dockPanel2_Container);
         this.dockPanel2.DockedAsTabbedDocument = true;
         this.dockPanel2.ID = new System.Guid("55dcaba2-97a8-4e66-9a91-9d581d0449dd");
         this.dockPanel2.Name = "dockPanel2";
         this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
         this.dockPanel2.SavedIndex = 1;
         this.dockPanel2.SavedMdiDocument = true;
         this.dockPanel2.Text = "dockPanel2";
         // 
         // dockPanel2_Container
         // 
         this.dockPanel2_Container.Controls.Add(this.uc_SQL1);
         this.dockPanel2_Container.Location = new System.Drawing.Point(4, 24);
         this.dockPanel2_Container.Name = "dockPanel2_Container";
         this.dockPanel2_Container.Size = new System.Drawing.Size(192, 172);
         this.dockPanel2_Container.TabIndex = 0;
         // 
         // uc_SQL1
         // 
         this.uc_SQL1.Database = null;
         this.uc_SQL1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.uc_SQL1.Location = new System.Drawing.Point(0, 0);
         this.uc_SQL1.Name = "uc_SQL1";
         this.uc_SQL1.Query = null;
         this.uc_SQL1.QueryName = null;
         this.uc_SQL1.QueryRunImmediate = false;
         this.uc_SQL1.Server = null;
         this.uc_SQL1.Size = new System.Drawing.Size(192, 172);
         this.uc_SQL1.TabIndex = 0;
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
         this.barManager1.DockManager = this.dockManager1;
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAdd});
         this.barManager1.MaxItemId = 1;
         this.barManager1.OptionsLayout.AllowAddNewItems = false;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAdd)});
         this.bar1.Text = "Tools";
         // 
         // btnAdd
         // 
         this.btnAdd.Caption = "Refresh";
         this.btnAdd.Id = 0;
         this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
         this.btnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.LargeImage")));
         this.btnAdd.Name = "btnAdd";
         toolTipTitleItem1.Text = "Refresh Databases tree";
         superToolTip1.Items.Add(toolTipTitleItem1);
         this.btnAdd.SuperTip = superToolTip1;
         this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(1027, 31);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 531);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(1027, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 500);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(1027, 31);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 500);
         // 
         // dockPanel1
         // 
         this.dockPanel1.Controls.Add(this.dockPanel1_Container);
         this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
         this.dockPanel1.ID = new System.Guid("9008e510-9814-42dd-abb5-519148030fad");
         this.dockPanel1.Location = new System.Drawing.Point(0, 31);
         this.dockPanel1.Name = "dockPanel1";
         this.dockPanel1.OriginalSize = new System.Drawing.Size(345, 200);
         this.dockPanel1.Size = new System.Drawing.Size(345, 500);
         this.dockPanel1.Text = "Databases";
         // 
         // dockPanel1_Container
         // 
         this.dockPanel1_Container.Controls.Add(this.layoutControl1);
         this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
         this.dockPanel1_Container.Name = "dockPanel1_Container";
         this.dockPanel1_Container.Size = new System.Drawing.Size(336, 473);
         this.dockPanel1_Container.TabIndex = 0;
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.txtFilter);
         this.layoutControl1.Controls.Add(this.treeDatabases);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(336, 473);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtFilter
         // 
         this.txtFilter.Location = new System.Drawing.Point(33, 2);
         this.txtFilter.MenuManager = this.barManager1;
         this.txtFilter.Name = "txtFilter";
         this.txtFilter.Size = new System.Drawing.Size(301, 20);
         this.txtFilter.StyleController = this.layoutControl1;
         this.txtFilter.TabIndex = 4;
         this.txtFilter.EditValueChanged += new System.EventHandler(this.txtFilter_EditValueChanged);
         // 
         // treeDatabases
         // 
         this.treeDatabases.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName,
            this.colDatabase,
            this.colServer});
         this.treeDatabases.KeyFieldName = "Data.Version";
         this.treeDatabases.Location = new System.Drawing.Point(2, 26);
         this.treeDatabases.Name = "treeDatabases";
         this.treeDatabases.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.TextAndVisual;
         this.treeDatabases.OptionsFilter.ExpandNodesOnFiltering = true;
         this.treeDatabases.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Extended;
         this.treeDatabases.OptionsFilter.ShowAllValuesInFilterPopup = true;
         this.treeDatabases.OptionsFind.ExpandNodesOnIncrementalSearch = true;
         this.treeDatabases.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always;
         this.treeDatabases.ParentFieldName = "Parent.Version";
         this.treeDatabases.Size = new System.Drawing.Size(332, 445);
         this.treeDatabases.StateImageList = this.imageCollection1;
         this.treeDatabases.TabIndex = 0;
         this.treeDatabases.FilterNode += new DevExpress.XtraTreeList.FilterNodeEventHandler(this.treeDatabases_FilterNode);
         this.treeDatabases.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeDatabases_GetStateImage);
         this.treeDatabases.PopupMenuShowing += new DevExpress.XtraTreeList.PopupMenuShowingEventHandler(this.treeDatabases_PopupMenuShowing);
         this.treeDatabases.VirtualTreeGetChildNodes += new DevExpress.XtraTreeList.VirtualTreeGetChildNodesEventHandler(this.treeDatabases_VirtualTreeGetChildNodes);
         this.treeDatabases.VirtualTreeGetCellValue += new DevExpress.XtraTreeList.VirtualTreeGetCellValueEventHandler(this.treeDatabases_VirtualTreeGetCellValue);
         // 
         // colName
         // 
         this.colName.Caption = "Name";
         this.colName.FieldName = "Name";
         this.colName.MinWidth = 34;
         this.colName.Name = "colName";
         this.colName.Visible = true;
         this.colName.VisibleIndex = 0;
         // 
         // colDatabase
         // 
         this.colDatabase.Caption = "Database";
         this.colDatabase.FieldName = "Database";
         this.colDatabase.Name = "colDatabase";
         // 
         // colServer
         // 
         this.colServer.Caption = "Server";
         this.colServer.FieldName = "Server";
         this.colServer.Name = "colServer";
         // 
         // imageCollection1
         // 
         this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
         this.imageCollection1.InsertGalleryImage("db", "images/data/database_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/data/database_16x16.png"), 0);
         this.imageCollection1.Images.SetKeyName(0, "db");
         this.imageCollection1.InsertGalleryImage("server", "images/data/servermode_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/data/servermode_16x16.png"), 1);
         this.imageCollection1.Images.SetKeyName(1, "server");
         this.imageCollection1.InsertGalleryImage("env", "office2013/navigation/home_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/navigation/home_16x16.png"), 2);
         this.imageCollection1.Images.SetKeyName(2, "env");
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "layoutControlGroup1";
         this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.layoutControlGroup1.Size = new System.Drawing.Size(336, 473);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.treeDatabases;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(336, 449);
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtFilter;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(336, 24);
         this.layoutControlItem2.Text = "Filter:";
         this.layoutControlItem2.TextSize = new System.Drawing.Size(28, 13);
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // documentManager1
         // 
         this.documentManager1.ContainerControl = this;
         this.documentManager1.MenuManager = this.barManager1;
         this.documentManager1.View = this.tabbedView1;
         this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
         // 
         // tabbedView1
         // 
         this.tabbedView1.RootContainer.Element = null;
         // 
         // popupMenu1
         // 
         this.popupMenu1.Manager = this.barManager1;
         this.popupMenu1.Name = "popupMenu1";
         // 
         // Frm_SQLQueries
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1027, 531);
         this.Controls.Add(this.dockPanel1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_SQLQueries";
         this.Text = "SQL Queries";
         this.Load += new System.EventHandler(this.Frm_SQLQueries_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
         this.dockPanel2.ResumeLayout(false);
         this.dockPanel2_Container.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         this.dockPanel1.ResumeLayout(false);
         this.dockPanel1_Container.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.treeDatabases)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraBars.Docking.DockManager dockManager1;
      private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
      private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
      private DevExpress.XtraTreeList.TreeList treeDatabases;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
      private DevExpress.XtraTreeList.Columns.TreeListColumn colServer;
      private DevExpress.XtraTreeList.Columns.TreeListColumn colDatabase;
      private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
      private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
      private uc_SQL uc_SQL1;
      private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
      private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
      private DevExpress.XtraBars.PopupMenu popupMenu1;
      private DevExpress.Utils.ImageCollection imageCollection1;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem btnAdd;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraEditors.TextEdit txtFilter;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
   }
}