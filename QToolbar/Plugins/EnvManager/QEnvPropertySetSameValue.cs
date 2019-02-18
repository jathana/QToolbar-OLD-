using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvPropertySetSameValue : QEnvPropertySet
    {
        private string _MatchRegexPattern;

        public string MatchRegexPattern
        {
            get
            {
                return _MatchRegexPattern;
            }

            set
            {
                _MatchRegexPattern = value;
            }
        }

        public Errors Validate()
        {
            Errors retval = new Errors();

            string value = string.Empty;
            // check same value
            if (this.Where(p => !p.EmptyValue).GroupBy(p => p.Value).Count() > 1)
            {
                retval.AddError($"Properties {string.Join(",", this.Select(p => p.Name).ToList())} should have the same value.", "");
            }


            // set CheckRegex             
            foreach (QEnvProperty property in this)
            {

                // should ends with
                if (!property.EmptyValue && !string.IsNullOrEmpty(MatchRegexPattern))
                {
                    Regex reg = new Regex(MatchRegexPattern);
                    if (!reg.IsMatch(property.Value))
                    {
                        retval.AddError($"{property.ToString()}: Value \"{property.Value}\"should follow patern \"{MatchRegexPattern}\".", "");
                    }
                }
                
            }

            return retval;
        }



    }
}
