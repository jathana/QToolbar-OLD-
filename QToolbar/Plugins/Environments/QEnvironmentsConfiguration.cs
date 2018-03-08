using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentsConfiguration
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


      private void Load(string configurationFile)
      {
         if(File.Exists(configurationFile))
         {
            _XmlDoc = new XmlDocument();
            try
            {
               _XmlDoc.Load(configurationFile);

            }
            catch (Exception ex)
            {
               throw new Exception($"Invalid enviromnents configuration file {configurationFile}.", ex);
            }
         }

      }
         
         
         

   }
}
