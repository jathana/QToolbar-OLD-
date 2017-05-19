using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class ClearMetadataButton
   {
      public void ClearMetadata()
      {
         if (!string.IsNullOrWhiteSpace(OptionsInstance.QCSAdminFolder))
         {
            if (Directory.Exists(OptionsInstance.QCSAdminFolder))
            {
               List<string> dirs = new List<string>(Directory.EnumerateDirectories(OptionsInstance.QCSAdminFolder));
               dirs = dirs.OrderByDescending(s => s).ToList<string>();
               Regex reg = new Regex("^(?<version>[0-9]+[.][0-9]+)");
               List<string> deletions = new List<string>();
               string newItem = "";
               foreach (string dir in dirs)
               {
                  Match match = reg.Match(Path.GetFileName(dir));
                  if (match.Success)
                  {
                     newItem = string.Format("del %temp%\\{0}*", match.Groups["version"]);
                     if (!deletions.Contains(newItem))
                     {
                        deletions.Add(newItem);
                     }
                  }
               }

               // create batch content
               StringBuilder builder = new StringBuilder();

               builder.AppendLine("del %temp%\\current*");
               foreach (var del in deletions)
               {
                  builder.AppendLine(del);
               }
               builder.AppendLine("echo \"Deleting Local Metadata\"");
               builder.AppendLine("del %userprofile%\\documents\\*.metadata");
               builder.AppendLine("del %userprofile%\\documents\\*.localization.dat");
               builder.AppendLine("del %TEMP%\\*.metadata");
               builder.AppendLine("del %TEMP%\\*localization.dat");
               builder.AppendLine("taskkill /IM iisexpress.exe");
               builder.AppendLine("echo \"Deleting Silverlight Metadata\"");
               builder.AppendLine("del /S %userprofile%\\AppData\\LocalLow\\Microsoft\\Silverlight\\*24_*");
               builder.AppendLine("del /S %userprofile%\\AppData\\Local\\Temp\\*pr_*");
               builder.AppendLine("del /Q %temp%\\*.*");

               string FILE_NAME = Path.Combine(AppInstance.CacheDirectory, "clear_metadata.bat");
               FileStream fs = null;
               try
               {
                  fs = new FileStream(FILE_NAME, FileMode.Create);
                  using (StreamWriter writer = new StreamWriter(fs))
                  {
                     writer.Write(builder.ToString());
                     writer.Close();
                  }
                  Process.Start(FILE_NAME);
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format("Failed to clear metadata {0}", ex.Message));
               }
               finally
               {
                  if (fs != null)
                     fs.Dispose();
               }
            }
            else
            {
               XtraMessageBox.Show(string.Format("QVS Admin Folder \"{0}\" not found.", OptionsInstance.QCSAdminFolder));
            }
         }
      }
   }
}
