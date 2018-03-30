using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class CfValidator
   {

      public Errors Validate(string cfFile, List<string> keysToCheck)
      {
         Errors retVal = new Errors();

         // check if  sections Servers,DatabaseName and Passwords contains the same keys
         if (File.Exists(cfFile))
         {
            CfFile cfObj = new CfFile(cfFile);
            List<Tuple<string, string>> dbs = cfObj.GetDatabases();
            List<Tuple<string, string>> servers = cfObj.GetServers();
            List<Tuple<string, string>> pwds = cfObj.GetPasswords();

            // 
            foreach(var key in keysToCheck)
            {

               // check key existence in dbs
               if (dbs.Where(i => i.Item1 == key) == null)
                  retVal.AddError($"Key {key} does not exist in [DatabaseName] section of {cfFile}");

               // check key existence in servers
               if (servers.Where(i => i.Item1 == key) == null)
                  retVal.AddError($"Key {key} does not exist in [Servers] section of {cfFile}");

               // check key existence in dbs
               if (pwds.Where(i => i.Item1 == key) == null)
                  retVal.AddError($"Key {key} does not exist in [Passwords] section of {cfFile}");

            }

            Dictionary<string, int> info = new Dictionary<string, int>();
            foreach (var item in dbs)
            {
               if (!IsInCheckList(item.Item1, keysToCheck)) continue;

               if (info.ContainsKey(item.Item1))
                  info[item.Item1]++;
               else
                  info.Add(item.Item1, 1);
            }
            foreach (var item in servers)
            {
               if (!IsInCheckList(item.Item1, keysToCheck)) continue;

               if (info.ContainsKey(item.Item1))
                  info[item.Item1]++;
               else
                  info.Add(item.Item1, 1);
            }
            foreach (var item in pwds)
            {
               if (!IsInCheckList(item.Item1, keysToCheck)) continue;

               if (info.ContainsKey(item.Item1))
                  info[item.Item1]++;
               else
                  info.Add(item.Item1, 1);
            }

            var lst = info.Where(i => i.Value != 3);
            foreach (var item in lst)
            {
               retVal.AddError($"file {cfFile} : {item.Key} found {item.Value} times instead of 3.");
            }

            // for each database check key and value
            foreach (var item in dbs)
            {
               if (!IsInCheckList(item.Item1, keysToCheck)) continue;

               if (!item.Item2.Contains(item.Item1))
               {
                  retVal.AddWarning($"file {cfFile} : {item.Item1} is not contained in {item.Item2}");
               }
            }

            


         }
         return retVal;
      }


      private bool IsInCheckList(string key, List<string> keysToCheck)
      {

         return (keysToCheck.Count==0)  ||
                (keysToCheck != null && keysToCheck.Count > 0 && keysToCheck.Contains(key));
      }

      

   }
}
