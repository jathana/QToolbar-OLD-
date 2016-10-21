using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            return Properties.Settings.Default.TestingFolder;
         }
      }
      public static string DesignersFolder
      {
         get
         {
            return Properties.Settings.Default.DesignersFolder;
         }
      }
      public static string QCSAdminFolder
      {
         get
         {
            return Properties.Settings.Default.QCSAdminFolder;
         }
      }
      public static string QCSAgentFolder
      {
         get
         {
            return Properties.Settings.Default.QCSAgentFolder;
         }
      }
      public static string SQLFolder
      {
         get
         {
            return Properties.Settings.Default.SQLFolder;
         }
      }
      public static string ExecutorConfiguratorFolder
      {
         get
         {
            return Properties.Settings.Default.ExecutorConfiguratorFolder;
         }
      }
      public static string DatabaseScripterFolder
      {
         get
         {
            return Properties.Settings.Default.DatabaseScripterFolder;
         }
      }
      public static string FieldsExplorerFolder
      {
         get
         {
            return Properties.Settings.Default.FieldsExplorerFolder;
         }
      }

      public static string EnvironmentsConfigurationFolder
      {
         get
         {
            return Properties.Settings.Default.EnvironmentsConfigurationFolder;
         }
      }

      public static StringCollection Folders
      {
         get
         {
            return (StringCollection)Properties.Settings.Default.Folders;
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
