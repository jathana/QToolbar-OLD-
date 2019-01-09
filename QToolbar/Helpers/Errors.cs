using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class Errors:List<Error>
   {

      public void AddError(string msg,string file, string hint="")
      {
         Add(new Error() { Message = msg, ErrorType = ErrorType.Error, Path = file, Hint = hint });
      }
      public void AddWarning(string msg, string file, string hint = "")
      {
         Add(new Error() { Message = msg, ErrorType = ErrorType.Warning, Path = file, Hint = hint });
      }

      public void AddInfo(string msg, string file, string hint = "")
      {
         Add(new Error() { Message = msg, ErrorType = ErrorType.Info, Path = file, Hint = hint });
      }

      public void AddSuccess(string msg, string file, string hint = "")
      {
         Add(new Error() { Message = msg, ErrorType = ErrorType.Success, Path = file, Hint = hint });
      }


      public string GetStrongestDesc()
      {
         string retval=string.Empty;

         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Success) != null) retval = $"{ ErrorType.Success.ToString()}";
         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Info) != null) retval = $"{ ErrorType.Info.ToString()}s";
         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Warning) != null) retval = $"{ ErrorType.Warning.ToString()}s";
         if (this.FirstOrDefault(i => i.ErrorType == ErrorType.Error) != null) retval = $"{ErrorType.Error.ToString()}s";

         return retval;
      }
   }
}
