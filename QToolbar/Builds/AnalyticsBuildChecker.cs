using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Builds
{
    public class AnalyticsBuildChecker : BuildCheckerBase
    {
        public AnalyticsBuildChecker() : base()
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
                foreach (string file in files)
                {
                    fileOk = true;
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
            }
            else
            {
                Inform("Next Build folder does not exist.", CheckResult.Warning);
                _Errors = true;
            }
            if (!_Errors && _Table.Rows.Count == 0)
            {
                Inform("Everything ok!", CheckResult.OK);
            }
            if (!_Errors && _Table.Rows.Count == 0)
            {
                Inform("Everything ok!", CheckResult.OK);
            }
        }

    }
}
