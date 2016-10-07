using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QToolbar
{
   public static class Options
   {
      public const string TESTING_FOLDER_SETTING = "TestingFolder";
      public const string DESIGNERS_FOLDER_SETTING = "DesignersFolder";
      public const string QCSADMIN_FOLDER_SETTING = "QCSAdminFolder";
      public const string QCSAGENT_FOLDER_SETTING = "QCSAgentFolder";
      public const string SQL_FOLDER_SETTING = "SQLFolder";
      public const string EXECUTOR_CONFIGURATOR_FOLDER_SETTING = "ExecutorConfiguratorFolder";
      public const string DATABASE_SCRIPTER_FOLDER_SETTING = "DatabaseScripterFolder";
      public const string FIELDS_EXPLORER_FOLDER_SETTING = "FieldsExplorerFolder";


      public static string TestingFolder
      {
         get
         {
            return GetStringSetting(TESTING_FOLDER_SETTING);
         }
      }
      public static string DesignersFolder
      {
         get
         {
            return GetStringSetting(DESIGNERS_FOLDER_SETTING);
         }
      }
      public static string QCSAdminFolder
      {
         get
         {
            return GetStringSetting(QCSADMIN_FOLDER_SETTING);            
         }
      }
      public static string QCSAgentFolder
      {
         get
         {
            return GetStringSetting(QCSAGENT_FOLDER_SETTING);
         }
      }
      public static string SQLFolder
      {
         get
         {
            return GetStringSetting(SQL_FOLDER_SETTING);
         }
      }
      public static string ExecutorConfiguratorFolder
      {
         get
         {
            return GetStringSetting(EXECUTOR_CONFIGURATOR_FOLDER_SETTING);
         }
      }
      public static string DatabaseScripterFolder
      {
         get
         {
            return GetStringSetting(DATABASE_SCRIPTER_FOLDER_SETTING);
         }
      }
      public static string FieldsExplorerFolder
      {
         get
         {
            return GetStringSetting(FIELDS_EXPLORER_FOLDER_SETTING);
         }
      }

      #region helpers
      public static string GetStringSetting(string settingKey)
      {
         string retval = "";
         Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
         if (config.AppSettings.Settings.AllKeys.Contains(settingKey))
         {
            retval = config.AppSettings.Settings[settingKey].Value;
         }
         return retval;
      }

      public static bool GetBoolSetting(string settingKey)
      {
         bool retval = false;
         Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
         if (config.AppSettings.Settings.AllKeys.Contains(settingKey))
         {
            bool.TryParse(config.AppSettings.Settings[settingKey].Value, out retval);
         }
         return retval;
      }
      #endregion
   }
}
