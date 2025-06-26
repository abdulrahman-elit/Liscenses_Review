using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation;
using MidLayer.Process;
using MidLayer.Validation;
//using MidLayer.Validation;

namespace MidLayer.B_CRUDOperation
{
    public class B_Delete
    {
        //public static bool Row<T>(string TableName, string ConditionColumn, T ConditionValue) where T : IConvertible
        //{
        //    return _Process.PerformOperation(TableName, ConditionColumn, ConditionValue, (table, column, value) => D_Delete.Row(table, column, value, _ValidationService.ParameterName(table)));
        //}
        public static async Task<bool> Row<T>(string TableName, string ConditionColumn, T ConditionValue) where T : IConvertible
        {
            return await _Process.PerformOperation(TableName, ConditionColumn, ConditionValue, async (table, column, value) => await D_Delete.Row(table, column, value, _ValidationService.ParameterName(table)).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}
#region tries
/*public static bool Row<T>(string TableName, string ConditionColumn, T ConditionValue) where T : IConvertible
{
    try
    {
        if (_ValidationService.Validation(TableName) && _ValidationService.IsAllowedType<T>()&& _ValidationService.Validation(ConditionColumn))
        return D_Delete.Row(TableName, ConditionColumn, ConditionValue,_ValidationService.ParameterName(TableName));

    }
    catch (Exception ex)
    {
        throw ex;
    }
    return false;
}*/
#endregion