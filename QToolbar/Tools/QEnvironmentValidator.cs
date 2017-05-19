using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Tools
{
   public class QEnvironmentValidator
   {
      private QEnvironment _Env;

      public List<string> Validate(QEnvironment env)
      {
         List<string> retval = new List<string>();

         _Env = env;

         // check server and qc db connection
         AddMessage(retval, CheckServerAndDb(_Env.Server, _Env.Database));

         // check server and nightly db connection
         AddMessage(retval, CheckServerAndDb(_Env.NightlyBuildDBServer, _Env.NightlyBuildDB));



         return retval;
      }

      private void AddMessage(List<string> msgs, string msg)
      {
         if(!string.IsNullOrEmpty(msg))
         {
            msgs.Add(msg);
         }

      }

      private string CheckServerAndDb(string server, string db)
      {
         string retval = "";
         using (SqlConnection con = new SqlConnection())
         {
            try
            {
               con.ConnectionString = Utils.GetConnectionString(server, db);
               SqlCommand com = new SqlCommand("SELECT GETDATE()");
               com.ExecuteScalar();
               com.Dispose();               
            }
            catch(Exception ex)
            {
               retval = $"Failed to connect to {server}.{db} ({ex.Message}).";
            }

         };
         return retval;

      }

      private string CheckService(string batchServicePath)
      {
         // check path ex
         string retval = "";
         //using (SqlConnection con = new SqlConnection())
         //{
         //   try
         //   {
         //      con.ConnectionString = Utils.GetConnectionString(server, db);
         //      SqlCommand com = new SqlCommand("SELECT GETDATE()");
         //      com.ExecuteScalar();
         //   }
         //   catch (Exception ex)
         //   {
         //      retval = $"Failed to connect to {server}.{db} ({ex.Message}).";
         //   }

         //};
         return retval;

      }


   }
}
