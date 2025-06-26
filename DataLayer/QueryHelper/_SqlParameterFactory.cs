using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer.QueryHelper
{
    internal static class _SqlParameterFactory
    {
        private static HashSet<string> ExtractParameterNames(string query)
        {
            MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(query, @"@(\w+)");

            HashSet<string> parameterNames = new HashSet<string>();

            foreach (Match match in matches)
                     if (match.Groups.Count > 1)
                           parameterNames.Add(match.Groups[1].Value);

            return parameterNames;
        }
        internal static SqlParameter[] CreateParameters<T>(string query, Func<string, Dictionary<string, (SqlDbType type, int? size)>,T, SqlParameter> filter, Dictionary<string, (SqlDbType type, int? size)> map,T arg)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("The query string cannot be null or empty.", nameof(query));

            if (filter == null)
                throw new ArgumentNullException(nameof(filter), "The filter function cannot be null.");

            HashSet<string> parameterNames = ExtractParameterNames(query);

            if (parameterNames.Count == 0)
                return Array.Empty<SqlParameter>();

            SqlParameter[] parameters = new SqlParameter[parameterNames.Count];

            //for (int i = 0; i < parameterNames.Count; i++)
            int i = 0;
               foreach(var Name in parameterNames)
                parameters[i++] = filter(Name,map,arg);
            

            return parameters;
        }
    }

}



        /*
        //public static SqlParameter[] operation<T>(string query, Func<string, T> filter)
        //{
        //    Queue<string> Query = opration_(query);
        //    if (Query.Count <= 0 || Query is null)
        //        return null;
        //    SqlParameter[] parameters = new SqlParameter[Query.Count];
        //    int count = 0;
        //    foreach (var queue in Query)
        //    {
        //        parameters[count++] = new SqlParameter(queue, filter(queue));
        //    }
        //    return parameters;
        //}
        //public static SqlParameter[] operation<T>(string query, Func<string,SqlParameter> filter)
        //{
        //    Queue<string> Query = opration_(query);
        //    if (Query.Count <= 0 || Query is null)
        //        return null;
        //    SqlParameter[] parameters = new SqlParameter[Query.Count];
        //    int count = 0;
        //    foreach (var queue in Query)
        //    {
        //        parameters[count++] = filter(queue);
        //    }
        //    return parameters;
        //}
        //public static  Queue<string> opration_(string Query)
        //    {
        //        MatchCollection mathes = System.Text.RegularExpressions.Regex.Matches(Query, "@(\\w+)");
        //        Queue<string> query=new Queue<string>();
        //        int c = 0;
        //        for (int i = 0; i < mathes.Count; i++)
        //            query.Enqueue(mathes[i].Groups[1].Value);
        //        return query;
        //    }
        */