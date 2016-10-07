namespace QToolbar
{
   partial class Frm_FileViewer
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
         this.memContent = new DevExpress.XtraEditors.MemoEdit();
         ((System.ComponentModel.ISupportInitialize)(this.memContent.Properties)).BeginInit();
         this.SuspendLayout();
         // 
         // memContent
         // 
         this.memContent.Dock = System.Windows.Forms.DockStyle.Fill;
         this.memContent.Location = new System.Drawing.Point(0, 0);
         this.memContent.Name = "memContent";
         this.memContent.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
         this.memContent.Properties.Appearance.Options.UseFont = true;
         this.memContent.Properties.ReadOnly = true;
         this.memContent.Size = new System.Drawing.Size(964, 629);
         this.memContent.TabIndex = 0;
         // 
         // Frm_FileViewer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(964, 629);
         this.Controls.Add(this.memContent);
         this.Name = "Frm_FileViewer";
         this.Text = "File Viewer";
         ((System.ComponentModel.ISupportInitialize)(this.memContent.Properties)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraEditors.MemoEdit memContent;
   }
}