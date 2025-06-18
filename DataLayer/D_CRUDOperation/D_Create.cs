using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseOperation;
using DataLayer.QueryHelper;

namespace DataLayer.D_CRUDOperation
{
     public static class D_Create
    {
        //public static int Row<T>(string TableName, HashSet<string> ColumnName, T Value, Dictionary<string, (SqlDbType, int?)> Parameters)
        //{

        //    //string query = _QueryHandler.GenerateQuery($@"INSERT INTO {TableName} VALUES ( ", ColumnName,(string Name) => $@" @{Name} ");
        //    string query = string.Join(" ",_QueryHandler.GenerateQuery($@"INSERT INTO {TableName} VALUES ( ", ColumnName, (string Name) => $@" @{Name} "), ");Select Scope_Identity();");


        //    try
        //    {
        //        return int.TryParse((Base.ExecuteOperation(Query: query, myopration: _Scaler.Scaler, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameters, Value))).ToString(), out int result) ? result : -1 ;
        //    }
        //    catch 
        //    {
        //        throw ;
        //    }
        //}
        public static async Task<int> Row<T>(string TableName, HashSet<string> ColumnName, T Value, Dictionary<string, (SqlDbType, int?)> Parameters)
        {

            //string query = _QueryHandler.GenerateQuery($@"INSERT INTO {TableName} VALUES ( ", ColumnName,(string Name) => $@" @{Name} ");
            string query = string.Join(" ", _QueryHandler.GenerateQuery($@"INSERT INTO {TableName} VALUES ( ", ColumnName, (string Name) => $@" @{Name} "), ");Select Scope_Identity();");


            try
            {
                return int.TryParse((await Base.ExecuteOperation(Query: query, myopration: _Scaler.Scaler, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameters, Value)).ConfigureAwait(false)).ToString(), out int result) ? result : -1;
            }
            catch
            {
                throw;
            }
        }
    }
}
//string Query = @"INSERT INTO[dbo].[Countries]([CountryName])VALUES(@CountryName,@Code,@PhoneCode);Select Scope_Identity();";
