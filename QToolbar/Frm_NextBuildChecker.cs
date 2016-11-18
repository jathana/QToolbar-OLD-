using Microsoft.SqlServer.Management.SqlParser.Parser;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

      private string _CheckoutName = "";
      private string _CheckoutPath = "";
      private bool _Errors = false;

      private DataTable _Table = new DataTable();

      public Frm_NextBuildChecker()
      {
         InitializeComponent();
      }


      public void Show(string checkoutName, string checkoutPath)
      {
         _Errors = false;
         _CheckoutName = checkoutName;
         _CheckoutPath = checkoutPath;
         _Table.Columns.Add("File", typeof(string));
         _Table.Columns.Add("Message", typeof(string));
         _Table.Columns.Add("Tag", typeof(string));
         _Table.Columns.Add("Result", typeof(string));

         Text = string.Format("{0} - {1}", _CheckoutName, _CheckoutPath);
         Show();
         Run();
         gridView1.OptionsBehavior.Editable = false;
         //gridView1.DoubleClick += gridView1_DoubleClick;
         gridResults.DataSource = _Table;

      }

      private void Run()
      {
         if(NextBuildFolderExists())
         {
            Init();
            string[] files = Directory.GetFiles(_CheckoutPath, "*.*", SearchOption.AllDirectories);
            string content = "";
            string lowerContent = "";
            bool fileOk = true;
            foreach (string file in files)
            {
               fileOk = true;
               StreamReader rdr = new StreamReader(file, true);
               content = rdr.ReadToEnd();
               lowerContent = content.ToLower();

               // check unicode
               fileOk = fileOk && CheckUnicode(file);

               // create ddl
               fileOk = fileOk && ParseForCreateObjects(lowerContent, file);

               // parse sql file
               fileOk = fileOk && ParseSqlFile(content, file);

               if (fileOk) Inform(file, "File passed all checks!","", CheckResult.OK);

               rdr.Close();
               rdr.Dispose();
            }

            // EOD Metadata files 
            fileOk = fileOk && CheckForEODMetadataFiles();

            // configuration files
            CheckConfigurationFiles();

         }
         else
         {
            Inform("Next Build folder does not exist.", CheckResult.Error);
            _Errors = true;
         }
         if(!_Errors && _Table.Rows.Count==0)
         {
            Inform("Everything ok!",CheckResult.OK);
         }
      }

      #region logic
      private bool NextBuildFolderExists()
      {
         return !string.IsNullOrEmpty(_CheckoutPath) && Directory.Exists(_CheckoutPath);
      }

     
      /// <summary>
      /// Checks validity of xml configuration files 
      /// </summary>
      /// <returns></returns>
      private bool CheckConfigurationFiles()
      {
         bool retval = true;
         XmlDocument xml = new XmlDocument();

         string conf = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, _CheckoutName, "Configuration.xml");
         string envs = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, _CheckoutName, "EnvironmentsConfiguration.xml");
         if(File.Exists(conf))
         {
            try
            {
               xml.Load(conf);               
               Inform(string.Format("Configuration file ok!! \"{0}\"", conf), CheckResult.OK);
            }
            catch(Exception ex)
            {
               Inform(string.Format("Configuration file \"{0}\": {1}", conf, ex.Message), CheckResult.Error);
            }
         }
         else
         {
            Inform(string.Format("Configuration file not found \"{0}\"", conf), CheckResult.Error);
            _Errors = true;
            retval = false;
         }
         if (File.Exists(envs))
         {
            try
            {
               xml.Load(envs);
               Inform(string.Format("Environments Configuration file ok!! \"{0}\"", envs), CheckResult.OK);
            }
            catch (Exception ex)
            {
               Inform(string.Format("Environments Configuration file \"{0}\": {1}", envs, ex.Message), CheckResult.Error);
            }
         }
         else
         {
            Inform(string.Format("Configuration file not found \"{0}\"", envs), CheckResult.Error);
            _Errors = true;
            retval = false;
         }

         return retval;
      }

      private bool CheckUnicode(string file)
      {
         bool retval = true;

         using (StreamReader sr = new StreamReader(file,  Encoding.ASCII,  true))
         {
            sr.Peek();
            if (sr.CurrentEncoding.BodyName != "utf-8" && sr.CurrentEncoding.BodyName != "utf-16")
            {
               Inform(file, "not in unicode", "", CheckResult.Error);
               _Errors = true;
               retval = false;
            }
         }
         return retval;
      }


      private bool CheckForEODMetadataFiles()
      {
         bool retval = true;
         if (Directory.Exists(_CheckoutPath))
         {
            if (File.Exists(Path.Combine(_CheckoutPath, "5102.2. EODMonitorMetadata.sql")) &&
                File.Exists(Path.Combine(_CheckoutPath, "5102.2. FullEODMonitorMetadata.sql")))
            {
               Inform("Found both \"5102.2.EODMonitorMetadata.sql\" and \"5102.2.FullEODMonitorMetadata.sql\". Please keep only the \"5102.2.FullEODMonitorMetadata.sql\"", CheckResult.Error);
               _Errors = true;
               retval = false;
            }

         }
         return retval;
      }

      private bool ParseForCreateObjects(string content, string file)
      {
         bool retval = true;
         if (Path.GetExtension(file).ToLower().Equals(".sql"))
         {
            retval = retval && CheckForDDL(file, content, "create\\s+", "CAUTION!! CREATE ddl in sql file.", "create");
            retval = retval && CheckForDDL(file, content, "alter\\s+", "CAUTION!!  ALTER ddl in sql file.", "alter");
         }
         return retval;
      }

      private bool ParseSqlFile(string content, string file)
      {
         bool retval = true;
         if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
         {
            ParseResult result = Parser.Parse(content);            
            if(result.Errors.Count() >0)
            {
               foreach(var error in result.Errors)
               {
                  Inform(file, error.Message, "",CheckResult.Error);
                  _Errors = true;
               }
               retval = false;
            }
         }
         return retval;
      }

      private void Inform(string file, string message, string tag, CheckResult result)
      {
         if (!string.IsNullOrEmpty(file))
         {
            string msg = string.Format("{0} :: {1}", message, Path.GetFileName(file));
            DataRow row=_Table.NewRow();
            row["File"] = file;
            row["Message"] = message;
            row["Tag"] = tag;
            row["Result"] = result.ToString();
            _Table.Rows.Add(row);
            WriteLog(msg);
         }
      }

      private void Inform(string message, CheckResult result)
      {
         DataRow row = _Table.NewRow();
         row["File"] = "";
         row["Message"] = message;
         row["Tag"] = "";
         row["Result"] = result.ToString();
         _Table.Rows.Add(row);
         WriteLog(message);
      }

      private bool CheckForDDL(string file, string content, string pattern, string message, string tag)
      {
         bool retval = true;
         Regex reg = new Regex(pattern);
         Match createProc = reg.Match(content);
         if (createProc.Success)
         {
            Inform(file, message, tag, CheckResult.Warning);
            _Errors = true;
            retval = false;
         }
         return retval;
      }

      private void WriteLog(string log)
      {
         File.AppendAllText(LOG_FILE, log + "\r\n");
      }
      private void Init()
      {
         _Table.Rows.Clear();
         if(File.Exists(LOG_FILE))
         {
            File.Delete(LOG_FILE);
         }
      }
      #endregion

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
   }
}
