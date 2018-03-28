using QToolbar.Plugins.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class EnvInfoEventArgs : EventArgs
   {
      public  QEnvironment Environment { get; set; }

      public EnvInfoEventArgs(QEnvironment env)
      {
         Environment = env;
      }
   }
}
