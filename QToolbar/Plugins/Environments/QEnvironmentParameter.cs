using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentParameter
   {
      private string _Name;
      private QEnvironmentParameterTypeEnum _ParamType = QEnvironmentParameterTypeEnum.SimpleValue;
      private string _Desc = string.Empty;
      private string _Source = string.Empty;
      private string _Value;
      private bool _Mandatory = true;
      private bool _MustHaveSpecificValue = false;
      private string _SpecificValue = string.Empty;    

      public string Name
      {
         get
         {
            return _Name;
         }

         set
         {
            _Name = value;
         }
      }

      public QEnvironmentParameterTypeEnum ParamType
            {
               get
               {
                  return _ParamType;
               }

               set
               {
                  _ParamType = value;
               }
            }
      public string Desc
      {
         get
         {
            return _Desc;
         }

         set
         {
            _Desc = value;
         }
      }

      public string Source
      {
         get
         {
            return _Source;
         }

         set
         {
            _Source = value;
         }
      }

      public string Value
      {
         get
         {
            return _Value;
         }

         set
         {
            _Value = value;
         }
      }

      public bool Mandatory
      {
         get
         {
            return _Mandatory;
         }

         set
         {
            _Mandatory = value;
         }
      }

      public bool MustHaveSpecificValue
      {
         get
         {
            return _MustHaveSpecificValue;
         }

         set
         {
            _MustHaveSpecificValue = value;
         }
      }

      public string SpecificValue
      {
         get
         {
            return _SpecificValue;
         }

         set
         {
            _SpecificValue = value;
         }
      }

      

      public virtual Errors Validate()
      {
         Errors ret = new Errors();
         if(_Mandatory)
         {
            if(string.IsNullOrEmpty(_Value))
            {
               ret.AddError($"Parameter {_Name} should have value.", "");
            }
         }
         if(_MustHaveSpecificValue)
         {
            if(!string.IsNullOrEmpty(_SpecificValue))
            {
               if(!_SpecificValue.ToLower().Equals(_Value.ToLower()))
               {
                  ret.AddError($"Parameter {_Name} should have the value {_SpecificValue} but found {_Value}.", "");
               }
            }
         }

         if(_ParamType == QEnvironmentParameterTypeEnum.Directory)
         {
            // check if exists
            if(!Directory.Exists(_Value))
            {
               ret.AddError($"Directory parameter {_Name} = {_Value} does not exist!",_Value);
            }
         }
         else if (_ParamType == QEnvironmentParameterTypeEnum.File)
         {
            // check if exists
            if (!File.Exists(_Value))
            {
               ret.AddError($"File parameter {_Name} = {_Value} does not exist!", _Value);
            }
         }

         return ret;
      }
      
   }
}
