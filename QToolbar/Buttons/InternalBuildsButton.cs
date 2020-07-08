using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class InternalBuildsButton:ButtonBase
   {
      public InternalBuildsButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.InternalBuildsFolder, barManager, menu)
      {
      }

      public override void CreateMenuItems()
      {
         CreateInternalBuildsMenu();
      }

      private void CreateInternalBuildsMenu()
      {
         _Menu.ClearLinks();
         string sqlFolder = OptionsInstance.InternalBuildsFolder;
         if (!string.IsNullOrEmpty(sqlFolder))
         {
            if (Directory.Exists(sqlFolder))
            {
               try
               {
                  List<string> dirs = Utils.SortByDirectory(sqlFolder);
                  List<Tuple<string, List<string>>> builds = new List<Tuple<string, List<string>>>();
                  foreach (string dir in dirs)
                  {
                     string[] majorArr = dir.Split('.');
                     if (majorArr.Length > 1)
                     {
                        string major = $"{majorArr[0]}.{majorArr[1]}";
                        if (builds.Any(i=>i.Item1==major))
                        {
                           // update
                           var item = builds.Where(i => i.Item1 == major).ToList();
                           item[0].Item2.Add(dir);
                        }
                        else
                        {
                           builds.Add(new Tuple<string,List<string>>(major, new List<string>() { dir }));
                        }
                     }
                  }
                  
                  // add items
                  foreach (var item in builds)
                  {
                     // add major
                     BarSubItem menuItem = new BarSubItem(_BarManager, Path.GetFileName(item.Item1), 0);
                     _Menu.AddItem(menuItem);

                     // add release folders
                     List<string> files = Utils.SortByDirectory(sqlFolder);

                     foreach (string dir in item.Item2)
                     {
                        BarButtonItem subMenuItem = new BarButtonItem(_BarManager, Path.GetFileName(dir), 1);
                        subMenuItem.Tag = dir;
                        subMenuItem.ItemClick += MenuItemClick;
                        menuItem.AddItem(subMenuItem);
                     }
                  }
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to get subfolders of sqlfolder {0}", sqlFolder));
               }
            }
         }
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string path = e.Item.Tag.ToString(); //  Path.Combine(OptionsInstance.InternalBuildsFolder, e.Item.Caption);
            Frm_InternalBuilds f = new Frm_InternalBuilds();
            f.Show(path);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }
   }
}
