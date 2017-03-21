using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar
{
   public enum InfoType
   {
      Database,
      Server,
      Version
   }

   public class ConnectionInfo
   {
      public string CFPath { get; set; }
      public string Environment { get; set; }
      public string Server { get; set; }
      public string Database { get; set; }
      public string Version
      {
         get
         {
            string retVal = "";
            if(!string.IsNullOrWhiteSpace(CFPath))
            {
               retVal = Path.GetFileName(CFPath);
            }
            return retVal;
         }
      }
      public InfoType InfoType { get; set; }
   }


}
