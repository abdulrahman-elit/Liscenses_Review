using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.QueryHelper;
using DataLayer.BaseOperation;

namespace DataLayer.D_CRUDOperation.D_Read
{
    public static class D_Fetch
    {
        //public static DataTable Rows<T>(string TableName,HashSet<string> ShowedColumn=null,string ColumnName =null,T Value= default, Dictionary<string, (SqlDbType, int?)> Parameter=null,bool OnlyRow=false)
        //{
        //    //string query =string.Join( $@"Select "+(OnlyRow?" Top (1) " :""),$"* From {TableName}" , ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+$@" @{ColumnName}":""));
        //    //string query =string.Join(" ", $@"Select " , (OnlyRow ? " Top (1) " : " ", (ShowedColumn!=null)?_QueryHandler.GenerateQuery(" ",ShowedColumn,name=>$@"{name}"):" * ", $" From {TableName}", ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+ $@" @{ColumnName}":"")));
        //    string query = myquery(TableName, ShowedColumn, ColumnName, Value, OnlyRow);
        //    //query += $" Order By {Parameter.Keys.First()} Desc";
        //    try
        //    {

        //        return (ColumnName!=null)? Base.ExecuteOperation(Query: query, myopration: _Reader.Read, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value))
        //                                 : Base.ExecuteOperation(Query: query, myopration: _Reader.Read);  
        //    }
        //    catch
        //    {
        //        throw ;
        //    }

        //}
        //public static DataTable Rows<T>(string TableName, HashSet<string> ShowedColumn = null, HashSet<string> ColumnName = null, T Value = default, Dictionary<string, (SqlDbType, int?)> Parameter = null, bool OnlyRow = false)
        //{
        //    //string query =string.Join( $@"Select "+(OnlyRow?" Top (1) " :""),$"* From {TableName}" , ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+$@" @{ColumnName}":""));
        //    //string query =string.Join(" ", $@"Select " , (OnlyRow ? " Top (1) " : " ", (ShowedColumn!=null)?_QueryHandler.GenerateQuery(" ",ShowedColumn,name=>$@"{name}"):" * ", $" From {TableName}", ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+ $@" @{ColumnName}":"")));
        //    string query = myquery(TableName, ShowedColumn, ColumnName, Value, OnlyRow);
        //    //query += $" Order By {Parameter.Keys.First()} Desc";
        //    try
        //    {

        //        return (ColumnName != null) ? Base.ExecuteOperation(Query: query, myopration: _Reader.Read, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value))
        //                                 : Base.ExecuteOperation(Query: query, myopration: _Reader.Read);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        public static async Task<DataTable> Rows<T>(string TableName, HashSet<string> ShowedColumn = null, string ColumnName = null, T Value = default, Dictionary<string, (SqlDbType, int?)> Parameter = null, bool OnlyRow = false)
        {
            //string query =string.Join( $@"Select "+(OnlyRow?" Top (1) " :""),$"* From {TableName}" , ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+$@" @{ColumnName}":""));
            //string query =string.Join(" ", $@"Select " , (OnlyRow ? " Top (1) " : " ", (ShowedColumn!=null)?_QueryHandler.GenerateQuery(" ",ShowedColumn,name=>$@"{name}"):" * ", $" From {TableName}", ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+ $@" @{ColumnName}":"")));
            string query = myquery(TableName, ShowedColumn, ColumnName, Value, OnlyRow);
            //query += $" Order By {Parameter.Keys.First()} Desc";
            try
            {

                return (ColumnName != null) ? await Base.ExecuteOperation(Query: query, myopration: _Reader.Read, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value)).ConfigureAwait(false)
                                         : await Base.ExecuteOperation(Query: query, myopration: _Reader.Read).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }

        }
        public static async Task<DataTable> Rows<T>(string TableName, HashSet<string> ShowedColumn = null, HashSet<string> ColumnName = null, T Value = default, Dictionary<string, (SqlDbType, int?)> Parameter = null, bool OnlyRow = false)
        {
            //string query =string.Join( $@"Select "+(OnlyRow?" Top (1) " :""),$"* From {TableName}" , ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+$@" @{ColumnName}":""));
            //string query =string.Join(" ", $@"Select " , (OnlyRow ? " Top (1) " : " ", (ShowedColumn!=null)?_QueryHandler.GenerateQuery(" ",ShowedColumn,name=>$@"{name}"):" * ", $" From {TableName}", ((ColumnName != null)? $@" Where {ColumnName} " + ((Value is string) ? @"Like" : @"=")+ $@" @{ColumnName}":"")));
            string query = myquery(TableName, ShowedColumn, ColumnName, Value, OnlyRow);
            //query += $" Order By {Parameter.Keys.First()} Desc";
            try
            {

                return (ColumnName != null) ?await Base.ExecuteOperation(Query: query, myopration: _Reader.Read, parameters: _SqlParameterFactory.CreateParameters(query, _Condition.SetParameter, Parameter, Value)).ConfigureAwait(false)
                                         : await Base.ExecuteOperation(Query: query, myopration: _Reader.Read).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }

        }
        private static string myquery<T>(string TableName, HashSet<string> ShowedColumn = null, string ColumnName = null,T Value = default, bool OnlyRow= false)
        { 
            // Initialize StringBuilder
            StringBuilder sb = new StringBuilder();

            // Build SELECT clause
            sb.Append("SELECT ");
            if (OnlyRow) sb.Append("TOP (1) ");

            // Build column list
            sb.Append( (ShowedColumn != null)? _QueryHandler.GenerateQuery("", ShowedColumn, name => name):" * ");
            

            // Build FROM clause
            sb.Append($" FROM {TableName}");

            // Build WHERE clause
            if (!string.IsNullOrWhiteSpace(ColumnName))
            {

                sb.Append($" WHERE {ColumnName} ");
                sb.Append(Value is string ? "LIKE" : "=");
                sb.Append($" @{ColumnName}");

            }

         
            return sb.ToString();
        }
      
        public static async Task<DataTable> Rows(string TableName)
        {
            return await Rows<object>(TableName,null,"");
        }
        private static string myquery<T>(string TableName, HashSet<string> ShowedColumn = null, HashSet<string> ColumnName = null, T Value = default, bool OnlyRow = false)
        {
            // Initialize StringBuilder
            StringBuilder sb = new StringBuilder();

            // Build SELECT clause
            sb.Append("SELECT ");
            if (OnlyRow) sb.Append("TOP (1) ");

            // Build column list
            sb.Append((ShowedColumn != null) ? _QueryHandler.GenerateQuery("", ShowedColumn, name => name) : " * ");


            // Build FROM clause
            sb.Append($" FROM {TableName}");

            // Build WHERE clause
            if (ColumnName != null)
            {
                bool flag = true ;
                //_QueryHandler.GenerateQuery(" Where ", ColumnName, name => $" and {name} = ");
                int i = ColumnName.Count; 
                foreach (var col in ColumnName)
                {
                    i--;
                    if (flag)
                    {
                        sb.Append($" WHERE {col} ");
                        flag = false;
                    }
                    else
                        sb.Append(col);
                    sb.Append( " = ");
                    sb.Append($" @{col} ");
                    if(i>0)
                        sb.Append(" and ");
                    

                }

            }

           
            return sb.ToString();
        }
    }
}
