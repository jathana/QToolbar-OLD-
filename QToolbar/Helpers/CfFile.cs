using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class CfFile
   {
      private string _File;

      public string File
      {
         get
         {
            return _File;
         }

         set
         {
            _File = value;
         }
      }

      public CfFile(string cfFile)
      {
         _File = cfFile;
      }

      public string GetValue(string section, string key)
      {
         return IniFile2.ReadValue(section, key, _File);
      }

      public List<Tuple<string, string>> GetServers()
      {
         return FromKeyValuePairs(IniFile2.ReadKeyValuePairs("Servers", _File));
      }

      public string GetServer(string key)
      {
         return IniFile2.ReadValue("Servers", key, _File);
      }

      public List<Tuple<string, string>> GetDatabases()
      {
         return FromKeyValuePairs(IniFile2.ReadKeyValuePairs("DatabaseName", _File));
      }

      public string GetDatabase(string key)
      {
         return IniFile2.ReadValue("DatabaseName", key, _File);
      }

      public List<Tuple<string, string>> GetPasswords()
      {
         return FromKeyValuePairs(IniFile2.ReadKeyValuePairs("Passwords", _File));
      }

      public string GetPassword(string key)
      {
         return IniFile2.ReadValue("Passwords", key, _File);
      }

      public List<string> GetKeys()
      {
         return IniFile2.ReadKeys("Servers", _File).ToList<string>();
      }

      public List<string> GetUniqueKeys()
        {
            List<string> ret = new List<string>();
            ret.AddRange(IniFile2.ReadKeys("Servers", _File).ToList<string>());
            ret.AddRange(IniFile2.ReadKeys("DatabaseName", _File).ToList<string>());
            ret.AddRange(IniFile2.ReadKeys("Passwords", _File).ToList<string>());
            return ret.Distinct().ToList();
        }

      public List<string> GetKeys(string server, string database)
      {
         List<string> retVal = new List<string>();
         var dbs = GetDatabases().Where(i=>i.Item2==database);
         foreach(var item in dbs)
         {
            var srv = GetServer(item.Item1);
            if (!string.IsNullOrEmpty(srv))
               retVal.Add(item.Item1);
         }
         return retVal;
      }

      private List<Tuple<string, string>> FromKeyValuePairs(string[] keyValuePairs)
      {
         List<Tuple<string, string>> list = new List<Tuple<string, string>>();

         if (keyValuePairs != null)
         {
            foreach(string kvp in keyValuePairs)
            {
               string[] v=kvp.Split('=');
               if(v!=null)
               {
                  if (v.Length == 2)
                  {
                     list.Add(new Tuple<string, string>(v[0], v[1]));
                  }
                  else
                     throw new Exception($"Invalid key value pair {kvp}, file:{_File}.");
               }
                  
            }
         }
         return list;
      }



   }
}
