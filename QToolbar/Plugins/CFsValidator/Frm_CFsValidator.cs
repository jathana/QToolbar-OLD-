using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QToolbar.Plugins.CFsValidator
{
   public partial class Frm_CFsValidator : DevExpress.XtraEditors.XtraForm
   {
      CFsValidator _Validator = new CFsValidator();

      public Frm_CFsValidator()
      {
         InitializeComponent();
      }

      private void btnValidate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         gridResults.DataSource = null;
         backgroundWorker1.RunWorkerAsync();
        
      }

      private void Validator_CfFileLoaded(object sender, CfFileValidationEventArgs e)
      {
         backgroundWorker1.ReportProgress(1, e);
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {

         _Validator.CfFileLoaded += Validator_CfFileLoaded;
         _Validator.Validate();
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         lblInfo.Caption = "Finished";
         gridResults.DataSource = _Validator.Data;
         gridViewResults.BestFitColumns();
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         CfFileValidationEventArgs obj = (CfFileValidationEventArgs)e.UserState;
         lblInfo.Caption = $"{obj.Source}-{obj.Version}-{obj.Repository}-{obj.CfFile}";
      }

      private void Frm_CFsValidator_Load(object sender, EventArgs e)
      {
         gridViewResults.OptionsBehavior.Editable = false;
      }

      private void gridViewResults_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
      {
         
      }

      private void gridViewResults_DoubleClick(object sender, EventArgs e)
      {
         string file = gridViewResults.GetFocusedDataRow()["CF_FILE"].ToString();
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            Frm_FileViewer f1 = new Frm_FileViewer();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }
   }
}