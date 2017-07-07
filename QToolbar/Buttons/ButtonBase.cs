using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public delegate bool ShouldAddItem(BarItem item);


   public class ButtonBase
   {

      #region old

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
      /// <param name="folder"></param>
      /// <param name="barManager"></param>
      /// <param name="menu"></param>
      /// <param name="handler"></param>
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
                  List<string> dirs = new List<string>(Directory.EnumerateDirectories(_Folder));
                  dirs = dirs.OrderByDescending(s => s).ToList<string>();
                  foreach (string dir in dirs)
                  {
                     BarButtonItem menuItem = new BarButtonItem(_BarManager, Path.GetFileName(dir));
                     if(ShouldAddMenuItem(menuItem))
                     {
                        menuItem.ItemClick += MenuItemClick;
                        _Menu.AddItem(menuItem);
                     }
                  }
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to retrieve subfolders of  {0}", _Folder));
               }
            }
         }
      }

      #endregion

      #region new
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

      #endregion
   }
}
