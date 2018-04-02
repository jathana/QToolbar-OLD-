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
         Add(new Error() { Message = msg, ErrorType = ErrorType.Error });
      }
      public void AddWarning(string msg)
      {
         Add(new Error() { Message = msg, ErrorType = ErrorType.Warning });
      }


      public string GetStrongestDesc()
      {
         string retval=string.Empty;

         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Warning) != null) retval = $"{ ErrorType.Warning.ToString()}s";
         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Error) != null) retval = $"{ErrorType.Error.ToString()}s";

         return retval;
      }
   }
}
