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
   public class ButtonBase
   {
      protected BarManager _BarManager;
      protected BarSubItem _Menu;
      protected ItemClickEventHandler _ItemClickHandler;
      protected string _Folder;


      public ButtonBase(string folder, BarManager barManager, BarSubItem menu, ItemClickEventHandler handler)
      {
         _Menu = menu;
         _BarManager = barManager;
         _ItemClickHandler = handler;
         _Folder = folder;
      }

      /// <summary>
      /// Used mostly for click once applications
      /// </summary>
      /// <param name="folder"></param>
      /// <param name="barManager"></param>
      /// <param name="menu"></param>
      /// <param name="handler"></param>
      protected void CreateClickOnce()
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
                     menuItem.ItemClick += _ItemClickHandler;
                     _Menu.AddItem(menuItem);
                  }
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to retrieve subfolders of  {0}", _Folder));
               }
            }
         }
      }
   }
}
