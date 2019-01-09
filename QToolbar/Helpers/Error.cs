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
      private ErrorType _ErrorType;
      private string _Message;
      private string _Path;
      private string _Hint;
      #endregion

      #region properties
      public ErrorType ErrorType
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

      public string Path
      {
         get
         {
            return _Path;
         }

         set
         {
            _Path = value;
         }
      }

      public string Hint
      {
         get
         {
            return _Hint;
         }

         set
         {
            _Hint = value;
         }
      }

      #endregion
   }
}
