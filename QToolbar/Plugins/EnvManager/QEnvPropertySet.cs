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
        public string Name { get; set; }
        public string Description { get; set; }
        public QEnvPropertySetType SetType { get; set; }


        public QEnvPropertySet(QEnvPropertySetType setType, string name)
        {
            SetType = setType;
            Name = name;
        }

        public Errors Validate()
        {
            Errors retval = new Errors();

            switch (SetType)
            {
                case QEnvPropertySetType.List:
                    retval.AddRange(Validate_List());
                    break;

                case QEnvPropertySetType.MustHaveSameValue:
                    retval.AddRange(Validate_MustHaveSameValue());
                    break;

                case QEnvPropertySetType.DataBaseConnection:
                    retval.AddRange(Validate_DataBaseConnection());
                    break;
            }
            return retval;
        }

        private Errors Validate_List()
        {
            Errors retval = new Errors();
            foreach(var property in this.ToList())
            {
                retval.AddRange(property.Validate());
            }

            return retval;
        }

        private Errors Validate_MustHaveSameValue()
        {
            Errors retval = new Errors();

            string value = string.Empty;

            if (this.Where(p=>!p.EmptyValue).GroupBy(p => p.Value).Count() > 1)
            {
                retval.AddError($"Properties {string.Join(",", this.Select(p => p.Name).ToList())} should have the same value.", "");
            }

            return retval;
        }

        private Errors Validate_DataBaseConnection()
        {
            Errors retval = new Errors();

            string server = this.FirstOrDefault(p => p is QEnvServerProperty).Value;
            string db = this.FirstOrDefault(p => p is QEnvDatabaseProperty).Value;

            string constr = Utils.GetConnectionString(server, db);
            try
            {
                SqlConnection con = new SqlConnection();
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return retval;
        }


    }
}
