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
using DevExpress.XtraBars;
using System.IO;
using System.Collections;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using System.Diagnostics;
using System.Text.RegularExpressions;
using QToolbar.Options;
using QToolbar.Tools;
using QToolbar.Buttons;
using QToolbar.Plugins.Environments;
using System.Threading;

namespace QToolbar
{
   public partial class Frm_Main : DevExpress.XtraEditors.XtraForm
   {

      List<Task> tasks = new List<Task>();

      #region Fields
      private DatabaseScripterButton _DatabaseScripterButton;
      private DesignersButton _DesignersButton;
      private EnvironmentsConfigurationButton _EnvironmentsConfigurationButton;
      private ExecutorConfiguratorButton _ExecutorConfiguratorButton;
      private FieldsExplorerButton _FieldsExplorerButton;
      private FoldersButton _FoldersButton;
      private InternalBuildsButton _InternalBuildsButton;
      private LegalLinksButton _LegalLinksButton;
      private NextBuildButton _NextBuildButton;
      private QCSAdminButton _QCSAdminButton;
      private QCSAdminCFsButton _QCSAdminCFsButton;
      private QCSAgentButton _QCSAgentButton;
      private ShellCommandsButton _ShellCommandsButton;
      private ClearMetadataButton _ClearMetadataButton;
      private DesignersLocalButton _DesignersLocalButton;
      private PluginsButton _PluginsButton;
      private SynchronizationContext _SyncContext;
      #endregion

      private Blue.Windows.StickyWindow stickyWindow;
      public Frm_Main()
      {
         stickyWindow = new Blue.Windows.StickyWindow(this);
         InitializeComponent();

         _SyncContext = SynchronizationContext.Current;
      }

      private void btnOptions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Frm_Options f = new Frm_Options();
         if (f.ShowDialog() == DialogResult.OK)
         {
            btnRefresh_ItemClick(null, null);
         }
      }

      private void InitUI()
      {
         _DatabaseScripterButton = new DatabaseScripterButton(barManager1, mnuDatabaseScripter);
         _DesignersButton = new DesignersButton(barManager1, mnuDesigners);
         _EnvironmentsConfigurationButton = new EnvironmentsConfigurationButton(barManager1, mnuEnvironmentsConfiguration);
         _ExecutorConfiguratorButton = new ExecutorConfiguratorButton(barManager1, mnuExecutorConfiguration);
         _FieldsExplorerButton = new FieldsExplorerButton(barManager1, mnuFieldsExplorer);
         _FoldersButton = new FoldersButton(barManager1, mnuFolders);
         _InternalBuildsButton = new InternalBuildsButton(barManager1, mnuInternalBuilds);
         _LegalLinksButton = new LegalLinksButton(barManager1, mnuLegalLinks);
         _NextBuildButton = new NextBuildButton(barManager1, mnuNextBuild);
         _QCSAdminButton = new QCSAdminButton(barManager1, mnuQCSAdmin);
         _QCSAdminCFsButton = new QCSAdminCFsButton(barManager1, mnuQCSAdminCFs);
         _QCSAgentButton = new QCSAgentButton(barManager1, mnuQCSAgent);
         _ShellCommandsButton = new ShellCommandsButton(barManager1, mnuShellCommands);
         _ClearMetadataButton = new ClearMetadataButton();
         _DesignersLocalButton = new DesignersLocalButton(barManager1, mnuDesignersLocal);
         _PluginsButton = new PluginsButton(barManager1, mnuPlugins);

      }

      private void CreateMenuItems()
      {
         Text = "QToolbar (Loading...)";
         ribbonControl1.Enabled = false;
         Task.Run(() =>
         {
            _DatabaseScripterButton.CreateMenuItems();
            _DesignersButton.CreateMenuItems();
            _EnvironmentsConfigurationButton.CreateMenuItems();
            _ExecutorConfiguratorButton.CreateMenuItems();
            _FieldsExplorerButton.CreateMenuItems();
            _FoldersButton.CreateMenuItems();
            _InternalBuildsButton.CreateMenuItems();
            _LegalLinksButton.CreateMenuItems();
            _NextBuildButton.CreateMenuItems();
            _QCSAdminButton.CreateMenuItems();
            _QCSAdminCFsButton.CreateMenuItems();
            _QCSAgentButton.CreateMenuItems();
            _ShellCommandsButton.CreateMenuItems();
            _DesignersLocalButton.CreateMenuItems();
            _PluginsButton.CreateMenuItems();
         }).ContinueWith((x) =>
         {
            _SyncContext.Post((input) =>
            {

               Text = "QToolbar (Ready)";
               ribbonControl1.Enabled = true;
            }, null);
         });
      }

      private async void CreateMenuItems2()
      {
            mnuDatabaseScripter.Enabled = false;
            tasks.Add(Task.Run(() => { _DatabaseScripterButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuDatabaseScripter.Enabled = true; }, null); }));

            mnuDesigners.Enabled = false;
            tasks.Add(Task.Run(() => { _DesignersButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuDesigners.Enabled = true; }, null); }));

            mnuEnvironmentsConfiguration.Enabled = false;
            tasks.Add(Task.Run(() => { _EnvironmentsConfigurationButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuEnvironmentsConfiguration.Enabled = true; }, null); }));

            mnuExecutorConfiguration.Enabled = false;
            tasks.Add(Task.Run(() => { _ExecutorConfiguratorButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuExecutorConfiguration.Enabled = true; }, null); }));

            mnuFieldsExplorer.Enabled = false;
            tasks.Add(Task.Run(() => { _FieldsExplorerButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuFieldsExplorer.Enabled = true; }, null); }));

            mnuFolders.Enabled = false;
            tasks.Add(Task.Run(() => { _FoldersButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuFolders.Enabled = true; }, null); }));

            mnuInternalBuilds.Enabled = false;
            tasks.Add(Task.Run(() => { _InternalBuildsButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuInternalBuilds.Enabled = true; }, null); }));

            mnuLegalLinks.Enabled = false;
            tasks.Add(Task.Run(() => { _LegalLinksButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuLegalLinks.Enabled = true; }, null); }));

            mnuShellCommands.Enabled = false;
            tasks.Add(Task.Run(() => { _ShellCommandsButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuShellCommands.Enabled = true; }, null); }));

            mnuPlugins.Enabled = false;
            tasks.Add(Task.Run(() => { _PluginsButton.CreateMenuItems(); }).ContinueWith((x) => { _SyncContext.Post((input) => { mnuPlugins.Enabled = true; }, null); }));

            // those tasks were put together to avoid disposing errors
            mnuNextBuild.Enabled = false;
            mnuQCSAdmin.Enabled = false;
            mnuQCSAdminCFs.Enabled = false;
            mnuQCSAgent.Enabled = false;
            mnuDesignersLocal.Enabled = false;

            tasks.Add(Task.Run(() => {
               _NextBuildButton.CreateMenuItems();
               _QCSAdminButton.CreateMenuItems();
               _QCSAdminCFsButton.CreateMenuItems();
               _QCSAgentButton.CreateMenuItems();
               _DesignersLocalButton.CreateMenuItems();

            }).ContinueWith((x) => {
               _SyncContext.Post((input) => {
                  mnuNextBuild.Enabled = true;
                  mnuQCSAdmin.Enabled = true;
                  mnuQCSAdminCFs.Enabled = true;
                  mnuQCSAgent.Enabled = true;
                  mnuDesignersLocal.Enabled = true;
               }, null);
            }));
           
      }



      private void SetRibbonImages()
      {
         
      }


      private void btnOptions2_ItemClick(object sender, ItemClickEventArgs e)
      {
         Frm_Options f = new Frm_Options();
         if (f.ShowDialog() == DialogResult.OK)
         {
            btnRefresh_ItemClick(null, null);
         }
      }

      private void Frm_Main_Load(object sender, EventArgs e)
      {
         this.ClientSize = ribbonControl1.Size;         
         //this.FormBorderStyle = FormBorderStyle.FixedSingle;

         Location = Properties.Settings.Default.WindowLocation;         
         Size = Properties.Settings.Default.WindowSize;
         EnsureVisible(this);
         InitUI();
         CreateMenuItems();
      }

      private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
      {
         ClearAppInstance();
         InitUI();
         CreateMenuItems();
      }


      private void ClearAppInstance()
      {
         
      }

      private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
      {
         Properties.Settings.Default.WindowLocation = Location;
         Properties.Settings.Default.WindowSize = Size;
         Properties.Settings.Default.Save();
      }

      private void EnsureVisible(Control ctrl)
      {
         Rectangle ctrlRect = ctrl.DisplayRectangle; //The dimensions of the ctrl
         ctrlRect.Y = ctrl.Top; //Add in the real Top and Left Vals
         ctrlRect.X = ctrl.Left;
         Rectangle screenRect = Screen.GetWorkingArea(ctrl); //The Working Area fo the screen showing most of the Ctrl

         //Now tweak the ctrl's Top and Left until it's fully visible. 
         ctrl.Left += Math.Min(0, screenRect.Left + screenRect.Width - ctrl.Left - ctrl.Width);
         ctrl.Left -= Math.Min(0, ctrl.Left - screenRect.Left);
         ctrl.Top += Math.Min(0, screenRect.Top + screenRect.Height - ctrl.Top - ctrl.Height);
         ctrl.Top -= Math.Min(0, ctrl.Top - screenRect.Top);

      }

      private void btnClearMetadata_ItemClick(object sender, ItemClickEventArgs e)
      {
         _ClearMetadataButton.ClearMetadata();
      }

      private void btnSQL_ItemClick(object sender, ItemClickEventArgs e)
      {
         Frm_SQLQueries f = new Frm_SQLQueries();
         f.Show();
      }

      private void Frm_Main_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
      }

      private void btnCreateNewEnvironment_ItemClick(object sender, ItemClickEventArgs e)
      {
         Frm_CreateEnvironment f = new Frm_CreateEnvironment();
         f.Show();
      }

      private void btnCriteriaScripter_ItemClick(object sender, ItemClickEventArgs e)
      {
         Frm_ScriptCriteria f = new Frm_ScriptCriteria();
         f.Show();
      }

      private void btnCloseAllWindows_ItemClick(object sender, ItemClickEventArgs e)
      {
         Form[] formsList = Application.OpenForms.Cast<Form>().Where(x => x.Name != this.Name).ToArray();
         foreach (Form openForm in formsList)
         {
            openForm.Close();
         }
      }

      private void ribbonControl1_SizeChanged(object sender, EventArgs e)
      {
         this.ClientSize = ribbonControl1.Size;
      }
   }
}