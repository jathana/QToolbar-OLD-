using QToolbar.Helpers;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.CFsValidator
{
   public delegate void CfFileLoadedForValidationEventHandler(object sender, CfFileValidationEventArgs e);

   public class CFsValidator
   {
      private DataTable _Data=null;
      private DataTable _Errors = null;

      public DataTable Data
      {
         get
         {
            return _Data;
         }

         set
         {
            _Data = value;
         }
      }

      public DataTable Errors
      {
         get
         {
            return _Errors;
         }

         set
         {
            _Errors = value;
         }
      }

      public event CfFileLoadedForValidationEventHandler CfFileLoaded;

      public CFsValidator()
      {
         Data = new DataTable();

         Data.Columns.Add("VERSION", typeof(string));
         Data.Columns.Add("SOURCE", typeof(string)); // CHECKOUT, TESTING etc.
         Data.Columns.Add("REPOSITORY", typeof(string));
         Data.Columns.Add("CF_FILE", typeof(string));
         Data.Columns.Add("SECTION", typeof(string));
         Data.Columns.Add("KEY", typeof(string));
         Data.Columns.Add("VALUE", typeof(string));
         Data.Columns.Add("ERROR_MSG", typeof(string));
         Data.Columns.Add("HAS_ERROR", typeof(bool));


         Errors = new DataTable();
         Errors.Columns.Add("MSG", typeof(string));
      }

      public bool Validate()
      {
         bool retVal = true;

         Data.Clear();
         Errors.Clear();

         retVal = LoadCFs();

         retVal = ValidateCFs() && retVal;

         retVal = ValidateKeys() && retVal;

         return retVal;
      }

      #region validations
      private bool ValidateCFs()
      {
         bool retVal = ValidateServerDatabasePassword();
         retVal = ValidatePasswordValue() && retVal;

         return retVal;
      }

      private bool ValidateServerDatabasePassword()
      {
         bool retVal = true;

         //_Data.Select().GroupBy()
         // each key of a version having proteus must have QC and PROTEUS repository


         var grouped = from table in _Data.AsEnumerable()

                       group table by new { keyCol = table["KEY"], repoCol = table["REPOSITORY"] } into groupby

                       select new

                       {

                          Value = groupby.Key,
                          ColumnValues = groupby

                       };

         var s = grouped.ToList();

         var grouped2 = from table in s.AsEnumerable()

                        group table by new { keyCol2 = table.Value.keyCol }  into groupby
                        where groupby.Count()==1
                        select new
                        {
                           Value = groupby.Key,
                           ColumnValues = groupby,
                           Count = groupby.Count()
                        };

         return retVal;
      }

      private bool ValidatePasswordValue()
      {
         bool retVal = true;
         DataRow[] invalidPwds=_Data.Select("SECTION='PASSWORDS' AND KEY='PASSWORD' AND VALUE <> '6702F80E8CD674F9E97BF27871005CE3'");

         foreach(DataRow row in invalidPwds)
         {
            row["ERROR_MSG"] = "Invalid password Value.";
         }

         return retVal;
      }

      private bool ValidateKeys()
      {
         bool retVal = true;


         var grouped = from table in _Data.Select("SECTION='DBS'").AsEnumerable() 
                       group table by new { versionCol = table["VERSION"], valueCol = table["VALUE"], keyCol = table["KEY"] } into groupby
                       select new
                       {
                          Value = groupby.Key,
                          ColumnValues = groupby
                       };

         var s = grouped.ToList();

         var grouped2 = from table in s.AsEnumerable()

                        group table by new { keyCol2 = table.Value.valueCol } into groupby
                        where groupby.Count() > 1
                        select new
                        {
                           Value = groupby.Key,
                           ColumnValues = groupby,
                           Count = groupby.Count()
                        };

         foreach (var obj in grouped2.ToList())
         {
            foreach( DataRow row in _Data.Select($"SECTION='DBS' AND VALUE='{obj.Value.keyCol2}'"))
            {
               row["HAS_ERROR"] = true;
               row["ERROR_MSG"] = $"Database '{obj.Value.keyCol2}' found with key {row["KEY"]}";
            }
         }

         return retVal;
      }
      #endregion


      private bool LoadCFs()
      {
         bool retVal = true;

         LoadLocalCFs();
         LoadTestingAppsCFs();
         return retVal;
      }

      private bool LoadLocalCFs()
      {
         bool retVal = true;


         foreach (DataRow chRow in OptionsInstance.Checkouts.Data.Rows)
         {
            if (Directory.Exists(chRow["Path"].ToString()))
            {
               // qc
               foreach (DataRow cfRow in OptionsInstance.EnvCFs.Data.Select("Repository='QC'"))
               {
                  string cfFile = Path.Combine(chRow["Path"].ToString(), cfRow["Path"].ToString());
                  LoadCFData(chRow["Name"].ToString(), "CHECKOUT", "QC", cfFile);
               }

               // proteus
               foreach (DataRow cfRow in OptionsInstance.EnvCFs.Data.Select("Repository='PROTEUS'"))
               {
                  string cfFile = Path.Combine(chRow["ProteusPath"].ToString(), cfRow["Path"].ToString());
                  LoadCFData(chRow["Name"].ToString(), "CHECKOUT", "PROTEUS", cfFile);
               }

            }

         }

         return retVal;
      }


      private bool LoadTestingAppCFs(string appFolder,string cfFileName)
      {
         bool retVal = true;

         string[] dirs= Directory.GetDirectories(appFolder);

         foreach (string dir in dirs)
         {
            string qcsAdminCFDir = Path.Combine(dir, "Application Files");
            // find the newest dir
            if (Directory.Exists(qcsAdminCFDir))
            {
               string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
               string destDir = "";
               Version maxVersion = new Version(0, 0, 0, 0);
               foreach (string subdir in subdirs)
               {
                  Version curVersion = Utils.GetVersion(subdir, "_", 1);

                  if (curVersion != null)
                  {
                     if (maxVersion < curVersion)
                     {
                        maxVersion = curVersion;
                        destDir = subdir;
                     }
                  }
                  else
                  {
                     //XtraMessageBox.Show("Cannot parse directory name.");
                  }
               }
               string file = Path.Combine(destDir, cfFileName);
               LoadCFData(Path.GetFileName(dir), "TESTING", "QC", file);
            }
         }
         return retVal;
      }


      private bool LoadTestingAppsCFs()
      {
         return 
            LoadTestingAppCFs(OptionsInstance.QCSAdminFolder, "QBC_Admin.cf.deploy") &
            LoadTestingAppCFs(OptionsInstance.QCSAgentFolder, "QBC.cf.deploy");
      }


      private bool LoadCFData(string version, string source, string repository, string cfFile)
      {
         bool retVal = true;

         CfFile cfObj = new CfFile(cfFile);

         List<Tuple<string, string>> servers = cfObj.GetServers();
         List<Tuple<string, string>> dbs = cfObj.GetDatabases();
         List<Tuple<string, string>> passwords = cfObj.GetPasswords();

         foreach (var srv in servers)
         {
            AddRow(cfFile, version, source, repository, "SERVERS", srv.Item1, srv.Item2);
         }
         foreach (var db in dbs)
         {
            AddRow(cfFile, version, source, repository, "DBS", db.Item1, db.Item2);
         }
         foreach (var pwd in passwords)
         {
            AddRow(cfFile, version, source, repository, "PASSWORDS", pwd.Item1, pwd.Item2);
         }

         OnCfFileLoaded(new CfFileValidationEventArgs(version, source, repository, cfFile));
         return retVal;
      }


      private DataRow AddRow(string cfFile,string version,string source,string repository,string section,string key, string value)
      {
         DataRow newRow = Data.NewRow();
         newRow["CF_FILE"] = cfFile.ToLower();
         newRow["VERSION"] = version.ToLower();
         newRow["SOURCE"] = source.ToLower();
         newRow["REPOSITORY"] = repository.ToLower();
         newRow["SECTION"] = section;
         newRow["KEY"] = key.ToLower();
         newRow["VALUE"] = value.ToLower();
         Data.Rows.Add(newRow);

         return newRow;
      }

      protected virtual void OnCfFileLoaded(CfFileValidationEventArgs e)
      {
         CfFileLoadedForValidationEventHandler handler = CfFileLoaded;
         if (handler != null)
         {
            handler(this, e);
         }
      }
   }
}
