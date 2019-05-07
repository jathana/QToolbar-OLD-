using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{

   [Plugin(pluginName:"Environments Manager", pluginDesc: @"Environments Manager.")]
   public class EnvManagerPlugin : QPlugin, IQPlugin
   {
      public EnvManagerPlugin() { }

      public bool Run()
      {

         bool retVal = true;
        // allow only from ide until finish development
        if (Debugger.IsAttached)
        {
            PluginAttribute p = (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
            var f = new Frm_Environments()
            {
                Text = p.PluginName,
                //Message = p.PluginDesc,
                //Action=InternalRun
            };
            f.Show();
        }
         return retVal;
      }

      
   }
}
