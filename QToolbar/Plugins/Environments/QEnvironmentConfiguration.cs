using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar.Plugins.Environments
{
   /// <summary>
   /// Environment Configuration Information
   /// </summary>
   public class QEnvironmentConfiguration
   {
      #region fields
      private string _Id = "";
      private string _Name = "";
      private string _Server = "";
      private string _Database = "";
      private string _Username = "";
      private string _Password = "";
      private string _IsWinAuth = "";
      private string _NightlyBuildDBServer = "";
      private string _NightlyBuildDB = "";
      private string _GLMPrefix = "";
      private string _ConnectorRepositoryRoot = "";
      private string _ConnectorBranch = "";
      private string _EoDMonitorAlerts = "";
      private string _BatchServiceName = "";
      private string _BatchServicePath = "";
      private string _BatchServiceServer = "";
      private string _BatchServiceUNCPath = "";
      private string _EODServiceName = "";
      private string _EODServicePath = "";
      private string _EODServiceServer = "";
      private string _EODServiceUNCPath = "";
      private string _EnvironmentFlavor = "";
      private string _AnalyticsServer = "";
      private string _AnalyticsDatabase = "";
      private string _OLAPServer = "";
      private string _OLAPDatabase = "";
      private string _STDConnector = "";
      //
      private string _CheckoutPath = "";
      #endregion

      #region properties
      public string Id
      {
         get
         {
            return _Id;
         }

         set
         {
            _Id = value;
         }
      }

      public string Name
      {
         get
         {
            return _Name;
         }

         set
         {
            _Name = value;
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

      public string Username
      {
         get
         {
            return _Username;
         }

         set
         {
            _Username = value;
         }
      }

      public string Password
      {
         get
         {
            return _Password;
         }

         set
         {
            _Password = value;
         }
      }

      public string IsWinAuth
      {
         get
         {
            return _IsWinAuth;
         }

         set
         {
            _IsWinAuth = value;
         }
      }

      public string NightlyBuildDBServer
      {
         get
         {
            return _NightlyBuildDBServer;
         }

         set
         {
            _NightlyBuildDBServer = value;
         }
      }

      public string GLMPrefix
      {
         get
         {
            return _GLMPrefix;
         }

         set
         {
            _GLMPrefix = value;
         }
      }

      public string ConnectorRepositoryRoot
      {
         get
         {
            return _ConnectorRepositoryRoot;
         }

         set
         {
            _ConnectorRepositoryRoot = value;
         }
      }

      public string ConnectorBranch
      {
         get
         {
            return _ConnectorBranch;
         }

         set
         {
            _ConnectorBranch = value;
         }
      }

      public string EoDMonitorAlerts
      {
         get
         {
            return _EoDMonitorAlerts;
         }

         set
         {
            _EoDMonitorAlerts = value;
         }
      }

      public string BatchServiceName
      {
         get
         {
            return _BatchServiceName;
         }

         set
         {
            _BatchServiceName = value;
         }
      }

      public string BatchServicePath
      {
         get
         {
            return _BatchServicePath;
         }

         set
         {
            _BatchServicePath = value;
         }
      }

      public string BatchServiceServer
      {
         get
         {
            return _BatchServiceServer;
         }

         set
         {
            _BatchServiceServer = value;
         }
      }

      public string BatchServiceUNCPath
      {
         get
         {
            return _BatchServiceUNCPath;
         }

         set
         {
            _BatchServiceUNCPath = value;
         }
      }

      public string EODServiceName
      {
         get
         {
            return _EODServiceName;
         }

         set
         {
            _EODServiceName = value;
         }
      }

      public string EODServicePath
      {
         get
         {
            return _EODServicePath;
         }

         set
         {
            _EODServicePath = value;
         }
      }

      public string EODServiceServer
      {
         get
         {
            return _EODServiceServer;
         }

         set
         {
            _EODServiceServer = value;
         }
      }

      public string EODServiceUNCPath
      {
         get
         {
            return _EODServiceUNCPath;
         }

         set
         {
            _EODServiceUNCPath = value;
         }
      }

      public string EnvironmentFlavor
      {
         get
         {
            return _EnvironmentFlavor;
         }

         set
         {
            _EnvironmentFlavor = value;
         }
      }

      public string AnalyticsServer
      {
         get
         {
            return _AnalyticsServer;
         }

         set
         {
            _AnalyticsServer = value;
         }
      }

      public string AnalyticsDatabase
      {
         get
         {
            return _AnalyticsDatabase;
         }

         set
         {
            _AnalyticsDatabase = value;
         }
      }

      public string OLAPServer
      {
         get
         {
            return _OLAPServer;
         }

         set
         {
            _OLAPServer = value;
         }
      }

      public string OLAPDatabase
      {
         get
         {
            return _OLAPDatabase;
         }

         set
         {
            _OLAPDatabase = value;
         }
      }

      public string NightlyBuildDB
      {
         get
         {
            return _NightlyBuildDB;
         }

         set
         {
            _NightlyBuildDB = value;
         }
      }

      public string CheckoutPath
      {
         get
         {
            return _CheckoutPath;
         }

         set
         {
            _CheckoutPath = value;
         }
      }

      #endregion

      #region methods
      public void FromXml(XmlNode node)
      {
         try
         {
            _Id = node.ReadString("ID");
            _Name = node.ReadChildInnerTextString("Name");
            _Server = node.ReadChildInnerTextString("Server");
            _Database = node.ReadChildInnerTextString("Database");
            _Username = node.ReadChildInnerTextString("Username");
            _Password = node.ReadChildInnerTextString("Password");

            _IsWinAuth = node.ReadChildInnerTextString("IsWinAuth");
            _GLMPrefix = node.ReadChildInnerTextString("GLMPrefix");
            _ConnectorRepositoryRoot = node.ReadChildInnerTextString("ConnectorRepositoryRoot");
            _ConnectorBranch = node.ReadChildInnerTextString("ConnectorBranch");

            
            _NightlyBuildDBServer = node.ReadChildInnerTextString("NightlyBuildDBServer");
            _NightlyBuildDB = node.ReadChildInnerTextString("NightlyBuildDB");
            _BatchServiceName = node.ReadChildInnerTextString("BatchServiceName");
            _BatchServicePath = node.ReadChildInnerTextString("BatchServicePath");
            _BatchServiceServer = node.ReadChildInnerTextString("BatchServiceServer");
            _BatchServiceUNCPath = node.ReadChildInnerTextString("BatchServiceUNCPath");

            _EODServiceName = node.ReadChildInnerTextString("EODServiceName");
            _EODServicePath = node.ReadChildInnerTextString("EODServicePath");
            _EODServiceServer = node.ReadChildInnerTextString("EODServiceServer");
            _EODServiceUNCPath = node.ReadChildInnerTextString("EODServiceUNCPath");

            _EnvironmentFlavor = node.ReadChildInnerTextString("EnvironmentFlavor");
            _AnalyticsServer = node.ReadChildInnerTextString("AnalyticsServer");
            _AnalyticsDatabase = node.ReadChildInnerTextString("AnalyticsDatabase");

            _OLAPServer = node.ReadChildInnerTextString("OLAPServer");
            _OLAPDatabase = node.ReadChildInnerTextString("OLAPDatabase");
            _STDConnector = node.ReadChildInnerTextString("STDConnector");

         }
         catch(Exception ex)
         {
            throw new Exception($"xml element not read");
         }
      }
      #endregion
   }
}
