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
using QToolbar.Options;
using System.IO;
using System.Diagnostics;

namespace QToolbar.Plugins.NextBuildScript
{
   public partial class Frm_NextBuildScript : DevExpress.XtraEditors.XtraForm
   {

      public delegate Tuple<bool, string> RunAction(string branchPath); 
      private string _FilePath;
      private RunAction _Action;
      

      public Frm_NextBuildScript()
      {
         InitializeComponent();
         LoadCheckouts();
      }

      public string Message
      {
         get
         {
            return lblDescription.Text;
         }

         set
         {
            lblDescription.Text = value;
         }
      }


      public string SelectedPath
      {
         get { return cboBranch.SelectedIndex>=0?cboBranch.Properties.Items[cboBranch.SelectedIndex].Value.ToString():""; }
      }

      public string FilePath
      {
         get
         {
            return _FilePath;
         }

         set
         {
            _FilePath = value;
         }
      }

      public RunAction Action
      {
         get
         {
            return _Action;
         }

         set
         {
            _Action = value;
         }
      }

      private void LoadCheckouts()
      {
         // load checkouts
         cboBranch.Properties.Items.Clear();
         try
         {
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               if(Directory.Exists(row["Path"].ToString()))
               {
                  cboBranch.Properties.Items.Add(row["Name"].ToString(), row["Path"].ToString(), -1);
               }
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve local checkouts");
         }
      }

      private void btnApply_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(SelectedPath))
         {
            XtraMessageBox.Show("Branch is empty.");
         }
         else
         {
            Tuple<bool, string> retval = _Action.Invoke(SelectedPath);
            memResults.Text = retval.Item2;
            if (chkOpenFolder.Checked)
            {
               if (!string.IsNullOrEmpty(SelectedPath) && Directory.Exists(SelectedPath))
               {
                  Process.Start(Path.Combine(SelectedPath, @"Builds\Next Build"));
               }
            }
         }
      }
   }
}