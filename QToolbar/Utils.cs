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
   }
}
