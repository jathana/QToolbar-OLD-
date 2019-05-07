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
        File
        

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
    public enum QEnvPropSetDependency
    {
        List,
        SameValue_QBC_SERVER,
        SameValue_QBC_DB_NAME,

        SameValue_QBA_SERVER,
        SameValue_QBA_DB_NAME,

        SameValue_QD3F_SERVER,
        SameValue_QD3F_DB_NAME,

        SameValue_QBC_USER,
        SameValue_QBC_PASSW,

        SameValue_DIALER_SERVER,
        SameValue_DIALER_DB_NAME,

        SameValue_GLM_PREFIX,

        DBConnection_QBC_BI_GLM_INSTALLATION_QBC,
        DBConnection_QBC_BI_GLM_INSTALLATION_QBA,
        DBConnection_QBC_BI_GLM_INSTALLATION_DWH,
        DBConnection_QBC_BI_GLM_INSTALLATION_QD3F,
        DBConnection_QBC_AT_SYSTEM_PARAMS_ARCHIVE,

    }
}
