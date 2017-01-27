using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class ShellCommands : SerializableTable
   {
      private string[] columns = new string[] { "Name", "Command", "Arguments" };
      private Type[] types = new Type[] { typeof(string), typeof(string), typeof(string) };

      public ShellCommands()
      {
         Init(columns, types, columns[0]);
      }

      public ShellCommands(DataTable table) : base(table)
      {
         SortExpression = columns[0];
      }

      public ShellCommands(string xml) 
      {
         Init(columns, types, columns[0]);
         LoadXml(xml);
      }

   }
}
