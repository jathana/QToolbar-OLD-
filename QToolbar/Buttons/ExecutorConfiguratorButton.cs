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
   public class ExecutorConfiguratorButton : ButtonBase
   {
      public ExecutorConfiguratorButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.ExecutorConfiguratorFolder, barManager, menu, ExecutorConfiguration_ItemClick)
      {

         
      }

      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void ExecutorConfiguration_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = Path.Combine(OptionsInstance.ExecutorConfiguratorFolder, e.Item.Caption, "QC.ExecutorConfigurator.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
