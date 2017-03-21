﻿namespace QToolbar
{
   partial class Frm_Main
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
         DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
         DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
         DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
         DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
         DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
         this.btnOptions = new DevExpress.XtraBars.BarButtonItem();
         this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
         this.mnuDesigners = new DevExpress.XtraBars.BarSubItem();
         this.btnOptions2 = new DevExpress.XtraBars.BarButtonItem();
         this.mnuQCSAdminCFs = new DevExpress.XtraBars.BarSubItem();
         this.mnuQCSAdmin = new DevExpress.XtraBars.BarSubItem();
         this.mnuQCSAgent = new DevExpress.XtraBars.BarSubItem();
         this.mnuLegalLinks = new DevExpress.XtraBars.BarSubItem();
         this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
         this.mnuExecutorConfiguration = new DevExpress.XtraBars.BarSubItem();
         this.mnuFolders = new DevExpress.XtraBars.BarSubItem();
         this.mnuDatabaseScripter = new DevExpress.XtraBars.BarSubItem();
         this.mnuFieldsExplorer = new DevExpress.XtraBars.BarSubItem();
         this.btnClearMetadata = new DevExpress.XtraBars.BarButtonItem();
         this.mnuEnvironmentsConfiguration = new DevExpress.XtraBars.BarSubItem();
         this.mnuNextBuild = new DevExpress.XtraBars.BarSubItem();
         this.mnuInternalBuilds = new DevExpress.XtraBars.BarSubItem();
         this.mnuShellCommands = new DevExpress.XtraBars.BarSubItem();
         this.btnSQL = new DevExpress.XtraBars.BarButtonItem();
         this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
         this.grpApplications = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.grpBuilds = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.grpFiles = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.grpSQL = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         this.grpTools = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
         this.SuspendLayout();
         // 
         // barManager1
         // 
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Images = this.imageCollection1;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnOptions});
         this.barManager1.MaxItemId = 1;
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(1114, 0);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 94);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(1114, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 94);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(1114, 0);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 94);
         // 
         // imageCollection1
         // 
         this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
         this.imageCollection1.InsertGalleryImage("open_16x16.png", "images/actions/open_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/open_16x16.png"), 0);
         this.imageCollection1.Images.SetKeyName(0, "open_16x16.png");
         this.imageCollection1.InsertGalleryImage("textbox_16x16.png", "images/content/textbox_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/content/textbox_16x16.png"), 1);
         this.imageCollection1.Images.SetKeyName(1, "textbox_16x16.png");
         this.imageCollection1.InsertGalleryImage("drilldown_16x16.png", "images/chart/drilldown_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/chart/drilldown_16x16.png"), 2);
         this.imageCollection1.Images.SetKeyName(2, "drilldown_16x16.png");
         this.imageCollection1.InsertGalleryImage("math&trig_16x16.png", "images/function%20library/math&trig_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/function%20library/math&trig_16x16.png"), 3);
         this.imageCollection1.Images.SetKeyName(3, "math&trig_16x16.png");
         // 
         // btnOptions
         // 
         this.btnOptions.Caption = "Options";
         this.btnOptions.Id = 0;
         this.btnOptions.Name = "btnOptions";
         this.btnOptions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOptions_ItemClick);
         // 
         // ribbonControl1
         // 
         this.ribbonControl1.AllowMinimizeRibbon = false;
         this.ribbonControl1.ExpandCollapseItem.Id = 0;
         this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.mnuDesigners,
            this.btnOptions2,
            this.mnuQCSAdminCFs,
            this.mnuQCSAdmin,
            this.mnuQCSAgent,
            this.mnuLegalLinks,
            this.btnRefresh,
            this.mnuExecutorConfiguration,
            this.mnuFolders,
            this.mnuDatabaseScripter,
            this.mnuFieldsExplorer,
            this.btnClearMetadata,
            this.mnuEnvironmentsConfiguration,
            this.mnuNextBuild,
            this.mnuInternalBuilds,
            this.mnuShellCommands,
            this.btnSQL});
         this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
         this.ribbonControl1.MaxItemId = 31;
         this.ribbonControl1.Name = "ribbonControl1";
         this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
         this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
         this.ribbonControl1.Size = new System.Drawing.Size(1114, 95);
         this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
         // 
         // mnuDesigners
         // 
         this.mnuDesigners.Caption = "Designers";
         this.mnuDesigners.Id = 10;
         this.mnuDesigners.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuDesigners.ImageOptions.Image")));
         this.mnuDesigners.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuDesigners.ImageOptions.LargeImage")));
         this.mnuDesigners.Name = "mnuDesigners";
         toolTipTitleItem1.Text = "Opens Designer";
         superToolTip1.Items.Add(toolTipTitleItem1);
         this.mnuDesigners.SuperTip = superToolTip1;
         // 
         // btnOptions2
         // 
         this.btnOptions2.Caption = "Options";
         this.btnOptions2.Id = 11;
         this.btnOptions2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions2.ImageOptions.Image")));
         this.btnOptions2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOptions2.ImageOptions.LargeImage")));
         this.btnOptions2.Name = "btnOptions2";
         toolTipTitleItem2.Text = "Opens options dialog";
         superToolTip2.Items.Add(toolTipTitleItem2);
         this.btnOptions2.SuperTip = superToolTip2;
         this.btnOptions2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOptions2_ItemClick);
         // 
         // mnuQCSAdminCFs
         // 
         this.mnuQCSAdminCFs.Caption = "QCSAdmin CFs";
         this.mnuQCSAdminCFs.Id = 11;
         this.mnuQCSAdminCFs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuQCSAdminCFs.ImageOptions.Image")));
         this.mnuQCSAdminCFs.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuQCSAdminCFs.ImageOptions.LargeImage")));
         this.mnuQCSAdminCFs.Name = "mnuQCSAdminCFs";
         toolTipTitleItem3.Text = "Opens the latest cf file of the selected version";
         superToolTip3.Items.Add(toolTipTitleItem3);
         this.mnuQCSAdminCFs.SuperTip = superToolTip3;
         // 
         // mnuQCSAdmin
         // 
         this.mnuQCSAdmin.Caption = "QCS Admin";
         this.mnuQCSAdmin.Id = 14;
         this.mnuQCSAdmin.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuQCSAdmin.ImageOptions.LargeImage")));
         this.mnuQCSAdmin.Name = "mnuQCSAdmin";
         toolTipTitleItem4.Text = "Opens QCS Admin application";
         superToolTip4.Items.Add(toolTipTitleItem4);
         this.mnuQCSAdmin.SuperTip = superToolTip4;
         // 
         // mnuQCSAgent
         // 
         this.mnuQCSAgent.Caption = "QCS Agent";
         this.mnuQCSAgent.Id = 15;
         this.mnuQCSAgent.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuQCSAgent.ImageOptions.LargeImage")));
         this.mnuQCSAgent.Name = "mnuQCSAgent";
         toolTipTitleItem5.Text = "Opens QCS Agent application";
         superToolTip5.Items.Add(toolTipTitleItem5);
         this.mnuQCSAgent.SuperTip = superToolTip5;
         // 
         // mnuLegalLinks
         // 
         this.mnuLegalLinks.Caption = "Legal";
         this.mnuLegalLinks.Id = 16;
         this.mnuLegalLinks.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuLegalLinks.ImageOptions.Image")));
         this.mnuLegalLinks.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuLegalLinks.ImageOptions.LargeImage")));
         this.mnuLegalLinks.Name = "mnuLegalLinks";
         // 
         // btnRefresh
         // 
         this.btnRefresh.Caption = "Refresh";
         this.btnRefresh.Id = 17;
         this.btnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.Image")));
         this.btnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.LargeImage")));
         this.btnRefresh.Name = "btnRefresh";
         this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
         // 
         // mnuExecutorConfiguration
         // 
         this.mnuExecutorConfiguration.Caption = "Executor Configuration";
         this.mnuExecutorConfiguration.Id = 18;
         this.mnuExecutorConfiguration.ImageOptions.LargeImage = global::QToolbar.Properties.Resources.activity_48;
         this.mnuExecutorConfiguration.Name = "mnuExecutorConfiguration";
         // 
         // mnuFolders
         // 
         this.mnuFolders.Caption = "My Folders";
         this.mnuFolders.Id = 19;
         this.mnuFolders.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuFolders.ImageOptions.Image")));
         this.mnuFolders.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuFolders.ImageOptions.LargeImage")));
         this.mnuFolders.Name = "mnuFolders";
         // 
         // mnuDatabaseScripter
         // 
         this.mnuDatabaseScripter.Caption = "Database Scripter";
         this.mnuDatabaseScripter.Id = 21;
         this.mnuDatabaseScripter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuDatabaseScripter.ImageOptions.Image")));
         this.mnuDatabaseScripter.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuDatabaseScripter.ImageOptions.LargeImage")));
         this.mnuDatabaseScripter.Name = "mnuDatabaseScripter";
         // 
         // mnuFieldsExplorer
         // 
         this.mnuFieldsExplorer.Caption = "Fields Explorer";
         this.mnuFieldsExplorer.Id = 22;
         this.mnuFieldsExplorer.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuFieldsExplorer.ImageOptions.LargeImage")));
         this.mnuFieldsExplorer.Name = "mnuFieldsExplorer";
         // 
         // btnClearMetadata
         // 
         this.btnClearMetadata.Caption = "Clear Metadata";
         this.btnClearMetadata.Id = 24;
         this.btnClearMetadata.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMetadata.ImageOptions.Image")));
         this.btnClearMetadata.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnClearMetadata.ImageOptions.LargeImage")));
         this.btnClearMetadata.Name = "btnClearMetadata";
         this.btnClearMetadata.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearMetadata_ItemClick);
         // 
         // mnuEnvironmentsConfiguration
         // 
         this.mnuEnvironmentsConfiguration.Caption = "Environments Configuration";
         this.mnuEnvironmentsConfiguration.Id = 25;
         this.mnuEnvironmentsConfiguration.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuEnvironmentsConfiguration.ImageOptions.Image")));
         this.mnuEnvironmentsConfiguration.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuEnvironmentsConfiguration.ImageOptions.LargeImage")));
         this.mnuEnvironmentsConfiguration.Name = "mnuEnvironmentsConfiguration";
         // 
         // mnuNextBuild
         // 
         this.mnuNextBuild.Caption = "Next Build";
         this.mnuNextBuild.Id = 27;
         this.mnuNextBuild.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuNextBuild.ImageOptions.Image")));
         this.mnuNextBuild.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuNextBuild.ImageOptions.LargeImage")));
         this.mnuNextBuild.Name = "mnuNextBuild";
         // 
         // mnuInternalBuilds
         // 
         this.mnuInternalBuilds.Caption = "Internal Builds";
         this.mnuInternalBuilds.Id = 28;
         this.mnuInternalBuilds.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuInternalBuilds.ImageOptions.Image")));
         this.mnuInternalBuilds.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuInternalBuilds.ImageOptions.LargeImage")));
         this.mnuInternalBuilds.Name = "mnuInternalBuilds";
         // 
         // mnuShellCommands
         // 
         this.mnuShellCommands.Caption = "Shell Commands";
         this.mnuShellCommands.Id = 29;
         this.mnuShellCommands.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuShellCommands.ImageOptions.Image")));
         this.mnuShellCommands.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuShellCommands.ImageOptions.LargeImage")));
         this.mnuShellCommands.Name = "mnuShellCommands";
         // 
         // btnSQL
         // 
         this.btnSQL.Caption = "Run SQL";
         this.btnSQL.Id = 30;
         this.btnSQL.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSQL.ImageOptions.Image")));
         this.btnSQL.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSQL.ImageOptions.LargeImage")));
         this.btnSQL.Name = "btnSQL";
         this.btnSQL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSQL_ItemClick);
         // 
         // ribbonPage1
         // 
         this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpApplications,
            this.grpBuilds,
            this.grpFiles,
            this.grpSQL,
            this.grpTools});
         this.ribbonPage1.Name = "ribbonPage1";
         this.ribbonPage1.Text = "QIndex";
         // 
         // grpApplications
         // 
         this.grpApplications.ItemLinks.Add(this.mnuDesigners);
         this.grpApplications.ItemLinks.Add(this.mnuQCSAdmin);
         this.grpApplications.ItemLinks.Add(this.mnuQCSAgent);
         this.grpApplications.ItemLinks.Add(this.mnuExecutorConfiguration);
         this.grpApplications.ItemLinks.Add(this.mnuFieldsExplorer);
         this.grpApplications.ItemLinks.Add(this.mnuDatabaseScripter);
         this.grpApplications.ItemLinks.Add(this.mnuLegalLinks);
         this.grpApplications.Name = "grpApplications";
         this.grpApplications.Text = "Applications";
         // 
         // grpBuilds
         // 
         this.grpBuilds.ItemLinks.Add(this.mnuNextBuild);
         this.grpBuilds.ItemLinks.Add(this.mnuInternalBuilds);
         this.grpBuilds.Name = "grpBuilds";
         this.grpBuilds.Text = "Builds";
         // 
         // grpFiles
         // 
         this.grpFiles.ItemLinks.Add(this.btnClearMetadata);
         this.grpFiles.ItemLinks.Add(this.mnuQCSAdminCFs);
         this.grpFiles.ItemLinks.Add(this.mnuEnvironmentsConfiguration);
         this.grpFiles.ItemLinks.Add(this.mnuFolders);
         this.grpFiles.ItemLinks.Add(this.mnuShellCommands);
         this.grpFiles.Name = "grpFiles";
         this.grpFiles.Text = "Files - Folders";
         // 
         // grpSQL
         // 
         this.grpSQL.ItemLinks.Add(this.btnSQL);
         this.grpSQL.Name = "grpSQL";
         this.grpSQL.Text = "SQL";
         // 
         // grpTools
         // 
         this.grpTools.ItemLinks.Add(this.btnRefresh);
         this.grpTools.ItemLinks.Add(this.btnOptions2);
         this.grpTools.Name = "grpTools";
         this.grpTools.Text = "Tools";
         // 
         // Frm_Main
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1114, 94);
         this.Controls.Add(this.ribbonControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MaximumSize = new System.Drawing.Size(1130, 132);
         this.MinimumSize = new System.Drawing.Size(1130, 82);
         this.Name = "Frm_Main";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "QToolbar";
         this.TopMost = true;
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
         this.Load += new System.EventHandler(this.Frm_Main_Load);
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem btnOptions;
      private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
      private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpApplications;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpTools;
      private DevExpress.XtraBars.BarSubItem mnuDesigners;
      private DevExpress.XtraBars.BarButtonItem btnOptions2;
      private DevExpress.XtraBars.BarSubItem mnuQCSAdminCFs;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpFiles;
      private DevExpress.XtraBars.BarSubItem mnuQCSAdmin;
      private DevExpress.XtraBars.BarSubItem mnuQCSAgent;
      private DevExpress.XtraBars.BarSubItem mnuLegalLinks;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpSQL;
      private DevExpress.XtraBars.BarButtonItem btnRefresh;
      private DevExpress.XtraBars.BarSubItem mnuExecutorConfiguration;
      private DevExpress.Utils.ImageCollection imageCollection1;
      private DevExpress.XtraBars.BarSubItem mnuFolders;
      private DevExpress.XtraBars.BarSubItem mnuDatabaseScripter;
      private DevExpress.XtraBars.BarSubItem mnuFieldsExplorer;
      private DevExpress.XtraBars.BarButtonItem btnClearMetadata;
      private DevExpress.XtraBars.BarSubItem mnuEnvironmentsConfiguration;
      private DevExpress.XtraBars.BarSubItem mnuNextBuild;
      private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpBuilds;
      private DevExpress.XtraBars.BarSubItem mnuInternalBuilds;
      private DevExpress.XtraBars.BarSubItem mnuShellCommands;
      private DevExpress.XtraBars.BarButtonItem btnSQL;
   }
}