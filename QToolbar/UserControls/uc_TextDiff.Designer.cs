namespace QToolbar.UserControls
{
   partial class uc_TextDiff
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_TextDiff));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtRight = new FastColoredTextBoxNS.FastColoredTextBox();
         this.txtLeft = new FastColoredTextBoxNS.FastColoredTextBox();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtRight)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtLeft)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.txtRight);
         this.layoutControl1.Controls.Add(this.txtLeft);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(704, 433);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtRight
         // 
         this.txtRight.AutoCompleteBracketsList = new char[] {
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
         this.txtRight.AutoIndentCharsPatterns = "";
         this.txtRight.AutoIndentExistingLines = false;
         this.txtRight.AutoScrollMinSize = new System.Drawing.Size(32, 15);
         this.txtRight.BackBrush = null;
         this.txtRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtRight.CharHeight = 15;
         this.txtRight.CharWidth = 7;
         this.txtRight.CommentPrefix = "--";
         this.txtRight.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtRight.DelayedEventsInterval = 200;
         this.txtRight.DelayedTextChangedInterval = 500;
         this.txtRight.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtRight.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtRight.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtRight.IsReplaceMode = false;
         this.txtRight.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtRight.LeftBracket = '(';
         this.txtRight.Location = new System.Drawing.Point(354, 28);
         this.txtRight.Name = "txtRight";
         this.txtRight.Paddings = new System.Windows.Forms.Padding(0);
         this.txtRight.ReservedCountOfLineNumberChars = 2;
         this.txtRight.RightBracket = ')';
         this.txtRight.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtRight.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtRight.ServiceColors")));
         this.txtRight.Size = new System.Drawing.Size(338, 393);
         this.txtRight.TabIndex = 6;
         this.txtRight.Zoom = 100;
         // 
         // txtLeft
         // 
         this.txtLeft.AutoCompleteBracketsList = new char[] {
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
         this.txtLeft.AutoIndentCharsPatterns = "";
         this.txtLeft.AutoIndentExistingLines = false;
         this.txtLeft.AutoScrollMinSize = new System.Drawing.Size(32, 15);
         this.txtLeft.BackBrush = null;
         this.txtLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtLeft.CharHeight = 15;
         this.txtLeft.CharWidth = 7;
         this.txtLeft.CommentPrefix = "--";
         this.txtLeft.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtLeft.DelayedEventsInterval = 200;
         this.txtLeft.DelayedTextChangedInterval = 500;
         this.txtLeft.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtLeft.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtLeft.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtLeft.IsReplaceMode = false;
         this.txtLeft.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtLeft.LeftBracket = '(';
         this.txtLeft.Location = new System.Drawing.Point(12, 28);
         this.txtLeft.Name = "txtLeft";
         this.txtLeft.Paddings = new System.Windows.Forms.Padding(0);
         this.txtLeft.ReservedCountOfLineNumberChars = 2;
         this.txtLeft.RightBracket = ')';
         this.txtLeft.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtLeft.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtLeft.ServiceColors")));
         this.txtLeft.Size = new System.Drawing.Size(333, 393);
         this.txtLeft.TabIndex = 5;
         this.txtLeft.Zoom = 100;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(704, 433);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.txtLeft;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(337, 413);
         this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
         this.layoutControlItem1.TextSize = new System.Drawing.Size(93, 13);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtRight;
         this.layoutControlItem2.Location = new System.Drawing.Point(342, 0);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(342, 413);
         this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
         this.layoutControlItem2.TextSize = new System.Drawing.Size(93, 13);
         // 
         // splitterItem1
         // 
         this.splitterItem1.AllowHotTrack = true;
         this.splitterItem1.Location = new System.Drawing.Point(337, 0);
         this.splitterItem1.Name = "splitterItem1";
         this.splitterItem1.Size = new System.Drawing.Size(5, 413);
         // 
         // uc_TextDiff
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.layoutControl1);
         this.Name = "uc_TextDiff";
         this.Size = new System.Drawing.Size(704, 433);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtRight)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtLeft)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private FastColoredTextBoxNS.FastColoredTextBox txtRight;
      private FastColoredTextBoxNS.FastColoredTextBox txtLeft;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.SplitterItem splitterItem1;
   }
}
