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

namespace QToolbar
{
   public partial class Frm_Main : DevExpress.XtraEditors.XtraForm
   {
      private Blue.Windows.StickyWindow stickyWindow;
      public Frm_Main()
      {
         stickyWindow = new Blue.Windows.StickyWindow(this);
         InitializeComponent();

      }

      private void btnOptions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Frm_Options f = new Frm_Options();
         if (f.ShowDialog() == DialogResult.OK)
         {
            btnRefresh_ItemClick(null, null);
         }
      }

      #region initialize UI


      private void CreateDirMenuItems(string folder, BarSubItem menu, ItemClickEventHandler handler)
      {
         if (!string.IsNullOrWhiteSpace(folder))
         {
            if (Directory.Exists(folder))
            {
               try
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
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to retrieve subfloders of  {0}", folder));
               }
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
         mnuEnvironmentsConfiguration.ClearLinks();
         mnuNextBuild.ClearLinks();
         mnuInternalBuilds.ClearLinks();


      }

      private void InitUI()
      {
         Task.Run(() =>
         {
            // init designers menu
            CreateDirMenuItems(OptionsInstance.DesignersFolder, mnuDesigners, DesignerMenuItem_ItemClick);

            // init QCSAdmin CFs
            CreateDirMenuItems(OptionsInstance.QCSAdminFolder, mnuQCSAdminCFs, QCSAdminCFMenuItem_ItemClick);

            // init QCS Admin menu         
            CreateDirMenuItems(OptionsInstance.QCSAdminFolder, mnuQCSAdmin, QCSAdminMenuItem_ItemClick);

            // init QCS Agent menu  
            CreateDirMenuItems(OptionsInstance.QCSAgentFolder, mnuQCSAgent, QCSAgentMenuItem_ItemClick);

            // init Executor Configurator menu  
            CreateDirMenuItems(OptionsInstance.ExecutorConfiguratorFolder, mnuExecutorConfiguration, ExecutorConfiguration_ItemClick);

            // init Database scripter menu  
            CreateDirMenuItems(OptionsInstance.DatabaseScripterFolder, mnuDatabaseScripter, DatabaseScripter_ItemClick);

            // init Fields Explorer menu  
            CreateDirMenuItems(OptionsInstance.FieldsExplorerFolder, mnuFieldsExplorer, FieldsExplorer_ItemClick);

            // init sql menu
            CreateSQLMenu();

            // create folders menu
            CreateFoldersMenu();

            // Environments Configuration
            CreateDirMenuItems(OptionsInstance.EnvironmentsConfigurationFolder, mnuEnvironmentsConfiguration, EnvironmentsConfiguration_ItemClick);

            // Create Next Build menu items
            CreateNextBuildMenu();

            // Create Internal Builds menu
            CreateDirMenuItems(OptionsInstance.InternalBuildsFolder, mnuInternalBuilds, InternalBuilds_ItemClick);

         });
      }





      private void AddFolderItem(string folder)
      {
         BarButtonItem folderItem = new BarButtonItem(barManager1, folder, 0);
         folderItem.ItemClick += FolderItem_ItemClick;
         mnuFolders.AddItem(folderItem);
      }

      private void FolderItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            Process.Start(e.Item.Caption);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      #region NextBuild
      private void CreateNextBuildMenu()
      {
         // load custom folders
         try
         {
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               AddNextBuildItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void AddNextBuildItem(DataRow row)
      {
         BarButtonItem nextBuildItem = new BarButtonItem(barManager1, row["Name"].ToString(), 0);
         nextBuildItem.ItemClick += NextBuild_ItemClick;
         nextBuildItem.Tag = row;
         mnuNextBuild.AddItem(nextBuildItem);
      }

      private void NextBuild_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            DataRow row = (DataRow)e.Item.Tag;
            Frm_NextBuildChecker f = new Frm_NextBuildChecker();
            f.Show(row["Name"].ToString(), row["Path"].ToString());
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void AddInternalBuildsItem(DataRow row)
      {
         BarButtonItem internalBuildItem = new BarButtonItem(barManager1, row["Name"].ToString(), 0);
         internalBuildItem.ItemClick += InternalBuilds_ItemClick;
         internalBuildItem.Tag = row;
         mnuInternalBuilds.AddItem(internalBuildItem);
      }

      private void InternalBuilds_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string path = Path.Combine(OptionsInstance.InternalBuildsFolder, e.Item.Caption);
            Frm_InternalBuilds f = new Frm_InternalBuilds();
            f.Show(path);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      #endregion
      private void CreateFoldersMenu()
      {
         // load custom folders
         try
         {
            foreach (string folder in OptionsInstance.Folders)
            {
               AddFolderItem(folder);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void CreateSQLMenu()
      {
         string sqlFolder = OptionsInstance.SQLFolder;
         if (!string.IsNullOrEmpty(sqlFolder))
         {
            if (Directory.Exists(sqlFolder))
            {
               try
               {
                  List<string> dirs = Directory.GetDirectories(sqlFolder).ToList<string>();
                  dirs.Sort();
                  AddChildrenSQL(sqlFolder, mnuSQL);
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to get subfolders of sqlfolder {0}", sqlFolder));
               }
            }
         }
      }

      private void AddChildrenSQL(string sqlFolder, BarSubItem menuItem)
      {
         // add folders         
         if (Directory.Exists(sqlFolder))
         {
            List<string> dirs = Directory.GetDirectories(sqlFolder).ToList<string>();
            dirs.Sort();
            foreach (string dir in dirs)
            {
               BarSubItem subMenuItem = new BarSubItem(barManager1, Path.GetFileName(dir), 0);
               menuItem.AddItem(subMenuItem);
               AddChildrenSQL(dir, subMenuItem);
            }

            // add files
            List<string> files = Directory.GetFiles(sqlFolder).ToList<string>();
            files.Sort();
            foreach (string file in files)
            {
               BarButtonItem item = new BarButtonItem(barManager1, Path.GetFileName(file), 1);
               item.Tag = file;
               item.ItemClick += SQLMenuItem_ItemClick;
               menuItem.AddItem(item);
            }
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
            string qcsadmin = Path.Combine(OptionsInstance.QCSAdminFolder, e.Item.Caption, "QCS.Client.application");
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }



      private void QCSAgentMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            string qcsadmin = Path.Combine(OptionsInstance.QCSAgentFolder, e.Item.Caption, "CollectionAgentSystem.Client.application");
            System.Diagnostics.Process.Start(qcsadmin);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void DesignerMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {

         try
         {
            string designer = Path.Combine(OptionsInstance.DesignersFolder, e.Item.Caption, "SCToolkit.Designers.Client.application");
            System.Diagnostics.Process.Start(designer);
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void QCSAdminCFMenuItem_ItemClick(object sender, ItemClickEventArgs e)
      {
         string qcsAdminCFDir = Path.Combine(OptionsInstance.QCSAdminFolder, e.Item.Caption, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            string destDir = "";
            Version maxVersion = new Version(0,0,0,0);
            foreach (string dir in subdirs)
            {
               Version curVersion = Utils.GetVersion(dir, "_", 1);
               
               if (curVersion != null && maxVersion < curVersion)
               {
                     maxVersion = curVersion;
                     destDir = dir;
               }
               else
               {
                  XtraMessageBox.Show("Cannot parse directory name.");
               }
            }
            //List<string> sorted = subdirs.ToList<string>();
            //sorted.Sort();
            string file = Path.Combine(destDir, "QBC_Admin.cf.deploy");
            try
            {
               //Frm_FileViewer f = new Frm_FileViewer();
               //f.ViewFile(file);

               PowerfulSample f = new PowerfulSample();
               f.Size = new Size(800, 800);

               f.ViewFile(file, FastColoredTextBoxNS.Language.CSharp);
            }
            catch (Exception ex)
            {
               XtraMessageBox.Show(ex.Message);
            }
         }
      }

      private void EnvironmentsConfiguration_ItemClick(object sender, ItemClickEventArgs e)
      {
         string EnvDir = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, e.Item.Caption);
         if (Directory.Exists(EnvDir))
         {
            string file = Path.Combine(EnvDir, "EnvironmentsConfiguration.xml");
            try
            {
               //Frm_FileViewer f = new Frm_FileViewer();
               //f.ViewFile(file);
               PowerfulSample f = new PowerfulSample();
               f.Size = new Size(1200, 800);
               f.ViewFile(file, FastColoredTextBoxNS.Language.XML);

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
            btnRefresh_ItemClick(null, null);
         }
      }

      private void Frm_Main_Load(object sender, EventArgs e)
      {
         this.ClientSize = new Size(ribbonControl1.Width, ribbonControl1.Height);
         //this.FormBorderStyle = FormBorderStyle.FixedSingle;

         Location = Properties.Settings.Default.WindowLocation;
         Size = Properties.Settings.Default.WindowSize;
         EnsureVisible(this);
         InitUI();
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
            string app = Path.Combine(OptionsInstance.ExecutorConfiguratorFolder, e.Item.Caption, "QC.ExecutorConfigurator.application");
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
            string app = Path.Combine(OptionsInstance.DatabaseScripterFolder, e.Item.Caption, "QCSScript.application");
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
            string app = Path.Combine(OptionsInstance.FieldsExplorerFolder, e.Item.Caption, "SCToolkit.Utilities.FieldExplorer.application");
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
         if (!string.IsNullOrWhiteSpace(OptionsInstance.QCSAdminFolder))
         {
            if (Directory.Exists(OptionsInstance.QCSAdminFolder))
            {


               List<string> dirs = new List<string>(Directory.EnumerateDirectories(OptionsInstance.QCSAdminFolder));
               dirs = dirs.OrderByDescending(s => s).ToList<string>();
               Regex reg = new Regex("^(?<version>[0-9]+[.][0-9]+)");
               List<string> deletions = new List<string>();
               string newItem = "";
               foreach (string dir in dirs)
               {
                  Match match = reg.Match(Path.GetFileName(dir));
                  if (match.Success)
                  {
                     newItem = string.Format("del %temp%\\{0}*", match.Groups["version"]);
                     if (!deletions.Contains(newItem))
                     {
                        deletions.Add(newItem);
                     }
                  }
               }

               // create batch content
               StringBuilder builder = new StringBuilder();

               builder.AppendLine("del %temp%\\current*");
               foreach (var del in deletions)
               {
                  builder.AppendLine(del);
               }
               builder.AppendLine("del %userprofile%\\documents\\*.metadata");
               builder.AppendLine("del %userprofile%\\documents\\*.localization.dat");
               builder.AppendLine("del %TEMP%\\*.metadata");
               builder.AppendLine("del %TEMP%\\*localization.dat");
               builder.AppendLine("taskkill /IM iisexpress.exe");


               string FILE_NAME = Path.Combine(Path.GetTempPath(), "clear_metadata.bat");
               FileStream fs = null;
               try
               {
                  fs = new FileStream(FILE_NAME, FileMode.Create);
                  using (StreamWriter writer = new StreamWriter(fs))
                  {
                     writer.Write(builder.ToString());
                     writer.Close();
                  }
                  Process.Start(FILE_NAME);
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to clear metadata {0}", ex.Message));
               }
               finally
               {
                  if (fs != null)
                     fs.Dispose();
               }
            }
            else
            {
               XtraMessageBox.Show(string.Format("QVS Admin Folder \"{0}\" not found.", OptionsInstance.QCSAdminFolder));
            }
         }
      }
   }
}