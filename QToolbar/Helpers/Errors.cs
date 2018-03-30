using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class Errors:List<Error>
   {

      public void AddError(string msg)
      {
         Add(new Error() { Message = msg, ErrorType = "Error" });
      }
      public void AddWarning(string msg)
      {
         Add(new Error() { Message = msg, ErrorType = "Warning" });
      }
   }
}
