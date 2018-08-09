using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar
{
   public static class StringExtensions
   {
      public static string RemoveAtEnd(this string value, string removeString)
      {
         string retval = value;
         if (!string.IsNullOrEmpty(value) && value.EndsWith(removeString))
         {
            retval = value.Substring(0, value.Length - removeString.Length);
         }
         return retval;
      }

   }
}
