using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QToolbar.Helpers;
using System.IO;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvUNCDirProperty : QEnvProperty
    {

        private int _Permissions = -1;

        public string LocalPath { get; set; }
        public string AccessRights { get; set; }
        public bool Resolved { get; internal set; }
        public bool FullAccessRequired { get; set; }
        public string Host { get; set; }
        public bool ResolveUNC { get; set; }

        protected override void OnValueSet(string oldValue, string newValue)
        {
            if (ResolveUNC)
            {
                Resolve();
            }
        }

        public QEnvUNCDirProperty()
        {
            ResolveUNC = false;
        }
        private void Resolve()
        {
            bool unresolved = false;
            LocalPath = Utils.GetPath(Value, out _Permissions, out unresolved);

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

            if (Active)
            {
                if (FullAccessRequired)
                {
                    if (_Permissions != Utils.FILE_PERMISSION_FULL_ACCESS)
                    {
                        retval.AddError($"{ToString()} : Full Access permission is required \"{Value}\"", Value);
                    }
                    if (!Resolved)
                    {
                        retval.AddError($"{ToString()} : Unresolved dir {Description} : {Value}.", Value);
                    }
                }

                if (!Directory.Exists(Value))
                {
                    retval.AddError($"{ToString()} : Dir not found \"{Value}\"", Value);
                }
            }
            return retval;
        }
    }
}
