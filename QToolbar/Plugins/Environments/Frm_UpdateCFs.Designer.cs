namespace QToolbar.Plugins.Environments
{
   partial class Frm_UpdateCFs
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
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.memOutput = new DevExpress.XtraEditors.MemoEdit();
         this.txtKey = new DevExpress.XtraEditors.TextEdit();
         this.btnApply = new DevExpress.XtraEditors.SimpleButton();
         this.btnClose = new DevExpress.XtraEditors.SimpleButton();
         this.UXGrid = new DevExpress.XtraGrid.GridControl();
         this.UXGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.txtPassword = new DevExpress.XtraEditors.TextEdit();
         this.txtDatabase = new DevExpress.XtraEditors.TextEdit();
         this.txtServer = new DevExpress.XtraEditors.TextEdit();
         this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
         this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.memOutput.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtDatabase.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.memOutput);
         this.layoutControl1.Controls.Add(this.txtKey);
         this.layoutControl1.Controls.Add(this.btnApply);
         this.layoutControl1.Controls.Add(this.btnClose);
         this.layoutControl1.Controls.Add(this.UXGrid);
         this.layoutControl1.Controls.Add(this.txtPassword);
         this.layoutControl1.Controls.Add(this.txtDatabase);
         this.layoutControl1.Controls.Add(this.txtServer);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2067, 216, 558, 400);
         this.layoutControl1.Root = this.Root;
         this.layoutControl1.Size = new System.Drawing.Size(1237, 646);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // memOutput
         // 
         this.memOutput.Location = new System.Drawing.Point(12, 464);
         this.memOutput.Name = "memOutput";
         this.memOutput.Size = new System.Drawing.Size(1213, 144);
         this.memOutput.StyleController = this.layoutControl1;
         this.memOutput.TabIndex = 11;
         // 
         // txtKey
         // 
         this.txtKey.Enabled = false;
         this.txtKey.Location = new System.Drawing.Point(61, 12);
         this.txtKey.Name = "txtKey";
         this.txtKey.Size = new System.Drawing.Size(1164, 20);
         this.txtKey.StyleController = this.layoutControl1;
         this.txtKey.TabIndex = 10;
         // 
         // btnApply
         // 
         this.btnApply.Location = new System.Drawing.Point(1049, 612);
         this.btnApply.Name = "btnApply";
         this.btnApply.Size = new System.Drawing.Size(81, 22);
         this.btnApply.StyleController = this.layoutControl1;
         this.btnApply.TabIndex = 9;
         this.btnApply.Text = "Apply";
         this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
         // 
         // btnClose
         // 
         this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnClose.Location = new System.Drawing.Point(1144, 612);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(81, 22);
         this.btnClose.StyleController = this.layoutControl1;
         this.btnClose.TabIndex = 8;
         this.btnClose.Text = "Close";
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // UXGrid
         // 
         this.UXGrid.Location = new System.Drawing.Point(12, 108);
         this.UXGrid.MainView = this.UXGridView;
         this.UXGrid.Name = "UXGrid";
         this.UXGrid.Size = new System.Drawing.Size(1213, 331);
         this.UXGrid.TabIndex = 7;
         this.UXGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXGridView});
         this.UXGrid.DoubleClick += new System.EventHandler(this.UXGrid_DoubleClick);
         // 
         // UXGridView
         // 
         this.UXGridView.GridControl = this.UXGrid;
         this.UXGridView.Name = "UXGridView";
         // 
         // txtPassword
         // 
         this.txtPassword.EditValue = "6702F80E8CD674F9E97BF27871005CE3";
         this.txtPassword.Enabled = false;
         this.txtPassword.Location = new System.Drawing.Point(61, 84);
         this.txtPassword.Name = "txtPassword";
         this.txtPassword.Size = new System.Drawing.Size(1164, 20);
         this.txtPassword.StyleController = this.layoutControl1;
         this.txtPassword.TabIndex = 6;
         // 
         // txtDatabase
         // 
         this.txtDatabase.Enabled = false;
         this.txtDatabase.Location = new System.Drawing.Point(61, 60);
         this.txtDatabase.Name = "txtDatabase";
         this.txtDatabase.Size = new System.Drawing.Size(1164, 20);
         this.txtDatabase.StyleController = this.layoutControl1;
         this.txtDatabase.TabIndex = 5;
         // 
         // txtServer
         // 
         this.txtServer.Enabled = false;
         this.txtServer.Location = new System.Drawing.Point(61, 36);
         this.txtServer.Name = "txtServer";
         this.txtServer.Size = new System.Drawing.Size(1164, 20);
         this.txtServer.StyleController = this.layoutControl1;
         this.txtServer.TabIndex = 4;
         // 
         // Root
         // 
         this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.Root.GroupBordersVisible = false;
         this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.splitterItem1});
         this.Root.Name = "Root";
         this.Root.Size = new System.Drawing.Size(1237, 646);
         this.Root.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.txtServer;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(1217, 24);
         this.layoutControlItem1.Text = "Server";
         this.layoutControlItem1.TextSize = new System.Drawing.Size(46, 13);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtDatabase;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(1217, 24);
         this.layoutControlItem2.Text = "Database";
         this.layoutControlItem2.TextSize = new System.Drawing.Size(46, 13);
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.txtPassword;
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(1217, 24);
         this.layoutControlItem3.Text = "Password";
         this.layoutControlItem3.TextSize = new System.Drawing.Size(46, 13);
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.UXGrid;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 96);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(1217, 335);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.btnClose;
         this.layoutControlItem5.Location = new System.Drawing.Point(1132, 600);
         this.layoutControlItem5.MaxSize = new System.Drawing.Size(85, 26);
         this.layoutControlItem5.MinSize = new System.Drawing.Size(85, 26);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(85, 26);
         this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem5.TextVisible = false;
         // 
         // layoutControlItem6
         // 
         this.layoutControlItem6.Control = this.btnApply;
         this.layoutControlItem6.Location = new System.Drawing.Point(1037, 600);
         this.layoutControlItem6.MaxSize = new System.Drawing.Size(85, 26);
         this.layoutControlItem6.MinSize = new System.Drawing.Size(85, 26);
         this.layoutControlItem6.Name = "layoutControlItem6";
         this.layoutControlItem6.Size = new System.Drawing.Size(85, 26);
         this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem6.TextVisible = false;
         // 
         // emptySpaceItem1
         // 
         this.emptySpaceItem1.AllowHotTrack = false;
         this.emptySpaceItem1.Location = new System.Drawing.Point(0, 600);
         this.emptySpaceItem1.Name = "emptySpaceItem1";
         this.emptySpaceItem1.Size = new System.Drawing.Size(1037, 26);
         this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
         // 
         // emptySpaceItem2
         // 
         this.emptySpaceItem2.AllowHotTrack = false;
         this.emptySpaceItem2.Location = new System.Drawing.Point(1122, 600);
         this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 26);
         this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 26);
         this.emptySpaceItem2.Name = "emptySpaceItem2";
         this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
         this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layoutControlItem7
         // 
         this.layoutControlItem7.Control = this.txtKey;
         this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem7.Name = "layoutControlItem7";
         this.layoutControlItem7.Size = new System.Drawing.Size(1217, 24);
         this.layoutControlItem7.Text = "Key";
         this.layoutControlItem7.TextSize = new System.Drawing.Size(46, 13);
         // 
         // layoutControlItem8
         // 
         this.layoutControlItem8.Control = this.memOutput;
         this.layoutControlItem8.Location = new System.Drawing.Point(0, 436);
         this.layoutControlItem8.Name = "layoutControlItem8";
         this.layoutControlItem8.Size = new System.Drawing.Size(1217, 164);
         this.layoutControlItem8.Text = "Output";
         this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
         this.layoutControlItem8.TextSize = new System.Drawing.Size(46, 13);
         // 
         // splitterItem1
         // 
         this.splitterItem1.AllowHotTrack = true;
         this.splitterItem1.Location = new System.Drawing.Point(0, 431);
         this.splitterItem1.Name = "splitterItem1";
         this.splitterItem1.Size = new System.Drawing.Size(1217, 5);
         // 
         // Frm_UpdateCFs
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1237, 646);
         this.Controls.Add(this.layoutControl1);
         this.Name = "Frm_UpdateCFs";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Update CFs";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_UpdateCFs_FormClosing);
         this.Load += new System.EventHandler(this.Frm_UpdateCFs_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.memOutput.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtDatabase.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup Root;
      private DevExpress.XtraGrid.GridControl UXGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXGridView;
      private DevExpress.XtraEditors.TextEdit txtPassword;
      private DevExpress.XtraEditors.TextEdit txtDatabase;
      private DevExpress.XtraEditors.TextEdit txtServer;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraEditors.SimpleButton btnApply;
      private DevExpress.XtraEditors.SimpleButton btnClose;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
      private DevExpress.XtraEditors.TextEdit txtKey;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
      private DevExpress.XtraEditors.MemoEdit memOutput;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
      private DevExpress.XtraLayout.SplitterItem splitterItem1;
   }
}