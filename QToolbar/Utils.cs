using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QToolbar
{
   public static class Utils
   {

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

         if (!string.IsNullOrEmpty(versionStr))
         {
            string[] splitted = versionStr.Split(new string[] { delimiter }, StringSplitOptions.None);
            if(splitted.Length==5)
            {
               StringBuilder b = new StringBuilder(splitted[start]); 
               for(int i = start+1; i < start + 4; i++)
               {
                  b.Append(".");
                  b.Append(splitted[i]);
               }
               retval = new Version(b.ToString());
            }
         }
         return retval;
      }

      public static Dictionary<string,string> GetSectionItems(string section)
      {

         Dictionary<string, string> retval = new Dictionary<string, string>();
         Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
         ConfigurationSection foldersSection = config.GetSection(section);
         XmlDocument doc = new XmlDocument();
         doc.LoadXml(foldersSection.SectionInformation.GetRawXml());
         foreach (XmlNode child in doc.ChildNodes[0].ChildNodes)
         {
            if (child.Attributes.GetNamedItem("key") != null && child.Attributes.GetNamedItem("value") != null)
            {
               retval.Add(child.Attributes["key"].Value, child.Attributes["value"].Value);
            }
         }
         return retval;
      }


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
      
   }
}
