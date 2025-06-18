using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using DataLayer.D_CRUDOperation;
using MidLayer.Process;
using MidLayer.Validation;

namespace MidLayer.B_CRUDOperation
{
    internal class _B_Create 
    {
        //internal static bool Row<T>(string TableName, HashSet<string> ColumnName, T Value) where T :ISupportedType<T>
        //{

        //    int result = -1;
        //    bool succes = _Process.PerformOperation(TableName, ColumnName, Value, null, (tableName, columnName, value, conditionColumn) => { result = D_Create.Row(tableName, columnName, value, _ValidationService.ParameterName(TableName)); return result != -1; });
        //    ISupportedType<T> supported = Value;
        //    if (succes)
        //        supported.PrimaryKey(result);
        //    return succes;
        //}
        internal static async Task< bool> Row<T>(string TableName, HashSet<string> ColumnName, T Value) where T : ISupportedType<T>
        {

            int result = -1;
            bool succes = await _Process.PerformOperation(TableName, ColumnName, Value, null,async (tableName, columnName, value, conditionColumn) => { result = await D_Create.Row(tableName, columnName, value, _ValidationService.ParameterName(TableName)).ConfigureAwait(false); return result != -1; }).ConfigureAwait(false);
            ISupportedType<T> supported = Value;
            if (succes)
                supported.PrimaryKey(result);
            return succes;
        }

    }
}
#region tries
/*internal static bool Row<T>(string TableName, HashSet<string> ColumnName, T Value) where T : ISupportedType<T>
{
    try
    {
        if (_ValidationService.Validation(TableName, ColumnName) && Value is ISupportedType<T> supported)
            if (supported.Validation(Value)&&_ValidationService.Validate(supported.PraimeryIntColumnIDName))
            {
                ColumnName.Remove(supported.PraimeryIntColumnIDName);
                int result = D_Create.Row(TableName, ColumnName, Value, _ValidationService.ParameterName(TableName));
                if (result != -1)
                {
                        typeof(T).GetProperty(supported.PraimeryIntColumnIDName).SetValue(Value,result);
                        return true;
                }
            }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    return false;
}*/
//Value.GetType().GetProperty(IDColumn).SetValue(Value, D_Create.Row(TableName, ColumnName, Value, _ValidationService.ParameterName(TableName)));
//supported.ID = D_Create.Row(TableName, ColumnName, Value, _ValidationService.ParameterName(TableName));
#endregion