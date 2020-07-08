using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public delegate bool ShouldAddItem(BarItem item);


   public class ButtonBase
   {
      protected BarManager _BarManager;
      protected BarSubItem _Menu;
      protected string _Folder;

      public ButtonBase(string folder, BarManager barManager, BarSubItem menu)
      {
         _Menu = menu;
         _BarManager = barManager;
         _Folder = folder;
      }


      /// <summary>
      /// The default menu items creation assumes click once applications. 
      /// </summary>
      public virtual void CreateMenuItems()
      {
         // clear links
         _Menu.ClearLinks();
         
         // add items
         if (!string.IsNullOrWhiteSpace(_Folder))
         {
            if (Directory.Exists(_Folder))
            {
               try
               {
                  var  dirs = Utils.SortByDirectory(_Folder);

                  BarSubItem cutOffMenu = new BarSubItem(_BarManager, "Other", 0);
                  
                  for(int i = 0; i < dirs.Count; i++)
                  {

                     BarButtonItem menuItem = new BarButtonItem(_BarManager, Path.GetFileName(dirs[i]));
                     if (ShouldAddMenuItem(menuItem))
                     {
                        menuItem.ItemClick += MenuItemClick;
                        if (i >= Options.OptionsInstance.MaxMenuItems)
                        {
                           cutOffMenu.AddItem(menuItem);
                        }
                        else
                        {
                           _Menu.AddItem(menuItem);
                        }
                     }
                  }
                  if(cutOffMenu.ItemLinks.Count>0)
                  {
                     _Menu.AddItem(cutOffMenu);
                  }
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to retrieve subfolders of  {0}", _Folder));
               }
            }
         }
      }


      /// <summary>
      /// Answers if an item should be added to menu.
      /// </summary>
      /// <returns></returns>
      protected virtual bool ShouldAddMenuItem(BarButtonItem menuItem)
      {
         return true;
      }

      protected virtual void MenuItemClick(object sender, ItemClickEventArgs e)
      {

      }
   }
}
