using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvDatabasesLoader
    {

        public void Load(QEnv objEnv, List<string> eodFlows, CancellationToken cancelToken)
        {
            LoadFromQBCollectionPlusDB(objEnv, eodFlows, cancelToken);
            LoadFromAnalyticsDB(objEnv, cancelToken);
            LoadFromD3FDB(objEnv, cancelToken);
            LoadFromArchiveDB(objEnv, cancelToken);
        }

        #region private  
        private void LoadFromQBCollectionPlusDB(QEnv objEnv, List<string> eodFlows, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

            if (objEnv.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.EmptyValue || objEnv.QCS_CLIENT.QBC_ADMIN.QBC_DB.EmptyValue)
            {
                objEnv.Errors.AddError("Unable to connect to and retrieve information from QBCollectionPlus db. QBC_NAME or QBC_SERVER is empty in qbc_admin.cf", "");
                return;
            }

            MultiSqlExecutor executor = new MultiSqlExecutor();
            // TLK_DATABASE_VERSIONS
            executor.AddSql("TLK_DATABASE_VERSIONS", "SELECT top(1) ID, MAJOR, MINOR, PATCH, CONVERT(NVARCHAR,MAJOR)+'.' + CONVERT(NVARCHAR,MINOR) AS VERSION FROM TLK_DATABASE_VERSIONS ORDER BY 1 DESC");

            // BI_GLM_INSTALLATION
            executor.AddSql("BI_GLM_INSTALLATION", "SELECT * FROM BI_GLM_INSTALLATION");

            // AT_SYSTEM_PREF
            executor.AddSql("AT_SYSTEM_PREF", @"SELECT * FROM AT_SYSTEM_PREF 
                                             WHERE SPR_TYPE IN ('ATTACHMENTS_DIRECTORY'                     , 'BULK_OUTPUT_EXPORT_DIRECTORY'     , 'CRITERIA_PUBLISHED_PATH', 
					                                                 'FIELD_AGENT_INTEGRATION_APPLICATION_WS_URL', 'LEGAL_APP_PROCESS_MAPPING_WS_URL' , 'WORDTEMPLATESFOLDER',
					                                                 'EXTERNAL_AGENCIES_PERFORMANCE_FILES_PATH'  , 'INTRADAY_FILE_UPLOAD_DIRECTORY'   , 'REPORT_DATABASE')
                                             ORDER BY SPR_TYPE");

            // AT_SYSTEM_PARAMS
            executor.AddSql("AT_SYSTEM_PARAMS", @"SELECT * FROM AT_SYSTEM_PARAMS 
                                               WHERE SPRA_TYPE IN('DIALER_DB_NAME' , 'QBC_NAME',   'QBC_SERVER'       , 'FILE_SERVER_NAME', 'SPRA_INSTALLATION_CRITERIA_NAME', 
                                                                  'QBA_NAME'       , 'QBA_SERVER', 'DB_NAME_ANALYTICS', 'PATH_DATA_TRANSFORMATION_EXECUTABLE', 
                                                                  'DB_OLAP_DB_NAME', 'ARCHIVE_SERVER', 'ARCHIVE_DATABASE')
                                                ORDER BY SPRA_TYPE");

            // OLAP DB
            executor.AddSql("BI_GLM_STEP_LIB_PARAMS", @"SELECT SP.SPAR_VALUE, SLPARAM.SLPAR_DESC
                                                   FROM DBO.BI_GLM_FLOW F 
                                                   INNER JOIN DBO.BI_GLM_CONTAINER C ON F.FLOW_ID = C.FLOW_ID 
                                                   INNER JOIN DBO.BI_GLM_PACKAGE P ON C.CNT_ID = P.CNT_ID 
                                                   INNER JOIN DBO.BI_GLM_STEP S ON P.PACK_ID = S.PACK_ID 
                                                   INNER JOIN DBO.BI_GLM_STEP_LIB_PARAMS SLPARAM ON SLPARAM.SLIB_ID = S.SLIB_ID
                                                   INNER JOIN DBO.BI_GLM_STEP_PARAMETERS SP ON S.STEP_ID = SP.STEP_ID AND SP.SPAR_ORDER = SLPARAM.SLPAR_ORDER
                                                   WHERE SLPAR_DESC IN ('@SERVER_ANL', '@DATABASE_ANL')
                                                   GROUP BY SP.SPAR_VALUE, SLPARAM.SLPAR_DESC");

            // DialerParams
            executor.AddSql("DialerParams", @"SELECT * FROM DialerParams");

            // AT_EXTERNAL_COMPANIES
            executor.AddSql("AT_EXTERNAL_COMPANIES", @"SELECT EXTC_IO_DIR, EXTC_NAME, EXTC_INTCODE, * FROM AT_EXTERNAL_COMPANIES");

            // Man Client Flows
            executor.AddSql("Man_ClientFlows", @"declare @prefix nvarchar(10)
                                            select @prefix = INST.INST_PREFIX
                                            from Enterprises ENT
                                            INNER JOIN AT_INSTALLATIONS INST ON ENT.INST_CODE = INST.INST_CODE

                                            SELECT EOFC_FLOW_NAME
                                            FROM Man_ClientFlows(@prefix)
                                            WHERE EOFC_ACTIVE = 1 ORDER BY FLOW_ORDERING");


            try
            {
                DataSet dataSet = executor.Execute(Utils.GetConnectionString(objEnv.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value, objEnv.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value));

                // set properties - QBCollection_Plus - TLK_DATABASE_VERSIONS
                SetPropertiesValues(dataSet, objEnv, "TLK_DATABASE_VERSIONS", QEnvPropCategory.QBCollection_Plus, QEnvPropSubCategory.TLK_DATABASE_VERSIONS, cancelToken);

                // set properties - QBCollection_Plus - BI_GLM_INSTALLATION
                SetPropertiesValues(dataSet, objEnv, "BI_GLM_INSTALLATION", QEnvPropCategory.QBCollection_Plus, QEnvPropSubCategory.BI_GLM_INSTALLATION, cancelToken);

                // set properties - QBCollection_Plus - AT_SYSTEM_PREF
                SetPropertiesValues(dataSet, objEnv, "AT_SYSTEM_PREF", QEnvPropCategory.QBCollection_Plus, QEnvPropSubCategory.AT_SYSTEM_PREF, "SPR_TYPE", "SPR_VALUE", cancelToken);

                // set properties - QBCollection_Plus - AT_SYSTEM_PARAMS
                SetPropertiesValues(dataSet, objEnv, "AT_SYSTEM_PARAMS", QEnvPropCategory.QBCollection_Plus, QEnvPropSubCategory.AT_SYSTEM_PARAMS, "SPRA_TYPE", "SPRA_VALUE", cancelToken);

                // set properties - QBCollection_Plus - DialerParams
                objEnv.QBC.DialerParams.db.Value = dataSet.Tables["DialerParams"].Select("code='db'")[0]["val"].ToString();

                // set properties - QBCollection_Plus - External Companies
                AddOrUpdateProperties<QEnvUNCDirProperty>(dataSet, objEnv, "AT_EXTERNAL_COMPANIES", QEnvPropCategory.QBCollection_Plus, QEnvPropSubCategory.AT_EXTERNAL_COMPANIES, "EXTC_NAME", "EXTC_IO_DIR", cancelToken);

                // SET SYSTEM_FOLDER
                objEnv.QBC.AT_SYSTEM_PREF.SYSTEM_FOLDER.Value = GetSystemFolder(objEnv);

                // ManClientFlows
                eodFlows = GetFlowsForEodIni(dataSet, cancelToken);

                
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"QBCollection Plus db error ({ex.Message})", "");
            }
        }

        private void LoadFromAnalyticsDB(QEnv objEnv, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

            if (objEnv.QBC.BI_GLM_INSTALLATION.QBA_SERVER.EmptyValue || objEnv.QBC.BI_GLM_INSTALLATION.QBA_db_NAME.EmptyValue)
            {
                objEnv.Errors.AddError("Unable to connect to and retrieve information from QBAnalytics db. QBA_NAME or QBA_SERVER is missing from QBCollection_Plus.BI_GLM_INSTALLATION table.", "");
                return;
            }

            MultiSqlExecutor executor = new MultiSqlExecutor();
            executor.AddSql("ADMIN_WRAPPER_SETTINGS", "SELECT * FROM ADMIN_WRAPPER_SETTINGS");
            try
            {
                DataSet dataSet = executor.Execute(Utils.GetConnectionString(objEnv.QBC.BI_GLM_INSTALLATION.QBA_SERVER.Value, objEnv.QBC.BI_GLM_INSTALLATION.QBA_db_NAME.Value));
                SetPropertiesValues(dataSet, objEnv, "ADMIN_WRAPPER_SETTINGS", QEnvPropCategory.QBAnalytics, QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, cancelToken);
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"Analytics db error ({ex.Message})", "");
            }
        }

        private void LoadFromD3FDB(QEnv objEnv, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

            if (objEnv.QBC.BI_GLM_INSTALLATION.QD3F_SERVER.EmptyValue || objEnv.QBC.BI_GLM_INSTALLATION.QD3F_db_NAME.EmptyValue)
            {
                objEnv.Errors.AddWarning("Unable to connect to and retrieve information from QD3F db. QD3F_SERVER or QD3F_db_NAME is missing from QBCollection_Plus.BI_GLM_INSTALLATION table.", "");
                return;
            }
            MultiSqlExecutor executor = new MultiSqlExecutor();
            executor.AddSql("ADMIN_WRAPPER_SETTINGS", "SELECT * FROM ADMIN_WRAPPER_SETTINGS");
            try
            {
                DataSet dataSet = executor.Execute(Utils.GetConnectionString(objEnv.QBC.BI_GLM_INSTALLATION.QD3F_SERVER.Value, objEnv.QBC.BI_GLM_INSTALLATION.QD3F_db_NAME.Value));
                SetPropertiesValues(dataSet, objEnv, "ADMIN_WRAPPER_SETTINGS", QEnvPropCategory.QBC_D3F_Intermediate, QEnvPropSubCategory.ADMIN_WRAPPER_SETTINGS, cancelToken);
            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"QBC_D3F db error ({ex.Message})", "");
            }
        }

        private void LoadFromArchiveDB(QEnv objEnv, CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) return;

            if (objEnv.QBC.AT_SYSTEM_PARAMS.ARCHIVE_SERVER.EmptyValue || objEnv.QBC.AT_SYSTEM_PARAMS.ARCHIVE_DATABASE.EmptyValue)
            {
                objEnv.Errors.AddWarning("Unable to connect to and retrieve information from QBArchive db. QBCollection_Plus's AT_SYSTEM_PARAMS ARCHIVE_SERVER or ARCHIVE_DATABASE is empty", "");
                return;
            }
            MultiSqlExecutor executor = new MultiSqlExecutor();

            // BI_GLM_DARC_TEMPLATED_VALUES
            executor.AddSql("BI_GLM_DARC_TEMPLATED_VALUES", "SELECT * FROM BI_GLM_DARC_TEMPLATED_VALUES");

            // AT_SYSTEM_PARAMS
            executor.AddSql("BI_GLM_DARC_TEMPLATED_VALUES", "SELECT * FROM AT_SYSTEM_PARAMS WHERE SPRA_TYPE IN('QBC_NAME', 'QBC_SERVER', 'QBA_NAME', 'QBA_SERVER')");

            try
            {
                DataSet dataSet = executor.Execute(Utils.GetConnectionString(objEnv.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value, objEnv.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value));

                // set properties - QBArchive - BI_GLM_DARC_TEMPLATED_VALUES
                SetPropertiesValues(dataSet, objEnv, "BI_GLM_DARC_TEMPLATED_VALUES", QEnvPropCategory.QC_Archive, QEnvPropSubCategory.BI_GLM_DARC_TEMPLATED_VALUES, cancelToken);

                // set properties - QBArchive - AT_SYSTEM_PARAMS
                SetPropertiesValues(dataSet, objEnv, "AT_SYSTEM_PARAMS", QEnvPropCategory.QC_Archive, QEnvPropSubCategory.AT_SYSTEM_PARAMS, "SPRA_TYPE", "SPRA_VALUE", cancelToken);

            }
            catch (Exception ex)
            {
                objEnv.Errors.AddError($"QBArchive db error ({ex.Message})", "");
            }
        }


        /// <summary>
        /// Set properties values from table's first row.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objEnv"></param>
        /// <param name="tableName"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        private void SetPropertiesValues(DataSet dataSet, QEnv objEnv, string tableName, QEnvPropCategory category, QEnvPropSubCategory subCategory, CancellationToken cancelToken)
        {
            // set properties - QBCollection_Plus - TLK_DATABASE_VERSIONS
            QEnvProperty verProp;
            foreach (DataColumn col in dataSet.Tables[tableName].Columns)
            {
                verProp = objEnv.Properties.FirstOrDefault(p => p.Category == category && p.SubCategory == subCategory && p.Name == col.ColumnName);
                if (verProp != null)
                {
                    verProp.Value = dataSet.Tables[tableName].Rows[0][col].ToString();
                }
                if (cancelToken.IsCancellationRequested) break;
            }
        }

        /// <summary>
        /// Set properties from tables rows. Matches properties using propertyNameField.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objEnv"></param>
        /// <param name="tableName"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        /// <param name="propertyNameField"></param>
        /// <param name="propertyValueField"></param>
        private void SetPropertiesValues(DataSet dataSet, QEnv objEnv, string tableName, QEnvPropCategory category, QEnvPropSubCategory subCategory, string propertyNameField, string propertyValueField, CancellationToken cancelToken)
        {
            // set properties - QBCollection_Plus - AT_SYSTEM_PREF
            foreach (QEnvProperty prop in objEnv.Properties.Where(p => p.Category == category && p.SubCategory == subCategory).ToList())
            {
                DataRow[] result = dataSet.Tables[tableName].Select($"{propertyNameField}='{prop.Name}'");
                if (result.Length > 0)
                {
                    prop.Value = result[0][propertyValueField].ToString();
                }
                if (cancelToken.IsCancellationRequested) break;
            }
        }

        /// <summary>
        /// Adds properties.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objEnv"></param>
        /// <param name="tableName"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        /// <param name="propertyNameField"></param>
        /// <param name="propertyValueField"></param>
        private void AddOrUpdateProperties<T>(DataSet dataSet, QEnv objEnv, string tableName, QEnvPropCategory category, QEnvPropSubCategory subCategory, string propertyNameField, string propertyValueField, CancellationToken cancelToken) where T : QEnvProperty, new()
        {
            // set properties - QBCollection_Plus - External Companies
            foreach (DataRow row in dataSet.Tables[tableName].Rows)
            {
                T prop = objEnv.GetProperty<T>(category, subCategory, row[propertyNameField].ToString());
                if (prop == null)
                {
                    objEnv.Properties.Add(new T()
                    {
                        Category = category,
                        SubCategory = subCategory,
                        Name = row[propertyNameField].ToString(),
                        Value = row[propertyValueField].ToString(),
                    });
                }
                else
                {                    
                    prop.Value = row[propertyValueField].ToString();
                }
                if (cancelToken.IsCancellationRequested) break;
            }
        }

        private string GetSystemFolder(QEnv objEnv)
        {
            string retval = string.Empty;

            // get shared dirs from SYSTEM_PREF
            var sharedDirs = objEnv.Properties.Where(p => "WORDTEMPLATESFOLDER,ATTACHMENTS_DIRECTORY,BULK_OUTPUT_EXPORT_DIRECTORY,CRITERIA_PUBLISHED_PATH".Split(',').Contains(p.Name));

            // get first resolved path
            QEnvUNCDirProperty prop = (QEnvUNCDirProperty)sharedDirs.FirstOrDefault(s => (s as QEnvUNCDirProperty).Resolved);

            // set return value
            retval = Directory.GetParent($"\\\\{prop.Host}\\{prop.LocalPath.Replace(":", "$")}").FullName;

            return retval;
        }


        private List<string> GetFlowsForEodIni(DataSet dataSet, CancellationToken cancelToken)
        {
            List<string> retval = new List<string>();
            foreach (DataRow row in dataSet.Tables["Man_ClientFlows"].Rows)
            {
                retval.Add(row["EOFC_FLOW_NAME"].ToString());
                if (cancelToken.IsCancellationRequested) break;
            }
            return retval;
        }

        #endregion
    }
}
