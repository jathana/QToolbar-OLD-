using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins
{

   [AttributeUsage(AttributeTargets.Class)]
   public class PluginAttribute : Attribute
   {
      // Private fields.
      private string _PluginName;
      private string _PluginDesc;

      // This constructor defines two required parameters: name and level.

      public PluginAttribute(string pluginName, string pluginDesc)
      {
         _PluginName = pluginName;
         _PluginDesc = pluginDesc;
      }

      // Define Name property.
      // This is a read-only attribute.

      public virtual string PluginName
      {
         get { return _PluginName; }
      }
      public virtual string PluginDesc
      {
         get { return _PluginDesc; }
      }

   }

}
