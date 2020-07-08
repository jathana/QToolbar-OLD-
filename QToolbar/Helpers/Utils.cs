using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QToolbar
{
   public class Utils
   {
      private static object _Locker = new object();
      public const int FILE_PERMISSION_FULL_ACCESS = 2032127;

      #region String Manipulation
      /// <summary>
      /// 
      /// </summary>
      /// <param name="name">Version that includes at least Major[delimiters]Minor</param>
      /// <param name="delimiters">delimiters</param>
      /// <param name="joinDelimiter">What delimiter to use to join value</param>
      /// <param name="padChar"></param>
      /// <param name="padLength"></param>
      /// <param name="cutOffValue">The cut off version where less than this version noted as cutOff. Always as major.minor</param>
      /// <param name="nonNumericValue">non numeric value handled</param>
      /// <param name="cutOff">if is cut off value</param>
      /// <param name="applyToFirstOnly">applies to first only numeric part of version</param>
      /// <returns></returns>
      public static string GetSortName(string name, char[] delimiters, char joinDelimiter, char padChar, int padLength, bool applyToFirstOnly = false)
      {
         string result = name;
         bool nonNumericValue = false;

         if (!string.IsNullOrEmpty(name))
         {
            List<string> db = name.Split(delimiters).ToList();
            List<string> res = new List<string>();
            for (int i = 0; i < db.Count; i++)
            {
               if (db[i].All(Char.IsDigit))
               {
                  if (!applyToFirstOnly || (applyToFirstOnly && i == 0))
                  {
                     res.Add(db[i].PadLeft(padLength, padChar));
                  }
                  else
                  {
                     res.Add(db[i]);
                  }                  
               }
               else
               {
                  res.Add(db[i]);
                  nonNumericValue = true;
               }
            }
            if (nonNumericValue)  // sort them below all
            {
               result = $"__________{string.Join(joinDelimiter.ToString(), res)}";
            }
            else
            {
               result = string.Join(joinDelimiter.ToString(), res);
            }
         }
         return result;
      }

      #endregion
      #region IO


      public static List<string> SortByDirectory(string folder)
      {
         List<string> dirs = new List<string>(Directory.EnumerateDirectories(folder));
         List<Tuple<string, string>> x = new List<Tuple<string, string>>();

         foreach (var dir in dirs)
         {
            string dirName = Path.GetFileName(dir);
            string sortName = GetSortName(dirName, new Char[] { '\\', '.', ' ' }, '_', '0', 5);
            if(!string.IsNullOrEmpty(sortName))
            {
               x.Add(new Tuple<string, string>(sortName, dir));
            }
         }
         var result = x.OrderByDescending(item => item.Item1).Select(item => item.Item2).ToList();

         return result;
      }

      public static DataTable SortByDirectory(DataTable table, string pathColumn)
      {
         List<Tuple<string, DataRow>> x = new List<Tuple<string, DataRow>>();

         foreach (DataRow row in table.Rows)
         {
            x.Add(new Tuple<string, DataRow>(GetSortName(row.Field<string>(pathColumn), new Char[] { '\\', '.', ' ' }, '_', '0', 5), row));
         }

         DataTable result = table.Clone();
         var sorted = x.OrderByDescending(item => item.Item1);
         sorted.AsEnumerable().ToList().ForEach(item=> result.ImportRow(item.Item2));

         return result;
      }
            

      public static bool EnsureFolder(string dir)
      {
         bool retval = true;
         try
         {
            if (!Directory.Exists(dir))
            {
               Directory.CreateDirectory(dir);
            }
         }
         catch (Exception ex)
         {
            retval = false;
         }
         return retval;
      }

      public static string ReadFile(string file)
      {
         string retVal = "";
         if (File.Exists(file))
         {

            try
            {
               retVal = File.ReadAllText(file);
            }
            catch (Exception ex)
            {
               throw new Exception($"Cannot open file \"{file}\" ({ex.Message}).",ex);
            }
         }
         else
         {
            throw new Exception($"File not found \"{file}\".");
         }
         return retVal;
      }

      

      public static string GetPath(string uncPath, out int permissions, out bool unresolved)
      {
         permissions = -1;
         unresolved = false;
         lock (_Locker)
         {
            try
            {

               // remove the "\\" from the UNC path and split the path
               uncPath = uncPath.Replace(@"\\", "");
               string[] uncParts = uncPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
               if (uncParts.Length < 2)
               {
                  unresolved = true;
                  return "[UNRESOLVED UNC PATH: " + uncPath + "]";
               }
               // Get a connection to the server as found in the UNC path
               ManagementScope scope = new ManagementScope(@"\\" + uncParts[0] + @"\root\cimv2");
               // Query the server for the share name
               SelectQuery query = new SelectQuery("Select * From Win32_Share Where Name = '" + uncParts[1] + "'");
               //SelectQuery query = new SelectQuery("Select * From Win32_Share");
               string path = string.Empty;
               using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
               {
                  // Get the path
                  foreach (ManagementObject obj in searcher.Get())
                  {
                     if (obj != null && obj["path"] != null)
                        path = obj["path"].ToString();

                     try
                     {
                        //get the access values you have
                        ManagementBaseObject result = obj.InvokeMethod("GetAccessMask", null, null);

                        //value meanings: http://msdn.microsoft.com/en-us/library/aa390438(v=vs.85).aspx
                        permissions = Convert.ToInt32(result.Properties["ReturnValue"].Value);
                     }
                     catch (ManagementException me)
                     {
                        permissions = -1; //no permissions are set on the share
                     }
                  }
                  Debug.WriteLine(path);
                  // Append any additional folders to the local path name
                  if (uncParts.Length > 2)
                  {
                     for (int i = 2; i < uncParts.Length; i++)
                        path = path.EndsWith(@"\") ? path + uncParts[i] : path + @"\" + uncParts[i];
                  }
               }
               if(string.IsNullOrWhiteSpace(path))
               {
                  unresolved = true;
               }
               return path;
            }
            catch (Exception ex)
            {
               unresolved = true;
               return "[ERROR RESOLVING UNC PATH: " + uncPath + ": " + ex.Message + "]";
            }
         }
      }

      public static string GetPermissionsDesc(int permissions)
      {
            string retval = string.Empty;
            switch (permissions)
            {
               case -1:
                  retval = string.Empty;
                  break;
               case FILE_PERMISSION_FULL_ACCESS:
                  retval = "Full Access";
                  break;
               default:
                  retval = $"Limited Access ({permissions})";
                  break;
            }
            return retval;
      }

      #endregion

      /// <summary>
      /// constructs a version object from string. 
      /// </summary>
      /// <param name="versionStr"></param>
      /// <param name="delimiter"></param>
      /// <param name="start">How many delimiters should be skipped to get version major.</param>
      /// <returns></returns>
      public static Version GetVersion(string versionStr, string delimiter, int start)
      {
         Version retval = null;
         try
         {
            if (!string.IsNullOrEmpty(versionStr))
            {
               string[] splitted = versionStr.Split(new string[] { delimiter }, StringSplitOptions.None);
               if (splitted.Length == 5)
               {
                  StringBuilder b = new StringBuilder(splitted[start]);
                  for (int i = start + 1; i < start + 4; i++)
                  {
                     b.Append(".");
                     b.Append(splitted[i]);
                  }
                  retval = new Version(b.ToString());
               }
            }
         }
         catch { }
         return retval;
      }

      //public static Dictionary<string,string> GetSectionItems(string section)
      //{

      //   Dictionary<string, string> retval = new Dictionary<string, string>();
      //   Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
      //   ConfigurationSection foldersSection = config.GetSection(section);
      //   XmlDocument doc = new XmlDocument();
      //   doc.LoadXml(foldersSection.SectionInformation.GetRawXml());
      //   foreach (XmlNode child in doc.ChildNodes[0].ChildNodes)
      //   {
      //      if (child.Attributes.GetNamedItem("key") != null && child.Attributes.GetNamedItem("value") != null)
      //      {
      //         retval.Add(child.Attributes["key"].Value, child.Attributes["value"].Value);
      //      }
      //   }
      //   return retval;
      //}


      public static Encoding GetEncoding(string srcFile)
      {
         // *** Use Default of Encoding.Default (Ansi CodePage)
         Encoding enc = Encoding.Default;

         // *** Detect byte order mark if any - otherwise assume default
         byte[] buffer = new byte[5];
         using (FileStream file = new FileStream(srcFile, FileMode.Open))
         {
            file.Read(buffer, 0, 5);

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
               enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
               enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
               enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
               enc = Encoding.UTF7;
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
               // 1201 unicodeFFFE Unicode (Big-Endian)
               enc = Encoding.GetEncoding(1201);
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
               // 1200 utf-16 Unicode
               enc = Encoding.GetEncoding(1200);
         }

         return enc;
      }


      #region database
      public static string GetConnectionString(string server, string database)
      {
         return $"Server={server};Database={database};Integrated Security=SSPI;";
      }


      #endregion
      #region sql

      public static Tuple<string, int>[] GetSQLKeywords()
      {
         return new Tuple<string, int>[]
         {
            new Tuple<string,int>("CUME_DIST",2012),
            new Tuple<string,int>("FIRST_VALUE",2012),
            new Tuple<string,int>("LAG",2012),
            new Tuple<string,int>("LAST_VALUE",2012),
            new Tuple<string,int>("LEAD",2012),
            new Tuple<string,int>("PERCENTILE_CONT",2012),
            new Tuple<string,int>("PERCENTILE_DISC",2012),
            new Tuple<string,int>("PERCENT_RANK",2012),
            new Tuple<string,int>("PARSE",2012),
            new Tuple<string,int>("TRY_CAST",2012),
            new Tuple<string,int>("TRY_CONVERT",2012),
            new Tuple<string,int>("TRY_PARSE",2012),
            new Tuple<string,int>("DATEDIFF_BIG",2016),
            new Tuple<string,int>("DATEFROMPARTS",2012),
            new Tuple<string,int>("DATETIME2FROMPARTS",2012),
            new Tuple<string,int>("DATETIMEFROMPARTS",2012),
            new Tuple<string,int>("DATETIMEOFFSETFROMPARTS",2012),
            new Tuple<string,int>("EOMONTH",2012),
            new Tuple<string,int>("SMALLDATETIMEFROMPARTS",2012),
            new Tuple<string,int>("TIMEFROMPARTS",2012),
            new Tuple<string,int>("ISJSON",2016),
            new Tuple<string,int>("JSON_VALUE",2016),
            new Tuple<string,int>("JSON_QUERY",2016),
            new Tuple<string,int>("JSON_MODIFY",2016),
            new Tuple<string,int>("CHOOSE",2012),
            new Tuple<string,int>("IIF",2012),
            new Tuple<string,int>("NEXT VALUE FOR",2012),
            new Tuple<string,int>("PARSENAME",2012),
            new Tuple<string,int>("OPENJSON",2016),
            new Tuple<string,int>("CERTENCODED",2012),
            new Tuple<string,int>("CERTPRIVATEKEY",2012),
            new Tuple<string,int>("IS_ROLEMEMBER",2012),
            new Tuple<string,int>("CONCAT",2012),
            new Tuple<string,int>("CONCAT_WS",2012),
            new Tuple<string,int>("FORMAT",2012),
            new Tuple<string,int>("STRING_AGG",2017),
            new Tuple<string,int>("STRING_ESCAPE",2016),
            new Tuple<string,int>("STRING_SPLIT",2016),
            new Tuple<string,int>("TRANSLATE",2017),
            new Tuple<string,int>("TRIM",2017),
            new Tuple<string,int>("COMPRESS",2016),
            new Tuple<string,int>("CURRENT_TRANSACTION_ID",2016),
            new Tuple<string,int>("DECOMPRESS",2016),
            new Tuple<string,int>("HOST_NAME",2016),
            new Tuple<string,int>("SESSION_CONTEXT",2016)

         };
      }

      #endregion

      #region private
      #endregion
   }
}
