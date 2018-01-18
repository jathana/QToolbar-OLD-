using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class EnvCFs : SerializableTable
   {
      private string[] columns = new string[] { "Repository","Path" };
      private Type[] types = new Type[] { typeof(string), typeof(string) };

      public EnvCFs()
      {
         Init(columns, types, columns[0]);
      }

      public EnvCFs(DataTable table) : base(table)
      {
         SortExpression = columns[0];
      }

      public EnvCFs(string xml) 
      {
         Init(columns, types, columns[0]);
         LoadXml(xml);
      }

   }
}
