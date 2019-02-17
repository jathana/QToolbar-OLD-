using QToolbar.Helpers;
using System;
using System.Collections.Generic;
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
            SetType = SetType;
            Name = name;
        }

        public Errors Validate()
        {
            Errors retval = new Errors();

            switch (SetType)
            {
                case QEnvPropertySetType.List:
                    Validate_List();
                    break;

                case QEnvPropertySetType.MustHaveSameValue:
                    Validate_MustHaveSameValue();
                    break;

                case QEnvPropertySetType.DataBaseConnection:
                    Validate_DataBaseConnection();
                    break;
            }
            return retval;
        }

        private Errors Validate_List()
        {
            Errors retval = new Errors();
            this.ForEach(delegate (QEnvProperty property)
            {
                retval.AddRange(property.Validate());
            });

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
            return retval;
        }


    }
}
