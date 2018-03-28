using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   /// <summary>
   /// Creates enviroment information
   /// </summary>
   public class QEnvironmentCreator
   {
      private QEnvironmentConfiguration _Env;

      public QEnvironmentCreator(QEnvironmentConfiguration env)
      {
         _Env = env;
      }

      /// <summary>
      /// Updates a cf file only if database info does not exist.
      /// </summary>
      /// <param name="cfFile"></param>
      /// <param name="server"></param>
      /// <param name="db"></param>
      /// <param name="password"></param>
      /// <returns></returns>
      public List<string> UpdateCF(string cfFile, string key, string server, string db, string password)
      {
         List<string> retval = new List<string>();
         try
         {
            if (File.Exists(cfFile))
            {
               IniFile ini = new IniFile(cfFile, "#");
               string curServer = ini.GetValue("Servers", key);
               string curDB = ini.GetValue("DatabaseName", key);
               string curPassword = ini.GetValue("Passwords", key);

               if (curServer != server && curDB != db && curPassword != password)
               {
                  // update cf appending values at the end of each section
                  IniFile2.WriteValue("Servers", key, server, cfFile);
                  IniFile2.WriteValue("DatabaseName", key, db, cfFile);
                  IniFile2.WriteValue("Passwords", key, password, cfFile);
               }
            }
         }
         catch(Exception ex)
         {
            retval.Add($"Failed to update cf {cfFile} (server:{server}, db:{db}, password:{password}.");
         }
         return retval;
      }

      public List<string> UpdateQCSolutionsCFs(string qcCheckoutPath, string key, string server, string db, string password)
      {
         List<string> retval = new List<string>();

         
         // CollectionAgentSystem
         string agentPathCF = Path.Combine(qcCheckoutPath, "CollectionAgentSystem\\CollectionAgentSystemClient\\QBC.cf");
         retval.AddRange(UpdateCF(agentPathCF, key, server, db, password));

         // MicroQCClient
         string microCF = Path.Combine(qcCheckoutPath, "QCS\\MicroQClient\\QBC_Admin.cf");
         retval.AddRange(UpdateCF(microCF, key, server, db, password));

         // QCSClient
         string qcsClientCF = Path.Combine(qcCheckoutPath, "QCS\\QCSClient\\QBC_Admin.cf");
         retval.AddRange(UpdateCF(qcsClientCF, key, server, db, password));

         // QCSWS
         string qcsWSCF = Path.Combine(qcCheckoutPath, "QCS\\QCSWS\\QBC.cf");
         retval.AddRange(UpdateCF(qcsWSCF, key, server, db, password));

         // SCToolkit\SCToolkitWS
         string toolkitWSCF = Path.Combine(qcCheckoutPath, "SCToolkit\\SCToolkitWS\\QBC.cf");
         retval.AddRange(UpdateCF(toolkitWSCF, key, server, db, password));

         return retval;
      }

      public List<string> UpdateQCArchiveCF(string qcCheckoutPath, string key, string server, string db, string password)
      {
         List<string> retval = new List<string>();


         // Archive Client
         string archivehCF = Path.Combine(qcCheckoutPath, @"QC.DataArchive\QC.DataArchive.Client\QC_DataArchive.cf");
         retval.AddRange(UpdateCF(archivehCF, key, server, db, password));


         return retval;
      }


   }
}
