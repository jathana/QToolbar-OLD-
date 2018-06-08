﻿using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentParameters : Dictionary<string, QEnvironmentParameter>
   {
      private Dictionary<string, List<string>> _EQIslands = new Dictionary<string, List<string>>();


      public QEnvironmentParameters()
      {
         CreateParameters();
         CreateEQISlands();
      }


      public Errors Validate()
      {
         Errors ret = new Errors();
         foreach(QEnvironmentParameter param in this.Values)
         {
            ret.AddRange(param.Validate());
         }

         ret.AddRange(ValidateEQIslands());

         return ret;
      }



      private void CreateParameters()
      {
         // QBC_ADMIN_CF_ENVIRONMENT_KEY
         Add(EnvironmentConsts.QBC_ADMIN_CF_ENVIRONMENT_KEY, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_ENVIRONMENT_KEY,
            Desc = "Environment's key in qbc_admin.cf.",
            Mandatory = true
         });

         // QC_CHECKOUT_PATH
         Add(EnvironmentConsts.QC_CHECKOUT_PATH, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QC_CHECKOUT_PATH,
            Desc = "QC local checkout path.",
            Mandatory = true
         });

         // PROTEUS_CHECKOUT_PATH
         Add(EnvironmentConsts.PROTEUS_CHECKOUT_PATH, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.PROTEUS_CHECKOUT_PATH,
            Desc = "Proteus local checkout path.",
            Mandatory = true
         });

         // QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER
         Add(EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER,
            Desc = "Environment's Sql server instance of QBCollection plus database in qbc_admin.cf.",
            Mandatory = true
         });

         // QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME
         Add(EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME,
            Desc = "Environment's QBCollection plus database in qbc_admin.cf.",
            Mandatory = true
         });

         // QBCOLLECTION_PLUS_MAJOR_VERSION
         Add(EnvironmentConsts.QBCOLLECTION_PLUS_MAJOR_VERSION, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBCOLLECTION_PLUS_MAJOR_VERSION,
            Desc = "Environment's QBCollection plus database major version.",
            Mandatory = true
         });

         // QBCOLLECTION_PLUS_MINOR_VERSION
         Add(EnvironmentConsts.QBCOLLECTION_PLUS_MINOR_VERSION, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBCOLLECTION_PLUS_MINOR_VERSION,
            Desc = "Environment's QBCollection plus database minor version.",
            Mandatory = true
         });

         // QBC_ADMIN_CF_APPLICATION_WS_URL
         Add(EnvironmentConsts.QBC_ADMIN_CF_APPLICATION_WS_URL, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_APPLICATION_WS_URL,
            Desc = "Application's web service url found in qbc_admn.cf.",
            Mandatory = true
         });

         // QBC_ADMIN_CF_TOOLKIT_WS_URL
         Add(EnvironmentConsts.QBC_ADMIN_CF_TOOLKIT_WS_URL, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_TOOLKIT_WS_URL,
            Desc = "Toolkit web service url found in qbc_admn.cf.",
            Mandatory = true
         });

         // QBC_ADMIN_CF_FILE
         Add(EnvironmentConsts.QBC_ADMIN_CF_FILE, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.QBC_ADMIN_CF_FILE,
            Desc = "qbc_admin.cf file used to get information for environment.",
            Mandatory = true
         });


         // AT_SYSTEM_PARAMS_DIALER_DB_NAME
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_DIALER_DB_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_DIALER_DB_NAME,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='DIALER_DB_NAME'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_QBC_NAME
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_NAME,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_QBC_NAME'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_QBC_SERVER
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_SERVER,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_QBC_SERVER'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_FILE_SERVER_NAME
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_FILE_SERVER_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_FILE_SERVER_NAME,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_FILE_SERVER_NAME'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_SPRA_INSTALLATION_CRITERIA_NAME'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_QBA_NAME
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_NAME,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_QBA_NAME'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_QBA_SERVER
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_QBA_SERVER'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS'.",
            Mandatory = true
         });

         // AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE
         Add(EnvironmentConsts.AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE,
            Desc = "AT_SYSTEM_PARAMS table SPRA_TYPE='AT_SYSTEM_PARAMS_PATH_DATA_TRANSFORMATION_EXECUTABLE'.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_STEM_NAME
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_STEM_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_STEM_NAME,
            Desc = "BI_GLM_INSTALLATION table, field STEM_NAME.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_INST_ROOT
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_ROOT, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_ROOT,
            Desc = "BI_GLM_INSTALLATION table, field INST_ROOT.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_INST_STEM_NAME
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_STEM_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_STEM_NAME,
            Desc = "BI_GLM_INSTALLATION table, field INST_STEM_NAME.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_INST_SERVER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_SERVER,
            Desc = "BI_GLM_INSTALLATION table, field INST_SERVER.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_INST_DB_NAME
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_DB_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_DB_NAME,
            Desc = "BI_GLM_INSTALLATION table, field INST_DB_NAME.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_INST_DBUSER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_DBUSER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_DBUSER,
            Desc = "BI_GLM_INSTALLATION table, field INST_DBUSER.",
            Mandatory = true,
            MustHaveSpecificValue=true,
            SpecificValue="qbc_user"
         });

         // BI_GLM_INSTALLATION_INST_DBPASSW
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_INST_DBPASSW, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_INST_DBPASSW,
            Desc = "BI_GLM_INSTALLATION table, field INST_DBPASSW.",
            Mandatory = true,
            MustHaveSpecificValue = true,
            SpecificValue = "qbc_user"
         });

         // BI_GLM_INSTALLATION_QBA_SERVER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QBA_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QBA_SERVER,
            Desc = "BI_GLM_INSTALLATION table, field QBA_SERVER.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_QBA_DB_NAME
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DB_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DB_NAME,
            Desc = "BI_GLM_INSTALLATION table, field QBA_DB_NAME.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_QBA_DBUSER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DBUSER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DBUSER,
            Desc = "BI_GLM_INSTALLATION table, field QBA_DBUSER.",
            Mandatory = true,
            MustHaveSpecificValue = true,
            SpecificValue = "qbc_user"
         });

         // BI_GLM_INSTALLATION_QBA_DBPASSW
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DBPASSW, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DBPASSW,
            Desc = "BI_GLM_INSTALLATION table, field QBA_DBPASSW.",
            Mandatory = true,
            MustHaveSpecificValue = true,
            SpecificValue = "qbc_user"
         });

         // BI_GLM_INSTALLATION_QD3F_SERVER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_SERVER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_SERVER,
            Desc = "BI_GLM_INSTALLATION table, field QD3F_SERVER.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_QD3F_DB_NAME
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DB_NAME, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DB_NAME,
            Desc = "BI_GLM_INSTALLATION table, field QD3F_DB_NAME.",
            Mandatory = true
         });

         // BI_GLM_INSTALLATION_QD3F_DBUSER
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DBUSER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DBUSER,
            Desc = "BI_GLM_INSTALLATION table, field QD3F_DBUSER.",
            Mandatory = true,
            MustHaveSpecificValue = true,
            SpecificValue = "qbc_user"
         });

         // BI_GLM_INSTALLATION_QD3F_DBPASSW
         Add(EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DBPASSW, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.BI_GLM_INSTALLATION_QD3F_DBPASSW,
            Desc = "BI_GLM_INSTALLATION table, field QD3F_DBPASSW.",
            Mandatory = true,
            MustHaveSpecificValue = true,
            SpecificValue = "qbc_user"
         });

         // AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY
         Add(EnvironmentConsts.AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY,
            Desc = "AT_SYSTEM_PREF_ATTACHMENTS_DIRECTORY.",
            Mandatory = true
         });

         // AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY
         Add(EnvironmentConsts.AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY,
            Desc = "AT_SYSTEM_PREF_BULK_OUTPUT_EXPORT_DIRECTORY.",
            Mandatory = true
         });

         // AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH
         Add(EnvironmentConsts.AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH,
            Desc = "AT_SYSTEM_PREF_CRITERIA_PUBLISHED_PATH.",
            Mandatory = true
         });

         // AT_SYSTEM_PREF_WORDTEMPLATESFOLDER
         Add(EnvironmentConsts.AT_SYSTEM_PREF_WORDTEMPLATESFOLDER, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_WORDTEMPLATESFOLDER,
            Desc = "AT_SYSTEM_PREF_WORDTEMPLATESFOLDER.",
            Mandatory = true
         });

         // AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL
         Add(EnvironmentConsts.AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL,
            Desc = "AT_SYSTEM_PREF_FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL.",
            Mandatory = true
         });

         // AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL
         Add(EnvironmentConsts.AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL, new QEnvironmentParameter()
         {
            Name = EnvironmentConsts.AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL,
            Desc = "AT_SYSTEM_PREF_LEGAL_APP_PROCESS_MAPPING_WS_URL.",
            Mandatory = true
         });

      }

      private void CreateEQISlands()
      {
         // collection plus
         _EQIslands.Add("QBCollectionPlus Server", new List<string>() {
                                                            EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_SERVER,
                                                            EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_SERVER,
                                                            EnvironmentConsts.BI_GLM_INSTALLATION_INST_SERVER
         });

         _EQIslands.Add("QBCollectionPlus Database", new List<string>() {
                                                            EnvironmentConsts.QBC_ADMIN_CF_QBCOLLECTION_PLUS_DBNAME,
                                                            EnvironmentConsts.AT_SYSTEM_PARAMS_QBC_NAME,
                                                            EnvironmentConsts.BI_GLM_INSTALLATION_INST_DB_NAME
         });

         // analytics
         _EQIslands.Add("QBCollectionPlus Server", new List<string>() {
                                                            EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_SERVER,
                                                            EnvironmentConsts.BI_GLM_INSTALLATION_QBA_SERVER
         });

         _EQIslands.Add("QBCollectionPlus Database", new List<string>() {
                                                            EnvironmentConsts.AT_SYSTEM_PARAMS_QBA_NAME,
                                                            EnvironmentConsts.AT_SYSTEM_PARAMS_DB_NAME_ANALYTICS,
                                                            EnvironmentConsts.BI_GLM_INSTALLATION_QBA_DB_NAME
         });
      }

      private Errors ValidateEQIslands()
      {
         Errors ret = new Errors();

         foreach(KeyValuePair<string, List<string>> eqIsland in _EQIslands)
         {
            string val = this[eqIsland.Value[0]].Value;
            bool allEqual = true;
            StringBuilder b = new StringBuilder();
            b.AppendLine($"Found different values among {eqIsland.Key} parameters.");
            b.AppendLine($"Param {this[eqIsland.Value[0]].Name.ToString()} = {val}");

            for (int i=1;i< eqIsland.Value.Count;i++)
            {
               if(!val.ToLower().Equals(this[eqIsland.Value[i]].Value.ToLower()) && allEqual)
               {
                  allEqual = false;
               }
               b.AppendLine($"param {this[eqIsland.Value[i]].Name.ToString()} = {this[eqIsland.Value[i]].Value}");
            }

            if(!allEqual)
            {
               ret.AddError(b.ToString(), "");
            }
         }
         return ret;
      }


   }
}
