using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentsConfiguration:List<QEnvironmentConfiguration>
   {
      private XmlDocument _XmlDoc;
      private string _Version = "";
      private string _ConfigurationFile = "";

      public string Version
      {
         get
         {
            return _Version;
         }

         set
         {
            _Version = value;
         }
      }

      public string ConfigurationFile
      {
         get
         {
            return _ConfigurationFile;
         }

         set
         {
            _ConfigurationFile = value;
         }
      }

      public QEnvironmentsConfiguration(string version, string configurationFile)
      {
         _Version = version;
         _ConfigurationFile = configurationFile;
         
      }

      
      public void Load()
      {
         
         Clear();
         if(File.Exists(_ConfigurationFile))
         {
            _XmlDoc = new XmlDocument();
            try
            {
               _XmlDoc.Load(_ConfigurationFile);
               XmlNodeList envNodes = _XmlDoc.DocumentElement.GetElementsByTagName("Environment");
               foreach(XmlNode env in envNodes)
               {
                  QEnvironmentConfiguration newEnv = new QEnvironmentConfiguration();
                  newEnv.EnvironmentsConfigurationFile = _ConfigurationFile;
                  newEnv.FromXml(env);
                  Add(newEnv);                  
               }               
            }
            catch (Exception ex)
            {
               throw new Exception($"Invalid environments configuration file {_ConfigurationFile}.", ex);
            }
         }

      }

   }
}
