using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
               AddFolderItem(folder, false);
            }
            // load Next Build folders declared in system            
            string path = string.Empty;
            bool first = true;
            //< barSubItem >.AddItem(< barItem >).BeginGroup = true;
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               if (Directory.Exists(row["Path"].ToString()))
               {
                  path = Path.Combine(row["Path"].ToString(), "Builds\\Next Build");
                  if (!Directory.Exists(path))
                  {
                     path = Path.Combine(row["Path"].ToString(), "Builds");
                  }
                  AddFolderItem(path, first);
                  first = false;
               }
            }


         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void AddFolderItem(string folder, bool addSeparator)
      {
         if (Directory.Exists(folder))
         {
            BarButtonItem folderItem = new BarButtonItem(_BarManager, folder, 0);
            folderItem.ItemClick += MenuItemClick;

            _Menu.AddItem(folderItem).BeginGroup = addSeparator;
         }
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
