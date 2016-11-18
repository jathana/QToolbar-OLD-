using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class SerializableKeyValue: SerializableTable
   {
      public SerializableKeyValue(): base(new string[] { "key", "value" }, new Type[] { typeof(string), typeof(string) })
      {
      }

   }
}
