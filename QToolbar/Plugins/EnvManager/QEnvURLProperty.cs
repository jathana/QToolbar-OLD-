using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
   public class QEnvURLProperty: QEnvProperty
   {
      public string Host { get; set; }
      public string Port { get; set; }

      protected override void OnValueSet(string oldValue, string newValue)
      {
         if (!EmptyValue)
         {
            Uri uri = new Uri(newValue);
            Host = uri.Host;
            Port = uri.Port.ToString();
         }
      }
   }
}
