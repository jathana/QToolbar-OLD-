using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvPropertySet : List<QEnvProperty>
    {
        public string Description { get; set; }

        public QEnvPropSetDependency DependencyType { get; set; }

        public virtual Errors Validate()
        {
            Errors retval = new Errors();
            
            return retval;
        }



    }
}
