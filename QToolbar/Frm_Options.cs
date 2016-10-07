using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.IO;

namespace QToolbar
{
   public partial class Frm_Options : DevExpress.XtraEditors.XtraForm
   {
      
      public Frm_Options()
      {
         InitializeComponent();
      }

      private void txtTestingFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
         {
            txtTestingFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }



      private void btnOK_Click(object sender, EventArgs e)
      {
         SaveSetting(Options.TESTING_FOLDER_SETTING, txtTestingFolder.Text);
         SaveSetting(Options.DESIGNERS_FOLDER_SETTING, txtDesignersFolder.Text);
         SaveSetting(Options.QCSADMIN_FOLDER_SETTING, txtQCSAdminFolder.Text);
         SaveSetting(Options.QCSAGENT_FOLDER_SETTING, txtQCSAgentFolder.Text);
         SaveSetting(Options.SQL_FOLDER_SETTING, txtSQLFolder.Text);
         SaveSetting(Options.EXECUTOR_CONFIGURATOR_FOLDER_SETTING, txtExecutorConfigurationFolder.Text);
         SaveSetting(Options.DATABASE_SCRIPTER_FOLDER_SETTING, txtDatabaseScripterFolder.Text);
         SaveSetting(Options.FIELDS_EXPLORER_FOLDER_SETTING, txtFieldsExplorerFolder.Text);
      }
      
      private void SaveSetting(string key, string value)
      {
         Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
         if (config.AppSettings.Settings.AllKeys.Contains(key))
         {
            config.AppSettings.Settings[key].Value = value;
         }
         else
         {
            config.AppSettings.Settings.Add(key, value);
         }         
         config.Save(ConfigurationSaveMode.Modified);
      }

      //private string ReadStringSetting(string key)
      //{
      //   string retval = ""; 
      //   Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
      //   if (config.AppSettings.Settings.AllKeys.Contains(key))
      //   {
      //      retval = config.AppSettings.Settings[key].Value;
      //   }
      //   return retval;
      //}

   private void Frm_Options_Load(object sender, EventArgs e)
      {

         txtTestingFolder.Text = Options.GetStringSetting(Options.TESTING_FOLDER_SETTING);
         txtDesignersFolder.Text = Options.GetStringSetting(Options.DESIGNERS_FOLDER_SETTING);
         txtQCSAdminFolder.Text = Options.GetStringSetting(Options.QCSADMIN_FOLDER_SETTING);
         txtQCSAgentFolder.Text = Options.GetStringSetting(Options.QCSAGENT_FOLDER_SETTING);
         txtSQLFolder.Text = Options.GetStringSetting(Options.SQL_FOLDER_SETTING);
         txtExecutorConfigurationFolder.Text = Options.GetStringSetting(Options.EXECUTOR_CONFIGURATOR_FOLDER_SETTING);
         txtDatabaseScripterFolder.Text= Options.GetStringSetting(Options.DATABASE_SCRIPTER_FOLDER_SETTING);
         txtFieldsExplorerFolder.Text = Options.GetStringSetting(Options.FIELDS_EXPLORER_FOLDER_SETTING);
      }

      private void txtExecutorConfigurationFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtExecutorConfigurationFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void txtSQLFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtSQLFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void txtQCSAgentFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtQCSAgentFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void txtQCSAdminFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtQCSAdminFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void txtDesignersFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtDesignersFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void btnDatabaseScripterFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {

      }

      private void txtDatabaseScripterFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtDatabaseScripterFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void txtFieldsExplorerFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtFieldsExplorerFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }
   }
}