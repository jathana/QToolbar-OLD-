using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{

    [Plugin(pluginName:"Environments Information", pluginDesc: @"Environments Information.")]
    public class EnvironmentsInfoPlugin : QPlugin, IQPlugin
   {
      public EnvironmentsInfoPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p= (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_Environments()
         {
            Text = p.PluginName,
            //Message = p.PluginDesc,
            //Action=InternalRun
         };
         f.Show();
            
         return retVal;
      }

      
   }
}
