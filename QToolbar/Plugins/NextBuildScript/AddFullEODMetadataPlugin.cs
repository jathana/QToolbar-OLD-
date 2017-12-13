using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.NextBuildScript
{
   [Plugin(pluginName: "Add Full EOD Metadata", pluginDesc: @"Copies 5102.2. FullEODMonitorMetadata.sql to Next Build folder.")]
   public class AddFullEODMetadataPlugin : QPlugin, IQPlugin
   {

      public AddFullEODMetadataPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p = (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_NextBuildScript()
         {
            Text=p.PluginName,
            Message = p.PluginDesc,
            Action=InternalRun
         };
         f.ShowDialog();
            
         return retVal;
      }

      public Tuple<bool, string> InternalRun(string branchPath)
      {
         Tuple<bool, string> retVal = Tuple.Create(true, "");

         // source
         string sourceSubDir = @"Database Scripts\Schema Deployments\5100. EOD Monitor & Alerts Metadata";
         string sourceEODFilepath = Path.Combine(branchPath, sourceSubDir, "5102.2. EODMonitorMetadata.sql");
         string sourceFullEODFilepath = Path.Combine(branchPath, sourceSubDir, "5102.2. FullEODMonitorMetadata.sql");

         // dest 
         string destSubDir = @"Builds\Next Build";
         string destEODFilepath = Path.Combine(branchPath, destSubDir, "5102.2. EODMonitorMetadata.sql");
         string destFullEODFilepath = Path.Combine(branchPath, destSubDir, "5102.2. FullEODMonitorMetadata.sql");

         try
         {
            if (Directory.Exists(branchPath))
            {
               StringBuilder s = new StringBuilder();
               if (!File.Exists(destFullEODFilepath))
               {
                  Utils.EnsureFolder(Path.Combine(branchPath, destSubDir));
                  File.Copy(sourceFullEODFilepath, destFullEODFilepath);
                  s.AppendLine($"File copied: {destFullEODFilepath}");
               }
               else
               {
                  s.AppendLine($"File exists: {destFullEODFilepath}");
               }
               if (File.Exists(destEODFilepath))
               {
                  File.Delete(destEODFilepath);
                  s.AppendLine($"File deleted: {destEODFilepath}");
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
