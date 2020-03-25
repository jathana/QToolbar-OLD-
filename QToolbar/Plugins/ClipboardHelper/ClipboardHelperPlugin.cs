using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.ClipboardHelper
{

   [Plugin(pluginName: "Clipboard Helper", pluginDesc: @"Clipboard Helper.")]
   public class ClipboardHelperPlugin : QPlugin, IQPlugin
   {
      public ClipboardHelperPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p= (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_ClipboardHelper()
         {
            Text = p.PluginName
         };
         f.Show();
            
         return retVal;
      }

      public Tuple<bool,string> InternalRun(string branchPath)
      {
         Tuple<bool, string> retVal = Tuple.Create(true,"");
         // source
         string sourceSubDir = @"Database Scripts\Schema Deployments\5100. EOD Monitor & Alerts Metadata";
         string eodFileName = "5102.2. EODMonitorMetadata.sql";
         string fullEODFileName = "5102.2. FullEODMonitorMetadata.sql";
         string sourceEODFilepath = Path.Combine(branchPath, sourceSubDir, eodFileName);
         string sourceFullEODFilepath = Path.Combine(branchPath, sourceSubDir, fullEODFileName);

         // dest 
         string destSubDir = @"Builds\Next Build";

         string destEODFilepath = Path.Combine(branchPath, destSubDir, eodFileName);
         string destFullEODFilepath = Path.Combine(branchPath, destSubDir, fullEODFileName);

         try
         {
            if (Directory.Exists(branchPath))
            {
               StringBuilder s = new StringBuilder();
               if (!File.Exists(destEODFilepath) && !File.Exists(destFullEODFilepath))
               {
                  Utils.EnsureFolder(Path.Combine(branchPath, destSubDir));
                  File.Copy(sourceEODFilepath, destEODFilepath);
                  s.AppendLine($"File copied: {destEODFilepath}");
               }
               else if (!File.Exists(destEODFilepath))
               {
                  s.AppendLine($"File exists: {destEODFilepath}");
               }
               else if (!File.Exists(destFullEODFilepath))
               {
                  s.AppendLine($"File exists: {destFullEODFilepath}");
               }
               retVal = Tuple.Create(true, s.ToString());
            }
         }
         catch (Exception ex)
         {
            retVal = Tuple.Create(false, ex.Message);
         }

         return retVal;
      }
   }
}
