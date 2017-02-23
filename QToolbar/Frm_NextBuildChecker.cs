using Microsoft.SqlServer.Management.SqlParser.Parser;
using QToolbar.Builds;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QToolbar
{
   public partial class Frm_NextBuildChecker : Form
   {
      private const string LOG_FILE = "log.txt";

      //private string _CheckoutName = "";
      //private string _CheckoutPath = "";
      //private string _NextBuildPath = "";
      //private string _AnalyticsNextBuildPath = "";
      //private bool _Errors = false;

      //private DataTable _Table = new DataTable();
      //private DataTable _AnalyticsTable = new DataTable();

      //
      private BuildChecker _BuildChecker = null;
      private AnalyticsBuildChecker _AnalyticsBuildChecker = null;

      public Frm_NextBuildChecker()
      {
         InitializeComponent();
      }

      private void InitUI()
      {
         //_Table.Columns.Clear();
         //_Table.Columns.Add("File", typeof(string));
         //_Table.Columns.Add("Message", typeof(string));
         //_Table.Columns.Add("Tag", typeof(string));
         //_Table.Columns.Add("Result", typeof(string));

         //_AnalyticsTable.Columns.Clear();
         //_AnalyticsTable.Columns.Add("File", typeof(string));
         //_AnalyticsTable.Columns.Add("Message", typeof(string));
         //_AnalyticsTable.Columns.Add("Tag", typeof(string));
         //_AnalyticsTable.Columns.Add("Result", typeof(string));


         Text = $"{_BuildChecker.CheckoutName} - {_BuildChecker.NextBuildPath} & {_AnalyticsBuildChecker.NextBuildPath}";
         gridView1.OptionsBehavior.Editable = false;
         gridView2.OptionsBehavior.Editable = false;
      }

      public void Show(string checkoutName, string checkoutPath)
      {
        
         //_CheckoutName = checkoutName;
         //_CheckoutPath = checkoutPath;

         _BuildChecker = new BuildChecker(); 
         _BuildChecker.CheckoutName = checkoutName;
         _BuildChecker.CheckoutPath = checkoutPath;
         _BuildChecker.NextBuildPath = Path.Combine(checkoutPath, @"Builds\Next Build");

         _AnalyticsBuildChecker = new AnalyticsBuildChecker(); 
         _AnalyticsBuildChecker.CheckoutName = checkoutName;
         _AnalyticsBuildChecker.CheckoutPath = checkoutPath;
         _AnalyticsBuildChecker.NextBuildPath = Path.Combine(checkoutPath, @"AnalyticsBuilds\Next Build");


         //_NextBuildPath = Path.Combine(checkoutPath, @"Builds\Next Build");
         //_AnalyticsNextBuildPath = Path.Combine(checkoutPath, @"AnalyticsBuilds\Next Build");

         Show();
      }

      //private void OnCheck(string nextBuildPath)
      //{
         

      //   if (NextBuildFolderExists(nextBuildPath))
      //   {
            
      //      string[] files = Directory.GetFiles(nextBuildPath, "*.*", SearchOption.AllDirectories);
      //      string content = "";
      //      string lowerContent = "";
      //      bool fileOk = true;
      //      foreach (string file in files)
      //      {
      //         fileOk = true;
      //         StreamReader rdr = new StreamReader(file, true);
      //         content = rdr.ReadToEnd();
      //         lowerContent = content.ToLower();
      //         rdr.Close();
      //         rdr.Dispose();

      //         // check unicode
      //         fileOk = fileOk && CheckUnicode(file);

      //         // create ddl
      //         fileOk = fileOk && ParseForCreateObjects(lowerContent, file);

      //         // parse sql file
      //         fileOk = fileOk && ParseSqlFile(content, file);

      //         if (fileOk) Inform(file, "File passed all checks!","", CheckResult.OK);

      //      }

      //      // EOD Metadata files 
      //      CheckForEODMetadataFiles(nextBuildPath);

      //      // configuration files
      //      CheckConfigurationFiles();

      //      // check qbc_admin.cf
      //      CheckQBCAdminCF();
      //   }
      //   else
      //   {
      //      Inform("Next Build folder does not exist.", CheckResult.Error);
      //      _Errors = true;
      //   }
      //   if(!_Errors && _Table.Rows.Count==0)
      //   {
      //      Inform("Everything ok!",CheckResult.OK);
      //   }
      //}

      #region logic


      //private bool CheckForGOStatement(string content, string file)
      //{
      //   bool retval = true;
      //   Regex reg = new Regex(@"\s+go\S+");

         
      //   return retval;
      //}

      //private bool NextBuildFolderExists(string nextBuildPath)
      //{
      //   return !string.IsNullOrEmpty(nextBuildPath) && Directory.Exists(nextBuildPath);
      //}

     
      /// <summary>
      /// Checks validity of xml configuration files 
      /// </summary>
      /// <returns></returns>
      //private bool CheckConfigurationFiles()
      //{
      //   bool retval = true;
      //   XmlDocument xml = new XmlDocument();

      //   string conf = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, _CheckoutName, "Configuration.xml");
      //   string envs = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, _CheckoutName, "EnvironmentsConfiguration.xml");
      //   if(File.Exists(conf))
      //   {
      //      try
      //      {
      //         xml.Load(conf);               
      //         Inform(conf, string.Format("Configuration file ok!! \"{0}\"", conf),"", CheckResult.OK);
      //      }
      //      catch(Exception ex)
      //      {
      //         Inform(conf, string.Format("Configuration file \"{0}\": {1}", conf, ex.Message), "", CheckResult.Error);
      //      }
      //   }
      //   else
      //   {
      //      Inform(string.Format("Configuration file not found \"{0}\"", conf), CheckResult.Error);
      //      _Errors = true;
      //      retval = false;
      //   }
      //   if (File.Exists(envs))
      //   {
      //      try
      //      {
      //         xml.Load(envs);
      //         Inform(envs, string.Format("Environments Configuration file ok!! \"{0}\"", envs), "", CheckResult.OK);
      //      }
      //      catch (Exception ex)
      //      {
      //         Inform(envs, string.Format("Environments Configuration file \"{0}\": {1}", envs, ex.Message), "", CheckResult.Error);
      //      }
      //   }
      //   else
      //   {
      //      Inform(string.Format("Configuration file not found \"{0}\"", envs), CheckResult.Error);
      //      _Errors = true;
      //      retval = false;
      //   }

      //   return retval;
      //}

      //private bool CheckUnicode(string file)
      //{
      //   bool retval = true;
         
      //   Encoding encoding = Utils.GetEncoding(file);
      //   if (encoding.BodyName != "utf-8" && encoding.BodyName != "utf-16")
      //   {
      //      Inform(file, "not in unicode", "", CheckResult.Error);
      //      _Errors = true;
      //      retval = false;
      //   }
      //   return retval;
      //}


      //private bool CheckForEODMetadataFiles(string nextBuildPath)
      //{
      //   bool retval = true;
      //   if (Directory.Exists(nextBuildPath))
      //   {
      //      if (File.Exists(Path.Combine(nextBuildPath, "5102.2. EODMonitorMetadata.sql")) &&
      //          File.Exists(Path.Combine(nextBuildPath, "5102.2. FullEODMonitorMetadata.sql")))
      //      {
      //         Inform("Found both \"5102.2.EODMonitorMetadata.sql\" and \"5102.2.FullEODMonitorMetadata.sql\". Please keep only the \"5102.2.FullEODMonitorMetadata.sql\"", CheckResult.Error);
      //         _Errors = true;
      //         retval = false;
      //      }
      //   }
      //   return retval;
      //}

      //private bool ParseForCreateObjects(string content, string file)
      //{
      //   bool retval = true;
      //   if (Path.GetExtension(file).ToLower().Equals(".sql"))
      //   {
      //      retval = retval && CheckForDDL(file, content, "\\s+create\\s+", "CAUTION!! \"CREATE\" in sql file.", "create");
      //      retval = retval && CheckForDDL(file, content, "\\s+alter\\s+", "CAUTION!!  \"ALTER\"  in sql file.", "alter");
      //   }
      //   return retval;
      //}

      //private bool ParseSqlFile(string content, string file)
      //{
      //   bool retval = true;
      //   if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
      //   {
      //      ParseOptions options = new ParseOptions();
      //      // sql 2008 combatibility
      //      options.CompatibilityLevel = Microsoft.SqlServer.Management.SqlParser.Common.DatabaseCompatibilityLevel.Version100;
      //      options.TransactSqlVersion = Microsoft.SqlServer.Management.SqlParser.Common.TransactSqlVersion.Version105;

      //      ParseResult result = Parser.Parse(content, options);            
      //      if(result.Errors.Count() >0)
      //      {
      //         string msg = "";
      //         foreach(var error in result.Errors)
      //         {
      //            msg = string.Format("{0} (Start line:{1}, col:{2}, End line:{3}, col:{4})", error.Message,
      //                  error.Start.LineNumber, error.Start.ColumnNumber,
      //                  error.End.LineNumber, error.End.ColumnNumber);
      //            Inform(file, msg, "", CheckResult.Error);
      //            _Errors = true;
      //         }
      //         retval = false;
      //      }
      //   }
      //   return retval;
      //}

      //private void Inform(string file, string message, string tag, CheckResult result)
      //{
      //   if (!string.IsNullOrEmpty(file))
      //   {
      //      string msg = string.Format("{0} :: {1}", message, Path.GetFileName(file));
      //      DataRow row=_Table.NewRow();
      //      row["File"] = file;
      //      row["Message"] = message;
      //      row["Tag"] = tag;
      //      row["Result"] = result.ToString();
      //      _Table.Rows.Add(row);
      //      WriteLog(msg);
      //   }
      //}

      //private void Inform(string message, CheckResult result)
      //{
      //   DataRow row = _Table.NewRow();
      //   row["File"] = "";
      //   row["Message"] = message;
      //   row["Tag"] = "";
      //   row["Result"] = result.ToString();
      //   _Table.Rows.Add(row);
      //   WriteLog(message);
      //}

      //private bool CheckForDDL(string file, string content, string pattern, string message, string tag)
      //{
      //   bool retval = true;
      //   Regex reg = new Regex(pattern);
      //   Match createProc = reg.Match(content);
      //   if (createProc.Success)
      //   {
      //      Inform(file, message, tag, CheckResult.Warning);
      //      _Errors = true;
      //      retval = false;
      //   }
      //   return retval;
      //}

      //private bool CheckQBCAdminCF()
      //{
      //   bool retval = true;
      //   string path = Path.Combine(_CheckoutPath, @"VS Projects\QCS\QCSClient\QBC_Admin.cf");
      //   string content = File.ReadAllText(path);

      //   // check application code
      //   Regex reg = new Regex(@"ApplicationCode\s*=\s*(?<Num>[0-9])");
      //   Match match = reg.Match(content);
      //   string appCode = match.Groups["Num"].Value;
      //   if (match.Success && appCode!="8")
      //   {
      //      Inform(path, $"ApplicationCode is {appCode}", "cf error", CheckResult.Error);
      //      _Errors = true;
      //      retval = false;
      //   }

      //   // check 
      //   string[] lit = new string[] { "ApplicationServiceAssembly", "ApplicationServiceType", "ToolkitWSURL", "ApplicationWSURL" };
      //   foreach (string s in lit)
      //   {
      //      match = Regex.Match(content, $@"[#]\s*{s}");
      //      if (match.Success)
      //      {
      //         Inform(path, $"Found comment in cf \"{s}\"!", "cf error", CheckResult.Error);
      //         _Errors = true;
      //         retval = false;
      //      }
      //   }
      //   return retval;
      //}



      //private void WriteLog(string log)
      //{
      //   File.AppendAllText(LOG_FILE, log + "\r\n");
      //}
      //private void InitState()
      //{

      //   _Errors = false;
      //   _Table.Rows.Clear();
      //   _AnalyticsTable.Rows.Clear();
      //   if(File.Exists(LOG_FILE))
      //   {
      //      File.Delete(LOG_FILE);
      //   }
      //}
      #endregion


      private void BeforeCheck()
      {
         Cursor.Current = Cursors.WaitCursor;
         gridResults.DataSource = null;
         grdAnalyticsResults.DataSource = null;
      }


      private void CheckAsync()
      {
         backgroundWorker1.RunWorkerAsync();
      }

      private void AfterCheck()
      {
         gridResults.DataSource = _BuildChecker.Table;
         grdAnalyticsResults.DataSource = _AnalyticsBuildChecker.Table;
         Cursor.Current = Cursors.Default;
      }

      private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
      {
      }

      private void gridView1_DoubleClick(object sender, EventArgs e)
      {
         string file = gridView1.GetFocusedDataRow()["File"].ToString();
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            PowerfulSample f1 = new PowerfulSample();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }

      private void RunAsync()
      {
         try
         {
            if (!backgroundWorker1.IsBusy)
            {
               BeforeCheck();
               CheckAsync();
            }
         }
         catch (Exception ex)
         {

         }
         finally
         {

         }
      }

      private void btnCheck_Click(object sender, EventArgs e)
      {
          RunAsync();
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         _BuildChecker.InitState();
         _AnalyticsBuildChecker.InitState();

         _BuildChecker.Check();
         _AnalyticsBuildChecker.Check();

         //InitState();
         //OnCheck(_NextBuildPath);
         //  OnCheck(_AnalyticsNextBuildPath);
      }

      private void Frm_NextBuildChecker_Load(object sender, EventArgs e)
      {
         InitUI();

         RunAsync();
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         AfterCheck();
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {

      }

      private void btnCheckBuild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         RunAsync();
      }

      private void gridView2_DoubleClick(object sender, EventArgs e)
      {
         string file = gridView2.GetFocusedDataRow()["File"].ToString();
         if (!string.IsNullOrEmpty(file) && File.Exists(file))
         {
            PowerfulSample f1 = new PowerfulSample();
            f1.Size = new Size(800, 800);

            f1.ViewFile(file, FastColoredTextBoxNS.Language.SQL);
         }
      }
   }
}
