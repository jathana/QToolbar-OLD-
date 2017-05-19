using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class QCSAdminButton:ButtonBase
   {
      public QCSAdminButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.QCSAdminFolder, barManager, menu, QCSAdminMenuItem_ItemClick)
      {

         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void QCSAdminMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string qcsadmin = Path.Combine(OptionsInstance.QCSAdminFolder, e.Item.Caption, "QCS.Client.application");
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
