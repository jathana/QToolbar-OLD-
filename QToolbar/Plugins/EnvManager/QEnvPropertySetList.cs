using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvPropertySetList : QEnvPropertySet
    {

        public override Errors Validate()
        {
            Errors retval = new Errors();
            foreach (var property in this.ToList())
            {
                retval.AddRange(property.Validate());
            }

            return retval;
        }

    }
}
