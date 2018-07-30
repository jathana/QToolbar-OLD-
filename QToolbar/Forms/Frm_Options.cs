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
using System.Collections.Specialized;
using System.Collections;
using System.Xml;
using QToolbar.Options;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

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
         Properties.Settings.Default.TestingFolder =  txtTestingFolder.Text;
         Properties.Settings.Default.DesignersFolder = txtDesignersFolder.Text;
         Properties.Settings.Default.QCSAdminFolder = txtQCSAdminFolder.Text;
         Properties.Settings.Default.QCSAgentFolder = txtQCSAgentFolder.Text;
         Properties.Settings.Default.ExecutorConfiguratorFolder = txtExecutorConfigurationFolder.Text;
         Properties.Settings.Default.DatabaseScripterFolder = txtDatabaseScripterFolder.Text;
         Properties.Settings.Default.FieldsExplorerFolder = txtFieldsExplorerFolder.Text;
         Properties.Settings.Default.EnvironmentsConfigurationFolder = txtEnvironmentsConfiguration.Text;
         Properties.Settings.Default.InternalBuildsFolder = txtInternalBuildsFolder.Text;
         Properties.Settings.Default.ClearLegalMetadata = chkClearLegalMetadata.Checked;
         Properties.Settings.Default.DevSQLInstances = txtDevSQLInstances.Text;

         // save folders
         SaveFolders();

         SaveCheckouts();

         SaveShellCommands();

         // save databases
         SaveLegalLinks();

         // save sql queries
         SaveSQLQueries();

         // save env cfs
         SaveEnvCFs();

         Properties.Settings.Default.Save();
      }

      private void Frm_Options_Load(object sender, EventArgs e)
      {

         txtTestingFolder.Text = Properties.Settings.Default.TestingFolder;
         txtDesignersFolder.Text = Properties.Settings.Default.DesignersFolder;
         txtQCSAdminFolder.Text = Properties.Settings.Default.QCSAdminFolder;
         txtQCSAgentFolder.Text = Properties.Settings.Default.QCSAgentFolder;
         txtExecutorConfigurationFolder.Text = Properties.Settings.Default.ExecutorConfiguratorFolder;
         txtDatabaseScripterFolder.Text= Properties.Settings.Default.DatabaseScripterFolder;
         txtFieldsExplorerFolder.Text = Properties.Settings.Default.FieldsExplorerFolder;
         txtEnvironmentsConfiguration.Text = Properties.Settings.Default.EnvironmentsConfigurationFolder;
         txtInternalBuildsFolder.Text = Properties.Settings.Default.InternalBuildsFolder;
         chkClearLegalMetadata.Checked = Properties.Settings.Default.ClearLegalMetadata;
         txtDevSQLInstances.Text = Properties.Settings.Default.DevSQLInstances;

         // custom edit form for sql queries
         gviewSQLQueries.OptionsEditForm.CustomEditFormLayout = new uc_SQLQueriesEdit();

         LoadFolders();
         LoadCheckouts();
         LoadShellCommands();
         LoadLegalLinks();
         LoadSQLQueries();
         LoadEnvCFs();
      }


      private void LoadCheckouts()
      {
         Checkouts checkouts = new Checkouts(Properties.Settings.Default.Checkouts);
         gridCheckouts.DataSource = checkouts.Data;
      }

      private void SaveCheckouts()
      {
         Checkouts checkouts = new Checkouts((DataTable)gridCheckouts.DataSource);         
         Properties.Settings.Default.Checkouts = checkouts.ToXml();
      }

      private void LoadShellCommands()
      {
         ShellCommands shellCommands = new ShellCommands(Properties.Settings.Default.ShellCommands);
         grdShellCommands.DataSource = shellCommands.Data;
      }

      private void LoadLegalLinks()
      {
         ShellCommands legalLinks = new ShellCommands(Properties.Settings.Default.LegalLinks);
         gridLegalLiks.DataSource = legalLinks.Data;
      }

      private void LoadSQLQueries()
      {
         SQLQueries sqlQueries = new SQLQueries(Properties.Settings.Default.SQLQueries);
         gridSQLQueries.DataSource = sqlQueries.Data;
      }

      private void SaveShellCommands()
      {
         ShellCommands shellCommands = new ShellCommands((DataTable)grdShellCommands.DataSource);
         Properties.Settings.Default.ShellCommands = shellCommands.ToXml();
      }

      private void SaveLegalLinks()
      {
         ShellCommands legalLinks = new ShellCommands((DataTable)gridLegalLiks.DataSource);
         Properties.Settings.Default.LegalLinks = legalLinks.ToXml();
      }

      private void SaveSQLQueries()
      {
         SQLQueries sqlQueries = new SQLQueries((DataTable)gridSQLQueries.DataSource);
         Properties.Settings.Default.SQLQueries = sqlQueries.ToXml();
      }

      private void LoadEnvCFs()
      {
         EnvCFs envCFs = new EnvCFs(Properties.Settings.Default.EnvCFs);
         gridEnvCFs.DataSource = envCFs.Data;
      }

      private void SaveEnvCFs()
      {
         EnvCFs envCFs = new EnvCFs((DataTable)gridEnvCFs.DataSource);
         Properties.Settings.Default.EnvCFs = envCFs.ToXml();
      }

      private void LoadFolders()
      {
         StringCollection col = (StringCollection)Properties.Settings.Default.Folders;
         StringBuilder builder = new StringBuilder();
         bool first = true;
         foreach (string s in col)
         {
            if(!first) builder.Append("\r\n");
            builder.Append(s);
            if (first) first = false;
         }
         memFolders.Text = builder.ToString();
      }

      private void SaveFolders()
      {
         string[] folders = memFolders.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
         StringCollection  col = new StringCollection();
         col.AddRange(folders);
         Properties.Settings.Default.Folders = col;
      }

      private void txtExecutorConfigurationFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtExecutorConfigurationFolder.Text = folderBrowserDialog1.SelectedPath;
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

      private void txtEnvironmentsConfiguration_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtEnvironmentsConfiguration.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void Grid_ProcessGridKey(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
         {
            if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
              DialogResult.Yes)
               return;
            GridView view = ((GridControl)sender).DefaultView as GridView;
            view.DeleteRow(view.FocusedRowHandle);
         }
      }

      private void txtInternalBuildsFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
      {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
         {
            txtInternalBuildsFolder.Text = folderBrowserDialog1.SelectedPath;
         }
      }

      private void gviewSQLQueries_ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
      {
         e.EditForm.StartPosition = FormStartPosition.CenterScreen;         
      }

      private void gviewSQLQueries_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
      {
         e.Panel.Controls[0].Controls[0].Dock = DockStyle.Fill;
      }
   }
}