using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class Checkouts : SerializableTable
   {
      private string[] columns = new string[] { "Name", "Path", "ProteusPath" };
      private Type[] types = new Type[] { typeof(string), typeof(string), typeof(string) };

      public Checkouts()
      {
         Init(columns, types, columns[0]);
      }

      public Checkouts(DataTable table) : base(table)
      {
         SortExpression = columns[0];
      }

      public Checkouts(string xml) 
      {
         Init(columns, types, columns[0]);
         LoadXml(xml);
      }

   }
}
