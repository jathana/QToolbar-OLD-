namespace QToolbar.Forms
{
   partial class Frm_FieldsHelper
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FieldsHelper));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.lblProgress = new DevExpress.XtraEditors.LabelControl();
         this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
         this.txtGenerateSQL = new FastColoredTextBoxNS.FastColoredTextBox();
         this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
         this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
         this.btnCompare = new DevExpress.XtraEditors.SimpleButton();
         this.btnGenerateSQL = new DevExpress.XtraEditors.SimpleButton();
         this.lkpField = new DevExpress.XtraEditors.LookUpEdit();
         this.lkpTable = new DevExpress.XtraEditors.LookUpEdit();
         this.lkpDatabase = new DevExpress.XtraEditors.LookUpEdit();
         this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
         this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
         this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layProgress = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
         this.workerFetchTables = new System.ComponentModel.BackgroundWorker();
         this.workerFetchFields = new System.ComponentModel.BackgroundWorker();
         this.workerGenerateSQL = new System.ComponentModel.BackgroundWorker();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtGenerateSQL)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
         this.panelControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
         this.layoutControl2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.lkpField.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.lkpTable.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.lkpDatabase.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layProgress)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.lblProgress);
         this.layoutControl1.Controls.Add(this.progressBar);
         this.layoutControl1.Controls.Add(this.txtGenerateSQL);
         this.layoutControl1.Controls.Add(this.panelControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1130, 330, 450, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(790, 559);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // lblProgress
         // 
         this.lblProgress.Location = new System.Drawing.Point(12, 533);
         this.lblProgress.Name = "lblProgress";
         this.lblProgress.Size = new System.Drawing.Size(559, 14);
         this.lblProgress.StyleController = this.layoutControl1;
         this.lblProgress.TabIndex = 9;
         this.lblProgress.Text = "labelControl1";
         // 
         // progressBar
         // 
         this.progressBar.EditValue = 0;
         this.progressBar.Location = new System.Drawing.Point(575, 533);
         this.progressBar.Name = "progressBar";
         this.progressBar.Size = new System.Drawing.Size(203, 14);
         this.progressBar.StyleController = this.layoutControl1;
         this.progressBar.TabIndex = 8;
         // 
         // txtGenerateSQL
         // 
         this.txtGenerateSQL.AutoCompleteBracketsList = new char[] {
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
         this.txtGenerateSQL.AutoIndentCharsPatterns = "";
         this.txtGenerateSQL.AutoIndentExistingLines = false;
         this.txtGenerateSQL.AutoScrollMinSize = new System.Drawing.Size(2, 15);
         this.txtGenerateSQL.BackBrush = null;
         this.txtGenerateSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.txtGenerateSQL.CharHeight = 15;
         this.txtGenerateSQL.CharWidth = 7;
         this.txtGenerateSQL.CommentPrefix = "--";
         this.txtGenerateSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.txtGenerateSQL.DelayedEventsInterval = 200;
         this.txtGenerateSQL.DelayedTextChangedInterval = 500;
         this.txtGenerateSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.txtGenerateSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
         this.txtGenerateSQL.ImeMode = System.Windows.Forms.ImeMode.Off;
         this.txtGenerateSQL.IsReplaceMode = false;
         this.txtGenerateSQL.Language = FastColoredTextBoxNS.Language.SQL;
         this.txtGenerateSQL.LeftBracket = '(';
         this.txtGenerateSQL.Location = new System.Drawing.Point(12, 165);
         this.txtGenerateSQL.Name = "txtGenerateSQL";
         this.txtGenerateSQL.Paddings = new System.Windows.Forms.Padding(0);
         this.txtGenerateSQL.ReservedCountOfLineNumberChars = 2;
         this.txtGenerateSQL.RightBracket = ')';
         this.txtGenerateSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.txtGenerateSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtGenerateSQL.ServiceColors")));
         this.txtGenerateSQL.Size = new System.Drawing.Size(766, 364);
         this.txtGenerateSQL.TabIndex = 7;
         this.txtGenerateSQL.Zoom = 100;
         // 
         // panelControl1
         // 
         this.panelControl1.Controls.Add(this.layoutControl2);
         this.panelControl1.Location = new System.Drawing.Point(12, 12);
         this.panelControl1.Name = "panelControl1";
         this.panelControl1.Size = new System.Drawing.Size(766, 149);
         this.panelControl1.TabIndex = 4;
         // 
         // layoutControl2
         // 
         this.layoutControl2.Controls.Add(this.btnCompare);
         this.layoutControl2.Controls.Add(this.btnGenerateSQL);
         this.layoutControl2.Controls.Add(this.lkpField);
         this.layoutControl2.Controls.Add(this.lkpTable);
         this.layoutControl2.Controls.Add(this.lkpDatabase);
         this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl2.Location = new System.Drawing.Point(2, 2);
         this.layoutControl2.Name = "layoutControl2";
         this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(741, 474, 450, 400);
         this.layoutControl2.Root = this.layoutControlGroup2;
         this.layoutControl2.Size = new System.Drawing.Size(762, 145);
         this.layoutControl2.TabIndex = 0;
         this.layoutControl2.Text = "layoutControl2";
         // 
         // btnCompare
         // 
         this.btnCompare.Location = new System.Drawing.Point(485, 108);
         this.btnCompare.Name = "btnCompare";
         this.btnCompare.Size = new System.Drawing.Size(126, 25);
         this.btnCompare.StyleController = this.layoutControl2;
         this.btnCompare.TabIndex = 8;
         this.btnCompare.Text = "Compare";
         this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
         // 
         // btnGenerateSQL
         // 
         this.btnGenerateSQL.Location = new System.Drawing.Point(615, 108);
         this.btnGenerateSQL.Name = "btnGenerateSQL";
         this.btnGenerateSQL.Size = new System.Drawing.Size(135, 25);
         this.btnGenerateSQL.StyleController = this.layoutControl2;
         this.btnGenerateSQL.TabIndex = 7;
         this.btnGenerateSQL.Text = "Generate SQL";
         this.btnGenerateSQL.Click += new System.EventHandler(this.btnGenerateSQL_Click);
         // 
         // lkpField
         // 
         this.lkpField.Location = new System.Drawing.Point(200, 60);
         this.lkpField.Name = "lkpField";
         this.lkpField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.lkpField.Size = new System.Drawing.Size(550, 20);
         this.lkpField.StyleController = this.layoutControl2;
         this.lkpField.TabIndex = 6;
         // 
         // lkpTable
         // 
         this.lkpTable.Location = new System.Drawing.Point(200, 36);
         this.lkpTable.Name = "lkpTable";
         this.lkpTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.lkpTable.Size = new System.Drawing.Size(550, 20);
         this.lkpTable.StyleController = this.layoutControl2;
         this.lkpTable.TabIndex = 5;
         this.lkpTable.EditValueChanged += new System.EventHandler(this.lkpTable_EditValueChanged);
         // 
         // lkpDatabase
         // 
         this.lkpDatabase.Location = new System.Drawing.Point(200, 12);
         this.lkpDatabase.Name = "lkpDatabase";
         this.lkpDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.lkpDatabase.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DATABASE", "DATABASE"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CON_INFO", "CON_INFO", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
         this.lkpDatabase.Size = new System.Drawing.Size(550, 20);
         this.lkpDatabase.StyleController = this.layoutControl2;
         this.lkpDatabase.TabIndex = 4;
         // 
         // layoutControlGroup2
         // 
         this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup2.GroupBordersVisible = false;
         this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutControlItem7});
         this.layoutControlGroup2.Name = "Root";
         this.layoutControlGroup2.Size = new System.Drawing.Size(762, 145);
         this.layoutControlGroup2.TextVisible = false;
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.Control = this.lkpDatabase;
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(742, 24);
         this.layoutControlItem3.Text = "Target Database (Current is excluded)";
         this.layoutControlItem3.TextSize = new System.Drawing.Size(185, 13);
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.lkpTable;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(742, 24);
         this.layoutControlItem4.Text = "Table (from Current)";
         this.layoutControlItem4.TextSize = new System.Drawing.Size(185, 13);
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.lkpField;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(742, 24);
         this.layoutControlItem5.Text = "Field (from Current)";
         this.layoutControlItem5.TextSize = new System.Drawing.Size(185, 13);
         // 
         // layoutControlItem6
         // 
         this.layoutControlItem6.Control = this.btnGenerateSQL;
         this.layoutControlItem6.Location = new System.Drawing.Point(603, 96);
         this.layoutControlItem6.MaxSize = new System.Drawing.Size(139, 29);
         this.layoutControlItem6.MinSize = new System.Drawing.Size(139, 29);
         this.layoutControlItem6.Name = "layoutControlItem6";
         this.layoutControlItem6.Size = new System.Drawing.Size(139, 29);
         this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem6.TextVisible = false;
         // 
         // emptySpaceItem3
         // 
         this.emptySpaceItem3.AllowHotTrack = false;
         this.emptySpaceItem3.Location = new System.Drawing.Point(0, 72);
         this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 24);
         this.emptySpaceItem3.MinSize = new System.Drawing.Size(104, 24);
         this.emptySpaceItem3.Name = "emptySpaceItem3";
         this.emptySpaceItem3.Size = new System.Drawing.Size(742, 24);
         this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
         // 
         // emptySpaceItem1
         // 
         this.emptySpaceItem1.AllowHotTrack = false;
         this.emptySpaceItem1.Location = new System.Drawing.Point(0, 96);
         this.emptySpaceItem1.Name = "emptySpaceItem1";
         this.emptySpaceItem1.Size = new System.Drawing.Size(473, 29);
         this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
         // 
         // layoutControlItem7
         // 
         this.layoutControlItem7.Control = this.btnCompare;
         this.layoutControlItem7.Location = new System.Drawing.Point(473, 96);
         this.layoutControlItem7.MaxSize = new System.Drawing.Size(130, 29);
         this.layoutControlItem7.MinSize = new System.Drawing.Size(130, 29);
         this.layoutControlItem7.Name = "layoutControlItem7";
         this.layoutControlItem7.Size = new System.Drawing.Size(130, 29);
         this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem7.TextVisible = false;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layProgress,
            this.layoutControlItem8});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(790, 559);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.panelControl1;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 153);
         this.layoutControlItem1.MinSize = new System.Drawing.Size(270, 153);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(770, 153);
         this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.txtGenerateSQL;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 153);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(770, 368);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layProgress
         // 
         this.layProgress.Control = this.progressBar;
         this.layProgress.Location = new System.Drawing.Point(563, 521);
         this.layProgress.MaxSize = new System.Drawing.Size(207, 18);
         this.layProgress.MinSize = new System.Drawing.Size(207, 18);
         this.layProgress.Name = "layProgress";
         this.layProgress.Size = new System.Drawing.Size(207, 18);
         this.layProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layProgress.TextSize = new System.Drawing.Size(0, 0);
         this.layProgress.TextVisible = false;
         // 
         // layoutControlItem8
         // 
         this.layoutControlItem8.Control = this.lblProgress;
         this.layoutControlItem8.Location = new System.Drawing.Point(0, 521);
         this.layoutControlItem8.MinSize = new System.Drawing.Size(67, 17);
         this.layoutControlItem8.Name = "layoutControlItem8";
         this.layoutControlItem8.Size = new System.Drawing.Size(563, 18);
         this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
         this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem8.TextVisible = false;
         // 
         // workerFetchTables
         // 
         this.workerFetchTables.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerFetchTables_DoWork);
         this.workerFetchTables.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerFetchTables_RunWorkerCompleted);
         // 
         // workerFetchFields
         // 
         this.workerFetchFields.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerFetchFields_DoWork);
         this.workerFetchFields.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerFetchFields_RunWorkerCompleted);
         // 
         // workerGenerateSQL
         // 
         this.workerGenerateSQL.WorkerReportsProgress = true;
         this.workerGenerateSQL.WorkerSupportsCancellation = true;
         this.workerGenerateSQL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerGenerateSQL_DoWork);
         this.workerGenerateSQL.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerGenerateSQL_ProgressChanged);
         this.workerGenerateSQL.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerGenerateSQL_RunWorkerCompleted);
         // 
         // Frm_FieldsHelper
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(790, 559);
         this.Controls.Add(this.layoutControl1);
         this.Name = "Frm_FieldsHelper";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Fields Helper";
         this.Load += new System.EventHandler(this.Frm_FieldsHelper_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtGenerateSQL)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
         this.panelControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
         this.layoutControl2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.lkpField.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.lkpTable.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.lkpDatabase.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layProgress)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraEditors.PanelControl panelControl1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private FastColoredTextBoxNS.FastColoredTextBox txtGenerateSQL;
      private DevExpress.XtraLayout.LayoutControl layoutControl2;
      private DevExpress.XtraEditors.LookUpEdit lkpField;
      private DevExpress.XtraEditors.LookUpEdit lkpTable;
      private DevExpress.XtraEditors.LookUpEdit lkpDatabase;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraEditors.SimpleButton btnGenerateSQL;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
      private System.ComponentModel.BackgroundWorker workerFetchTables;
      private System.ComponentModel.BackgroundWorker workerFetchFields;
      private System.ComponentModel.BackgroundWorker workerGenerateSQL;
      private DevExpress.XtraEditors.LabelControl lblProgress;
      private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
      private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
      private DevExpress.XtraLayout.LayoutControlItem layProgress;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
      private DevExpress.XtraEditors.SimpleButton btnCompare;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
   }
}