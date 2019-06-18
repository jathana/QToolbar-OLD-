namespace QToolbar.Forms
{
    partial class Frm_TxtDiff
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TxtDiff));
         this.label6 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.ofdFile = new System.Windows.Forms.OpenFileDialog();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtRight = new DevExpress.XtraEditors.TextEdit();
         this.txtLeft = new DevExpress.XtraEditors.TextEdit();
         this.fctb2 = new FastColoredTextBoxNS.FastColoredTextBox();
         this.fctb1 = new FastColoredTextBoxNS.FastColoredTextBox();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layLeft = new DevExpress.XtraLayout.LayoutControlItem();
         this.layRight = new DevExpress.XtraLayout.LayoutControlItem();
         this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar2 = new DevExpress.XtraBars.Bar();
         this.btnViewLeftFile = new DevExpress.XtraBars.BarButtonItem();
         this.btnViewRightFile = new DevExpress.XtraBars.BarButtonItem();
         this.bar3 = new DevExpress.XtraBars.Bar();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtRight.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtLeft.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.fctb2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.fctb1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layLeft)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layRight)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         this.SuspendLayout();
         // 
         // label6
         // 
         this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label6.Location = new System.Drawing.Point(143, 550);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(84, 20);
         this.label6.TabIndex = 24;
         this.label6.Text = "Deleted lines";
         // 
         // label7
         // 
         this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label7.BackColor = System.Drawing.Color.Pink;
         this.label7.Location = new System.Drawing.Point(117, 550);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(22, 20);
         this.label7.TabIndex = 23;
         this.label7.Text = " ";
         // 
         // label5
         // 
         this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label5.Location = new System.Drawing.Point(36, 550);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(77, 20);
         this.label5.TabIndex = 22;
         this.label5.Text = "Inserted lines";
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.BackColor = System.Drawing.Color.PaleGreen;
         this.label4.Location = new System.Drawing.Point(12, 550);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(20, 20);
         this.label4.TabIndex = 21;
         this.label4.Text = " ";
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.txtRight);
         this.layoutControl1.Controls.Add(this.txtLeft);
         this.layoutControl1.Controls.Add(this.fctb2);
         this.layoutControl1.Controls.Add(this.fctb1);
         this.layoutControl1.Controls.Add(this.label6);
         this.layoutControl1.Controls.Add(this.label7);
         this.layoutControl1.Controls.Add(this.label5);
         this.layoutControl1.Controls.Add(this.label4);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 24);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2141, 278, 897, 713);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(970, 582);
         this.layoutControl1.TabIndex = 29;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtRight
         // 
         this.txtRight.Enabled = false;
         this.txtRight.Location = new System.Drawing.Point(497, 12);
         this.txtRight.Name = "txtRight";
         this.txtRight.Size = new System.Drawing.Size(461, 20);
         this.txtRight.StyleController = this.layoutControl1;
         this.txtRight.TabIndex = 28;
         // 
         // txtLeft
         // 
         this.txtLeft.Enabled = false;
         this.txtLeft.Location = new System.Drawing.Point(12, 12);
         this.txtLeft.Name = "txtLeft";
         this.txtLeft.Size = new System.Drawing.Size(476, 20);
         this.txtLeft.StyleController = this.layoutControl1;
         this.txtLeft.TabIndex = 27;
         // 
         // fctb2
         // 
         this.fctb2.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
         this.fctb2.AutoIndentCharsPatterns = "";
         this.fctb2.AutoIndentExistingLines = false;
         this.fctb2.AutoScrollMinSize = new System.Drawing.Size(2, 15);
         this.fctb2.BackBrush = null;
         this.fctb2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.fctb2.CharHeight = 15;
         this.fctb2.CharWidth = 7;
         this.fctb2.CommentPrefix = "--";
         this.fctb2.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.fctb2.DelayedEventsInterval = 200;
         this.fctb2.DelayedTextChangedInterval = 500;
         this.fctb2.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.fctb2.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.fctb2.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.fctb2.IsReplaceMode = false;
         this.fctb2.Language = FastColoredTextBoxNS.Language.SQL;
         this.fctb2.LeftBracket = '(';
         this.fctb2.Location = new System.Drawing.Point(497, 36);
         this.fctb2.Name = "fctb2";
         this.fctb2.Paddings = new System.Windows.Forms.Padding(0);
         this.fctb2.ReadOnly = true;
         this.fctb2.ReservedCountOfLineNumberChars = 2;
         this.fctb2.RightBracket = ')';
         this.fctb2.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.fctb2.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb2.ServiceColors")));
         this.fctb2.Size = new System.Drawing.Size(461, 510);
         this.fctb2.TabIndex = 26;
         this.fctb2.Zoom = 100;
         // 
         // fctb1
         // 
         this.fctb1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
         this.fctb1.AutoIndentCharsPatterns = "";
         this.fctb1.AutoIndentExistingLines = false;
         this.fctb1.AutoScrollMinSize = new System.Drawing.Size(2, 15);
         this.fctb1.BackBrush = null;
         this.fctb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.fctb1.CharHeight = 15;
         this.fctb1.CharWidth = 7;
         this.fctb1.CommentPrefix = "--";
         this.fctb1.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.fctb1.DelayedEventsInterval = 200;
         this.fctb1.DelayedTextChangedInterval = 500;
         this.fctb1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.fctb1.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.fctb1.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.fctb1.IsReplaceMode = false;
         this.fctb1.Language = FastColoredTextBoxNS.Language.SQL;
         this.fctb1.LeftBracket = '(';
         this.fctb1.Location = new System.Drawing.Point(12, 36);
         this.fctb1.Name = "fctb1";
         this.fctb1.Paddings = new System.Windows.Forms.Padding(0);
         this.fctb1.ReadOnly = true;
         this.fctb1.ReservedCountOfLineNumberChars = 2;
         this.fctb1.RightBracket = ')';
         this.fctb1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.fctb1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb1.ServiceColors")));
         this.fctb1.Size = new System.Drawing.Size(476, 510);
         this.fctb1.TabIndex = 25;
         this.fctb1.Zoom = 100;
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
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layLeft,
            this.layRight,
            this.splitterItem1});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(970, 582);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.label5;
         this.layoutControlItem1.Location = new System.Drawing.Point(24, 538);
         this.layoutControlItem1.MaxSize = new System.Drawing.Size(81, 24);
         this.layoutControlItem1.MinSize = new System.Drawing.Size(81, 24);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(81, 24);
         this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.label4;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 538);
         this.layoutControlItem2.MaxSize = new System.Drawing.Size(24, 24);
         this.layoutControlItem2.MinSize = new System.Drawing.Size(24, 24);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(24, 24);
         this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.label7;
         this.layoutControlItem3.Location = new System.Drawing.Point(105, 538);
         this.layoutControlItem3.MaxSize = new System.Drawing.Size(26, 24);
         this.layoutControlItem3.MinSize = new System.Drawing.Size(26, 24);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(26, 24);
         this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem3.TextVisible = false;
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.label6;
         this.layoutControlItem4.Location = new System.Drawing.Point(131, 538);
         this.layoutControlItem4.MaxSize = new System.Drawing.Size(88, 24);
         this.layoutControlItem4.MinSize = new System.Drawing.Size(88, 24);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(88, 24);
         this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // emptySpaceItem1
         // 
         this.emptySpaceItem1.AllowHotTrack = false;
         this.emptySpaceItem1.Location = new System.Drawing.Point(219, 538);
         this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 24);
         this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 24);
         this.emptySpaceItem1.Name = "emptySpaceItem1";
         this.emptySpaceItem1.Size = new System.Drawing.Size(731, 24);
         this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.fctb1;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(480, 514);
         this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem5.TextVisible = false;
         // 
         // layoutControlItem6
         // 
         this.layoutControlItem6.Control = this.fctb2;
         this.layoutControlItem6.Location = new System.Drawing.Point(485, 24);
         this.layoutControlItem6.Name = "layoutControlItem6";
         this.layoutControlItem6.Size = new System.Drawing.Size(465, 514);
         this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem6.TextVisible = false;
         // 
         // layLeft
         // 
         this.layLeft.Control = this.txtLeft;
         this.layLeft.Location = new System.Drawing.Point(0, 0);
         this.layLeft.Name = "layLeft";
         this.layLeft.Size = new System.Drawing.Size(480, 24);
         this.layLeft.Text = "Left";
         this.layLeft.TextSize = new System.Drawing.Size(0, 0);
         this.layLeft.TextVisible = false;
         // 
         // layRight
         // 
         this.layRight.Control = this.txtRight;
         this.layRight.Location = new System.Drawing.Point(485, 0);
         this.layRight.Name = "layRight";
         this.layRight.Size = new System.Drawing.Size(465, 24);
         this.layRight.Text = "Right";
         this.layRight.TextSize = new System.Drawing.Size(0, 0);
         this.layRight.TextVisible = false;
         // 
         // splitterItem1
         // 
         this.splitterItem1.AllowHotTrack = true;
         this.splitterItem1.Location = new System.Drawing.Point(480, 0);
         this.splitterItem1.Name = "splitterItem1";
         this.splitterItem1.Size = new System.Drawing.Size(5, 538);
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnViewLeftFile,
            this.btnViewRightFile});
         this.barManager1.MainMenu = this.bar2;
         this.barManager1.MaxItemId = 2;
         this.barManager1.StatusBar = this.bar3;
         // 
         // bar2
         // 
         this.bar2.BarName = "Main menu";
         this.bar2.DockCol = 0;
         this.bar2.DockRow = 0;
         this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnViewLeftFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnViewRightFile)});
         this.bar2.OptionsBar.MultiLine = true;
         this.bar2.OptionsBar.UseWholeRow = true;
         this.bar2.Text = "Main menu";
         // 
         // btnViewLeftFile
         // 
         this.btnViewLeftFile.Caption = "View Left File";
         this.btnViewLeftFile.Id = 0;
         this.btnViewLeftFile.Name = "btnViewLeftFile";
         this.btnViewLeftFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnViewLeftFile_ItemClick);
         // 
         // btnViewRightFile
         // 
         this.btnViewRightFile.Caption = "View Right File";
         this.btnViewRightFile.Id = 1;
         this.btnViewRightFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnViewRightFile.ImageOptions.Image")));
         this.btnViewRightFile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnViewRightFile.ImageOptions.LargeImage")));
         this.btnViewRightFile.Name = "btnViewRightFile";
         this.btnViewRightFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnViewRightFile_ItemClick);
         // 
         // bar3
         // 
         this.bar3.BarName = "Status bar";
         this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
         this.bar3.DockCol = 0;
         this.bar3.DockRow = 0;
         this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
         this.bar3.OptionsBar.AllowQuickCustomization = false;
         this.bar3.OptionsBar.DrawDragBorder = false;
         this.bar3.OptionsBar.UseWholeRow = true;
         this.bar3.Text = "Status bar";
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(970, 24);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 606);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(970, 23);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 582);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(970, 24);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 582);
         // 
         // Frm_TxtDiff
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(970, 629);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_TxtDiff";
         this.Text = "DiffMergeSample";
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtRight.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtLeft.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.fctb2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.fctb1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layLeft)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layRight)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog ofdFile;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
      private FastColoredTextBoxNS.FastColoredTextBox fctb2;
      private FastColoredTextBoxNS.FastColoredTextBox fctb1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
      private DevExpress.XtraLayout.SplitterItem splitterItem1;
      private DevExpress.XtraEditors.TextEdit txtRight;
      private DevExpress.XtraEditors.TextEdit txtLeft;
      private DevExpress.XtraLayout.LayoutControlItem layLeft;
      private DevExpress.XtraLayout.LayoutControlItem layRight;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar2;
      private DevExpress.XtraBars.Bar bar3;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem btnViewLeftFile;
      private DevExpress.XtraBars.BarButtonItem btnViewRightFile;
   }
}