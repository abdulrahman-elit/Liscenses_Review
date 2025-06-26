using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataLayer.D_CRUDOperation;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using MidLayer.Process;
using MidLayer.Validation;
namespace MidLayer.B_CRUDOperation.B_Read
{
    public class B_Fetch
    {
        //public DataTable Rows<T>(string TableName, HashSet<string> ShowedColumn = null, string ColumnName = null, bool ExactValue = false, bool OnlyRow = false, T Value = default) where T : IConvertible
        //{
        //    return _Process.PerformOperation(TableName, ColumnName, Value, (table, column, value) => D_Fetch.Rows(table, ShowedColumn, column, value, _ValidationService.ParameterName(table), OnlyRow), ExactValue);
        //}
        //public DataTable Rows(string TableName, HashSet<string> ShowedColumn = null)
        //{
        //    return Rows<bool>(TableName, ShowedColumn);
        //}
        public async Task<DataTable> Rows<T>(string TableName, HashSet<string> ShowedColumn = null, string ColumnName = null, bool ExactValue = false, bool OnlyRow = false, T Value = default) where T : IConvertible
        {
            return await _Process.PerformOperation(TableName, ColumnName, Value, async (table, column, value) => await D_Fetch.Rows(table, ShowedColumn, column, value, _ValidationService.ParameterName(table), OnlyRow).ConfigureAwait(false), ExactValue).ConfigureAwait(false);
        }
        public async Task<DataTable> Rows(string TableName, HashSet<string> ShowedColumn = null)
        {
            return await Rows<bool>(TableName, ShowedColumn).ConfigureAwait(false);
        }
    }
}
#region tries
/*public DataTable Rows<T>(string TableName, string ColumnName = null, bool ExactValue = false, bool OnlyRow=false, T Value=default) where T:IConvertible
{
    try
    {
        if (_ValidationService.Validation(TableName) && _ValidationService.IsAllowedType<T>())
        {
            if (Value is string column)
            {
                if (!ExactValue)
                    column += "%";
                return D_Fetch.Rows(TableName, ColumnName, column, _ValidationService.ParameterName(TableName), OnlyRow);
            }
            return D_Fetch.Rows(TableName, ColumnName, Value, _ValidationService.ParameterName(TableName), OnlyRow);
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    return null;
}
public DataTable Rows(string TableName)
{
    return Rows<bool>(TableName);
}


}*/
/*
 * 
 * 
 * 
 * 
   public DataTable Rows<T>(string TableName,bool ExactValue,bool Opreation=false, string ColumnName = null, T Value=default)
        {
            try
            {       
                   if(Opreation)
                    return D_Read.Rows(TableName);
                if (_ValidationService.Validation(TableName))
                {
                    if (Value is ISupportedType<T> supportedValue)
                        if (supportedValue.Validation(Value))
                            return D_Read.Rows(TableName, ColumnName,Value, _ValidationService.ParameterName(TableName));
                        else if (Value is string column)
                        {
                            column += (ExactValue ? "" : "%");
                            return D_Read.Rows(TableName, ColumnName, column, _ValidationService.ParameterName(TableName));
                        }
                        else
                            throw new ArgumentException("Un Supported Type");

                }
            }
 */
#endregion