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

namespace QToolbar
{
   public partial class Frm_Main : DevExpress.XtraEditors.XtraForm
   {
      private Blue.Windows.StickyWindow stickyWindow;
      public Frm_Main()
      {
         stickyWindow = new Blue.Windows.StickyWindow(this);
         InitializeComponent();
         InitUI();
      }

      private void btnOptions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Frm_Options f = new Frm_Options();
         f.ShowDialog();
      }

      #region initialize UI


      private void CreateDirMenuItems(string folder, BarSubItem menu, ItemClickEventHandler handler)
      {
         if (!string.IsNullOrWhiteSpace(folder))
         {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(folder));
            dirs = dirs.OrderByDescending(s => s).ToList<string>();
            foreach (string dir in dirs)
            {
               BarButtonItem menuItem = new BarButtonItem(barManager1, Path.GetFileName(dir));
               menuItem.ItemClick += handler;               
               menu.AddItem(menuItem);               
            }
         }
      }

      private void ClearUI()
      {
         mnuDesigners.ClearLinks();
         mnuQCSAdminCFs.ClearLinks();
         mnuQCSAdmin.ClearLinks();
         mnuQCSAgent.ClearLinks();
         mnuSQL.ClearLinks();
         mnuExecutorConfiguration.ClearLinks();
         mnuFolders.ClearLinks();
         mnuDatabaseScripter.ClearLinks();
         mnuFieldsExplorer.ClearLinks();
         
      }

      private void InitUI()
      {
         Task.Run(() => { 
            // init designers menu
            CreateDirMenuItems(Options.DesignersFolder, mnuDesigners, DesignerMenuItem_ItemClick);

            // init QCSAdmin CFs
            CreateDirMenuItems(Options.QCSAdminFolder, mnuQCSAdminCFs, QCSAdminCFMenuItem_ItemClick);

            // init QCS Admin menu         
            CreateDirMenuItems(Options.QCSAdminFolder, mnuQCSAdmin, QCSAdminMenuItem_ItemClick);

            // init QCS Agent menu  
            CreateDirMenuItems(Options.QCSAgentFolder, mnuQCSAgent, QCSAgentMenuItem_ItemClick);

            // init Executor Configurator menu  
            CreateDirMenuItems(Options.ExecutorConfiguratorFolder, mnuExecutorConfiguration,  ExecutorConfiguration_ItemClick);

            // init sql menu
            CreateSQLMenu();

            // create folders menu
            CreateFoldersMenu();

            // init Database scripter menu  
            CreateDirMenuItems(Options.DatabaseScripterFolder, mnuDatabaseScripter, DatabaseScripter_ItemClick);

            // init Fields Explorer menu  
            CreateDirMenuItems(Options.FieldsExplorerFolder, mnuFieldsExplorer, FieldsExplorer_ItemClick);

         });
      }

      private void AddFolderItem(string folder)
      {
         BarButtonItem folderItem = new BarButtonItem(barManager1, folder,0);
         folderItem.ItemClick += FolderItem_ItemClick;
         mnuFolders.AddItem(folderItem);
      }

      private void FolderItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            Process.Start(e.Item.Caption);
         }
         catch(Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void CreateFoldersMenu()
      {
         // load standard folders
         //AddFolderItem(Utils.DesignersFolder);
         //AddFolderItem(Utils.QCSAdminFolder);
         //AddFolderItem(Utils.QCSAgentFolder);
         //AddFolderItem(Utils.ExecutorConfiguratorFolder);
         //AddFolderItem(Utils.TestingFolder);

         // load custom folders
         Dictionary<string,string> custFolders = Utils.GetSectionItems("folders");
         foreach(string folder in custFolders.Values)
         {
            AddFolderItem(folder);
         }
      }

      private void CreateSQLMenu()
      {
         string sqlFolder = Options.SQLFolder;
         if (!string.IsNullOrEmpty(sqlFolder))
         {
            List<string> dirs = Directory.GetDirectories(sqlFolder).ToList<string>();
            dirs.Sort();
            AddChildrenSQL(sqlFolder, mnuSQL);
         }
      }

      private void AddChildrenSQL(string sqlFolder, BarSubItem menuItem)
      {
         // add folders         
         List<string> dirs = Directory.GetDirectories(sqlFolder).ToList<string>();
         dirs.Sort();
         foreach (string dir in dirs)
         {
            BarSubItem subMenuItem = new BarSubItem(barManager1, Path.GetFileName(dir),0);
            menuItem.AddItem(subMenuItem);
            AddChildrenSQL(dir, subMenuItem);
         }
         // add files
         List<string> files = Directory.GetFiles(sqlFolder).ToList<string>();
         files.Sort();
         foreach (string file in files)
         {
            BarButtonItem item = new BarButtonItem(barManager1, Path.GetFileName(file),1);
            item.Tag = file;
            item.ItemClick += SQLMenuItem_ItemClick;
            menuItem.AddItem(item);            
         }
      }

      private void SQLMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         if (e.Item is BarButtonItem && !string.IsNullOrEmpty(Convert.ToString(e.Item.Tag)))
         {
            string sqlFile = Convert.ToString(e.Item.Tag);
            if (File.Exists(sqlFile))
            {
               try
               {
                  string content = File.ReadAllText(sqlFile);
                  Clipboard.SetText(content);
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(ex.Message);
               }
            }
         }
      }


      private void QCSAdminMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string qcsadmin = Path.Combine(Options.QCSAdminFolder, e.Item.Caption, "QCS.Client.application");
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch(Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }



      private void QCSAgentMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         { 
            string qcsadmin = Path.Combine(Options.QCSAgentFolder, e.Item.Caption, "CollectionAgentSystem.Client.application");
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch(Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void DesignerMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {

         try
         {
            string designer = Path.Combine(Options.DesignersFolder, e.Item.Caption, "SCToolkit.Designers.Client.application");
            System.Diagnostics.Process.Start(designer);
         }
         catch(Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void QCSAdminCFMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         string qcsAdminCFDir = Path.Combine(Options.QCSAdminFolder, e.Item.Caption, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            int maxRelease = -1;
            string destDir = "";
            foreach(string dir in subdirs)
            {
               string[] splitted= dir.Split('_');
               int release = -1;
               if (splitted.Length == 5 && int.TryParse(splitted[3], out release))
               {                  
                  if (release > maxRelease)
                  {
                     maxRelease = release;
                     destDir = dir;
                  }
               }
               else
               {
                  XtraMessageBox.Show("Cannot parse directory name.");
               }
            }
            //List<string> sorted = subdirs.ToList<string>();
            //sorted.Sort();
            string file=Path.Combine(destDir, "QBC_Admin.cf.deploy");
            try
            {
               Frm_FileViewer f = new Frm_FileViewer();
               f.ViewFile(file);
            }
            catch (Exception ex)
            {
               XtraMessageBox.Show(ex.Message);
            }
         }
      }
      #endregion

      private void btnOptions2_ItemClick(object sender, ItemClickEventArgs e)
      {
         Frm_Options f = new Frm_Options();
         if (f.ShowDialog() == DialogResult.OK)
         {
            
         }
      }

      private void Frm_Main_Load(object sender, EventArgs e)
      {
         this.ClientSize = new Size(ribbonControl1.Width,ribbonControl1.Height);
         //this.FormBorderStyle = FormBorderStyle.FixedSingle;

         Location = Properties.Settings.Default.WindowLocation;
         Size = Properties.Settings.Default.WindowSize;
      }

      private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
      {
         ClearUI();
         InitUI();
      }

      private void ExecutorConfiguration_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = Path.Combine(Options.ExecutorConfiguratorFolder, e.Item.Caption, "QC.ExecutorConfigurator.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void DatabaseScripter_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = Path.Combine(Options.DatabaseScripterFolder, e.Item.Caption, "QCSScript.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void FieldsExplorer_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string app = Path.Combine(Options.FieldsExplorerFolder, e.Item.Caption, "SCToolkit.Utilities.FieldExplorer.application");
            System.Diagnostics.Process.Start(app);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }


      private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
      {
         Properties.Settings.Default.WindowLocation = Location;
         Properties.Settings.Default.WindowSize = Size;
         Properties.Settings.Default.Save();
      }
   }
}