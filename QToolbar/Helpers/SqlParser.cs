using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class SqlParser
   {
      private TSqlFragment _SqlFragment;
      private IList<ParseError> _ParseErrors;
      private TSql140Parser _Parser;
      private List<FunctionCall> _FunctionCalls;

      public List<FunctionCall> FunctionCalls
      {
         get
         {
            return _FunctionCalls;
         }
      }

      public IList<ParseError> ParseErrors
      {
         get
         {
            return _ParseErrors;
         }
      }

      public SqlParser()
      {
         _Parser = new TSql140Parser(false);
         _FunctionCalls = new List<FunctionCall>();
      }

      public bool Parse(string path)
      {
         bool retval = true;
         try
         {
            _SqlFragment = _Parser.Parse(File.OpenText(path), out _ParseErrors);
            List<Type> visited = new List<Type>();
            //ReadPropertiesRecursive(_SqlFragment.GetType(), visited);
            _FunctionCalls.Clear();            
            SearchParseInfo(_SqlFragment, 5);
            retval = ParseErrors.Count == 0;
         }
         catch(Exception ex)
         {
            
         }

         return retval;
      }
      

      private void SearchParseInfo(object obj, int indent)
      {
         if (obj == null)
         {
            return;
         }
         string indentString = new string(' ', indent);
         Type objType = obj.GetType();
         PropertyInfo[] properties = objType.GetProperties();
         foreach (PropertyInfo property in properties)
         {
            object propValue = null;

            if (property.GetIndexParameters().Length==0)  
               propValue = property.GetValue(obj);
            else
            {
               propValue = property.GetValue(obj, new object[] { 0 });
            }
            if (propValue is TSqlFragment)
            {
               if(propValue is FunctionCall)
               {
                  FunctionCalls.Add((FunctionCall)propValue);
                  //Console.WriteLine("{0}{1}: {2} {3}", indentString, property.Name, propValue, ((FunctionCall)propValue).FunctionName.Value);
               }
            }
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
               Console.WriteLine("{0}{1}:", indentString, property.Name);
               if (propValue != null && property.PropertyType != typeof(string[]))
               {
                  IEnumerable enumerable = (IEnumerable)propValue;
                  foreach (object child in enumerable)
                  {
                     if (child is TSqlFragment)
                     {
                        SearchParseInfo(child, indent + 2);
                     }
                  }
               }
            }
            else
            {
               Console.WriteLine("{0}{1}:", indentString, property.Name);
               SearchParseInfo(propValue, indent + 2);
            }
         }
      }

      private bool IsSimpleType(Type type)
      {
         return
             type.IsValueType ||
             type.IsPrimitive ||
             new Type[]
             {
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
             }.Contains(type) ||
             Convert.GetTypeCode(type) != TypeCode.Object;
      }




   }
}
