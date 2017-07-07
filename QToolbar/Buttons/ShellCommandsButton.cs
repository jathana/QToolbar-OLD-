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
   public class ShellCommandsButton : ButtonBase
   {
      
      public ShellCommandsButton(BarManager barManager, BarSubItem menu):base("",barManager,menu)
      {
      }

      public override void CreateMenuItems()
      {
         _Menu.ClearLinks();
         // load shell commands
         try
         {
            foreach (DataRow row in OptionsInstance.ShellCommands.Data.Rows)
            {
               AddShellCommandsItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve shell commands");
         }
      }

      private void AddShellCommandsItem(DataRow row)
      {
         BarButtonItem shellCommandItem = new BarButtonItem(_BarManager, row["Name"].ToString(), 2);
         shellCommandItem.ItemClick += MenuItemClick;
         shellCommandItem.Tag = row;
         _Menu.AddItem(shellCommandItem);
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
