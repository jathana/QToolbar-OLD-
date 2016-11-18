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
      private string[] columns = new string[] { "Name", "Path" };
      private Type[] types = new Type[] { typeof(string), typeof(string) };

      public Checkouts()
      {
         Init(columns, types);
      }

      public Checkouts(DataTable table) : base(table)
      {

      }

      public Checkouts(string xml) 
      {
         Init(columns, types);
         LoadXml(xml);
      }

   }
}
