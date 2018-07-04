using QToolbar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.Environments
{
   public class QEnvironmentParametersSet: List<QEnvironmentParameter>
   {
      private string _Name = string.Empty;
      private QEnvironmentParametersSetType _SetType=QEnvironmentParametersSetType.None;
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

      public QEnvironmentParametersSetType SetType
      {
         get
         {
            return _SetType;
         }

         set
         {
            _SetType = value;
         }
      }


      public Errors Validate()
      {
         Errors ret = new Errors();
         switch(_SetType)
         {
            case QEnvironmentParametersSetType.ParametersShouldHaveEqualValues:
               ret.AddRange(ValidateParametersShouldHaveEqualValues());
               break;
         }
         return ret;

      }

      private Errors ValidateParametersShouldHaveEqualValues()
      {
         Errors ret = new Errors();
            string val = this[0].Value;
            bool allEqual = true;
            StringBuilder b = new StringBuilder();
            b.AppendLine($"Found different values among {_Name} parameters.");
            b.AppendLine($"Param {this[0].Name.ToString()} = {val}");

            for (int i = 1; i < this.Count; i++)
            {
               if (!val.ToLower().Equals(this[i].Value.ToLower()) && allEqual)
               {
                  allEqual = false;
               }
               b.AppendLine($"param {this[i].Name.ToString()} = {this[i].Value}");
            }

            if (!allEqual)
            {
               ret.AddError(b.ToString(), "");
            }
         return ret;

      }
   }
}
