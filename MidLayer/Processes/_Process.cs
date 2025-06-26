using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Interfaces;
//using MidLayer.Validation;
namespace MidLayer.Process
{
    internal class _Process
    {
        internal static async Task<T> PerformOperation<T, U>(string TableName, string ColumnName, U Value, Func<string, string, U, Task<T>> filter, bool ExactValue = false) where U : IConvertible
        {
            try
            {
             //   if (_ValidationService.Validation(TableName) && _ValidationService.IsAllowedType<U>())
                {

                    if (Value is string str && ExactValue)
                    {
                        str += "%";
                        Value = (U)(object)str;
                    }
                    return await filter(TableName, ColumnName, Value).ConfigureAwait(false);
                }

            }
            catch
            {
                throw;
            }
            
        }

        internal static async Task< bool> PerformOperation<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn, Func<string, HashSet<string>, T, string,Task< bool>> operation) where T : ISupportedType<T>
        {
            try
            {
                if (Value is ISupportedType<T> supported &&await supported.Validation())
                //if (Value is ISupportedType<T> supported && supported.Validation(Value))
                {
                   // if (_ValidationService.Validation(TableName, ColumnName) && _ValidationService.Validate(supported.PrimaryIntColumnIDName))
                    {
                        ColumnName.Remove(supported.PrimaryIntColumnIDName);

                        if (!string.IsNullOrWhiteSpace(ConditionColumn))
                            ColumnName.Remove(ConditionColumn);
                        return await operation(TableName, ColumnName, Value, ConditionColumn ?? supported.PrimaryIntColumnIDName).ConfigureAwait(false);
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;
        }

    }
}
//if ((int)supported.GetType().GetProperty(supported.PrimaryIntColumnIDName).GetValue(Value) == -1)
//if((int)typeof(T).GetProperty(supported.PrimaryIntColumnIDName).GetValue(Value) == -1)

