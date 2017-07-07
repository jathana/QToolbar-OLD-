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
   public class QCSAgentButton:ButtonBase
   {
      public QCSAgentButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.QCSAgentFolder, barManager, menu, QCSAgentMenuItem_ItemClick, ShouldAddItem)
      {

         
      }


      public void CreateItems()
      {
         CreateClickOnce();
      }

      private static void QCSAgentMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string qcsadmin = GetItemPath(e.Item.Caption);
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private static new bool ShouldAddItem(BarItem item)
      {
         return File.Exists(GetItemPath(item.Caption));
      }

      private static string GetItemPath(string itemCaption)
      {
         return Path.Combine(OptionsInstance.QCSAgentFolder, itemCaption, "CollectionAgentSystem.Client.application");
      }
   }
}
