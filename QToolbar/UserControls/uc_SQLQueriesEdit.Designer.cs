namespace QToolbar
{
   partial class uc_SQLQueriesEdit
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_SQLQueriesEdit));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.txtSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.chkRunImmediate = new DevExpress.XtraEditors.CheckEdit();
         this.txtName = new DevExpress.XtraEditors.TextEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.RunImmediate = new DevExpress.XtraLayout.LayoutControlItem();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkRunImmediate.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.RunImmediate)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.SetBoundPropertyName(this.layoutControl1, "");
         this.layoutControl1.Controls.Add(this.txtSQL);
         this.layoutControl1.Controls.Add(this.chkRunImmediate);
         this.layoutControl1.Controls.Add(this.txtName);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1096, 97, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(736, 543);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // txtSQL
         // 
         this.txtSQL.AutoCompleteBracketsList = new char[] {
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
         this.txtSQL.AutoIndentCharsPatterns = "";
         this.txtSQL.AutoIndentExistingLines = false;
         this.txtSQL.AutoScrollMinSize = new System.Drawing.Size(2, 15);
         this.txtSQL.BackBrush = null;
         this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.SetBoundFieldName(this.txtSQL, "SQL");
         this.txtSQL.CharHeight = 15;
         this.txtSQL.CharWidth = 7;
         this.txtSQL.CommentPrefix = "--";
         this.txtSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtSQL.DelayedEventsInterval = 200;
         this.txtSQL.DelayedTextChangedInterval = 500;
         this.txtSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtSQL.IsReplaceMode = false;
         this.txtSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtSQL.LeftBracket = '(';
         this.txtSQL.Location = new System.Drawing.Point(42, 36);
         this.txtSQL.Name = "txtSQL";
         this.txtSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtSQL.ReservedCountOfLineNumberChars = 2;
         this.txtSQL.RightBracket = ')';
         this.txtSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSQL.ServiceColors")));
         this.txtSQL.Size = new System.Drawing.Size(682, 495);
         this.txtSQL.TabIndex = 6;
         this.txtSQL.Zoom = 100;
         // 
         // chkRunImmediate
         // 
         this.SetBoundFieldName(this.chkRunImmediate, "RunImmediate");
         this.SetBoundPropertyName(this.chkRunImmediate, "EditValue");
         this.chkRunImmediate.Location = new System.Drawing.Point(618, 12);
         this.chkRunImmediate.Name = "chkRunImmediate";
         this.chkRunImmediate.Properties.Caption = "Run Immediate";
         this.chkRunImmediate.Size = new System.Drawing.Size(106, 19);
         this.chkRunImmediate.StyleController = this.layoutControl1;
         this.chkRunImmediate.TabIndex = 5;
         // 
         // txtName
         // 
         this.SetBoundFieldName(this.txtName, "Name");
         this.SetBoundPropertyName(this.txtName, "EditValue");
         this.txtName.Location = new System.Drawing.Point(42, 12);
         this.txtName.Name = "txtName";
         this.txtName.Size = new System.Drawing.Size(572, 20);
         this.txtName.StyleController = this.layoutControl1;
         this.txtName.TabIndex = 4;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.RunImmediate});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(736, 543);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.txtSQL;
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(716, 499);
         this.layoutControlItem3.Text = "SQL";
         this.layoutControlItem3.TextSize = new System.Drawing.Size(27, 13);
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.txtName;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(606, 24);
         this.layoutControlItem1.Text = "Name";
         this.layoutControlItem1.TextSize = new System.Drawing.Size(27, 13);
         // 
         // RunImmediate
         // 
         this.RunImmediate.Control = this.chkRunImmediate;
         this.RunImmediate.Location = new System.Drawing.Point(606, 0);
         this.RunImmediate.Name = "RunImmediate";
         this.RunImmediate.Size = new System.Drawing.Size(110, 24);
         this.RunImmediate.TextSize = new System.Drawing.Size(0, 0);
         this.RunImmediate.TextVisible = false;
         // 
         // uc_SQLQueriesEdit
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.layoutControl1);
         this.Name = "uc_SQLQueriesEdit";
         this.Size = new System.Drawing.Size(736, 543);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkRunImmediate.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.RunImmediate)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraEditors.CheckEdit chkRunImmediate;
      private DevExpress.XtraEditors.TextEdit txtName;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraLayout.LayoutControlItem RunImmediate;
      private FastColoredTextBoxNS.FastColoredTextBox txtSQL;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
   }
}
