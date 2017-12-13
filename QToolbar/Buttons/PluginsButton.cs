using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using QToolbar.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QToolbar.Buttons
{
   public class PluginsButton : ButtonBase
   {
      public PluginsButton(BarManager barManager, BarSubItem menu):base(OptionsInstance.DesignersFolder, barManager, menu)
      {
      }


      public override void CreateMenuItems()
      {
         _Menu.ClearLinks();

         Type plugin = typeof(IQPlugin);
         IEnumerable<Type> plugins = GetType().Assembly.GetTypes().Where(
                 t => plugin.IsAssignableFrom(t) && !t.IsInterface);
         StringBuilder s = new StringBuilder();
         foreach(var p in plugins)
         {

            PluginAttribute attr = (PluginAttribute)p.GetCustomAttributes(false).Where(t=>t is PluginAttribute).FirstOrDefault();
           
            BarButtonItem menuItem = new BarButtonItem(_BarManager, "<b>"+attr.PluginName +"</b><br>" + attr.PluginDesc, 4);
            menuItem.AllowHtmlText=DefaultBoolean.True;
            menuItem.ItemInMenuAppearance.SetWordWrap(WordWrap.Wrap);
            menuItem.Tag = p;
            menuItem.ItemClick += MenuItemClick;
            _Menu.AddItem(menuItem);
         }

      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            
            var o= Activator.CreateInstance((Type)e.Item.Tag);

            IQPlugin p= (IQPlugin)o;
            p.Run();
         }

         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }



   }
}
