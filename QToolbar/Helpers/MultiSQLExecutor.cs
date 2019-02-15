using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class MultiSqlExecutor
   {
      public class SQLStatement
      {
         public string Sql { get; set; }
         public string MappedTable { get; set; }
      }

      public MultiSqlExecutor()
      {
         _SqlStatements = new List<SQLStatement>();
      }

      #region fields
      private List<SQLStatement> _SqlStatements;


      #endregion
      #region properties
      public List<SQLStatement> SqlStatements
      {
         get
         {
            return _SqlStatements;
         }
      }
      #endregion

      #region public methods
      public void AddSql(string mappedTable, string sql)
      {
         _SqlStatements.Add(new SQLStatement()
         {
            Sql = sql,
            MappedTable = mappedTable
         });
      }

      public DataSet Execute(string connectionString)
      {
         DataSet retval = new DataSet();
         using (SqlConnection con = new SqlConnection(connectionString))
         {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            
            StringBuilder builder = new StringBuilder();
            SqlDataAdapter adapter = new SqlDataAdapter();
            int tableNo = 0;
            string tableStr = string.Empty;
            foreach (var item in _SqlStatements)
            {
               builder.AppendLine(item.Sql);
               tableStr = tableNo == 0 ? string.Empty : tableNo.ToString();
               adapter.TableMappings.Add($"Table{tableStr}", item.MappedTable);
               tableNo++;
            }
            try
            {
               con.Open();
               cmd.CommandText = builder.ToString();
               adapter.SelectCommand = cmd;
               adapter.Fill(retval);
            }
            catch(Exception ex)
            {
               throw new Exception($"error while running multi sql {connectionString}", ex);
            }
            finally
            {
               con.Close();
            }
         }
         return retval;
      }
      #endregion

   }
}
