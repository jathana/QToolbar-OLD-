﻿using DevExpress.XtraBars;
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
      public FoldersButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.InternalBuildsFolder, barManager, menu, FolderItem_ItemClick, ShouldAddItem)
      {
      }

      public void CreateItems()
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
         folderItem.ItemClick += FolderItem_ItemClick;
         _Menu.AddItem(folderItem);
      }

      private static void FolderItem_ItemClick(object sender, ItemClickEventArgs e)
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
