using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class SharedDirsHelper:List<SharedDirsHelper.SharedDir>
   {
      public class SharedDir
      {
         public string UNC { get; set; }
         public string LocalDir { get; set; }
      }

      public string GetPath(string uncPath)
      {
         try
         {
            // remove the "\\" from the UNC path and split the path
            uncPath = uncPath.Replace(@"\\", "");
            string[] uncParts = uncPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (uncParts.Length < 2)
               return "[UNRESOLVED UNC PATH: " + uncPath + "]";
            // Get a connection to the server as found in the UNC path
            ManagementScope scope = new ManagementScope(@"\\" + uncParts[0] + @"\root\cimv2");
            // Query the server for the share name
            SelectQuery query = new SelectQuery("Select * From Win32_Share");
            //SelectQuery query = new SelectQuery("Select * From Win32_Share");
            string path = string.Empty;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
               // Get the path
               foreach (ManagementObject obj in searcher.Get())
               {

                  path = obj["path"].ToString();
               }
               //Debug.WriteLine(path);
               // Append any additional folders to the local path name
               if (uncParts.Length > 2)
               {
                  for (int i = 2; i < uncParts.Length; i++)
                     path = path.EndsWith(@"\") ? path + uncParts[i] : path + @"\" + uncParts[i];
               }
            }
            return path;
         }
         catch (Exception ex)
         {
            return "[ERROR RESOLVING UNC PATH: " + uncPath + ": " + ex.Message + "]";
         }
      }

   }
}
