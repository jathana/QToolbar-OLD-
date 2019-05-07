using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{

   public class QEnvironment
   {
      public class CfInfo
      {
         #region fields
         private string _Name;
         private string _Path;
         private string _Repository;
         #endregion

         #region properties
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
         public string Path
         {
            get
            {
               return _Path;
            }

            set
            {
               _Path = value;
            }
         }
         public string Repository
         {
            get
            {
               return _Repository;
            }

            set
            {
               _Repository = value;
            }
         }
            #endregion

            public override string ToString()
            {
                return $"{_Repository} - {_Name} - {_Path}";
            }
        }

      public class OtherFile
      {
         #region fields
         private string _Name;
         private string _Path;
         #endregion

         #region properties
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
         public string Path
         {
            get
            {
               return _Path;
            }

            set
            {
               _Path = value;
            }
         }
         #endregion
      }

      public class SharedDir
      {
         #region fields
         private string _Description;
         private string _UNC;
         private string _LocalPath;
         private int _Permissions;
         #endregion

         
         #region properties

         public List<string> Test { get; set; }

         public string Description
         {
            get
            {
               return _Description;
            }

            set
            {
               _Description = value;
            }
         }
         public string UNC
         {
            get
            {
               return _UNC;
            }

            set
            {
               _UNC = value;
            }
         }
         public string LocalPath
         {
            get
            {
               return _LocalPath;
            }

            set
            {
               _LocalPath = value;
            }
         }

         
         internal int Permissions
         {
            get
            {
               return _Permissions;
            }

            set
            {
               _Permissions = value;
            }
         }

         public string PermissionsDesc
         {
            get
            {
               return Utils.GetPermissionsDesc(Permissions);               
            }

         }

         


         #endregion
      }

      public class BIGlmInstallation
      {
         #region properties
         // QBCollectionPlus
         public string InstName { get; set; }
         public string InstStemName { get; set; }
         public string InstServer { get; set; }
         public string InstDbName { get; set; }
         public string InstDbUser { get; set; }
         public string InstDbPassword { get; set; }
         // Analytics
         public string QBAServer { get; set; }
         public string QBADbName { get; set; }
         public string QBAUser { get; set; }
         public string QBAPassword { get; set; }
         // Dialer
         public string DWHServer { get; set; }
         public string DWHDbName { get; set; }
         public string DWHUser { get; set; }
         public string DWHPassword { get; set; }
         // D3F
         public string QD3FServer { get; set; }
         public string QD3FDbName { get; set; }
         public string QD3FUser { get; set; }
         public string QD3FPassword { get; set; }
         #endregion


         public BIGlmInstallation()
         {
            InitProperties();
         }

         public void Clear()
         {
            InitProperties();
         }

         private void InitProperties()
         {
            InstName = string.Empty;
            InstStemName = string.Empty;
            InstServer = string.Empty;
            InstDbName = string.Empty;
            InstDbUser = string.Empty;
            InstDbPassword = string.Empty;
            // Analytics
            QBAServer = string.Empty;
            QBADbName = string.Empty;
            QBAUser = string.Empty;
            QBAPassword = string.Empty;
            // Dialer
            DWHServer = string.Empty;
            DWHDbName = string.Empty;
            DWHUser = string.Empty;
            DWHPassword = string.Empty;
            // D3F
            QD3FServer = string.Empty;
            QD3FDbName = string.Empty;
            QD3FUser = string.Empty;
            QD3FPassword = string.Empty;
         }
      }


      public class SimpleProperty
      {
         #region fields
         private string _Name;
         private string _Value;
         #endregion

         #region properties
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
         public string Value
         {
            get
            {
               return _Value;
            }

            set
            {
               _Value = value;
            }
         }
         #endregion
      }

      #region fields

      private Dictionary<string, string> _Properties = new Dictionary<string, string>();
      private List<SharedDir> _QCSystemSharedDirs = new List<SharedDir>();
      private List<CfInfo> _CFs= new List<CfInfo>();
      private Errors _Errors = new Errors();
      private List<OtherFile> _OtherFiles = new List<OtherFile>();
      #endregion

      #region properties
      public string Status
      {
         get
         {
            return _Properties["Status"];
         }

         set
         {
            _Properties["Status"] = value;
         }
      }

      public string Name
      {
         get
         {
            return _Properties["Name"];
         }

         set
         {
            _Properties["Name"] = value;
         }
      }
      public string CheckoutPath
      {
         get
         {
            return _Properties["CheckoutPath"];
         }

         set
         {
            _Properties["CheckoutPath"] = value;
         }
      }
      public string ProteusCheckoutPath
      {
         get
         {
            return _Properties["ProteusCheckoutPath"];
         }

         set
         {
            _Properties["ProteusCheckoutPath"] = value;
         }
      }
      public string QBCAdminCfPath
      {
         get
         {
            return _Properties["QBCAdminCfPath"];
         }

         set
         {
            _Properties["QBCAdminCfPath"] = value;
         }
      }
      public string DBCollectionPlusVersion
      {
         get
         {
            return _Properties["DBCollectionPlusVersion"];
         }

         set
         {
            _Properties["DBCollectionPlusVersion"] = value;
         }
      }
      public string DBCollectionPlusServer
      {
         get
         {
            return _Properties["DBCollectionPlusServer"];
         }

         set
         {
            _Properties["DBCollectionPlusServer"] = value;
         }
      }
      public string DBCollectionPlusName
      {
         get
         {
            return _Properties["DBCollectionPlusName"];
         }

         set
         {
            _Properties["DBCollectionPlusName"] = value;
         }
      }
      public string ToolkitWSUrl
      {
         get
         {
            return _Properties["ToolkitWSUrl"];
         }

         set
         {
            _Properties["ToolkitWSUrl"] = value;
         }
      }
      public string AppWSUrl
      {
         get
         {
            return _Properties["AppWSUrl"];
         }

         set
         {
            _Properties["AppWSUrl"] = value;
         }
      }
      public string BatchWinServiceUNC
      {
         get
         {
            return _Properties["BatchWinServiceUNC"];
         }

         set
         {
            _Properties["BatchWinServiceUNC"] = value;
         }
      }
      public string EodWinServiceUNC
      {
         get
         {
            return _Properties["EodWinServiceUNC"];
         }

         set
         {
            _Properties["EodWinServiceUNC"] = value;
         }
      }
      public string WinServicesUNC
      {
         get
         {
            return _Properties["WinServicesUNC"];
         }

         set
         {
            _Properties["WinServicesUNC"] = value;
         }
      }
      public string GLMDir
      {
         get
         {
            return _Properties["GLMDir"];
         }

         set
         {
            _Properties["GLMDir"] = value;
         }
      }
      public string GLMDirPermissions
      {
         get
         {
            return _Properties["GLMDirPermissions"];
         }

         set
         {
            _Properties["GLMDirPermissions"] = value;
         }
      }
      public string GLMLocalDir
      {
         get
         {
            return _Properties["GLMLocalDir"];
         }

         set
         {
            _Properties["GLMLocalDir"] = value;
         }
      }
      public string GLMLogDir
      {
         get
         {
            return _Properties["GLMLogDir"];
         }

         set
         {
            _Properties["GLMLogDir"] = value;
         }
      }

      public string GLMLogDirPermissions
      {
         get
         {
            return _Properties["GLMLogDirPermissions"];
         }

         set
         {
            _Properties["GLMLogDirPermissions"] = value;
         }
      }

      public string GLMLocalLogDir
      {
         get
         {
            return _Properties["GLMLocalLogDir"];
         }

         set
         {
            _Properties["GLMLocalLogDir"] = value;
         }
      }
      public List<SharedDir> QCSystemSharedDirs
      {
         get
         {
            return _QCSystemSharedDirs;
         }
      }

      public List<CfInfo> CFs
      {
         get
         {
            return _CFs;
         }
      }
      
      public string BatchWinServicePath
      {
         get
         {
            return _Properties["BatchWinServicePath"];
         }

         set
         {
            _Properties["BatchWinServicePath"] = value;
         }
      }

      public string EodWinServicePath
      {
         get
         {
            return _Properties["EodWinServicePath"];
         }

         set
         {
            _Properties["EodWinServicePath"] = value;
         }
      }

      public string WinServicesPath
      {
         get
         {
            return _Properties["WinServicesPath"];
         }

         set
         {
            _Properties["WinServicesPath"] = value;
         }
      }

      public List<OtherFile> OtherFiles
      {
         get
         {
            return _OtherFiles;
         }
      }

      // keep error as the last collection 
      public Errors Errors
      {
         get
         {
            return _Errors;
         }
      }

      public string SystemFolder
      {
         get
         {
            return _Properties["SystemFolder"];
         }

         set
         {
            _Properties["SystemFolder"] = value;
         }
      }

      public string DBCollectionPlusMinorVersion
      {
         get
         {
            return _Properties["DBCollectionPlusMinorVersion"];
         }

         set
         {
            _Properties["DBCollectionPlusMinorVersion"] = value;
         }
      }

      public string DBCollectionPlusMajorVersion
      {
         get
         {
            return _Properties["DBCollectionPlusMajorVersion"];
         }

         set
         {
            _Properties["DBCollectionPlusMajorVersion"] = value;
         }
      }

      public string GLMInstStemName
      {
         get
         {
            return _Properties["GLMInstStemName"];
         }

         set
         {
            _Properties["GLMInstStemName"] = value;
         }
      }

      public string LegalAppProcessMappingWSUrl
      {
         get
         {
            return _Properties["LegalAppProcessMappingWSUrl"];
         }

         set
         {
            _Properties["LegalAppProcessMappingWSUrl"] = value;
         }
      }

      public string LegalAppProcessMappingWSHost
      {
         get
         {
            return _Properties["LegalAppProcessMappingWSHost"];
         }

         set
         {
            _Properties["LegalAppProcessMappingWSHost"] = value;
         }
      }

      public string AnalyticsServer
      {
         get
         {
            return _Properties["AnalyticsServer"];
         }

         set
         {
            _Properties["AnalyticsServer"] = value;
         }
      }

      public string AnalyticsDBName
      {
         get
         {
            return _Properties["AnalyticsDBName"];
         }

         set
         {
            _Properties["AnalyticsDBName"] = value;
         }
      }

      public string D3FServer
      {
         get
         {
            return _Properties["D3FServer"];
         }

         set
         {
            _Properties["D3FServer"] = value;
         }
      }

      public string D3FDBName
      {
         get
         {
            return _Properties["D3FDBName"];
         }

         set
         {
            _Properties["D3FDBName"] = value;
         }
      }

      public string DialerServer
      {
         get
         {
            return _Properties["DialerServer"];
         }

         set
         {
            _Properties["DialerServer"] = value;
         }
      }

      public string DialerDBName
      {
         get
         {
            return _Properties["DialerDBName"];
         }

         set
         {
            _Properties["DialerDBName"] = value;
         }
      }

      public string AppWSUrlPort
      {
         get
         {
            return _Properties["AppWSUrlPort"];
         }

         set
         {
            _Properties["AppWSUrlPort"] = value;
         }
      }

      public string LegalAppWSUrlPort
      {
         get
         {
            return _Properties["LegalAppWSUrlPort"];
         }

         set
         {
            _Properties["LegalAppWSUrlPort"] = value;
         }
      }

      public string AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_URL
      {
         get
         {
            return _Properties["AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_URL"];
         }

         set
         {
            _Properties["AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_URL"] = value;
         }
      }

      public string OlapServer
      {
         get
         {
            return _Properties["OlapServer"];
         }

         set
         {
            _Properties["OlapServer"] = value;
         }
      }

      public string OlapDatabase
      {
         get
         {
            return _Properties["OlapDatabase"];
         }

         set
         {
            _Properties["OlapDatabase"] = value;
         }
      }

        public string QCSClientProductName
        {
            get
            {
                return _Properties["QCSClientProductName"];
            }

            set
            {
                _Properties["QCSClientProductName"] = value;
            }
        }

        public string BranchVersion
        {
            get
            {
                return _Properties["BranchVersion"];
            }

            set
            {
                _Properties["BranchVersion"] = value;
            }
        }
        public bool IsDevelopmentEnv
      {
         get
         {
            return Options.OptionsInstance.DevSQLInstances.Contains(DBCollectionPlusServer);
         }
      }

      internal Dictionary<string,string> Properties
      {
         get
         {
            return _Properties;
         }
      }

      public List<SimpleProperty> VisibleProperties
      {
         get
         {
            return _Properties.Select(i => new SimpleProperty() { Name = i.Key, Value = i.Value }).ToList();
         }
      }



      #endregion

      #region constructor
      public QEnvironment()
      {
         _Properties.Add("Status", string.Empty);
         _Properties.Add("Name", string.Empty);
         _Properties.Add("CheckoutPath", string.Empty);
         _Properties.Add("ProteusCheckoutPath", string.Empty);
         _Properties.Add("QBCAdminCfPath", string.Empty);
         _Properties.Add("DBCollectionPlusMinorVersion", string.Empty);
         _Properties.Add("DBCollectionPlusMajorVersion", string.Empty);
         _Properties.Add("DBCollectionPlusVersion", string.Empty);
         _Properties.Add("DBCollectionPlusServer", string.Empty);
         _Properties.Add("DBCollectionPlusName", string.Empty);
         _Properties.Add("ToolkitWSUrl", string.Empty);
         _Properties.Add("AppWSUrl", string.Empty);
         _Properties.Add("BatchWinServiceUNC", string.Empty);
         _Properties.Add("BatchWinServicePath", string.Empty);
         _Properties.Add("EodWinServiceUNC", string.Empty);
         _Properties.Add("EodWinServicePath", string.Empty);
         _Properties.Add("WinServicesUNC", string.Empty);
         _Properties.Add("WinServicesPath", string.Empty);
         _Properties.Add("GLMDir", string.Empty);
         _Properties.Add("GLMDirPermissions", string.Empty);
         _Properties.Add("GLMLocalDir", string.Empty);
         _Properties.Add("GLMLogDir", string.Empty);
         _Properties.Add("GLMLogDirPermissions", string.Empty);
         _Properties.Add("GLMLocalLogDir", string.Empty);
         _Properties.Add("SystemFolder", string.Empty);
         _Properties.Add("GLMInstStemName", string.Empty);  // from bi_glm_installation
         _Properties.Add("LegalAppProcessMappingWSUrl", string.Empty);
         _Properties.Add("LegalAppProcessMappingWSHost", string.Empty);
         _Properties.Add("AnalyticsServer", string.Empty);
         _Properties.Add("AnalyticsDBName", string.Empty);
         _Properties.Add("D3FServer", string.Empty);
         _Properties.Add("D3FDBName", string.Empty);
         _Properties.Add("DialerServer", string.Empty);
         _Properties.Add("DialerDBName", string.Empty);
         _Properties.Add("AppWSUrlPort", string.Empty);
         _Properties.Add("LegalAppWSUrlPort", string.Empty);
         _Properties.Add("OlapServer", string.Empty);
         _Properties.Add("OlapDatabase", string.Empty);
         _Properties.Add("AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_URL", string.Empty);
         _Properties.Add("QCSClientProductName", string.Empty);
         _Properties.Add("BranchVersion", string.Empty);

        }
      #endregion

      #region methods
      public void Clear()
      {

         foreach(var key in _Properties.Keys.ToList())
         {
            _Properties[key] = string.Empty;
         }

         _CFs.Clear();
         _Errors.Clear();
         _QCSystemSharedDirs.Clear();
         _OtherFiles.Clear();
      }

      #endregion
   }
}
