namespace QToolbar
{
   partial class Frm_NextBuildChecker
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
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
         this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.gridResults = new DevExpress.XtraGrid.GridControl();
         this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colFile = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colTag = new DevExpress.XtraGrid.Columns.GridColumn();
         this.colResult = new DevExpress.XtraGrid.Columns.GridColumn();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.gridResults);
         this.layoutControl1.Controls.Add(this.labelControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1040, 189, 250, 350);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(1030, 468);
         this.layoutControl1.TabIndex = 0;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(1030, 468);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // labelControl1
         // 
         this.labelControl1.Location = new System.Drawing.Point(12, 12);
         this.labelControl1.Name = "labelControl1";
         this.labelControl1.Size = new System.Drawing.Size(207, 13);
         this.labelControl1.StyleController = this.layoutControl1;
         this.labelControl1.TabIndex = 8;
         this.labelControl1.Text = "Ensure that you have the latest checkout!!";
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.labelControl1;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(1010, 17);
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // gridResults
         // 
         this.gridResults.Location = new System.Drawing.Point(12, 29);
         this.gridResults.MainView = this.gridView1;
         this.gridResults.Name = "gridResults";
         this.gridResults.Size = new System.Drawing.Size(1006, 427);
         this.gridResults.TabIndex = 9;
         this.gridResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
         // 
         // gridView1
         // 
         this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMessage,
            this.colResult,
            this.colFile,
            this.colTag});
         this.gridView1.GridControl = this.gridResults;
         this.gridView1.Name = "gridView1";
         this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
         this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.gridResults;
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 17);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(1010, 431);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // colMessage
         // 
         this.colMessage.Caption = "Message";
         this.colMessage.FieldName = "Message";
         this.colMessage.Name = "colMessage";
         this.colMessage.Visible = true;
         this.colMessage.VisibleIndex = 0;
         this.colMessage.Width = 555;
         // 
         // colFile
         // 
         this.colFile.Caption = "File";
         this.colFile.FieldName = "File";
         this.colFile.Name = "colFile";
         this.colFile.Visible = true;
         this.colFile.VisibleIndex = 2;
         this.colFile.Width = 371;
         // 
         // colTag
         // 
         this.colTag.Caption = "Tag";
         this.colTag.FieldName = "Tag";
         this.colTag.Name = "colTag";
         // 
         // colResult
         // 
         this.colResult.Caption = "Result";
         this.colResult.FieldName = "Result";
         this.colResult.Name = "colResult";
         this.colResult.Visible = true;
         this.colResult.VisibleIndex = 1;
         this.colResult.Width = 62;
         // 
         // Frm_NextBuildChecker
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1030, 468);
         this.Controls.Add(this.layoutControl1);
         this.Name = "Frm_NextBuildChecker";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Next Build Checker";
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
      private DevExpress.XtraEditors.LabelControl labelControl1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraGrid.GridControl gridResults;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraGrid.Columns.GridColumn colMessage;
      private DevExpress.XtraGrid.Columns.GridColumn colFile;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraGrid.Columns.GridColumn colTag;
      private DevExpress.XtraGrid.Columns.GridColumn colResult;
   }
}

