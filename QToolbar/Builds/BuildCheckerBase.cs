using Microsoft.SqlServer.Management.SqlParser.Parser;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar.Builds
{

   public abstract class BuildCheckerBase
   {
      private const string LOG_FILE = "log.txt";

      private string _CheckoutName = "";
      private string _CheckoutPath = "";
      protected string _NextBuildPath = "";
      protected string _LOGID = "GENERIC";

      protected DataTable _Table = new DataTable();
      protected bool _Errors = false;

      #region constructors
      public BuildCheckerBase()
      {
         InitUI();
      }
      #endregion

      #region properties
      public string NextBuildPath
      {
         get
         {
            return _NextBuildPath;
         }

         set
         {
            _NextBuildPath = value;
         }
      }


      public DataTable Table
      {
         get
         {
            return _Table;
         }
      }

      public string CheckoutName
      {
         get
         {
            return _CheckoutName;
         }

         set
         {
            _CheckoutName = value;
         }
      }

      public string CheckoutPath
      {
         get
         {
            return _CheckoutPath;
         }

         set
         {
            _CheckoutPath = value;
         }
      }

       
      
      #endregion

      #region private
      private void InitUI()
      {
         _Table.Columns.Clear();
         _Table.Columns.Add("File", typeof(string));
         _Table.Columns.Add("Message", typeof(string));
         _Table.Columns.Add("Tag", typeof(string));
         _Table.Columns.Add("Result", typeof(string));
      }


      protected bool NextBuildFolderExists(string nextBuildPath)
      {
         return !string.IsNullOrEmpty(nextBuildPath) && Directory.Exists(nextBuildPath);
      }

      protected bool CheckForGOStatement(string content, string file)
      {
         bool retval = true;
         Regex reg = new Regex(@"\s+go\S+");


         return retval;
      }


      protected bool ContainsNonAnsiChars(string content)
      {
         return content.ToCharArray().Count(c => c > sbyte.MaxValue) > 0;
      }


      protected bool CheckUnicode(string file, string content)
      {
         bool retval = true;

         Encoding encoding = Utils.GetEncoding(file);
         bool containsNonAnsiChars = ContainsNonAnsiChars(content);
         if (encoding.BodyName != "utf-8" && encoding.BodyName != "utf-16" && containsNonAnsiChars)
         {
            Inform(file, "not in unicode and contains non ansi chars", "", CheckResult.Error);
            _Errors = true;
            retval = false;
         }
         return retval;
      }


      protected bool CheckUnicode(string file)
      {
         bool retval = true;

         Encoding encoding = Utils.GetEncoding(file);

         if (encoding.BodyName != "utf-8" && encoding.BodyName != "utf-16")
         {
            Inform(file, "not in unicode", "", CheckResult.Error);
            _Errors = true;
            retval = false;
         }
         return retval;
      }

      protected void Inform(string file, string message, string tag, CheckResult result)
      {
         if (!string.IsNullOrEmpty(file))
         {
            string msg = string.Format("{0} :: {1}", message, Path.GetFileName(file));
            DataRow row = _Table.NewRow();
            row["File"] = file;
            row["Message"] = message;
            row["Tag"] = tag;
            row["Result"] = result.ToString();
            _Table.Rows.Add(row);
            WriteLog(msg);
         }
      }

      protected void Inform(string message, CheckResult result)
      {
         DataRow row = _Table.NewRow();
         row["File"] = "";
         row["Message"] = message;
         row["Tag"] = "";
         row["Result"] = result.ToString();
         _Table.Rows.Add(row);
         WriteLog(message);
      }

      private void WriteLog(string log)
      {
         File.AppendAllText(LOG_FILE, $"{DateTime.Now.ToShortTimeString()}::{_LOGID}::{log}\r\n");
      }

      public void InitState()
      {
         _Errors = false;
         _Table.Rows.Clear();
         //if (File.Exists(LOG_FILE))
         //{
         //   File.Delete(LOG_FILE);
         //}
      }

      protected bool CheckForDDL(string file, string content, string pattern, string message, string tag)
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

      protected bool CheckToken(Tokens inputToken, string file)
      {

         Dictionary<string, List<Tokens>> checkTokens = new Dictionary<string, List<Tokens>>();
         checkTokens.Add(".sql", new List<Tokens>() { Tokens.TOKEN_USEDB, Tokens.TOKEN_ALTER, Tokens.TOKEN_CREATE});
         checkTokens.Add(".bd", new List<Tokens>() { Tokens.TOKEN_USEDB});
         bool retval = true;

         string fileExt = Path.GetExtension(file).ToLower();
         if (checkTokens.ContainsKey(fileExt))
         {
            List<Tokens> tokens = checkTokens[fileExt];
            if(tokens.Contains(inputToken))
            { 
                  string msg = $"Found \"{inputToken.ToString()}\"";
                  Inform(file, msg, "", CheckResult.Warning);
                  _Errors = true;
                  retval = false;
            }
         }
         return retval;
      }

      protected bool ScanSqlFile(string content, string file)
      {
         bool retval = true;
         if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
         {
            ParseOptions options = new ParseOptions();
            // sql 2008 combatibility
            options.CompatibilityLevel = Microsoft.SqlServer.Management.SqlParser.Common.DatabaseCompatibilityLevel.Version100;
            options.TransactSqlVersion = Microsoft.SqlServer.Management.SqlParser.Common.TransactSqlVersion.Version105;

            Scanner scanner = new Scanner(options);
            scanner.SetSource(content, 0);

            Tokens token;
            int state = 0;
            int start;
            int end;
            bool isPairMatch;
            bool isExecAutoParamHelp;
            while ((token = (Tokens)scanner.GetNext(ref state, out start, out end, out isPairMatch, out isExecAutoParamHelp)) != Tokens.EOF)
            {
               string str = content.Substring(start, end - start + 1);
               retval = retval && CheckToken(token, file);
            }
         }
         return retval;
      }


      protected bool ParseSqlFile(string content, string file)
      {
         bool retval = true;
         if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
         {
            ParseOptions options = new ParseOptions();
            // sql 2008 combatibility
            options.CompatibilityLevel = Microsoft.SqlServer.Management.SqlParser.Common.DatabaseCompatibilityLevel.Version100;
            options.TransactSqlVersion = Microsoft.SqlServer.Management.SqlParser.Common.TransactSqlVersion.Version105;

            ParseResult result = Parser.Parse(content, options);
            if (result.Errors.Count() > 0)
            {
               string msg = "";
               foreach (var error in result.Errors)
               {
                  msg = string.Format("{0} (Start line:{1}, col:{2}, End line:{3}, col:{4})", error.Message,
                        error.Start.LineNumber, error.Start.ColumnNumber,
                        error.End.LineNumber, error.End.ColumnNumber);
                  Inform(file, msg, "", CheckResult.Error);
                  _Errors = true;
               }
               retval = false;
            }
         }
         return retval;
      }

      protected bool CheckQBCAdminCF()
      {
         bool retval = true;
         string path = Path.Combine(CheckoutPath, @"VS Projects\QCS\QCSClient\QBC_Admin.cf");
         string content = File.ReadAllText(path);

         // check application code
         Regex reg = new Regex(@"ApplicationCode\s*=\s*(?<Num>[0-9])");
         Match match = reg.Match(content);
         string appCode = match.Groups["Num"].Value;
         if (match.Success && appCode != "8")
         {
            Inform(path, $"ApplicationCode is {appCode}", "cf error", CheckResult.Error);
            _Errors = true;
            retval = false;
         }

         // check 
         string[] lit = new string[] { "ApplicationServiceAssembly", "ApplicationServiceType", "ToolkitWSURL", "ApplicationWSURL" };
         foreach (string s in lit)
         {
            match = Regex.Match(content, $@"[#]\s*{s}");
            if (match.Success)
            {
               Inform(path, $"Found comment in cf \"{s}\"!", "cf error", CheckResult.Error);
               _Errors = true;
               retval = false;
            }
         }
         return retval;
      }


      /// <summary>
      /// Checks validity of xml configuration files 
      /// </summary>
      /// <returns></returns>
      protected bool CheckConfigurationFiles()
      {
         bool retval = true;
         XmlDocument xml = new XmlDocument();

         string conf = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, CheckoutName, "Configuration.xml");
         string envs = Path.Combine(OptionsInstance.EnvironmentsConfigurationFolder, CheckoutName, "EnvironmentsConfiguration.xml");
         if (File.Exists(conf))
         {
            try
            {
               xml.Load(conf);
               Inform(conf, string.Format("Configuration file ok!! \"{0}\"", conf), "", CheckResult.OK);
            }
            catch (Exception ex)
            {
               Inform(conf, string.Format("Configuration file \"{0}\": {1}", conf, ex.Message), "", CheckResult.Error);
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
               Inform(envs, string.Format("Environments Configuration file ok!! \"{0}\"", envs), "", CheckResult.OK);
            }
            catch (Exception ex)
            {
               Inform(envs, string.Format("Environments Configuration file \"{0}\": {1}", envs, ex.Message), "", CheckResult.Error);
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

      #endregion

      public virtual void Check()
      {

      }



   }
}
