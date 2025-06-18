using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataLayer.D_CRUDOperation.D_Read;
using DataLayer.D_CRUDOperation.D_Read;
using MidLayer.Validation;
using MidLayer.Process;
namespace MidLayer.B_CRUDOperation.B_Read
{
    public class B_Check
    {
        //public bool Row<T>(string TableName, string ColumnName, T Value, bool ExactValue = false) where T : IConvertible
        //{
        //    return _Process.PerformOperation(TableName, ColumnName, Value, (table, column, value) => D_Check.Row(table, column, value, _ValidationService.ParameterName(table)), ExactValue);
        //}
            public async Task<bool> Row<T>(string TableName, string ColumnName, T Value, bool ExactValue = false) where T : IConvertible
            {
                return await _Process.PerformOperation(TableName, ColumnName, Value, async (table, column, value) => await D_Check.Row(table, column, value, _ValidationService.ParameterName(table)).ConfigureAwait(false), ExactValue).ConfigureAwait(false);
            }

    }
}
#region tries
/* public  bool Row<T>(string TableName, string ColumnName, T Value, bool ExactValue = false) where T : IConvertible
 {
     return _Process.PerformOperation(TableName, ColumnName, Value,(table,column,value)=>D_Check.Row(table,column,value,_ValidationService.ParameterName(table)), ExactValue);
 }*/
/* public bool Row<T>(string TableName, string ColumnName, T Value, bool ExactValue = false) where T : IConvertible
 {
     return _Process.PerformOperation(TableName, ColumnName, Value, (table, column, value) =>
     {
         return D_Check.Row(table, column, value, _ValidationService.ParameterName(table)) ? (T)Convert.ChangeType(true, typeof(T)) : (T)Convert.ChangeType(false, typeof(T));
     }, ExactValue);
 }*/
/* public bool Row<T>(string TableName, string ColumnName, T Value, bool ExactValue = false) where T : IConvertible
 {
     try
     {
         if (_ValidationService.Validation(TableName) && _ValidationService.IsAllowedType<T>())
             if (Value is string column)
             {
                 if (!ExactValue)
                     column += "%";
                 return D_Check.Row(TableName, ColumnName, column, _ValidationService.ParameterName(TableName));
             }
             else
                 return D_Check.Row(TableName, ColumnName, Value, _ValidationService.ParameterName(TableName));

     }
     catch (Exception ex)
     {
         throw ex;
     }
             return false;
 }*/
#endregion
