using Microsoft.SqlServer.Management.SqlParser.Parser;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using QToolbar.Helpers;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar.Builds
{
   public delegate void FileCheckedEventHandler(object sender, BuildCheckerEventArgs e);

   public abstract class BuildCheckerBase
   {
      private const string LOG_FILE = "log.txt";

      private string _CheckoutName = "";
      private string _CheckoutPath = "";
      protected string _NextBuildPath = "";
      protected string _LOGID = "GENERIC";

      protected DataTable _Table = new DataTable();
      protected bool _Errors = false;

      protected SqlParser _SqlParser;

      private List<Tuple<string, string, string>> _TestDatabases;


      public event FileCheckedEventHandler FileChecked;

      #region constructors
      public BuildCheckerBase()
      {
         InitUI();
         _SqlParser = new SqlParser();
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

      #region Events
      protected virtual void OnFileChecked(BuildCheckerEventArgs e)
      {
         FileCheckedEventHandler handler = FileChecked;
         if (handler != null)
         {
            handler(this, e);
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
            OnFileChecked(new BuildCheckerEventArgs() { Message = $"File {file}" });
         }
         return retval;
      }

      protected bool ParseSqlFileNew(string content, string file)
      {
         bool retval = true;
         if (Path.GetExtension(file).ToLower().Equals(".sql") || Path.GetExtension(file).ToLower().Equals(".bd"))
         {
            retval = _SqlParser.Parse(file);
            AddParseErrors(file);

            //Tuple<string, int>[] keywords = Utils.GetSQLKeywords();
            //EnsureTestDatabases();

            //var dbs2008 = _TestDatabases.Where(database => database.Item3.Contains("2008")).ToList();
            //_SqlParser.Parse(file);
            //CheckTSQLKeywords2008(file, _SqlParser);
         }

         OnFileChecked(new BuildCheckerEventArgs() { Message = $"File {file}"});
      
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

        protected virtual bool CheckFileName(string fileName)
        {
            bool retval = true;
            // ensure that .bd files end with .sql.bd
            if(fileName.ToLower().EndsWith(".bd"))
            {
                if(!fileName.ToLower().EndsWith(".sql.bd"))
                {
                    Inform(fileName,  $".bd file \"{fileName}\" should end with .sql.bd", "", CheckResult.Error);
                    retval = false;
                }
            }
            return retval;
        }
      #endregion

      public virtual void Check()
      {

      }

      protected virtual List<Tuple<string,string,string>> GetTestDBs()
      {
         string cfFile = Path.Combine(CheckoutPath, @"VS Projects\QCS\QCSClient\QBC_Admin.cf");
         string[] keys = IniFile2.ReadKeys("Servers", cfFile);

         string server = "";
         string db = "";
         //SqlParser parser = new SqlParser();
         List<Tuple<string, string, string>> dbs = new List<Tuple<string, string, string>>();
         // get database versions
         foreach (var key in keys)
         {
            server = IniFile2.ReadValue("Servers", key, cfFile);
            db = IniFile2.ReadValue("DatabaseName", key, cfFile);
            using (SqlConnection con = new SqlConnection(Utils.GetConnectionString(server, db)))
            {
               try
               {
                  con.Open();
                  SqlCommand command = new SqlCommand("SELECT @@VERSION", con);

                  command.CommandType = CommandType.Text;
                  string ret = (string)command.ExecuteScalar();
                  Tuple<string, string, string> newItem = Tuple.Create<string, string, string>(server, db, ret);
                  dbs.Add(newItem);

               }
               catch (Exception ex)
               {

               }
               finally
               {
                  con.Close();
               }
            }
         }

         return dbs;
      }

      private void EnsureTestDatabases()
      {
         if (_TestDatabases==null)
         {
            _TestDatabases = GetTestDBs();
         }
      }

      protected virtual bool CheckDatabaseScripts()
      {
         bool retval = true;
         string cfFile = Path.Combine(CheckoutPath, @"VS Projects\QCS\QCSClient\QBC_Admin.cf");
         string[] keys = IniFile2.ReadKeys("Servers", cfFile);

         Tuple<string, int>[] keywords = Utils.GetSQLKeywords();

         EnsureTestDatabases();
         List<Tuple<string, string, string>> dbs = _TestDatabases;

         // check next build scripts


         // check views
         string viewsPath = Path.Combine(CheckoutPath, @"Database Scripts\Views");
         string[] views=Directory.GetFiles(viewsPath, "*.sql");
         var dbs2008 = dbs.Where(database => database.Item3.Contains("2008")).ToList();
         foreach(string view in views)
         {
            _SqlParser.Parse(view);
            AddParseErrors(view);
            CheckTSQLKeywords2008(view, _SqlParser);
            OnFileChecked(new BuildCheckerEventArgs() { Message = $"View: {view}" });
         }

         // check stored procs
         string spsPath = Path.Combine(CheckoutPath, @"Database Scripts\Stored Procedures");
         string[] sps = Directory.GetFiles(spsPath, "*.sql");
         foreach (string sp in sps)
         {
            string spContent = File.ReadAllText(sp);

            _SqlParser.Parse(sp);
            AddParseErrors(sp);
            CheckTSQLKeywords2008(sp, _SqlParser);
            OnFileChecked(new BuildCheckerEventArgs() { Message = $"Stored Procedure: {sp}" });
         }

         // user defined functions
         string udefsPath = Path.Combine(CheckoutPath, @"Database Scripts\User Defined Functions");
         string[] udefs = Directory.GetFiles(udefsPath, "*.sql");
         foreach (string udef in udefs)
         {
            string udefContent = File.ReadAllText(udef);
            _SqlParser.Parse(udef);
            AddParseErrors(udef);
            CheckTSQLKeywords2008(udef, _SqlParser);
            OnFileChecked(new BuildCheckerEventArgs() { Message = $"User Defined Func: {udef}" });
         }

         return retval;
      }


      private void AddParseErrors(string file)
      {
         if (_SqlParser.ParseErrors.Count > 0)
         {
            string msg = "";
            foreach (var error in _SqlParser.ParseErrors)
            {
               msg = $"Line:{error.Line}, Col:{error.Column}, Number:{error.Number} Msg:{error.Message}";

               Inform(file, msg, "", CheckResult.Error);
               _Errors = true;
            }

         }
      }


      private void CheckTSQLKeywords2008(string file, SqlParser parser)
      {
         return;
         Tuple<string, int>[] keywords = Utils.GetSQLKeywords();
         EnsureTestDatabases();

         var dbs2008 = _TestDatabases.Where(database => database.Item3.Contains("2008")).ToList();
         foreach (var keyword in keywords)
         {
            var func = parser.FunctionCalls.Where(f => f.FunctionName.Value.ToLower().Equals(keyword.Item1.ToString().ToLower())).FirstOrDefault();
            if (func != null)
            {
               if (dbs2008 != null)
               {
                  foreach (var db2008 in dbs2008)
                  {
                     Inform(file, $"File {Path.GetFileName(file)} contains {keyword.Item1} and it is not supported in db {db2008.Item1}.{db2008.Item2}", file, CheckResult.Warning);
                  }
               }
            }
         }
   }

   }
}
