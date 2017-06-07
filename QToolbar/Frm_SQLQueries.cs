﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using QToolbar.Options;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraTreeList;
using System.Threading;
using QToolbar.Tools;

namespace QToolbar
{
   public partial class Frm_SQLQueries : DevExpress.XtraEditors.XtraForm
   {
      TreeNode<ConnectionInfo> tree = new TreeNode<ConnectionInfo>();


      public Frm_SQLQueries()
      {
         InitializeComponent();

         backgroundWorker1.WorkerSupportsCancellation = true;
         backgroundWorker1.WorkerReportsProgress = true;
         treeDatabases.OptionsBehavior.Editable = false;
         treeDatabases.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
         treeDatabases.OptionsView.EnableAppearanceEvenRow = true;
         treeDatabases.OptionsView.EnableAppearanceOddRow = true;

         //treeDatabases.OptionsView.ShowColumns = false;
         
         
      }


      private void LoadDatabases()
      {
            // if cache file is present load from cache
            if (File.Exists(AppInstance.CFsTreeCacheFile))
            {
               TreeNodeSerializer<ConnectionInfo> ser = new TreeNodeSerializer<ConnectionInfo>();
               TreeNode<ConnectionInfo> tree = ser.Deserialize(AppInstance.CFsTreeCacheFile);
               PopulateDBTree(tree);
            }
            else
            {
               btnAdd.Enabled = false;
               treeDatabases.ClearNodes();
               treeDatabases.Cursor = Cursors.WaitCursor;
               EnableUI(false);
               // get all databases from cf
               backgroundWorker1.RunWorkerAsync();
            }
      }

      private void Frm_SQLQueries_Load(object sender, EventArgs e)
      {
                  
         LoadDatabases();
         
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         // read all cf
         string folder = OptionsInstance.QCSAdminFolder;
         if (!string.IsNullOrWhiteSpace(folder))
         {
            if (Directory.Exists(folder))
            {
               try
               {
                  tree = new TreeNode<ConnectionInfo>();
                  List<string> dirs = new List<string>(Directory.EnumerateDirectories(folder));
                  dirs = dirs.OrderByDescending(s => s).ToList<string>();
                  foreach (string dir in dirs)
                  {
                     TreeNode<ConnectionInfo> verNode = new TreeNode<ConnectionInfo>();
                     verNode.Data.CFPath = dir;
                     verNode.Data.InfoType = InfoType.Version;
                     verNode.Parent = tree;
                     tree.AddChild(verNode);
                     // parse cf and get dbs
                     List<ConnectionInfo> dbs = GetCFDBs(Path.GetFileName(dir));

                     var servers=dbs.GroupBy(i => i.Server).ToList();
                     foreach (var server in servers)
                     {
                                               
                        List<ConnectionInfo> serverItems = server.ToList<ConnectionInfo>();
                        TreeNode<ConnectionInfo> serverNode = new TreeNode<ConnectionInfo>();
                        serverNode.Data.Server = server.Key;
                        serverNode.Parent = verNode;
                        serverNode.Data.InfoType = InfoType.Server;
                        verNode.AddChild(serverNode);

                        foreach (ConnectionInfo item in serverItems)
                        {
                           TreeNode<ConnectionInfo> child = new TreeNode<ConnectionInfo>();
                           child.Data = item;
                           child.Data.InfoType = InfoType.Database;
                           child.Parent = serverNode;
                           serverNode.AddChild(child);
                        }
                     }
                  }
                  e.Result = tree;
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to retrieve subfolders of  {0}", folder));
               }
            }
         }

      }

      private List<ConnectionInfo> GetCFDBs(string VerDirName)
      {
         List<ConnectionInfo> retVal = new List<ConnectionInfo>();

         string qcsAdminCFDir = Path.Combine(OptionsInstance.QCSAdminFolder, VerDirName, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            string destDir = "";
            Version maxVersion = new Version(0, 0, 0, 0);
            foreach (string dir in subdirs)
            {
               Version curVersion = Utils.GetVersion(dir, "_", 1);

               if (curVersion != null)
               {
                  if (maxVersion < curVersion)
                  {
                     maxVersion = curVersion;
                     destDir = dir;
                  }
               }
               else
               {
                  XtraMessageBox.Show("Cannot parse directory name.");
               }
            }
            string file = Path.Combine(destDir, "QBC_Admin.cf.deploy");
            IniFile cfFile = new IniFile(file, "#");
            Dictionary<string, string> servers = cfFile.GetSectionPairs("[servers]");
            Dictionary<string, string> dbnames = cfFile.GetSectionPairs("[databasename]");
            if (servers.Count != dbnames.Count)
            {
               throw new Exception($"[Servers] and [DatabaseName] sections of {file} contain different number of items.");
            }
            else
            {
               foreach (KeyValuePair<string, string> server in servers)
               {
                  ConnectionInfo info = new ConnectionInfo()
                  {
                     Environment = server.Key,
                     Server = server.Value,
                     Database = dbnames[server.Key],
                     CFPath = destDir
                  };
                  retVal.Add(info);
               }
            }
         }
         return retVal;
      }


      private void EnableUI(bool enable)
      {
         txtFilter.Enabled = enable;
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         treeDatabases.ClearNodes();

          
         TreeNode<ConnectionInfo> tree = (TreeNode<ConnectionInfo>)e.Result;
         PopulateDBTree(tree);
         //AppInstance.CFDatabasesTree = tree;
         TreeNodeSerializer<ConnectionInfo> ser = new TreeNodeSerializer<ConnectionInfo>();
         ser.Serialize(tree, AppInstance.CFsTreeCacheFile);
         btnAdd.Enabled = true;         
         EnableUI(true);
         treeDatabases.Cursor = Cursors.Default;
      }

      private void PopulateDBTree(TreeNode<ConnectionInfo> tree)
      {
         treeDatabases.DataSource = tree;
         treeDatabases.ExpandAll();
         treeDatabases.BestFitColumns();
         
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {

      }

      private void treeDatabases_VirtualTreeGetChildNodes(object sender, DevExpress.XtraTreeList.VirtualTreeGetChildNodesInfo e)
      {
         if (e.Node is TreeNode<ConnectionInfo>)
         {
            e.Children = ((TreeNode<ConnectionInfo>)e.Node).Children;
         }
      }

      private void treeDatabases_VirtualTreeGetCellValue(object sender, DevExpress.XtraTreeList.VirtualTreeGetCellValueInfo e)
      {
         TreeNode<ConnectionInfo> node = (TreeNode<ConnectionInfo>)e.Node;

         if (e.Column.Caption == "Name")
         {
            if (node.Data.InfoType == InfoType.Database)
            {
               e.CellData = node.Data.Database;
            }
            else if (node.Data.InfoType == InfoType.Server)
            {
               e.CellData = ((TreeNode<ConnectionInfo>)e.Node).Data.Server;
            }
            else if (node.Data.InfoType == InfoType.Version)
            {
               e.CellData = ((TreeNode<ConnectionInfo>)e.Node).Data.Version;
            }
         }
         else if (e.Column.Caption == "Server")
         {
            if (node.Data.InfoType == InfoType.Server)
            {
               e.CellData = node.Data.Server;
            }
         }
         else if (e.Column.Caption == "Database")
         {
            if (node.Data.InfoType == InfoType.Database)
            {
               e.CellData = node.Data.Database;
            }

         }

      }

      private void treeDatabases_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
      {
         if (e.Menu is TreeListNodeMenu)
         {

            treeDatabases.FocusedNode = ((TreeListNodeMenu)e.Menu).Node;
            TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
            if (obj.Data.InfoType == InfoType.Database)
            {
               foreach (DataRow query in OptionsInstance.SQLQueries.Data.Rows)
               {
                  DevExpress.Utils.Menu.DXMenuItem mnuItem = new DevExpress.Utils.Menu.DXMenuItem(query["Name"].ToString(), query_ItemClick);
                  mnuItem.Tag = query;

                  e.Menu.Items.Add(mnuItem);
               }


               // script criteria
               DevExpress.Utils.Menu.DXMenuItem mnuItemScriptCriteria = new DevExpress.Utils.Menu.DXMenuItem("Script Criteria", scriptCriteria_ItemClick);
               mnuItemScriptCriteria.Tag = obj.Data;
               e.Menu.Items.Add(mnuItemScriptCriteria);

            }


         }
      }

      private void query_ItemClick(object sender, EventArgs e)
      {         
          
         StringBuilder builder = new StringBuilder();
         DevExpress.Utils.Menu.DXMenuItem mnuItem = (DevExpress.Utils.Menu.DXMenuItem)sender;
         uc_SQL ctr = new uc_SQL();

         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         DataRow query = (DataRow)mnuItem.Tag;
         // sql text
         builder.AppendLine($"--       Query : {mnuItem.Caption}");
         builder.AppendLine($"--      Server : {data.Server}");
         builder.AppendLine($"--    Database : {data.Database}");
         builder.AppendLine($"--     Version : {data.Version}");
         builder.AppendLine($"-- Environment : {data.Environment}");
         builder.AppendLine();
         builder.AppendLine(query["SQL"].ToString());
         // sql control
         ctr.Query = builder.ToString();
         ctr.Server = data.Server;
         ctr.Database = data.Database;
         ctr.QueryName = mnuItem.Caption;
         ctr.Initialize();

         if ( query["RunImmediate"] != DBNull.Value && Convert.ToBoolean(query["RunImmediate"])==true)
         {
            ctr.Run();
         }
         // tab 
         DevExpress.XtraBars.Docking2010.Views.Tabbed.Document doc=  (DevExpress.XtraBars.Docking2010.Views.Tabbed.Document)documentManager1.View.AddDocument(ctr);
         tabbedView1.Controller.Select(doc);
         doc.Caption = ctr.Database;
         

      }


      private void scriptCriteria_ItemClick(object sender, EventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_ScriptCriteria f = new Frm_ScriptCriteria();
         f.Show(data);
      }

      private void treeDatabases_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(e.Node);
         ConnectionInfo data = obj.Data;
         switch(data.InfoType)
         {
            case InfoType.Database: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("db");break;
            case InfoType.Server: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("server"); break;
            case InfoType.Version: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("env"); break;

         }
      }

      private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         File.Delete(AppInstance.CFsTreeCacheFile);
         LoadDatabases();
      }

      private void treeDatabases_FilterNode(object sender, FilterNodeEventArgs e)
      {

         if (txtFilter.EditValue  != null && !string.IsNullOrEmpty(txtFilter.EditValue.ToString()))
         {
            e.Node.Visible = e.Node["Name"].ToString().ToLower().Contains(txtFilter.EditValue.ToString().ToLower());
            e.Handled = true;
         }
      }

      private void txtFilter_EditValueChanged(object sender, EventArgs e)
      {
         treeDatabases.FilterNodes();
      }
   }
}