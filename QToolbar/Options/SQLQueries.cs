using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class SQLQueries : SerializableTable
   {
      private string[] columns = new string[] { "Name", "SQL", "RunImmediate" };
      private Type[] types = new Type[] { typeof(string), typeof(string), typeof(bool) };

      public SQLQueries()
      {
         Init(columns, types, columns[0]);
      }

      public SQLQueries(DataTable table) : base(table)
      {
         SortExpression = columns[0];
      }

      public SQLQueries(string xml) 
      {
         Init(columns, types, columns[0]);
         LoadXml(xml);
      }

   }
}
