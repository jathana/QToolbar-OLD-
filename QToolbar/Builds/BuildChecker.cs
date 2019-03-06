using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Builds
{
    public class BuildChecker : BuildCheckerBase
    {
        public BuildChecker() : base()
        {

        }


        public override void Check()
        {
            _Errors = false;

            base.Check();

            if (NextBuildFolderExists(_NextBuildPath))
            {

                string[] files = Directory.GetFiles(_NextBuildPath, "*.*", SearchOption.AllDirectories);
                string content = "";
                string lowerContent = "";
                bool fileOk = true;
                //SqlParser parser = new SqlParser();
                foreach (string file in files)
                {
                    fileOk = true;
                    Debug.WriteLine(file);
                    StreamReader rdr = new StreamReader(file, true);
                    content = rdr.ReadToEnd();
                    lowerContent = content.ToLower();
                    rdr.Close();
                    rdr.Dispose();

                    // check unicode
                    fileOk = CheckUnicode(file, content) && fileOk;

                    // scan sql file
                    fileOk = ScanSqlFile(content, file) && fileOk;

                    // parse sql file
                    fileOk = ParseSqlFile(content, file) && fileOk;

                    // check file name
                    fileOk = CheckFileName(file) && fileOk;

                    if (fileOk) Inform(file, "File passed all checks!", "", CheckResult.OK);
                }

                // EOD Metadata files 
                CheckForEODMetadataFiles(_NextBuildPath);
            }
            else
            {
                Inform("Next Build folder does not exist.", CheckResult.Warning);
                _Errors = true;
            }
            // configuration files
            CheckConfigurationFiles();

            // check qbc_admin.cf
            CheckQBCAdminCF();

            // check sql keywords
            // CheckDatabaseScripts();

            if (!_Errors && _Table.Rows.Count == 0)
            {
                Inform("Everything ok!", CheckResult.OK);
            }
        }


        private bool CheckForEODMetadataFiles(string nextBuildPath)
        {
            bool retval = true;
            if (Directory.Exists(nextBuildPath))
            {
                if (File.Exists(Path.Combine(nextBuildPath, "5102.2. EODMonitorMetadata.sql")) &&
                    File.Exists(Path.Combine(nextBuildPath, "5102.2. FullEODMonitorMetadata.sql")))
                {
                    Inform("Found both \"5102.2.EODMonitorMetadata.sql\" and \"5102.2.FullEODMonitorMetadata.sql\". Please keep only the \"5102.2.FullEODMonitorMetadata.sql\"", CheckResult.Error);
                    _Errors = true;
                    retval = false;
                }
            }
            return retval;
        }





    }
}
