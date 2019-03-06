using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    class QEnvDependenciesLoader
    {
        public void Load(QEnv objEnv)
        {
            
            // set MatchValue QBC_Server
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType ==  QEnvPropSetDependency.SameValue_QBC_SERVER) as QEnvPropertySetSameValue).MatchValue = objEnv.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value;

            // set CheckRegex to QBC same value dependency
            string regPattern = $"QBCollection[_]Plus[_][A-Z]+[_][{objEnv.QBC.TLK_DATABASE_VERSIONS.MAJOR.Value}][_][{objEnv.QBC.TLK_DATABASE_VERSIONS.MINOR.Value}][_]*[0-9]*[_]*[0-9]*";
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QBC_DB_NAME) as QEnvPropertySetSameValue).MatchRegexPattern = regPattern;
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QBC_DB_NAME) as QEnvPropertySetSameValue).MatchValue = objEnv.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value;

            
            // set MatchValue QBA_Server
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType ==  QEnvPropSetDependency.SameValue_QBA_SERVER) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.BI_GLM_INSTALLATION.QBA_SERVER.Value;

            // set CheckRegex to QBA same value dependency QBAnalytics_MONE_8_4_2
            regPattern = $"QBAnalytics[_][A-Z]+[_][{objEnv.QBC.TLK_DATABASE_VERSIONS.MAJOR.Value}][_][{objEnv.QBC.TLK_DATABASE_VERSIONS.MINOR.Value}][_]*[0-9]*[_]*[0-9]*";
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QBA_DB_NAME) as QEnvPropertySetSameValue).MatchRegexPattern = regPattern;
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QBA_DB_NAME) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.BI_GLM_INSTALLATION.QBA_db_NAME.Value;


            // set MatchValue QD3F_Server
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QD3F_SERVER) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.BI_GLM_INSTALLATION.QD3F_SERVER.Value;

            // set MatchValue QD3F_db_NAME
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_QD3F_DB_NAME) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.BI_GLM_INSTALLATION.QD3F_db_NAME.Value;

            // set MatchValue dialer server
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType == QEnvPropSetDependency.SameValue_DIALER_SERVER) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.BI_GLM_INSTALLATION.INST_SERVER.Value;

            // set MatchValue dialer database
            (objEnv.Dependencies.FirstOrDefault(d => d.DependencyType ==  QEnvPropSetDependency.SameValue_DIALER_DB_NAME) as QEnvPropertySetSameValue).MatchValue = objEnv.QBC.AT_SYSTEM_PARAMS.DIALER_DB_NAME.Value;


        }




    }
}
