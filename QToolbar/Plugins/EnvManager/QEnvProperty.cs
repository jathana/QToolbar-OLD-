using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvProperty
    {
        #region fields
        private string _Value = string.Empty;
        #endregion

        #region properties
        public QEnvPropCategory Category { get; set; }
        public QEnvPropSubCategory SubCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                string oldValue = _Value;
                _Value = value;
                OnValueSet(oldValue, _Value);
            }
        }
        public bool Required { get; set; }
        /// <summary>
        /// If false property is ignored during validation
        /// </summary>
        internal bool Active { get; set; }
        /// <summary>
        /// If true checks value to be nclosed in brackets during validation.
        /// </summary>
        internal bool CheckBrackets { get; set; }
        /// <summary>
        /// if has value then property value must equals this value.
        /// </summary>
        internal string CheckEqualsValue { get; set; }
        /// <summary>
        /// property value should match regular expression.
        /// </summary>
        internal string MatchRegexPattern { get; set; }
      public Errors Errors
        { get; internal set; }

        public QEnvProperty()
        {
            Errors = new Errors();
        }

        public bool EmptyValue
        {
            get { return string.IsNullOrEmpty(Value); }
        }

        #endregion

        public override string ToString()
        {
            return $"{Category} > {SubCategory} > {Name} : {Value}";
        }

        protected virtual void OnValueSet(string oldValue, string newValue)
        {

        }

        public virtual Errors Validate()
        {
            Errors.Clear();

            if (Active)
            {
                // check required
                if (Required && string.IsNullOrEmpty(Value))
                {
                    Errors.AddError($"{ToString()}: Value is required.", "");
                }

                // check brackets
                if (CheckBrackets && !string.IsNullOrEmpty(Value))
                {
                    if (!Value.StartsWith("[") || !Value.EndsWith("]"))
                    {
                        Errors.AddError($"{ToString()}: Value \"{Value}\"should be enclosed in brackets.", "");
                    }
                }

                // checks equal value
                if (!string.IsNullOrEmpty(CheckEqualsValue) && !string.IsNullOrEmpty(Value))
                {
                    if (!Value.Equals(CheckEqualsValue))
                    {
                        Errors.AddError($"{ToString()}: Value \"{Value}\"should be equals to \"{CheckEqualsValue}\".", "");
                    }
                }

                // should ends with
                if(!EmptyValue &&  !string.IsNullOrEmpty(MatchRegexPattern))
                {
                    Regex reg = new Regex(MatchRegexPattern);
                    if (!reg.IsMatch(Value))
                    {
                        Errors.AddError($"{ToString()}: Value \"{Value}\"should follow patern \"{MatchRegexPattern}\".", "");
                    }
                }
            }
            return Errors;
        }
    }
}
