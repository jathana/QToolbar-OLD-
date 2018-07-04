using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentParametersSets: Dictionary<string, QEnvironmentParametersSet>
   {
      private QEnvironmentParameters _EnvParams;

      public QEnvironmentParameters EnvParams
      {
         get
         {
            return _EnvParams;
         }

         set
         {
            _EnvParams = value;
         }
      }

      public QEnvironmentParametersSets(QEnvironmentParameters envParams)
      {
         _EnvParams = envParams;
         CreateParametersSets();
      }

      public Errors Validate()
      {
         Errors ret = new Errors();
         foreach(var item in this)
         {
            //ret.AddRange(item.Validate());
         }
         return ret;

      }


      private void CreateParametersSets()
      {
         // collection plus servers
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_EQ_COLLECTION_PLUS_SERVER,
                         QEnvironmentParametersSetType.ParametersShouldHaveEqualValues,
                         new string[] {EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_SERVER,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_INST_SERVER});

         // collection plus dbs
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_EQ_COLLECTION_PLUS_DBNAME,
                         QEnvironmentParametersSetType.ParametersShouldHaveEqualValues,
                        new string[] { EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_NAME,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_INST_DB_NAME });

         // analytics servers
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_EQ_ANALYTICS_SERVER,
                        QEnvironmentParametersSetType.ParametersShouldHaveEqualValues,
                        new string[] { EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_QBA_SERVER });

         // analytics dbs
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_EQ_ANALYTICS_DBNAME,
                        QEnvironmentParametersSetType.ParametersShouldHaveEqualValues,
                        new string[] { EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_NAME,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DB_NAME });

         // collection plus connection
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_QBC_ADMIN_CF_QBCOLLECTION_PLUS,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER,
                                       EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME });

         // dialer db connection
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_DIALER,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_DIALER_DB_NAME });

         // AT_SYSTEM_PARAMS:QBCollectionPlus
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_QBCOLLECTION_PLUS,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_SERVER,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_NAME });

         // AT_SYSTEM_PARAMS:Analytics
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_ANALYTICS,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_NAME });

         // AT_SYSTEM_PARAMS:Analytics
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_AT_SYSTEM_PARAMS_ANALYTICS2,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER,
                                       EnvironmentConsts.AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS });


         // BI_GLM_INSTALLATION:QBCollectionPlus
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_QBCOLLECTION_PLUS,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.BI_GLM_INSTALLATION_INST_SERVER,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_INST_DB_NAME });

         // BI_GLM_INSTALLATION:Analytics
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_ANALYTICS,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.BI_GLM_INSTALLATION_QBA_SERVER,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DB_NAME });

         // BI_GLM_INSTALLATION:D3F
         AddEnvParamsSet(EnvironmentConsts.PARAM_SET_CONNECTION_BI_GLM_INSTALLATION_D3F,
                        QEnvironmentParametersSetType.ParametersAreDBConnection,
                        new string[] { EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_SERVER,
                                       EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DB_NAME });
      }



      private void AddEnvParamsSet(string name, QEnvironmentParametersSetType setType, string[] setParams)
      {
         QEnvironmentParametersSet set = new Environments.QEnvironmentParametersSet()
         {
            Name = name,
            SetType = setType,
         };
         foreach(string param in setParams)
         {
            set.Add(_EnvParams[param]);
         }
         Add(name, set);
      }
   }
}
