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
         this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
         this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
         this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
         this.treeDatabases = new DevExpress.XtraTreeList.TreeList();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
         this.dockPanel1.SuspendLayout();
         this.dockPanel1_Container.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.treeDatabases)).BeginInit();
         this.SuspendLayout();
         // 
         // dockManager1
         // 
         this.dockManager1.Form = this;
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
         // dockPanel1
         // 
         this.dockPanel1.Controls.Add(this.dockPanel1_Container);
         this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
         this.dockPanel1.ID = new System.Guid("9008e510-9814-42dd-abb5-519148030fad");
         this.dockPanel1.Location = new System.Drawing.Point(0, 0);
         this.dockPanel1.Name = "dockPanel1";
         this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
         this.dockPanel1.Size = new System.Drawing.Size(200, 539);
         this.dockPanel1.Text = "Databases";
         // 
         // dockPanel1_Container
         // 
         this.dockPanel1_Container.Controls.Add(this.treeDatabases);
         this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
         this.dockPanel1_Container.Name = "dockPanel1_Container";
         this.dockPanel1_Container.Size = new System.Drawing.Size(191, 512);
         this.dockPanel1_Container.TabIndex = 0;
         // 
         // treeDatabases
         // 
         this.treeDatabases.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
         this.treeDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeDatabases.KeyFieldName = "Data.Version";
         this.treeDatabases.Location = new System.Drawing.Point(0, 0);
         this.treeDatabases.Name = "treeDatabases";
         this.treeDatabases.ParentFieldName = "Parent.Version";
         this.treeDatabases.Size = new System.Drawing.Size(191, 512);
         this.treeDatabases.TabIndex = 0;
         this.treeDatabases.VirtualTreeGetChildNodes += new DevExpress.XtraTreeList.VirtualTreeGetChildNodesEventHandler(this.treeDatabases_VirtualTreeGetChildNodes);
         this.treeDatabases.VirtualTreeGetCellValue += new DevExpress.XtraTreeList.VirtualTreeGetCellValueEventHandler(this.treeDatabases_VirtualTreeGetCellValue);
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // treeListColumn1
         // 
         this.treeListColumn1.Caption = "Version";
         this.treeListColumn1.FieldName = "Version";
         this.treeListColumn1.Name = "treeListColumn1";
         this.treeListColumn1.Visible = true;
         this.treeListColumn1.VisibleIndex = 0;
         // 
         // Frm_SQLQueries
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(887, 539);
         this.Controls.Add(this.dockPanel1);
         this.Name = "Frm_SQLQueries";
         this.Text = "SQL Queries";
         this.Load += new System.EventHandler(this.Frm_SQLQueries_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
         this.dockPanel1.ResumeLayout(false);
         this.dockPanel1_Container.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.treeDatabases)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraBars.Docking.DockManager dockManager1;
      private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
      private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
      private DevExpress.XtraTreeList.TreeList treeDatabases;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
   }
}