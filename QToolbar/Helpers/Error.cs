using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class Error
   {
      #region fields
      private string _ErrorType;
      private string _Message;
      #endregion

      #region properties
      public string ErrorType
      {
         get
         {
            return _ErrorType;
         }

         set
         {
            _ErrorType = value;
         }
      }
      public string Message
      {
         get
         {
            return _Message;
         }

         set
         {
            _Message = value;
         }
      }

      #endregion
   }
}
