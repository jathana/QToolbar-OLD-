using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            
            foreach(var key in keysToCheck)
            {
               // check key existence in dbs
               if (dbs.Count(i => i.Item1 == key) == 0)
                  retVal.AddError($"Key {key} does not exist in [DatabaseName] section",cfFile );

               // check key existence in servers
               if (servers.Count(i => i.Item1 == key) == 0)
                  retVal.AddError($"Key {key} does not exist in [Servers] section",cfFile);

               // check key existence in passwords
               if (pwds.Count(i => i.Item1 == key) == 0)
                  retVal.AddError($"Key {key} does not exist in [Passwords] section",cfFile);
            }


            // check that keys in servers are unique
            var duplSrvs = servers.GroupBy(x => x.Item1).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (duplSrvs != null)
            {
               foreach (var srv in duplSrvs)
               {
                  retVal.AddError($"Duplicate found {srv} in [Servers] section", cfFile);
               }
            }
            // check that keys in dbs are unique
            var duplDbs = dbs.GroupBy(x => x.Item1).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (duplDbs != null)
            {
               foreach(var db in duplDbs)
               {
                  retVal.AddError($"Duplicate found {db} in [DatabaseName] section", cfFile);
               }
            }
            // check that keys in passwords are unique
            var duplPwds = pwds.GroupBy(x => x.Item1).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (duplPwds != null)
            {
               foreach (var db in duplPwds)
               {
                  retVal.AddError($"Duplicate found {db} in [Passwords] section", cfFile);
               }
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
               retVal.AddError($"{item.Key} found {item.Value} times instead of 3.", cfFile);
            }

            // for each database check key and value
            foreach (var item in dbs)
            {
               if (!IsInCheckList(item.Item1, keysToCheck)) continue;

               Regex reg = new Regex("[A-Za-z_]+[_]*(?<major>[0-9]*)[_]*(?<minor>[0-9]*)[_]*(?<release>[0-9]*)");
               string keyMatch = string.Empty;
               string valueMatch = string.Empty;
               if (reg.IsMatch(item.Item1))
               {
                  Match match = reg.Match(item.Item1);
                  keyMatch = $"{match.Groups["major"].Value}_{match.Groups["minor"].Value}_{match.Groups["release"].Value}";
               }
               if (reg.IsMatch(item.Item2))
               {
                  Match match = reg.Match(item.Item2);
                  valueMatch = $"{match.Groups["major"].Value}_{match.Groups["minor"].Value}_{match.Groups["release"].Value}";
               }

               if(!keyMatch.Equals(valueMatch))
               {
                  retVal.AddWarning($"Check database key \"{item.Item1}\" against db name \"{item.Item2}\"", cfFile);
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
