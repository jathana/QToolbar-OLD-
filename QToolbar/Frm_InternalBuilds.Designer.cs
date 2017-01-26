namespace QToolbar
{
   partial class Frm_InternalBuilds
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_InternalBuilds));
         this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
         this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
         this.grpInfo = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.btnConnectorWarningsLog = new DevExpress.XtraBars.BarButtonItem();
         this.btnBrowseInternalBuildFolder = new DevExpress.XtraBars.BarButtonItem();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
         this.SuspendLayout();
         // 
         // ribbonControl1
         // 
         this.ribbonControl1.AllowMinimizeRibbon = false;
         this.ribbonControl1.ExpandCollapseItem.Id = 0;
         this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnConnectorWarningsLog,
            this.btnBrowseInternalBuildFolder});
         this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
         this.ribbonControl1.MaxItemId = 3;
         this.ribbonControl1.Name = "ribbonControl1";
         this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
         this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
         this.ribbonControl1.Size = new System.Drawing.Size(556, 95);
         this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
         // 
         // ribbonPage1
         // 
         this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpInfo});
         this.ribbonPage1.Name = "ribbonPage1";
         this.ribbonPage1.Text = "Info";
         // 
         // grpInfo
         // 
         this.grpInfo.ItemLinks.Add(this.btnConnectorWarningsLog);
         this.grpInfo.ItemLinks.Add(this.btnBrowseInternalBuildFolder);
         this.grpInfo.Name = "grpInfo";
         this.grpInfo.Text = "Info";
         // 
         // btnConnectorWarningsLog
         // 
         this.btnConnectorWarningsLog.Caption = "Connector Warnings Log";
         this.btnConnectorWarningsLog.Glyph = ((System.Drawing.Image)(resources.GetObject("btnConnectorWarningsLog.Glyph")));
         this.btnConnectorWarningsLog.Id = 1;
         this.btnConnectorWarningsLog.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnConnectorWarningsLog.LargeGlyph")));
         this.btnConnectorWarningsLog.Name = "btnConnectorWarningsLog";
         this.btnConnectorWarningsLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConnectorWarningsLog_ItemClick_1);
         // 
         // btnBrowseInternalBuildFolder
         // 
         this.btnBrowseInternalBuildFolder.Caption = "Browse";
         this.btnBrowseInternalBuildFolder.Glyph = ((System.Drawing.Image)(resources.GetObject("btnBrowseInternalBuildFolder.Glyph")));
         this.btnBrowseInternalBuildFolder.Id = 2;
         this.btnBrowseInternalBuildFolder.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnBrowseInternalBuildFolder.LargeGlyph")));
         this.btnBrowseInternalBuildFolder.Name = "btnBrowseInternalBuildFolder";
         this.btnBrowseInternalBuildFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBrowseInternalBuildFolder_ItemClick);
         // 
         // Frm_InternalBuilds
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(556, 419);
         this.Controls.Add(this.ribbonControl1);
         this.Name = "Frm_InternalBuilds";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Internal Builds";
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
      private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpInfo;
      private DevExpress.XtraBars.BarButtonItem btnConnectorWarningsLog;
      private DevExpress.XtraBars.BarButtonItem btnBrowseInternalBuildFolder;
   }
}