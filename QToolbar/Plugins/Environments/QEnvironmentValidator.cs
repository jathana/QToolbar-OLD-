using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentValidator
   {
      private QEnvironmentParameters _EnvParams;
      private QEnvironmentParametersSets _EnvParamsSets;


      public QEnvironmentValidator()
      {
         _EnvParams = new QEnvironmentParameters();
         _EnvParamsSets = new QEnvironmentParametersSets(_EnvParams);
      }


      private void Validate(string envName, CfFile qbcAdminCf, string checkoutPath, string proteusCheckoutPath)
      {
         // env key
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_ENVIRONMENT_KEY, envName);

         // checkout paths
         _EnvParams.SetParamValue(EnvironmentConsts.QC_CHECKOUT_PATH, checkoutPath);
         _EnvParams.SetParamValue(EnvironmentConsts.PROTEUS_CHECKOUT_PATH,proteusCheckoutPath);

         // set qbcollection plus server & name
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER, qbcAdminCf.GetServer(envName)); 
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME, qbcAdminCf.GetDatabase(envName));

         // set ToolkitWSUrl, AppWSUrl
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_APPLICATION_WS_URL, qbcAdminCf.GetValue("General", "ApplicationWSURL"));
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_TOOLKIT_WS_URL, qbcAdminCf.GetValue("General", "ToolkitWSURL"));

         // qbc_admin.cf file
         _EnvParams.SetParamValue(EnvironmentConsts.QBC_ADMIN_CF_FILE, qbcAdminCf.File);




      }


   }
}
