using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvPropertySetDBConnection : QEnvPropertySet
    {
        
        public override Errors Validate()
        {
            Errors retval = new Errors();

            string server = this.FirstOrDefault(p => p is QEnvServerProperty).Value;
            string db = this.FirstOrDefault(p => p is QEnvDatabaseProperty).Value;

            string constr = Utils.GetConnectionString(server, db);
            try
            {
                SqlConnection con = new SqlConnection();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return retval;
        }
    }
}
