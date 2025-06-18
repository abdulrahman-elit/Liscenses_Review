using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.BaseOperation;
using DataLayer.QueryHelper;

namespace DataLayer.D_CRUDOperation
{
    public static class D_Update
    {
        //public static bool Row<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn,  Dictionary<string, (SqlDbType, int?)> Parameters)
        //{

        //    //string query = _QueryHandler.GenerateQuery($@"Update {TableName} Set ", ColumnName,(string Name) => $@" {Name} = @{Name} ", ConditionColumn,true);
        //    string query = string.Join( " ",_QueryHandler.GenerateQuery($@"Update {TableName} Set ", ColumnName, (string Name) => $@" {Name} = @{Name} "),  $@" Where {ConditionColumn} = @{ConditionColumn}");

        //    try
        //    {
        //       return Base.ExecuteOperation(Query: query, myopration: _NonQuery.NonQuery, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter,Parameters, Value))>0;
        //    }
        //    catch 
        //    {
        //        throw ;
        //    }
        //}
        public static async Task<bool> Row<T>(string TableName, HashSet<string> ColumnName, T Value, string ConditionColumn, Dictionary<string, (SqlDbType, int?)> Parameters)
        {

            //string query = _QueryHandler.GenerateQuery($@"Update {TableName} Set ", ColumnName,(string Name) => $@" {Name} = @{Name} ", ConditionColumn,true);
            string query = string.Join(" ", _QueryHandler.GenerateQuery($@"Update {TableName} Set ", ColumnName, (string Name) => $@" {Name} = @{Name} "), $@" Where {ConditionColumn} = @{ConditionColumn}");

            try
            {
                return await Base.ExecuteOperation(Query: query, myopration: _NonQuery.NonQuery, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameters, Value)).ConfigureAwait(false) > 0;
            }
            catch
            {
                throw;
            }
        }

    }
}
//string Query = @"INSERT INTO[dbo].[Countries]([CountryName])VALUES(@CountryName,@Code,@PhoneCode);Select Scope_Identity();";
//string Query = @"UPDATE [dbo].[Contacts] SET FirstName = @FirstName,LastName = @LastName,Email = @Email,Phone = @Phone,Address = @Address,DateOfBirth = @DateOfBirth,Contacts.CountryID = @CountryID,ImagePath = @ImagePath WHERE ContactID=@ContactID";

#region origen
/*
   public static bool UpdateRow<T,U>(string TableName, string ColumnName, T Value, string ConditionColumn, U ConditionValue)
{
    string query = $@"Update {TableName} Set {ColumnName} = @{ColumnName} Where {ConditionColumn} = @{ConditionColumn}";
    try
    {
       return Base.opreation<int>(Query: query, myopration: _NonQuery.NonQuery, parameters: Fillter.CreateParameters(query, Condition.SetParameter, new Dictionary<string, (System.Data.SqlDbType, int?)> { { ColumnName, (System.Data.SqlDbType.VarChar, null) }, { ConditionColumn, (System.Data.SqlDbType.VarChar, null) } }, Value, ConditionValue))>0;
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
 */
#endregion
