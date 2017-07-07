using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class LegalLinksButton:ButtonBase
   {
      
      public LegalLinksButton(BarManager barManager, BarSubItem menu):base("",barManager,menu)
      {
      }

      public override void CreateMenuItems()
      {
        
         _Menu.ClearLinks();
         // load legal links
         try
         {
            foreach (DataRow row in OptionsInstance.LegalLinks.Data.Rows)
            {
               AddLegalLinksItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve legal links");
         }
      }

      private void AddLegalLinksItem(DataRow row)
      {
         BarButtonItem legalLinkItem = new BarButtonItem(_BarManager, row["Name"].ToString(), 3);
         // legal links are shell commands
         legalLinkItem.ItemClick += MenuItemClick;
         legalLinkItem.Tag = row;
         _Menu.AddItem(legalLinkItem);
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            DataRow row = (DataRow)e.Item.Tag;

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.StartInfo.FileName = row["Command"].ToString();
            process.StartInfo.Arguments = row["Arguments"].ToString();
            process.StartInfo.UseShellExecute = true;

            process.Start();


         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
