using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DataLayer.BaseOperation;
using DataLayer.QueryHelper;

namespace DataLayer.D_CRUDOperation.D_Read
{ 
    public static class D_Check
    {
        //public static bool Row<T>(string TableName,string ColumnName,T Value,Dictionary<string,(SqlDbType,int?)> Parameter )
        //   {
        //       //string query = $@"SELECT CAST(EXISTS (SELECT 1 FROM {TableName} WHERE {ColumnName}" + ((Value is string) ? @"Like" : @"=") + $@"{ColumnName}) AS BIT);";
        //        string query=   $@"SELECT CASE WHEN EXISTS (SELECT 1 FROM {TableName} Where {ColumnName} "+ ((Value is string) ? @"Like" : @"=") + $@" @{ColumnName}) THEN 1 ELSE 0 END;";
        //       try
        //       {
        //               var result= Convert.ToInt32((Base.ExecuteOperation(Query: query, myopration: _Scaler.Scaler, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value))));
        //           return result == 1;
        //       }
        //       catch
        //       {
        //           throw ;
        //       }
        //}
        public static async Task<bool> Row<T>(string TableName, string ColumnName, T Value, Dictionary<string, (SqlDbType, int?)> Parameter)
        {
            //string query = $@"SELECT CAST(EXISTS (SELECT 1 FROM {TableName} WHERE {ColumnName}" + ((Value is string) ? @"Like" : @"=") + $@"{ColumnName}) AS BIT);";
            string query = $@"SELECT CASE WHEN EXISTS (SELECT 1 FROM {TableName} Where {ColumnName} " + ((Value is string) ? @"Like" : @"=") + $@" @{ColumnName}) THEN 1 ELSE 0 END;";
            try
            {
                var result = Convert.ToInt32((await Base.ExecuteOperation(Query: query, myopration: _Scaler.Scaler, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value)).ConfigureAwait(false)));
                return result == 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
#region tries
/*
//    SELECT CASE
//    WHEN EXISTS(SELECT 1 FROM People WHERE secondName LIKE 'm')
//    THEN 1 ELSE 0 
//END;
            //string query = $@"SELECT CASE WHEN EXISTS (SELECT * People Where Name "+ ((Value is string) ? @"Like" : @"=") + $@" @Name) THEN 1 ELSE 0 END;";

        //@"SELECT top 1 NationalNo FROM People ;";
    //@"Select case when exsits(select NationalNo from people where NationalNo = @NationalNo) then 1 else 0 end";
    // @"Select * From People Where Id = @Id";
public static bool myfunc()
{
    return Row<bool>(true);
}
  public static T Row<T>(T NationalNo)
{
    string querey = @"SELECT top 1 NationalNo FROM People ;";
        //@"SELECT CASE WHEN EXISTS (SELECT 1 FROM People WHERE NationalNo = @NationalNo) THEN 1 ELSE 0 END;";
    //@"Select case when exsits(select NationalNo from people where NationalNo = @NationalNo) then 1 else 0 end";
    // @"Select * From People Where Id = @Id";
    try
    {
        return Base.opreation(Query: querey, myopration: _Scaler.Scaler<T>, parameters: Fillter.CreateParameters(querey,Condition.SetParameter, Person.person,NationalNo));
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
                //if (bool.TryParse((Base.ExecuteOperation(Query: query, myopration: _Scaler.Scaler, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value))).ToString(), out bool esult))
                //    return esult;
*/
            //string query = $@"SELECT CASE WHEN EXISTS (SELECT Top 1 FROM {TableName}"+ WHERE {ColumnName} = @{ColumnName}") THEN 1 ELSE 0 END;";
#endregion