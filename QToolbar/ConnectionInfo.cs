using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar
{
   public enum InfoType
   {
      Database,
      Server,
      Version,
      None
   }

   public class ConnectionInfo:IState
   {
      // fields


      private string _CFPath = "";
      private string _Environment = "";
      private string _Server = "";
      private string _Database = "";
      private InfoType _InfoType = InfoType.None;

      public string Version
      {
         get
         {
            string retVal = "";
            if (!string.IsNullOrWhiteSpace(CFPath))
            {
               retVal = Path.GetFileName(CFPath);
            }
            return retVal;
         }
      }

      public string Environment
      {
         get
         {
            return _Environment;
         }

         set
         {
            _Environment = value;
         }
      }

      public string Server
      {
         get
         {
            return _Server;
         }

         set
         {
            _Server = value;
         }
      }

      public string Database
      {
         get
         {
            return _Database;
         }

         set
         {
            _Database = value;
         }
      }

      public string CFPath { get { return _CFPath; } set { _CFPath = value; } }

      public InfoType InfoType
      {
         get
         {
            return _InfoType;
         }

         set
         {
            _InfoType = value;
         }
      }


      #region Serialization
      public string SaveState()
      {

         var sb = new StringBuilder();
         XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
         using (XmlWriter w = XmlWriter.Create(sb, settings))
         {
            w.WriteStartElement(GetType().Name);
            w.WriteAttributeString("cfpath", CFPath);
            w.WriteAttributeString("database", Database);
            w.WriteAttributeString("server", Server);
            w.WriteAttributeString("environment", Environment);
            w.WriteAttributeString("infotype", InfoType.ToString());
            w.WriteEndElement();
            w.Flush();
         }
         return sb.ToString();

      }

      public bool LoadState(XmlNode Node)
      {
         bool retval = true;
         try
         {
            CFPath = Node.ReadString("cfpath");
            Database = Node.ReadString("database");
            Server = Node.ReadString("server");
            Environment = Node.ReadString("environment");
            InfoType = Node.ReadEnum<InfoType>("infotype");
         }
         catch(Exception ex)
         {
            retval = false;
         }
         return retval;
      }

      #endregion

   }


}
