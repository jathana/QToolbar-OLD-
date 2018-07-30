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

namespace QToolbar.Options
{
   public static class OptionsInstance
   {

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

      public static string DevSQLInstances
      {
         get
         {
            return Properties.Settings.Default.DevSQLInstances;
         }
      }


      public static StringCollection Folders
      {
         get
         {
            return (StringCollection)Properties.Settings.Default.Folders;
         }
      }

      public static string InternalBuildsFolder
      {
         get
         {
            return Properties.Settings.Default.InternalBuildsFolder;
         }
      }

      public static Checkouts Checkouts
      {
         get
         {
            return new Checkouts(Properties.Settings.Default.Checkouts);
         }
      }

      public static ShellCommands ShellCommands
      {
         get
         {
            return new ShellCommands(Properties.Settings.Default.ShellCommands);
         }
      }

      public static SQLQueries SQLQueries
      {
         get
         {
            return new SQLQueries(Properties.Settings.Default.SQLQueries);
         }
      }

      public static ShellCommands LegalLinks
      {
         get
         {
            return new ShellCommands(Properties.Settings.Default.LegalLinks);
         }
      }

      public static bool ClearLegalMetadata
      {
         get
         {
            return Properties.Settings.Default.ClearLegalMetadata;
         }
      }

      public static EnvCFs EnvCFs
      {
         get
         {
            return new EnvCFs(Properties.Settings.Default.EnvCFs);
         }
      }


   }
}
