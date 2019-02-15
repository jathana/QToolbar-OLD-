using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
   public class QEnv
   {
      public class P_ENV_CONF
      {
         public class P_ENV
         {
            public QEnvProperty Name { get; internal set; }
            public QEnvProperty Server { get; internal set; }
            public QEnvProperty Database { get; internal set; }
            public QEnvProperty Username { get; internal set; }
            public QEnvProperty Password { get; internal set; }
            public QEnvProperty IsWinAuth { get; internal set; }
            public QEnvProperty GLMPrefix { get; internal set; }
            public QEnvProperty ConnectorRepositoryRoot { get; internal set; }
            public QEnvProperty ConnectorBranch { get; internal set; }
            public QEnvProperty EoDMonitorAlerts { get; internal set; }
            public QEnvProperty BatchServiceName { get; internal set; }
            public QEnvProperty BatchServicePath { get; internal set; }
            public QEnvProperty BatchServiceServer { get; internal set; }
            public QEnvUNCDirProperty BatchServiceUNCPath { get; internal set; }
            public QEnvProperty EODServiceName { get; internal set; }
            public QEnvProperty EODServicePath { get; internal set; }
            public QEnvProperty EODServiceServer { get; internal set; }
            public QEnvUNCDirProperty EODServiceUNCPath { get; internal set; }
            public QEnvProperty EnvironmentFlavor { get; internal set; }
            public QEnvProperty AnalyticsServer { get; internal set; }
            public QEnvProperty AnalyticsDatabase { get; internal set; }
            public QEnvProperty OLAPServer { get; internal set; }
            public QEnvProperty OLAPDatabase { get; internal set; }
            public QEnvProperty STDConnector { get; internal set; }
            public QEnvProperty SynchConnectors { get; internal set; }
            public QEnvProperty CloneConnectors { get; internal set; }

            // computed
            public QEnvUNCDirProperty WinServicesUNC { get; internal set; }
            public QEnvProperty WinServicesPath { get; internal set; }
         }

         public P_ENV ENV { get; internal set; }
   }

      public class P_QCS_CLIENT
      {
         public class P_QBC_ADMIN
         {
            public QEnvProperty QBC_SERVER { get; internal set; }
            public QEnvProperty QBC_DB { get; internal set; }
            public QEnvURLProperty TOOLKIT_WS_URL { get; internal set; }
            public QEnvURLProperty APPLICATION_WS_URL { get; internal set; }

         }
         public P_QBC_ADMIN QBC_ADMIN { get; internal set; }
      }

      public class P_QBC
      {
         public class P_TLK_DATABASE_VERSIONS
         {
            public QEnvProperty MAJOR { get; internal set; }
            public QEnvProperty MINOR { get; internal set; }
            public QEnvProperty PATCH { get; internal set; }
            public QEnvProperty VERSION { get; internal set; }
         }

         public class P_BI_GLM_INSTALLATION
         {
            public QEnvProperty INST_NAME { get; internal set; }
            public QEnvProperty INST_STEM_NAME { get; internal set; }
            public QEnvServerProperty INST_SERVER { get; internal set; }
            public QEnvDatabaseProperty INST_db_NAME { get; internal set; }
            public QEnvDBUserProperty INST_dbUSER { get; internal set; }
            public QEnvDBPasswordProperty INST_dbPASSW { get; internal set; }
            public QEnvUNCDirProperty INST_ROOT { get; internal set; }
            public QEnvServerProperty QBA_SERVER { get; internal set; }
            public QEnvDatabaseProperty QBA_db_NAME { get; internal set; }
            public QEnvDBUserProperty QBA_dbUSER { get; internal set; }
            public QEnvDBPasswordProperty QBA_dbPASSW { get; internal set; }
            public QEnvProperty INST_COMMENTS { get; internal set; }
            public QEnvServerProperty DWH_SERVER { get; internal set; }
            public QEnvDatabaseProperty DWH_db_NAME { get; internal set; }
            public QEnvDBUserProperty DWH_dbUSER { get; internal set; }
            public QEnvDBPasswordProperty DWH_dbPASSW { get; internal set; }
            public QEnvServerProperty QD3F_SERVER { get; internal set; }
            public QEnvDatabaseProperty QD3F_db_NAME { get; internal set; }
            public QEnvDBUserProperty QD3F_dbUSER { get; internal set; }
            public QEnvDBPasswordProperty QD3F_dbPASSW { get; internal set; }
         }

         public class P_AT_SYSTEM_PREF
         {
            // QBCollection_Plus - AT_SYSTEM_PREF
            public QEnvUNCDirProperty ATTACHMENTS_DIRECTORY { get; internal set; }
            public QEnvUNCDirProperty BULK_OUTPUT_EXPORT_DIRECTORY { get; internal set; }
            public QEnvUNCDirProperty CRITERIA_PUBLISHED_PATH { get; internal set; }
            public QEnvUNCDirProperty WORDTEMPLATESFOLDER { get; internal set; }
            public QEnvURLProperty FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL { get; internal set; }
            public QEnvURLProperty LEGAL_APP_PROCESS_MAPPING_WS_URL { get; internal set; }
            public QEnvUNCDirProperty EXTERNAL_AGENCIES_PERFORMANCE_FILES_PATH { get; internal set; }
            public QEnvUNCDirProperty INTRADAY_FILE_UPLOAD_DIRECTORY { get; internal set; }
            public QEnvUNCDirProperty SYSTEM_FOLDER { get; internal set; }
            public QEnvProperty REPORT_DATABASE { get; internal set; }
         }

         public class P_AT_SYSTEM_PARAMS
         {
            public QEnvProperty DIALER_DB_NAME { get; internal set; }
            public QEnvProperty QBC_NAME { get; internal set; }
            public QEnvProperty QBC_SERVER { get; internal set; }
            public QEnvProperty FILE_SERVER_NAME { get; internal set; }
            public QEnvProperty SPRA_INSTALLATION_CRITERIA_NAME { get; internal set; }
            public QEnvProperty QBA_NAME { get; internal set; }
            public QEnvProperty QBA_SERVER { get; internal set; }
            public QEnvProperty DB_NAME_ANALYTICS { get; internal set; }
            public QEnvProperty PATH_DATA_TRANSFORMATION_EXECUTABLE { get; internal set; }
            public QEnvProperty DB_OLAP_DB_NAME { get; internal set; }
            public QEnvProperty ARCHIVE_SERVER { get; internal set; }
            public QEnvProperty ARCHIVE_DATABASE { get; internal set; }
         }

         public class P_DialerParams
         {
            public QEnvProperty db { get; internal set; }
         }

         public class P_OlapParams
         {
            public QEnvProperty db { get; internal set; }
         }

         public P_TLK_DATABASE_VERSIONS TLK_DATABASE_VERSIONS { get; internal set; }
         public P_BI_GLM_INSTALLATION BI_GLM_INSTALLATION { get; internal set; }
         public P_AT_SYSTEM_PREF AT_SYSTEM_PREF { get; internal set; }
         public P_AT_SYSTEM_PARAMS AT_SYSTEM_PARAMS { get; internal set; }
         public P_DialerParams DialerParams { get; internal set; }
      }

      public class P_ARCHIVE
      {
         public class P_AT_SYSTEM_PARAMS
         {
            public QEnvProperty QBC_SERVER { get; internal set; }
            public QEnvProperty QBC_NAME { get; internal set; }
            public QEnvProperty QBA_SERVER { get; internal set; }
            public QEnvProperty QBA_NAME { get; internal set; }
         }

         public class P_BI_GLM_DARC_TEMPLATED_VALUES
         {
            public QEnvProperty REMOTE_PATH { get; internal set; }
            public QEnvProperty REMOTE_SYS_PATH { get; internal set; }
         }

         public P_AT_SYSTEM_PARAMS AT_SYSTEM_PARAMS { get; internal set; }
         public P_BI_GLM_DARC_TEMPLATED_VALUES BI_GLM_DARC_TEMPLATED_VALUES { get; internal set; }
      }

      public class P_QBA
      {
         public class P_ADMIN_WRAPPER_SETTINGS
         {
            public QEnvProperty QBC_SERVER_NAME { get; internal set; }
            public QEnvProperty QBC_DB_NAME { get; internal set; }
            public QEnvProperty DIALER_SERVER_NAME { get; internal set; }
            public QEnvProperty DIALER_DB_NAME { get; internal set; }
            public QEnvProperty QBA_LINKED_SERVER_NAME { get; internal set; }
            public QEnvProperty QBA_LINKED_DB_NAME { get; internal set; }

         }
         public P_ADMIN_WRAPPER_SETTINGS ADMIN_WRAPPER_SETTINGS { get; internal set; }
      }

      public class P_D3F
      {
         public class P_ADMIN_WRAPPER_SETTINGS
         {
            public QEnvProperty QBC_SERVER_NAME { get; internal set; }
            public QEnvProperty QBC_DB_NAME { get; internal set; }
            public QEnvProperty DIALER_SERVER_NAME { get; internal set; }
            public QEnvProperty DIALER_DB_NAME { get; internal set; }
            public QEnvProperty QBA_LINKED_SERVER_NAME { get; internal set; }
            public QEnvProperty QBA_LINKED_DB_NAME { get; internal set; }
            public QEnvProperty D3F_LINKED_SERVER_NAME { get; internal set; }
            public QEnvProperty D3F_LINKED_DB_NAME { get; internal set; }

         }
         public P_ADMIN_WRAPPER_SETTINGS ADMIN_WRAPPER_SETTINGS { get; internal set; }
      }


      #region Fields
      private QEnvPropertySet _Properties;
      private List<QEnvPropertySet> _Dependencies;

      private string _Name;
      #endregion

      #region properties
      public string Name { get; set; }
      public string CheckoutPath { get; set; }
      public string ProteusCheckoutPath { get; set; }
      public string QBCAdminCfPath { get; set; }
      public string Status { get; set; }

      public P_QCS_CLIENT QCS_CLIENT { get; internal set; }
      public P_QBC QBC { get; internal set; }
      public P_ARCHIVE ARCHIVE { get; internal set; }
      public P_QBA QBA { get; internal set; }
      public P_D3F D3F { get; internal set; }
      public P_ENV_CONF ENV_CONF { get; internal set; }
      public Errors Errors { get; internal set; }
      public QEnvPropertySet Properties { get { return _Properties; } }

      #endregion

      #region public methods
      public QEnv()
      {
         _Properties = new EnvManager.QEnvPropertySet(QEnvPropertySetType.List, "Environment properties");
         _Dependencies = new List<QEnvPropertySet>();
         Errors = new Errors();

         Define_Properties();
         Define_Dependencies();
      }

      public void Validate()
      {
         Errors.Clear();

         // validate properties
         foreach(QEnvProperty property in _Properties)
         {
            Errors.AddRange(property.Validate());
         }

         // validate dependencies
         foreach(QEnvPropertySet set in _Dependencies)
         {
            Errors.AddRange(set.Validate());
         }
      }
      #endregion

      private void Define_Properties()
      {
         // Environments Configuration
         ENV_CONF = new P_ENV_CONF()
         {
            ENV = new P_ENV_CONF.P_ENV()
            {
               Name = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "Name", Required = true }),
               Server = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "Server", Required = true }),
               Database = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "Database", Required = true }),
               Username = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "Username", Required = true }),
               Password = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "Password", Required = true }),
               IsWinAuth = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "IsWinAuth", Required = true }),
               GLMPrefix = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "GLMPrefix", Required = true }),
               ConnectorRepositoryRoot = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "ConnectorRepositoryRoot", Required = true }),
               ConnectorBranch = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "ConnectorBranch", Required = true }),
               EoDMonitorAlerts = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EoDMonitorAlerts", Required = true }),
               BatchServiceName = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "BatchServiceName", Required = true }),
               BatchServicePath = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "BatchServicePath", Required = true }),
               BatchServiceServer = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "BatchServiceServer", Required = true }),
               BatchServiceUNCPath = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "BatchServiceUNCPath", Required = true }),
               EODServiceName = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EODServiceName", Required = true }),
               EODServicePath = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EODServicePath", Required = true }),
               EODServiceServer = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EODServiceServer", Required = true }),
               EODServiceUNCPath = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EODServiceUNCPath", Required = true }),
               EnvironmentFlavor = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "EnvironmentFlavor", Required = true }),
               AnalyticsServer = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "AnalyticsServer", Required = true }),
               AnalyticsDatabase = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "AnalyticsDatabase", Required = true }),
               OLAPServer = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "OLAPServer", Required = true }),
               OLAPDatabase = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "OLAPDatabase", Required = true }),
               STDConnector = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "STDConnector", Required = true }),
               SynchConnectors = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "SynchConnectors", Required = true }),
               CloneConnectors = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "CloneConnectors", Required = true }),
               // computed
               WinServicesUNC = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "WinServicesUNC", Required = true }),
               WinServicesPath = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.Files, SubCategory = QEnvPropSubCategory.EnvironmentsConfiguration, Name = "WinServicesPath", Required = true }),
            }
         };

         // QCS Client - qbc_admin 
         QCS_CLIENT = new P_QCS_CLIENT()
         {
            QBC_ADMIN = new P_QCS_CLIENT.P_QBC_ADMIN()
            {
               QBC_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QCSClient, SubCategory = QEnvPropSubCategory.QBC_Admin, Name = "QBC_SERVER", Required = true }),
               QBC_DB = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QCSClient, SubCategory = QEnvPropSubCategory.QBC_Admin, Name = "QBC_DB", Required = true }),
               APPLICATION_WS_URL = AddProperty(new QEnvURLProperty() { Category = QEnvPropCategory.QCSClient, SubCategory = QEnvPropSubCategory.QBC_Admin, Name = "TOOLKIT_WS_URL", Required = true }),               
               TOOLKIT_WS_URL = AddProperty(new QEnvURLProperty() { Category = QEnvPropCategory.QCSClient, SubCategory = QEnvPropSubCategory.QBC_Admin, Name = "APPLICATION_WS_URL", Required = true }),
            }
         };

         // QBCollection_Plus
         QBC = new P_QBC()
         {
            // QBCollection_Plus - TLK_DATABASE_VERSIONS
            TLK_DATABASE_VERSIONS = new P_QBC.P_TLK_DATABASE_VERSIONS()
            {
               MAJOR = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.TLK_DATABASE_VERSIONS, Name = "MAJOR", Required = true }),
               MINOR = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.TLK_DATABASE_VERSIONS, Name = "MINOR", Required = true }),
               PATCH = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.TLK_DATABASE_VERSIONS, Name = "PATCH", Required = true }),
               VERSION = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.TLK_DATABASE_VERSIONS, Name = "VERSION", Required = true }),
            },

            // QBCollection_Plus - BI_GLM_INSTALLATION
            BI_GLM_INSTALLATION = new P_QBC.P_BI_GLM_INSTALLATION()
            {
               INST_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_NAME", Required = true }),
               INST_STEM_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_STEM_NAME", Required = true }),
               INST_SERVER = AddProperty(new QEnvServerProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_SERVER", Required = true }),
               INST_db_NAME = AddProperty(new QEnvDatabaseProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_db_NAME", Required = true }),
               INST_dbUSER = AddProperty(new QEnvDBUserProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_dbUSER", Required = true }),
               INST_dbPASSW = AddProperty(new QEnvDBPasswordProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_dbPASSW", Required = true }),
               INST_ROOT = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_ROOT", Required = true }),
               QBA_SERVER = AddProperty(new QEnvServerProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QBA_SERVER", Required = true }),
               QBA_db_NAME = AddProperty(new QEnvDatabaseProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QBA_db_NAME", Required = true }),
               QBA_dbUSER = AddProperty(new QEnvDBUserProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QBA_dbUSER", Required = true }),
               QBA_dbPASSW = AddProperty(new QEnvDBPasswordProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QBA_dbPASSW", Required = true }),
               INST_COMMENTS = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "INST_COMMENTS" }),
               DWH_SERVER = AddProperty(new QEnvServerProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "DWH_SERVER" }),
               DWH_db_NAME = AddProperty(new QEnvDatabaseProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "DWH_db_NAME" }),
               DWH_dbUSER = AddProperty(new QEnvDBUserProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "DWH_dbUSER" }),
               DWH_dbPASSW = AddProperty(new QEnvDBPasswordProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "DWH_dbPASSW" }),
               QD3F_SERVER = AddProperty(new QEnvServerProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QD3F_SERVER" }),
               QD3F_db_NAME = AddProperty(new QEnvDatabaseProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QD3F_db_NAME" }),
               QD3F_dbUSER = AddProperty(new QEnvDBUserProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QD3F_dbUSER" }),
               QD3F_dbPASSW = AddProperty(new QEnvDBPasswordProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.BI_GLM_INSTALLATION, Name = "QD3F_dbPASSW" })
            },

            // QBCollection_Plus - AT_SYSTEM_PREF
            AT_SYSTEM_PREF = new P_QBC.P_AT_SYSTEM_PREF()
            {
               ATTACHMENTS_DIRECTORY = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "ATTACHMENTS_DIRECTORY", Required = true }),
               BULK_OUTPUT_EXPORT_DIRECTORY = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "BULK_OUTPUT_EXPORT_DIRECTORY", Required = true }),
               CRITERIA_PUBLISHED_PATH = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "CRITERIA_PUBLISHED_PATH", Required = true }),
               WORDTEMPLATESFOLDER = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "WORDTEMPLATESFOLDER", Required = true }),
               FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL = AddProperty(new QEnvURLProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL", Required = true }),               
               LEGAL_APP_PROCESS_MAPPING_WS_URL = AddProperty(new QEnvURLProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "LEGAL_APP_PROCESS_MAPPING_WS_URL", Required = true }),
               EXTERNAL_AGENCIES_PERFORMANCE_FILES_PATH = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "EXTERNAL_AGENCIES_PERFORMANCE_FILES_PATH", Required = true }),
               INTRADAY_FILE_UPLOAD_DIRECTORY = AddProperty(new QEnvUNCDirProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "INTRADAY_FILE_UPLOAD_DIRECTORY", Required = true }),
               REPORT_DATABASE = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PREF, Name = "REPORT_DATABASE", Required = true })
            },
            // QBCollection_Plus - AT_SYSTEM_PARAMS
            AT_SYSTEM_PARAMS = new P_QBC.P_AT_SYSTEM_PARAMS()
            {
               DIALER_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "DIALER_DB_NAME", Required = true }),
               QBC_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBC_NAME", Required = true }),
               QBC_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBC_SERVER", Required = true }),
               FILE_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "FILE_SERVER_NAME", Required = true }),
               SPRA_INSTALLATION_CRITERIA_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "SPRA_INSTALLATION_CRITERIA_NAME", Required = true }),
               QBA_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBA_NAME", Required = true }),
               QBA_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBA_SERVER", Required = true }),
               DB_NAME_ANALYTICS = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "DB_NAME_ANALYTICS", Required = true }),
               PATH_DATA_TRANSFORMATION_EXECUTABLE = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "PATH_DATA_TRANSFORMATION_EXECUTABLE", Required = true }),
               DB_OLAP_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "DB_OLAP_DB_NAME", Required = true }),
               ARCHIVE_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "ARCHIVE_SERVER", Required = false }),
               ARCHIVE_DATABASE = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "ARCHIVE_DATABASE", Required = false })
            },
            // QBCollection_Plus -  DialerParams
            DialerParams = new P_QBC.P_DialerParams()
            {
               db = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBCollection_Plus, SubCategory = QEnvPropSubCategory.DialerParams, Name = "db", Required = false })
            }
         };

         // QC_Archive
         ARCHIVE = new P_ARCHIVE()
         {
            // QC_Archive - AT_SYSTEM_PARAMS
            AT_SYSTEM_PARAMS = new EnvManager.QEnv.P_ARCHIVE.P_AT_SYSTEM_PARAMS()
            {
               QBC_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBC_NAME", Required = true }),
               QBC_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBC_SERVER", Required = true }),
               QBA_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBA_NAME", Required = true }),
               QBA_SERVER = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.AT_SYSTEM_PARAMS, Name = "QBA_SERVER", Required = true }),
            },

            BI_GLM_DARC_TEMPLATED_VALUES = new P_ARCHIVE.P_BI_GLM_DARC_TEMPLATED_VALUES()
            {
               REMOTE_PATH = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.BI_GLM_DARC_TEMPLATED_VALUES, Name = "/*[___REMOTE_PATH___]*/", Required = true }),
               REMOTE_SYS_PATH = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QC_Archive, SubCategory = QEnvPropSubCategory.BI_GLM_DARC_TEMPLATED_VALUES, Name = "/*[___REMOTE_SYS_PATH___]*/", Required = true }),
            }
         };

         // QBAnalytics
         QBA = new P_QBA()
         {
            // QBAnalytics - ADMIN_WRAPPER_SETTINGS
            ADMIN_WRAPPER_SETTINGS = new P_QBA.P_ADMIN_WRAPPER_SETTINGS()
            {
               QBC_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBC_SERVER_NAME", Required = true, CheckBrackets = true }),
               QBC_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBC_DB_NAME", Required = true }),
               DIALER_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "DIALER_SERVER_NAME", Required = true, CheckBrackets = true }),
               DIALER_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "DIALER_DB_NAME", Required = true }),
               QBA_LINKED_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBA_LINKED_SERVER_NAME", Required = true, CheckBrackets = true }),
               QBA_LINKED_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBAnalytics, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBA_LINKED_DB_NAME", Required = true }),
            }
         };


         // QBC_D3F_Intermediate
         D3F = new P_D3F()
         {
            // QBC_D3F_Intermediate - ADMIN_WRAPPER_SETTINGS
            ADMIN_WRAPPER_SETTINGS = new P_D3F.P_ADMIN_WRAPPER_SETTINGS()
            {
               QBC_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBC_SERVER_NAME", Required = true }),
               QBC_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBC_DB_NAME", Required = true }),
               DIALER_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "DIALER_SERVER_NAME", Required = false }),
               DIALER_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "DIALER_DB_NAME", Required = false }),
               QBA_LINKED_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBA_LINKED_SERVER_NAME", Required = true }),
               QBA_LINKED_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "QBA_LINKED_DB_NAME", Required = true }),
               D3F_LINKED_SERVER_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "D3F_LINKED_SERVER_NAME", Required = true }),
               D3F_LINKED_DB_NAME = AddProperty(new QEnvProperty() { Category = QEnvPropCategory.QBC_D3F_Intermediate, SubCategory = QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, Name = "D3F_LINKED_DB_NAME", Required = true }),
            }
         };
      }

     

      private void Define_Dependencies()
      {
         // QBCollection Server
         QEnvPropertySet sameValues_QBCServer = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QBC_Server");
         sameValues_QBCServer.Add(QBC.BI_GLM_INSTALLATION.INST_SERVER);
         sameValues_QBCServer.Add(QBC.AT_SYSTEM_PARAMS.QBC_SERVER);
         sameValues_QBCServer.Add(ARCHIVE.AT_SYSTEM_PARAMS.QBC_SERVER);
         sameValues_QBCServer.Add(QBA.ADMIN_WRAPPER_SETTINGS.QBC_SERVER_NAME);
         sameValues_QBCServer.Add(D3F.ADMIN_WRAPPER_SETTINGS.QBC_SERVER_NAME);
         _Dependencies.Add(sameValues_QBCServer);

         // QBCollection_Plus
         QEnvPropertySet sameValues_QBCPlus = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QBCollection_Plus");
         sameValues_QBCPlus.Add(QBC.BI_GLM_INSTALLATION.INST_db_NAME);
         sameValues_QBCPlus.Add(QBC.AT_SYSTEM_PREF.REPORT_DATABASE);
         sameValues_QBCPlus.Add(QBC.AT_SYSTEM_PARAMS.QBC_NAME);
         sameValues_QBCPlus.Add(ARCHIVE.AT_SYSTEM_PARAMS.QBC_NAME);
         sameValues_QBCPlus.Add(QBA.ADMIN_WRAPPER_SETTINGS.QBC_DB_NAME);
         sameValues_QBCPlus.Add(D3F.ADMIN_WRAPPER_SETTINGS.QBC_DB_NAME);
         _Dependencies.Add(sameValues_QBCPlus);

         // Analytics Server
         QEnvPropertySet sameValues_QBAServer = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QBA_Server");
         sameValues_QBAServer.Add(QBC.BI_GLM_INSTALLATION.QBA_SERVER);
         sameValues_QBAServer.Add(QBC.AT_SYSTEM_PARAMS.QBA_SERVER);
         sameValues_QBAServer.Add(QBA.ADMIN_WRAPPER_SETTINGS.QBA_LINKED_SERVER_NAME);
         sameValues_QBAServer.Add(D3F.ADMIN_WRAPPER_SETTINGS.QBA_LINKED_SERVER_NAME);
         sameValues_QBAServer.Add(ARCHIVE.AT_SYSTEM_PARAMS.QBA_SERVER);
         _Dependencies.Add(sameValues_QBAServer);

         // Analytics DB
         QEnvPropertySet sameValues_QBAnalytics = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QBA_Database");
         sameValues_QBAnalytics.Add(QBC.BI_GLM_INSTALLATION.QBA_db_NAME);
         sameValues_QBAnalytics.Add(QBC.AT_SYSTEM_PARAMS.QBA_NAME);
         sameValues_QBAnalytics.Add(QBA.ADMIN_WRAPPER_SETTINGS.QBA_LINKED_DB_NAME);
         sameValues_QBAnalytics.Add(D3F.ADMIN_WRAPPER_SETTINGS.QBA_LINKED_DB_NAME);
         sameValues_QBAnalytics.Add(ARCHIVE.AT_SYSTEM_PARAMS.QBA_NAME);
         _Dependencies.Add(sameValues_QBAnalytics);

         // QD3F Server
         QEnvPropertySet sameValues_QD3F_Server = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QD3F_Server");
         sameValues_QBAnalytics.Add(QBC.BI_GLM_INSTALLATION.QD3F_SERVER);
         sameValues_QBAnalytics.Add(D3F.ADMIN_WRAPPER_SETTINGS.D3F_LINKED_SERVER_NAME);
         _Dependencies.Add(sameValues_QD3F_Server);

         // QD3F Database
         QEnvPropertySet sameValues_QD3F_Database = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "QD3F_Database");
         sameValues_QBAnalytics.Add(QBC.BI_GLM_INSTALLATION.QD3F_db_NAME);
         sameValues_QBAnalytics.Add(D3F.ADMIN_WRAPPER_SETTINGS.D3F_LINKED_DB_NAME);
         _Dependencies.Add(sameValues_QD3F_Database);

         // qbc_user
         QEnvPropertySet sameValues_QBCUser = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "qbc_user");
         sameValues_QBCUser.Add(QBC.BI_GLM_INSTALLATION.INST_dbUSER);
         sameValues_QBCUser.Add(QBC.BI_GLM_INSTALLATION.QBA_dbUSER);
         sameValues_QBCUser.Add(QBC.BI_GLM_INSTALLATION.DWH_dbUSER);
         sameValues_QBCUser.Add(QBC.BI_GLM_INSTALLATION.QD3F_dbUSER);
         _Dependencies.Add(sameValues_QBCUser);

         // qbc_passw
         QEnvPropertySet sameValues_QBCPassword = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "qbc_password");
         sameValues_QBCPassword.Add(QBC.BI_GLM_INSTALLATION.INST_dbPASSW);
         sameValues_QBCPassword.Add(QBC.BI_GLM_INSTALLATION.QBA_dbPASSW);
         sameValues_QBCPassword.Add(QBC.BI_GLM_INSTALLATION.DWH_dbPASSW);
         sameValues_QBCPassword.Add(QBC.BI_GLM_INSTALLATION.QD3F_dbPASSW);
         _Dependencies.Add(sameValues_QBCPassword);

         // dialer server
         QEnvPropertySet sameValues_DialerServer = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "dialer server");
         sameValues_DialerServer.Add(QBA.ADMIN_WRAPPER_SETTINGS.DIALER_SERVER_NAME);
         sameValues_DialerServer.Add(D3F.ADMIN_WRAPPER_SETTINGS.DIALER_SERVER_NAME);
         _Dependencies.Add(sameValues_DialerServer);

         // dialer database
         QEnvPropertySet sameValues_DialerDatabase = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "dialer database");
         sameValues_DialerDatabase.Add(QBC.AT_SYSTEM_PARAMS.DIALER_DB_NAME);
         sameValues_DialerDatabase.Add(QBA.ADMIN_WRAPPER_SETTINGS.DIALER_DB_NAME);
         sameValues_QBCPlus.Add(D3F.ADMIN_WRAPPER_SETTINGS.DIALER_DB_NAME);
         _Dependencies.Add(sameValues_DialerDatabase);

         // PSet_BI_GLM_INSTALLATION.INST_STEM_NAME = EnvironmentsConfiguration.GLMPrefix.
         QEnvPropertySet sameValues_GLMPrefix = new QEnvPropertySet(QEnvPropertySetType.MustHaveSameValue, "EnvironmentsConfiguration.GLMPrefix");
         sameValues_DialerDatabase.Add(ENV_CONF.ENV.GLMPrefix);
         sameValues_DialerDatabase.Add(QBC.BI_GLM_INSTALLATION.INST_STEM_NAME);
         _Dependencies.Add(sameValues_GLMPrefix);


         // BI_GLM_INSTALLATION Collection Plus Connection
         QEnvPropertySet dbConnection_INST_CollectionPlus = new QEnvPropertySet(QEnvPropertySetType.DataBaseConnection, "BI_GLM_INSTALLATION - QBCollection_Plus connection");
         dbConnection_INST_CollectionPlus.Add(QBC.BI_GLM_INSTALLATION.INST_SERVER);
         dbConnection_INST_CollectionPlus.Add(QBC.BI_GLM_INSTALLATION.INST_db_NAME);
         dbConnection_INST_CollectionPlus.Add(QBC.BI_GLM_INSTALLATION.INST_dbUSER);
         dbConnection_INST_CollectionPlus.Add(QBC.BI_GLM_INSTALLATION.INST_dbPASSW);
         _Dependencies.Add(dbConnection_INST_CollectionPlus);

         // BI_GLM_INSTALLATION Analytics Connection
         QEnvPropertySet dbConnection_INST_Analytics = new QEnvPropertySet(QEnvPropertySetType.DataBaseConnection, "BI_GLM_INSTALLATION - Analytics connection");
         dbConnection_INST_Analytics.Add(QBC.BI_GLM_INSTALLATION.QBA_SERVER);
         dbConnection_INST_Analytics.Add(QBC.BI_GLM_INSTALLATION.QBA_db_NAME);
         dbConnection_INST_Analytics.Add(QBC.BI_GLM_INSTALLATION.QBA_dbUSER);
         dbConnection_INST_Analytics.Add(QBC.BI_GLM_INSTALLATION.QBA_dbPASSW);
         _Dependencies.Add(dbConnection_INST_Analytics);

         // BI_GLM_INSTALLATION DWH Connection
         QEnvPropertySet dbConnection_INST_DWH = new QEnvPropertySet(QEnvPropertySetType.DataBaseConnection, "BI_GLM_INSTALLATION - DWH connection");
         dbConnection_INST_DWH.Add(QBC.BI_GLM_INSTALLATION.DWH_SERVER);
         dbConnection_INST_DWH.Add(QBC.BI_GLM_INSTALLATION.DWH_db_NAME);
         dbConnection_INST_DWH.Add(QBC.BI_GLM_INSTALLATION.DWH_dbUSER);
         dbConnection_INST_DWH.Add(QBC.BI_GLM_INSTALLATION.DWH_dbPASSW);
         _Dependencies.Add(dbConnection_INST_DWH);

         // BI_GLM_INSTALLATION DWH Connection
         QEnvPropertySet dbConnection_INST_QD3F = new QEnvPropertySet(QEnvPropertySetType.DataBaseConnection, "BI_GLM_INSTALLATION - QD3F connection");
         dbConnection_INST_QD3F.Add(QBC.BI_GLM_INSTALLATION.QD3F_SERVER);
         dbConnection_INST_QD3F.Add(QBC.BI_GLM_INSTALLATION.QD3F_db_NAME);
         dbConnection_INST_QD3F.Add(QBC.BI_GLM_INSTALLATION.QD3F_dbUSER);
         dbConnection_INST_QD3F.Add(QBC.BI_GLM_INSTALLATION.QD3F_dbPASSW);
         _Dependencies.Add(dbConnection_INST_QD3F);

      }

       private T AddProperty<T>(T prop) where T: QEnvProperty
      {
         _Properties.Add(prop);
         return prop;
      }

   }
}
