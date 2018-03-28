using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar
{
   public static class XmlNodeExtensions
   {
      public static string ReadString(this XmlNode node, string attribute, string defaultValue)
      {
         string retval = defaultValue;
         if(!string.IsNullOrEmpty(attribute) && node.Attributes[attribute]!=null)
         {
            retval = node.Attributes[attribute].Value;
         }
         return retval;
      }

      public static string ReadString(this XmlNode node, string attribute)
      {
         return ReadString(node, attribute, "");
      }


      public static T ReadEnum<T>(this XmlNode node, string attribute, T defaultValue)
      {
         T retval = defaultValue;
         if (!string.IsNullOrEmpty(attribute) && node.Attributes[attribute] != null)
         {
            retval = (T)Enum.Parse(typeof(T), node.Attributes[attribute].Value);
         }
         return retval;
      }

      public static T ReadEnum<T>(this XmlNode node, string attribute)
      {
         return ReadEnum(node, attribute, (T)Enum.GetValues(typeof(T)).GetValue(0));
      }

      public static bool ReadBool(this XmlNode node, string attribute, bool defaultValue)
      {
         bool retval = defaultValue;
         if (!string.IsNullOrEmpty(attribute) && node.Attributes[attribute] != null)
         {
            retval = Convert.ToBoolean(node.Attributes[attribute].Value);
         }
         return retval;
      }

      public static bool ReadBool(this XmlNode node, string attribute)
      {
         return ReadBool(node, attribute, false);
      }

      public static int ReadInt(this XmlNode node, string attribute, int defaultValue)
      {
         int retval = defaultValue;
         if (!string.IsNullOrEmpty(attribute) && node.Attributes[attribute] != null)
         {
            int tryValue;
            if(Int32.TryParse(node.Attributes[attribute].Value, out tryValue))
            {
               retval = tryValue;
            }
         }
         return retval;
      }


      public static string ReadChildInnerTextString(this XmlNode node, string childElemName, string defaultValue)
      {
         string retval = defaultValue;
         XmlNode child = node.SelectSingleNode(childElemName);
         if (!string.IsNullOrEmpty(childElemName) && child != null)
         {
            retval = child.InnerText;
         }
         return retval;
      }

      public static string ReadChildInnerTextString(this XmlNode node, string childElemName)
      {
         return ReadChildInnerTextString(node, childElemName,"");
      }

   }
}
