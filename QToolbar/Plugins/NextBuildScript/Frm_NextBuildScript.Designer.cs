namespace QToolbar.Plugins.NextBuildScript
{
   partial class Frm_NextBuildScript
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
         this.cboBranch = new DevExpress.XtraEditors.ImageComboBoxEdit();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.chkOpenFolder = new DevExpress.XtraEditors.CheckEdit();
         this.memResults = new DevExpress.XtraEditors.MemoEdit();
         this.lblDescription = new DevExpress.XtraEditors.LabelControl();
         this.btnApply = new DevExpress.XtraEditors.SimpleButton();
         this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
         ((System.ComponentModel.ISupportInitialize)(this.cboBranch.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.chkOpenFolder.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.memResults.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
         this.SuspendLayout();
         // 
         // cboBranch
         // 
         this.cboBranch.Location = new System.Drawing.Point(50, 29);
         this.cboBranch.Name = "cboBranch";
         this.cboBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.cboBranch.Size = new System.Drawing.Size(496, 20);
         this.cboBranch.StyleController = this.layoutControl1;
         this.cboBranch.TabIndex = 0;
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.chkOpenFolder);
         this.layoutControl1.Controls.Add(this.memResults);
         this.layoutControl1.Controls.Add(this.lblDescription);
         this.layoutControl1.Controls.Add(this.btnApply);
         this.layoutControl1.Controls.Add(this.btnCancel);
         this.layoutControl1.Controls.Add(this.cboBranch);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(973, 390, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(558, 233);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // chkOpenFolder
         // 
         this.chkOpenFolder.EditValue = true;
         this.chkOpenFolder.Location = new System.Drawing.Point(12, 53);
         this.chkOpenFolder.Name = "chkOpenFolder";
         this.chkOpenFolder.Properties.Caption = "Explore Next Build folder on completion.";
         this.chkOpenFolder.Size = new System.Drawing.Size(534, 19);
         this.chkOpenFolder.StyleController = this.layoutControl1;
         this.chkOpenFolder.TabIndex = 8;
         // 
         // memResults
         // 
         this.memResults.EditValue = "";
         this.memResults.Location = new System.Drawing.Point(12, 108);
         this.memResults.Name = "memResults";
         this.memResults.Properties.ReadOnly = true;
         this.memResults.Size = new System.Drawing.Size(534, 87);
         this.memResults.StyleController = this.layoutControl1;
         this.memResults.TabIndex = 7;
         // 
         // lblDescription
         // 
         this.lblDescription.Appearance.Options.UseTextOptions = true;
         this.lblDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
         this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
         this.lblDescription.Location = new System.Drawing.Point(12, 12);
         this.lblDescription.Name = "lblDescription";
         this.lblDescription.Size = new System.Drawing.Size(534, 13);
         this.lblDescription.StyleController = this.layoutControl1;
         this.lblDescription.TabIndex = 6;
         this.lblDescription.Text = "Description of file goes here....";
         // 
         // btnApply
         // 
         this.btnApply.Location = new System.Drawing.Point(366, 199);
         this.btnApply.Name = "btnApply";
         this.btnApply.Size = new System.Drawing.Size(80, 22);
         this.btnApply.StyleController = this.layoutControl1;
         this.btnApply.TabIndex = 5;
         this.btnApply.Text = "Apply";
         this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Location = new System.Drawing.Point(461, 199);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(85, 22);
         this.btnCancel.StyleController = this.layoutControl1;
         this.btnCancel.TabIndex = 4;
         this.btnCancel.Text = "Close";
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.layoutControlItem6});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(558, 233);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.cboBranch;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 17);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(538, 24);
         this.layoutControlItem1.Text = "Branch";
         this.layoutControlItem1.TextSize = new System.Drawing.Size(35, 13);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.btnCancel;
         this.layoutControlItem2.Location = new System.Drawing.Point(449, 187);
         this.layoutControlItem2.MaxSize = new System.Drawing.Size(89, 26);
         this.layoutControlItem2.MinSize = new System.Drawing.Size(89, 26);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(89, 26);
         this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.btnApply;
         this.layoutControlItem3.Location = new System.Drawing.Point(354, 187);
         this.layoutControlItem3.MaxSize = new System.Drawing.Size(84, 26);
         this.layoutControlItem3.MinSize = new System.Drawing.Size(84, 26);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(84, 26);
         this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem3.TextVisible = false;
         // 
         // emptySpaceItem2
         // 
         this.emptySpaceItem2.AllowHotTrack = false;
         this.emptySpaceItem2.Location = new System.Drawing.Point(438, 187);
         this.emptySpaceItem2.MaxSize = new System.Drawing.Size(11, 26);
         this.emptySpaceItem2.MinSize = new System.Drawing.Size(11, 26);
         this.emptySpaceItem2.Name = "emptySpaceItem2";
         this.emptySpaceItem2.Size = new System.Drawing.Size(11, 26);
         this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
         // 
         // emptySpaceItem3
         // 
         this.emptySpaceItem3.AllowHotTrack = false;
         this.emptySpaceItem3.Location = new System.Drawing.Point(0, 187);
         this.emptySpaceItem3.Name = "emptySpaceItem3";
         this.emptySpaceItem3.Size = new System.Drawing.Size(354, 26);
         this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.lblDescription;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(538, 17);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.memResults;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 80);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(538, 107);
         this.layoutControlItem5.Text = "Results";
         this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
         this.layoutControlItem5.TextSize = new System.Drawing.Size(35, 13);
         // 
         // emptySpaceItem1
         // 
         this.emptySpaceItem1.AllowHotTrack = false;
         this.emptySpaceItem1.Location = new System.Drawing.Point(0, 64);
         this.emptySpaceItem1.Name = "emptySpaceItem1";
         this.emptySpaceItem1.Size = new System.Drawing.Size(538, 16);
         this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layoutControlItem6
         // 
         this.layoutControlItem6.Control = this.chkOpenFolder;
         this.layoutControlItem6.Location = new System.Drawing.Point(0, 41);
         this.layoutControlItem6.Name = "layoutControlItem6";
         this.layoutControlItem6.Size = new System.Drawing.Size(538, 23);
         this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem6.TextVisible = false;
         // 
         // Frm_NextBuildScript
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(558, 233);
         this.Controls.Add(this.layoutControl1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "Frm_NextBuildScript";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "FrmAddEODMetadata";
         ((System.ComponentModel.ISupportInitialize)(this.cboBranch.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.chkOpenFolder.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.memResults.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraEditors.ImageComboBoxEdit cboBranch;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraEditors.SimpleButton btnApply;
      private DevExpress.XtraEditors.SimpleButton btnCancel;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
      private DevExpress.XtraEditors.LabelControl lblDescription;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraEditors.MemoEdit memResults;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
      private DevExpress.XtraEditors.CheckEdit chkOpenFolder;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
   }
}