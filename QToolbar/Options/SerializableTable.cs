using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Options
{
   public class SerializableTable
   {
      private DataTable _Data = new DataTable("table");
      private string _SortExpression = "";

      public DataTable Data
      {
         get
         {
            return _Data;
         }

         set
         {
            _Data = value;
         }
      }

      public string SortExpression
      {
         get
         {
            return _SortExpression;
         }

         set
         {
            _SortExpression = value;
         }
      }

      public SerializableTable()
      {

      }
      protected virtual void Init(string[] columnNames, Type[] columnTypes, string sortExpression)
      {
         SortExpression = sortExpression;
         for (int i = 0; i < columnNames.Length; i++)
         {
            Data.Columns.Add(columnNames[i], columnTypes[i]);
         }
         
      }

      public SerializableTable(string[] columnNames, Type[] columnTypes)
      {

         Init(columnNames, columnTypes, "");
      }

      public SerializableTable(DataTable table)
      {
         _Data = table;
      }

      public void LoadXml(string xml)
      {
         if (!string.IsNullOrEmpty(xml))
         {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            Data.ReadXml(stream);
         }
      }

      public string ToXml()
      {

         string retval = "";
         using (var stream = new MemoryStream())
         {
            Resort();
            Data.WriteXml(stream);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            retval = sr.ReadToEnd();
         }
         return retval;
      }


      private void Resort()
      {
         if (!string.IsNullOrEmpty(SortExpression))
         {
            Data.DefaultView.Sort = SortExpression;
            Data = Data.DefaultView.ToTable();
         }
      }

   }
}
