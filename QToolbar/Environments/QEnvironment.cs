using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Tools
{
   public class QEnvironment
   {

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
   }
}
