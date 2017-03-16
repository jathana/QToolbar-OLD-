using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar
{
   public class ConnectionInfo
   {
      public string Version { get; set; }
      public string Environment { get; set; }
      public string Server { get; set; }
      public string Database { get; set; }
      public bool VersionNode { get; set; }
   }
}
