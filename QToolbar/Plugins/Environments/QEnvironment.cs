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
      private string _Status = string.Empty;
      private string _Name = string.Empty;
      private string _CheckoutPath = string.Empty;
      private string _ProteusCheckoutPath = string.Empty;
      private string _QBCAdminCfPath = string.Empty;
      private string _DBCollectionPlusMinorVersion = string.Empty;
      private string _DBCollectionPlusMajorVersion = string.Empty;
      private string _DBCollectionPlusVersion = string.Empty;
      private string _DBCollectionPlusServer = string.Empty;
      private string _DBCollectionPlusName = string.Empty;
      private string _ToolkitWSUrl = string.Empty;
      private string _AppWSUrl = string.Empty;
      private string _BatchWinServiceUNC = string.Empty;
      private string _BatchWinServicePath = string.Empty;
      private string _EodWinServiceUNC = string.Empty;
      private string _EodWinServicePath = string.Empty;
      private string _WinServicesUNC = string.Empty;
      private string _WinServicesPath = string.Empty;
      private string _GLMDir = string.Empty;
      private string _GLMDirPermissions = string.Empty;
      private string _GLMLocalDir = string.Empty;
      private string _GLMLogDir = string.Empty;
      private string _GLMLogDirPermissions = string.Empty;
      private string _GLMLocalLogDir = string.Empty;
      private string _SystemFolder = string.Empty;
      private string _GLMInstStemName = string.Empty;  // from bi_glm_installation
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
      public string BatchWinServiceUNC
      {
         get
         {
            return _BatchWinServiceUNC;
         }

         set
         {
            _BatchWinServiceUNC = value;
         }
      }
      public string EodWinServiceUNC
      {
         get
         {
            return _EodWinServiceUNC;
         }

         set
         {
            _EodWinServiceUNC = value;
         }
      }
      public string WinServicesUNC
      {
         get
         {
            return _WinServicesUNC;
         }

         set
         {
            _WinServicesUNC = value;
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
      
      public string BatchWinServicePath
      {
         get
         {
            return _BatchWinServicePath;
         }

         set
         {
            _BatchWinServicePath = value;
         }
      }

      public string EodWinServicePath
      {
         get
         {
            return _EodWinServicePath;
         }

         set
         {
            _EodWinServicePath = value;
         }
      }

      public string WinServicesPath
      {
         get
         {
            return _WinServicesPath;
         }

         set
         {
            _WinServicesPath = value;
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
            return _SystemFolder;
         }

         set
         {
            _SystemFolder = value;
         }
      }

      public string DBCollectionPlusMinorVersion
      {
         get
         {
            return _DBCollectionPlusMinorVersion;
         }

         set
         {
            _DBCollectionPlusMinorVersion = value;
         }
      }

      public string DBCollectionPlusMajorVersion
      {
         get
         {
            return _DBCollectionPlusMajorVersion;
         }

         set
         {
            _DBCollectionPlusMajorVersion = value;
         }
      }

      public string GLMInstStemName
      {
         get
         {
            return _GLMInstStemName;
         }

         set
         {
            _GLMInstStemName = value;
         }
      }



      #endregion

      #region constructor
      public QEnvironment()
      {
      }
      #endregion

      #region methods
      public void Clear()
      {
         _Name = string.Empty;
         _Status = string.Empty;
         _CheckoutPath = string.Empty;
         _ProteusCheckoutPath = string.Empty;
         _QBCAdminCfPath = string.Empty;
         _DBCollectionPlusVersion = string.Empty;
         _DBCollectionPlusServer = string.Empty;
         _DBCollectionPlusName = string.Empty;
         _ToolkitWSUrl = string.Empty;
         _AppWSUrl = string.Empty;
         _BatchWinServiceUNC = string.Empty;
         _EodWinServiceUNC = string.Empty;
         _WinServicesUNC = string.Empty;
         _GLMDir = string.Empty;
         _GLMDirPermissions = string.Empty;
         _GLMLocalDir = string.Empty;
         _GLMLogDir = string.Empty;
         _GLMLogDirPermissions = string.Empty;
         _GLMLocalLogDir = string.Empty;
         _SystemFolder = string.Empty;
         _GLMInstStemName = string.Empty;
         _DBCollectionPlusMajorVersion = string.Empty;
         _DBCollectionPlusMinorVersion = string.Empty;
         _CFs.Clear();
         _Errors.Clear();
         _QCSystemSharedDirs.Clear();
         _OtherFiles.Clear();
      }

      #endregion
   }
}
