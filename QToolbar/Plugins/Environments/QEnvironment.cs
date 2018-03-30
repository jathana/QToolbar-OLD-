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

      

      #region fields
      private string _Status = "Loading...";
      private string _Name = "";
      private string _CheckoutPath = "";
      private string _ProteusCheckoutPath = "";
      private string _QBCAdminCfPath = "";
      private string _DBCollectionPlusVersion = "";
      private string _DBCollectionPlusServer = "";
      private string _DBCollectionPlusName = "";
      private string _ToolkitWSUrl = "";
      private string _AppWSUrl = "";
      private string _BatchExecutorWinServicePath = "";
      private string _EodExecutorWinServicePath = "";
      private string _WinServicesDir = "";
      private string _GLMDir = "";
      private string _GLMDirPermissions = "";
      private string _GLMLocalDir = "";
      private string _GLMLogDir = "";
      private string _GLMLogDirPermissions = "";
      private string _GLMLocalLogDir = "";
      private List<SharedDir> _QCSystemSharedDirs = new List<SharedDir>();
      private List<CfInfo> _CFs= new List<CfInfo>();
      private Errors _Errors = new Errors();

      #endregion

      #region properties
      public string Status
      {
         get
         {
            return _Status;
         }

         set
         {
            _Status = value;
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
      public string ProteusCheckoutPath
      {
         get
         {
            return _ProteusCheckoutPath;
         }

         set
         {
            _ProteusCheckoutPath = value;
         }
      }
      public string QBCAdminCfPath
      {
         get
         {
            return _QBCAdminCfPath;
         }

         set
         {
            _QBCAdminCfPath = value;
         }
      }
      public string DBCollectionPlusVersion
      {
         get
         {
            return _DBCollectionPlusVersion;
         }

         set
         {
            _DBCollectionPlusVersion = value;
         }
      }
      public string DBCollectionPlusServer
      {
         get
         {
            return _DBCollectionPlusServer;
         }

         set
         {
            _DBCollectionPlusServer = value;
         }
      }
      public string DBCollectionPlusName
      {
         get
         {
            return _DBCollectionPlusName;
         }

         set
         {
            _DBCollectionPlusName = value;
         }
      }
      public string ToolkitWSUrl
      {
         get
         {
            return _ToolkitWSUrl;
         }

         set
         {
            _ToolkitWSUrl = value;
         }
      }
      public string AppWSUrl
      {
         get
         {
            return _AppWSUrl;
         }

         set
         {
            _AppWSUrl = value;
         }
      }
      public string BatchExecutorWinServicePath
      {
         get
         {
            return _BatchExecutorWinServicePath;
         }

         set
         {
            _BatchExecutorWinServicePath = value;
         }
      }
      public string EodExecutorWinServicePath
      {
         get
         {
            return _EodExecutorWinServicePath;
         }

         set
         {
            _EodExecutorWinServicePath = value;
         }
      }
      public string WinServicesDir
      {
         get
         {
            return _WinServicesDir;
         }

         set
         {
            _WinServicesDir = value;
         }
      }
      public string GLMDir
      {
         get
         {
            return _GLMDir;
         }

         set
         {
            _GLMDir = value;
         }
      }
      public string GLMDirPermissions
      {
         get
         {
            return _GLMDirPermissions;
         }

         set
         {
            _GLMDirPermissions = value;
         }
      }
      public string GLMLocalDir
      {
         get
         {
            return _GLMLocalDir;
         }

         set
         {
            _GLMLocalDir = value;
         }
      }
      public string GLMLogDir
      {
         get
         {
            return _GLMLogDir;
         }

         set
         {
            _GLMLogDir = value;
         }
      }

      public string GLMLogDirPermissions
      {
         get
         {
            return _GLMLogDirPermissions;
         }

         set
         {
            _GLMLogDirPermissions = value;
         }
      }

      public string GLMLocalLogDir
      {
         get
         {
            return _GLMLocalLogDir;
         }

         set
         {
            _GLMLocalLogDir = value;
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

      
      public Errors Errors
      {
         get
         {
            return _Errors;
         }
      }





      #endregion

      #region constructor
      public QEnvironment()
      {
      }
      #endregion
   }
}
