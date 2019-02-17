using QToolbar.Plugins.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
   public class EnvInfoEventArgs : EventArgs
   {
      public  QEnv Environment { get; set; }

      public EnvInfoEventArgs(QEnv env)
      {
         Environment = env;
      }
   }
}
