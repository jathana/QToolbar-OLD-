using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QToolbar.Helpers;

namespace QToolbar.Plugins.EnvManager
{
   public class QEnvUNCDirProperty: QEnvProperty
   {

      private int _Permissions = -1;

      public string LocalPath { get; set; }
      public string AccessRights { get; set; }
      public bool Resolved { get; set; }
      public bool FullAccessRequired { get; set; }
      public string Host { get; set; }
      

      protected override void OnValueSet(string oldValue, string newValue)
      {         
         bool unresolved = false;
         LocalPath = Utils.GetPath(newValue, out _Permissions, out unresolved);

         Resolved = !unresolved;
         AccessRights = Utils.GetPermissionsDesc(_Permissions);

         if (!EmptyValue)
         {
            Uri uri = new Uri(Value);
            Host = uri.Host;
         }
      }

      public override Errors Validate()
      {
         Errors retval = new Errors();
         retval.AddRange(base.Validate());

         if(FullAccessRequired)
         {
            if (_Permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
            {
               retval.AddError($"Full Access permission is required \"{Value}\"", Value);
            }
            if (!Resolved)
            {
               retval.AddError($"Unresolved dir {Description} : {Value}.", Value);
            }
         }
         return retval;
      }
   }
}
