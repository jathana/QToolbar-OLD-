using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class Databases : SerializableTable
   {
      private string[] columns = new string[] { "Server", "DBName", "User", "Password" };
      private Type[] types = new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) };

      public Databases()
      {
         Init(columns, types, columns[0]);
      }

      public Databases(DataTable table) : base(table)
      {
         SortExpression = columns[0];
      }

      public Databases(string xml) 
      {
         Init(columns, types, columns[0]);
         LoadXml(xml);
      }

   }
}
