using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.CFsValidator
{
   public class CfFileValidationEventArgs:EventArgs
   {
      public string Version { get; set; }
      public string Source { get; set; }
      public string Repository { get; set; }
      public string CfFile { get; set; }

      public CfFileValidationEventArgs(string version, string source, string repository, string cfFile)
      {
         Version = version;
         Source = source;
         Repository = repository;
         CfFile = cfFile;
      }
   }
}
