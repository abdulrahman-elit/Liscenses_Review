using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation;
using EntityLayer.Interfaces;

using MidLayer.Process;
using MidLayer.Validation;
//using MidLayer.Validation;

namespace MidLayer.B_CRUDOperation
{
    internal class _B_Update
    {

        //internal static bool Row<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn = null) where T : ISupportedType<T>
        //{
        //    return  _Process.PerformOperation(TableName, ColumnName, Value, ConditionColumn, (tableName,columnName, value, conditionColumn) => D_Update.Row(tableName, columnName, value, conditionColumn, _ValidationService.ParameterName(TableName)));
        //}

        internal static async Task <bool> Row<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn = null) where T : ISupportedType<T>
        {
            return await _Process.PerformOperation(TableName, ColumnName, Value, ConditionColumn, async (tableName, columnName, value, conditionColumn) => await D_Update.Row(tableName, columnName, value, conditionColumn, _ValidationService.ParameterName(TableName)).ConfigureAwait(false)).ConfigureAwait(false);
        }


    }
}
#region tries
//internal static bool Row<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn=null)
//{
//    try
//    {
//        if (_ValidationService.Validation(TableName, ColumnName)  && Value is ISupportedType<T> supported)
//            if (supported.Validation(Value)&&_ValidationService.Validate(supported.PraimeryIntColumnIDName))
//            {
//                ColumnName.Remove(supported.PraimeryIntColumnIDName);
//                if (string.IsNullOrWhiteSpace(ConditionColumn))
//                {
//                    return D_Update.Row(TableName, ColumnName, Value,supported.PraimeryIntColumnIDName, _ValidationService.ParameterName(TableName));
//                }
//                else
//                {
//                    ColumnName.Remove(ConditionColumn);
//                    return D_Update.Row(TableName, ColumnName, Value, ConditionColumn, _ValidationService.ParameterName(TableName));
//                }

//            }
//    }
//    catch(Exception ex) 
//    {
//        throw ex;
//    }
//      return false;
//}
#endregion