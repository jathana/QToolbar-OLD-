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
using System.IO;
using QToolbar.Options;
using DevExpress.XtraTreeList.Nodes;

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
      }


      private void LoadDatabases()
      {
         treeDatabases.ClearNodes();
         // get all databases from cf
         backgroundWorker1.RunWorkerAsync();
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
                     verNode.Data.Version = dir;
                     verNode.Data.VersionNode = true;
                     verNode.Parent = tree;
                     tree.AddChild(verNode);
                     // parse cf and get dbs
                     List<ConnectionInfo> dbs = GetCFDBs(Path.GetFileName(dir));
                     foreach(ConnectionInfo item in dbs)
                     {
                        TreeNode<ConnectionInfo> child = new TreeNode<ConnectionInfo>();
                        child.Data = item;
                        child.Parent = verNode;
                        verNode.AddChild(child);
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
            Dictionary<string,string> servers=cfFile.GetSectionPairs("[servers]");
            Dictionary<string, string> dbnames = cfFile.GetSectionPairs("[databasename]");
            if (servers.Count != dbnames.Count)
            {
               throw new Exception($"[Servers] and [DatabaseName] sections of {file} contain different number of items.");
            }
            else
            {
               foreach(KeyValuePair<string,string> server in servers)
               {
                  ConnectionInfo info = new ConnectionInfo()
                  {
                     Environment = server.Key,
                     Server = server.Value,
                     Database = dbnames[server.Key],
                     Version = destDir
                  };
                  retVal.Add(info);

               }
            }
         }
         return retVal;
      }



      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         treeDatabases.ClearNodes();

         PopulateDBTree((TreeNode<ConnectionInfo>)e.Result);
      }

      private void PopulateDBTree(TreeNode<ConnectionInfo> tree)
      {
         treeDatabases.DataSource = tree;
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
         if (e.Column.Caption == "Version")
         {
            TreeNode<ConnectionInfo> node = (TreeNode<ConnectionInfo>)e.Node;
            if (node.Data.VersionNode)
            {
               e.CellData = Path.GetFileName(((TreeNode<ConnectionInfo>)e.Node).Data.Version);
            }
            else
            {
               e.CellData = $"{node.Data.Environment} => [{node.Data.Server}].[{node.Data.Database}]";
            }
         }
      }
   }
}