using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar
{
   public static class DateTimeExtensions
   {
      public static string TimeStamp(this DateTime value)
      {
         return value.ToString("yyyyMMddHHmmssffff");
      }

   }
}
