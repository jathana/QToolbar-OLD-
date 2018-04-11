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
         this.cboLegalUrl = new DevExpress.XtraEditors.LookUpEdit();
         this.btnCopySuccessEmailText = new DevExpress.XtraEditors.SimpleButton();
         this.memSuccessEmail = new DevExpress.XtraEditors.MemoEdit();
         this.btnCopyStarTeamLabel = new DevExpress.XtraEditors.SimpleButton();
         this.btnCopyVersion = new DevExpress.XtraEditors.SimpleButton();
         this.txtStarTeamLabel = new DevExpress.XtraEditors.TextEdit();
         this.txtVersion = new DevExpress.XtraEditors.TextEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layCboLegalUrl = new DevExpress.XtraLayout.LayoutControlItem();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.cboLegalUrl.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.memSuccessEmail.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtStarTeamLabel.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layCboLegalUrl)).BeginInit();
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
         this.ribbonControl1.Size = new System.Drawing.Size(556, 116);
         this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
         // 
         // btnConnectorWarningsLog
         // 
         this.btnConnectorWarningsLog.Caption = "Connector Warnings Log";
         this.btnConnectorWarningsLog.Id = 1;
         this.btnConnectorWarningsLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConnectorWarningsLog.ImageOptions.Image")));
         this.btnConnectorWarningsLog.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnConnectorWarningsLog.ImageOptions.LargeImage")));
         this.btnConnectorWarningsLog.Name = "btnConnectorWarningsLog";
         this.btnConnectorWarningsLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConnectorWarningsLog_ItemClick_1);
         // 
         // btnBrowseInternalBuildFolder
         // 
         this.btnBrowseInternalBuildFolder.Caption = "Browse";
         this.btnBrowseInternalBuildFolder.Id = 2;
         this.btnBrowseInternalBuildFolder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInternalBuildFolder.ImageOptions.Image")));
         this.btnBrowseInternalBuildFolder.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInternalBuildFolder.ImageOptions.LargeImage")));
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
         this.layoutControl1.Controls.Add(this.cboLegalUrl);
         this.layoutControl1.Controls.Add(this.btnCopySuccessEmailText);
         this.layoutControl1.Controls.Add(this.memSuccessEmail);
         this.layoutControl1.Controls.Add(this.btnCopyStarTeamLabel);
         this.layoutControl1.Controls.Add(this.btnCopyVersion);
         this.layoutControl1.Controls.Add(this.txtStarTeamLabel);
         this.layoutControl1.Controls.Add(this.txtVersion);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 116);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(866, 208, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(556, 303);
         this.layoutControl1.TabIndex = 6;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // cboLegalUrl
         // 
         this.cboLegalUrl.Location = new System.Drawing.Point(89, 64);
         this.cboLegalUrl.MenuManager = this.ribbonControl1;
         this.cboLegalUrl.Name = "cboLegalUrl";
         this.cboLegalUrl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.cboLegalUrl.Size = new System.Drawing.Size(375, 20);
         this.cboLegalUrl.StyleController = this.layoutControl1;
         this.cboLegalUrl.TabIndex = 10;
         this.cboLegalUrl.EditValueChanged += new System.EventHandler(this.cboLegalUrl_EditValueChanged);
         // 
         // btnCopySuccessEmailText
         // 
         this.btnCopySuccessEmailText.Location = new System.Drawing.Point(468, 88);
         this.btnCopySuccessEmailText.Name = "btnCopySuccessEmailText";
         this.btnCopySuccessEmailText.Size = new System.Drawing.Size(76, 22);
         this.btnCopySuccessEmailText.StyleController = this.layoutControl1;
         this.btnCopySuccessEmailText.TabIndex = 9;
         this.btnCopySuccessEmailText.Text = "Copy";
         this.btnCopySuccessEmailText.Click += new System.EventHandler(this.btnCopySuccessEmailText_Click);
         // 
         // memSuccessEmail
         // 
         this.memSuccessEmail.Location = new System.Drawing.Point(89, 88);
         this.memSuccessEmail.MenuManager = this.ribbonControl1;
         this.memSuccessEmail.Name = "memSuccessEmail";
         this.memSuccessEmail.Size = new System.Drawing.Size(375, 203);
         this.memSuccessEmail.StyleController = this.layoutControl1;
         this.memSuccessEmail.TabIndex = 8;
         // 
         // btnCopyStarTeamLabel
         // 
         this.btnCopyStarTeamLabel.Location = new System.Drawing.Point(468, 38);
         this.btnCopyStarTeamLabel.Name = "btnCopyStarTeamLabel";
         this.btnCopyStarTeamLabel.Size = new System.Drawing.Size(76, 22);
         this.btnCopyStarTeamLabel.StyleController = this.layoutControl1;
         this.btnCopyStarTeamLabel.TabIndex = 7;
         this.btnCopyStarTeamLabel.Text = "Copy";
         this.btnCopyStarTeamLabel.Click += new System.EventHandler(this.btnCopyStarTeamLabel_Click);
         // 
         // btnCopyVersion
         // 
         this.btnCopyVersion.Location = new System.Drawing.Point(468, 12);
         this.btnCopyVersion.Name = "btnCopyVersion";
         this.btnCopyVersion.Size = new System.Drawing.Size(76, 22);
         this.btnCopyVersion.StyleController = this.layoutControl1;
         this.btnCopyVersion.TabIndex = 6;
         this.btnCopyVersion.Text = "Copy";
         this.btnCopyVersion.Click += new System.EventHandler(this.btnCopyVersion_Click);
         // 
         // txtStarTeamLabel
         // 
         this.txtStarTeamLabel.Location = new System.Drawing.Point(89, 38);
         this.txtStarTeamLabel.MenuManager = this.ribbonControl1;
         this.txtStarTeamLabel.Name = "txtStarTeamLabel";
         this.txtStarTeamLabel.Properties.ReadOnly = true;
         this.txtStarTeamLabel.Size = new System.Drawing.Size(375, 20);
         this.txtStarTeamLabel.StyleController = this.layoutControl1;
         this.txtStarTeamLabel.TabIndex = 5;
         // 
         // txtVersion
         // 
         this.txtVersion.Location = new System.Drawing.Point(89, 12);
         this.txtVersion.MenuManager = this.ribbonControl1;
         this.txtVersion.Name = "txtVersion";
         this.txtVersion.Properties.ReadOnly = true;
         this.txtVersion.Size = new System.Drawing.Size(375, 20);
         this.txtVersion.StyleController = this.layoutControl1;
         this.txtVersion.TabIndex = 4;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layCboLegalUrl});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(556, 303);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.txtVersion;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(456, 26);
         this.layoutControlItem1.Text = "Version";
         this.layoutControlItem1.TextSize = new System.Drawing.Size(74, 13);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtStarTeamLabel;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(456, 26);
         this.layoutControlItem2.Text = "StarTeam Label";
         this.layoutControlItem2.TextSize = new System.Drawing.Size(74, 13);
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.btnCopyVersion;
         this.layoutControlItem3.Location = new System.Drawing.Point(456, 0);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(80, 26);
         this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem3.TextVisible = false;
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.btnCopyStarTeamLabel;
         this.layoutControlItem4.Location = new System.Drawing.Point(456, 26);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(80, 26);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.memSuccessEmail;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 76);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(456, 207);
         this.layoutControlItem5.Text = "Success Email";
         this.layoutControlItem5.TextSize = new System.Drawing.Size(74, 13);
         // 
         // layoutControlItem6
         // 
         this.layoutControlItem6.Control = this.btnCopySuccessEmailText;
         this.layoutControlItem6.Location = new System.Drawing.Point(456, 76);
         this.layoutControlItem6.Name = "layoutControlItem6";
         this.layoutControlItem6.Size = new System.Drawing.Size(80, 26);
         this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem6.TextVisible = false;
         // 
         // emptySpaceItem2
         // 
         this.emptySpaceItem2.AllowHotTrack = false;
         this.emptySpaceItem2.Location = new System.Drawing.Point(456, 102);
         this.emptySpaceItem2.Name = "emptySpaceItem2";
         this.emptySpaceItem2.Size = new System.Drawing.Size(80, 181);
         this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
         // 
         // emptySpaceItem3
         // 
         this.emptySpaceItem3.AllowHotTrack = false;
         this.emptySpaceItem3.Location = new System.Drawing.Point(456, 52);
         this.emptySpaceItem3.Name = "emptySpaceItem3";
         this.emptySpaceItem3.Size = new System.Drawing.Size(80, 24);
         this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layCboLegalUrl
         // 
         this.layCboLegalUrl.Control = this.cboLegalUrl;
         this.layCboLegalUrl.Location = new System.Drawing.Point(0, 52);
         this.layCboLegalUrl.Name = "layCboLegalUrl";
         this.layCboLegalUrl.Size = new System.Drawing.Size(456, 24);
         this.layCboLegalUrl.Text = "LegalUrl";
         this.layCboLegalUrl.TextSize = new System.Drawing.Size(74, 13);
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
         ((System.ComponentModel.ISupportInitialize)(this.cboLegalUrl.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.memSuccessEmail.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtStarTeamLabel.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layCboLegalUrl)).EndInit();
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
      private DevExpress.XtraEditors.SimpleButton btnCopySuccessEmailText;
      private DevExpress.XtraEditors.MemoEdit memSuccessEmail;
      private DevExpress.XtraEditors.SimpleButton btnCopyStarTeamLabel;
      private DevExpress.XtraEditors.SimpleButton btnCopyVersion;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
      private DevExpress.XtraEditors.LookUpEdit cboLegalUrl;
      private DevExpress.XtraLayout.LayoutControlItem layCboLegalUrl;
   }
}