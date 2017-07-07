using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class FoldersButton:ButtonBase
   {
      public FoldersButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.InternalBuildsFolder, barManager, menu)
      {
      }

      public override void CreateMenuItems()
      {
         CreateFoldersMenu();
      }

      private void CreateFoldersMenu()
      {
         _Menu.ClearLinks();
         try
         {
            // load custom folders
            foreach (string folder in OptionsInstance.Folders)
            {
               AddFolderItem(folder);
            }
            // load folders declared in system
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void AddFolderItem(string folder)
      {
         BarButtonItem folderItem = new BarButtonItem(_BarManager, folder, 0);
         folderItem.ItemClick += MenuItemClick;
         _Menu.AddItem(folderItem);
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            Process.Start(e.Item.Caption);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }

   
}
