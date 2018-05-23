using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class ValidationHelper
   {
      #region IO
      public void CheckFolderExistence(string path, Errors errors, string file, string messageSource)
      {
         if (!Directory.Exists(path))
         {
            errors.AddError($"{messageSource}.Folder not found \"{path}\"", file);
         }
      }

      public void CheckFileExistence(string path, Errors errors, string file, string messageSource)
      {
         if (!File.Exists(path))
         {
            errors.AddError($"{messageSource}.File not found \"{path}\"", file);
         }
      }

      public void CheckIfServerIsAccessible(string serverName, Errors errors, string file, string messageSource)
      {
         if (string.IsNullOrEmpty(serverName))
         {
            errors.AddError($"{messageSource}.UNC is empty", file);
         }
         else
         {
            // remove backslashes if unc like \\server-name
            serverName = serverName.Replace("\\\\", "");

            try
            {
               // If the server is available, format the network path properly to use it.
               if (Dns.GetHostEntry(serverName) == null)
               {
                  errors.AddError($"{messageSource}.UNC is not avalable {serverName}", file);
               }
            }
            // Eat any Host Not Found exceptions for if we can't connect to the server.
            catch (System.Net.Sockets.SocketException ex)
            {
               errors.AddError($"{messageSource}.UNC is not avalable {serverName} ({ex.Message}) ", file);
            }
         }
      }
      #endregion

      #region DB
      public void CheckDBConnection(string server, string db, Errors errors, string file, string messageSource)
      {
         using (SqlConnection con = new SqlConnection())
         {
            con.ConnectionString = Utils.GetConnectionString(server, db);

            try
            {
               con.Open();
               SqlCommand com = new SqlCommand();
               com.Connection = con;

               com.CommandText = "SELECT 1";
               com.ExecuteScalar();
            }
            catch (Exception ex)
            {
               errors.AddError($"{messageSource}.Error while connecting to {server}.{db} ({ex.Message})", file);
            }
            finally
            {
               if (con.State == ConnectionState.Open)
               {
                  con.Close();
               }
            }
         }
      }
      #endregion
   }
}
