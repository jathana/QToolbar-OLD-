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
         this.btnConnectorWarningsLog = new DevExpress.XtraBars.BarButtonItem();
         this.btnBrowseInternalBuildFolder = new DevExpress.XtraBars.BarButtonItem();
         this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
         this.grpInfo = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtStarTeamLabel = new DevExpress.XtraEditors.TextEdit();
         this.txtVersion = new DevExpress.XtraEditors.TextEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtStarTeamLabel.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
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
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.txtStarTeamLabel);
         this.layoutControl1.Controls.Add(this.txtVersion);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 95);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(556, 324);
         this.layoutControl1.TabIndex = 6;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtStarTeamLabel
         // 
         this.txtStarTeamLabel.Location = new System.Drawing.Point(89, 36);
         this.txtStarTeamLabel.MenuManager = this.ribbonControl1;
         this.txtStarTeamLabel.Name = "txtStarTeamLabel";
         this.txtStarTeamLabel.Properties.ReadOnly = true;
         this.txtStarTeamLabel.Size = new System.Drawing.Size(455, 20);
         this.txtStarTeamLabel.StyleController = this.layoutControl1;
         this.txtStarTeamLabel.TabIndex = 5;
         // 
         // txtVersion
         // 
         this.txtVersion.Location = new System.Drawing.Point(89, 12);
         this.txtVersion.MenuManager = this.ribbonControl1;
         this.txtVersion.Name = "txtVersion";
         this.txtVersion.Properties.ReadOnly = true;
         this.txtVersion.Size = new System.Drawing.Size(455, 20);
         this.txtVersion.StyleController = this.layoutControl1;
         this.txtVersion.TabIndex = 4;
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
         this.layoutControlGroup1.Size = new System.Drawing.Size(556, 324);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.txtVersion;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(536, 24);
         this.layoutControlItem1.Text = "Version";
         this.layoutControlItem1.TextSize = new System.Drawing.Size(74, 13);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtStarTeamLabel;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(536, 280);
         this.layoutControlItem2.Text = "StarTeam Label";
         this.layoutControlItem2.TextSize = new System.Drawing.Size(74, 13);
         // 
         // Frm_InternalBuilds
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(556, 419);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.ribbonControl1);
         this.Name = "Frm_InternalBuilds";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Internal Builds";
         this.Load += new System.EventHandler(this.Frm_InternalBuilds_Load);
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtStarTeamLabel.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
      private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpInfo;
      private DevExpress.XtraBars.BarButtonItem btnConnectorWarningsLog;
      private DevExpress.XtraBars.BarButtonItem btnBrowseInternalBuildFolder;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraEditors.TextEdit txtStarTeamLabel;
      private DevExpress.XtraEditors.TextEdit txtVersion;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
   }
}