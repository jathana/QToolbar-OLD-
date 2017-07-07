using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar
{
   public class NextBuildFile
   {
      public string File { get; set; }
      public string Message { get; set; }
      public string Tag { get; set; }

      public NextBuildFile(string file, string message, string tag)
      {
         File = file;
         Message = message;
         Tag = tag;
      }

      public override string ToString()
      {
         return Message;
      }
   }
}
