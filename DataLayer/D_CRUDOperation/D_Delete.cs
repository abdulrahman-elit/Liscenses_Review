using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.BaseOperation;
using DataLayer.QueryHelper;
namespace DataLayer.D_CRUDOperation
{
    public static class D_Delete
    {
        //public static bool Row<T>(string TableName, string ConditionColumn, T ConditionValue,  Dictionary<string, (SqlDbType, int?)> Parameters)
        //{
        //    string query = $@"Delete From {TableName} Where {ConditionColumn} = @{ConditionColumn}";
        //    try
        //    {
        //        return Base.ExecuteOperation(Query: query, myopration: _NonQuery.NonQuery, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter,Parameters, ConditionValue)) > 0;
        //    }
        //    catch 
        //    {
        //        throw ;
        //    }
        //}
        public static async Task< bool >Row<T>(string TableName, string ConditionColumn, T ConditionValue, Dictionary<string, (SqlDbType, int?)> Parameters)
        {
            string query = $@"Delete From {TableName} Where {ConditionColumn} = @{ConditionColumn}";
            try
            {
                return await Base.ExecuteOperation(Query: query, myopration: _NonQuery.NonQuery, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameters, ConditionValue)).ConfigureAwait(false) > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}

//return BaseOpration.Base.opreation<int>(Query: query, myopration: _NonQuery.NonQuery, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, new Dictionary<string, (System.Data.SqlDbType, int?)> { { ConditionColumn, (System.Data.SqlDbType.VarChar, null) } }, ConditionValue)) > 0;