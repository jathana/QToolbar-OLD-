using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
   public enum QEnvPropCategory
   {
      QBCollection_Plus,
      QC_Archive,
      QBAnalytics,
      QBC_D3F_Intermediate,
      QCSClient,
      CFFile,
      XmlFile,
      IniFile,

   }

   public enum QEnvPropSubCategory
   {
      BI_GLM_INSTALLATION,
      AT_SYSTEM_PREF,
      AT_SYSTEM_PARAMS,
      ADMIN_WRAPPER_SETTINGS,
      TLK_DATABASE_VERSIONS,
      DialerParams,
      AT_EXTERNAL_COMPANIES,
      QBC_Admin,
      BI_GLM_DARC_TEMPLATED_VALUES,
      CFFile,
      EnvironmentsConfiguration,
      BatchServiceConfig,
      EODServiceConfig,
      WinServicesCmdCommandsTxt,
      EODExecutorEODIniFile,      
      EODExecutorCFFile,
      ApplicationUpdateEodIniFile,
      ApplicationUpdateCFFile


    }
   public enum QEnvPropertySetType
   {
      List,
      MustHaveSameValue,
      DataBaseConnection
   }
}
