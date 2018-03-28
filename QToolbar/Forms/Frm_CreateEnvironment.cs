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

namespace QToolbar.Plugins.Environments
{
   public partial class Frm_CreateEnvironment : DevExpress.XtraEditors.XtraForm
   {
      public Frm_CreateEnvironment()
      {
         InitializeComponent();
      }

      private void btnUpdateQCSolutionsCF_Click(object sender, EventArgs e)
      {
         QEnvironmentConfiguration env = new QEnvironmentConfiguration();
         env.CheckoutPath = @"E:\Temp\TestCheckouts\7.2\VS Projects";

         QEnvironmentCreator creator = new QEnvironmentCreator(env);
         creator.UpdateQCSolutionsCFs(env.CheckoutPath, "ALBK_7_2_3", @"Q-SRV-DEVFPS\QBCAB", @"QBCollection_Plus_ALBK_7_2_3", "6702F80E8CD674F9E97BF27871005CE3");
         creator.UpdateQCArchiveCF(env.CheckoutPath, "Data Archive ALBK 7.2", @"Q-SRV-DEVFPS\QBCAB", @"QC_Archive_ALBK_7_2", "6702F80E8CD674F9E97BF27871005CE3");
      }
   }
}