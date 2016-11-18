using Microsoft.SqlServer.Management.SqlParser.Parser;
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

namespace QToolbar
{
   public partial class Frm_NextBuildChecker : Form
   {
      private const string LOG_FILE = "log.txt";

      private string _CheckoutName = "";
      private string _CheckoutPath = "";
      private bool _Errors = false;

      public Frm_NextBuildChecker()
      {
         InitializeComponent();
      }


      public void Show(string checkoutName, string checkoutPath)
      {
         _Errors = false;
         _CheckoutName = checkoutName;
         _CheckoutPath = checkoutPath;
         Text = string.Format("{0} - {1}", _CheckoutName, _CheckoutPath);
         Show();
         Run();
      }

      private void Run()
      {
         if(NextBuildFolderExists())
         {
            Init();
            string[] files = Directory.GetFiles(_CheckoutPath, "*.*", SearchOption.AllDirectories);
            string content = "";
            string lowerContent = "";
            foreach (string file in files)
            {
               StreamReader rdr = new StreamReader(file, true);
               content = rdr.ReadToEnd();
               lowerContent = content.ToLower();

               // check unicode
               CheckUnicode(rdr, file);

               // create ddl
               ParseForCreateObjects(lowerContent, file);

               // parse sql file
               ParseSqlFile(content, file);

               rdr.Close();
               rdr.Dispose();
            }
         }
         else
         {
            lstResults.Items.Add("Next Build folder does not exist.");
            _Errors = true;
         }
         if(!_Errors)
         {
            lstResults.Items.Add("Everything ok!");
         }
      }

      #region logic
      private bool NextBuildFolderExists()
      {
         return !string.IsNullOrEmpty(_CheckoutPath) && Directory.Exists(_CheckoutPath);
      }

     

      private void CheckUnicode(StreamReader rdr,string file)
      {

         if (rdr.CurrentEncoding.BodyName != "utf-8" && rdr.CurrentEncoding.BodyName != "utf-16")
         {
            Inform(file, "not in unicode","");
            _Errors = true;
         }
      }


      private void ParseForCreateObjects(string content, string file)
      {
         if (Path.GetExtension(file).ToLower().Equals(".sql"))
         {
            CheckForDDL(file, content, "create\\s+", "CAUTION!! CREATE ddl in sql file.", "create");
            CheckForDDL(file, content, "alter\\s+", "CAUTION!!  ALTER ddl in sql file.", "alter");
         }
      }

      private void ParseSqlFile(string content, string file)
      {
         if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
         {
            ParseResult result = Parser.Parse(content);            
            if(result.Errors.Count() >0)
            {
               foreach(var error in result.Errors)
               {
                  Inform(file, error.Message, "");
                  _Errors = true;
               }
            }
         }
      }

      private void Inform(string file, string message, string tag)
      {
         if (!string.IsNullOrEmpty(file))
         {
            string msg = string.Format("{0} :: {1}", message, Path.GetFileName(file));
            lstResults.Items.Add(new NextBuildFile(file, msg, tag));
            WriteLog(msg);
         }
      }


      private void CheckForDDL(string file, string content, string pattern, string message, string tag)
      {
         
         Regex reg = new Regex(pattern);
         Match createProc = reg.Match(content);
         if (createProc.Success)
         {
            Inform(file, message, tag);
            _Errors = true;
         }
      }

      private void WriteLog(string log)
      {
         File.AppendAllText(LOG_FILE, log + "\r\n");
      }
      private void Init()
      {
         lstResults.Items.Clear();
         if(File.Exists(LOG_FILE))
         {
            File.Delete(LOG_FILE);
         }
      }
      #endregion

      private void lstResults_DoubleClick(object sender, EventArgs e)
      {
         Frm_FileViewer f = new Frm_FileViewer();
         object selected = lstResults.SelectedItem;
         if (selected is NextBuildFile)
         {
            NextBuildFile bf = ((NextBuildFile)selected);

            f.ViewFile(bf.File);
         }
      }
   }
}
