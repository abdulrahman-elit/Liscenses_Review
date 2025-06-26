using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.QueryHelper
{
    public static class _QueryHandler
    {
        internal static string GenerateQuery(string SubQuery, HashSet<string> ColumnName,Func<string,string> Opration)//, string ConditionColumn = null, bool UpdateFlag = false)
        {

            StringBuilder Query = new StringBuilder();
            Query.Append(SubQuery);
            bool i = false;
            foreach (var column in ColumnName)
            {
                if (i)
                    Query.Append($@",");
                else
                    i = true;
                    Query.Append(Opration(column));
            }
            return Query.ToString();
            //return Query.Append((UpdateFlag) ? $@" Where {ConditionColumn} = @{ConditionColumn}" : ");Select Scope_Identity();").ToString();

        }
    }
}
#region test
/*
 *   /*for (int i = 0; i < ColumnName.Count; i++)
            {
                Query += (UpdateFlag) ?
                (i != 0)
                ?

                      $@", {ColumnName[i]} = @{ColumnName[i]}"

                :

                     $@" {ColumnName[i]} = @{ColumnName[i]}"
                :

                 (i != 0)
                ?

                      $@", @{ColumnName[i]}"

                :

                     $@" (@{ColumnName[i]}";


            }*/
/*  internal static string GenerateQuery(string TableName, List<string> ColumnName, string SubQuery, string ConditionColumn = null, bool UpdateFlag = false)
        {

            string Query = SubQuery;
            for (int i = 0; i < ColumnName.Count; i++)
            {
                Query += (UpdateFlag) ?
                (i != 0)
                ?

                      $@", {ColumnName[i]} = @{ColumnName[i]}"

                :

                     $@" {ColumnName[i]} = @{ColumnName[i]}"
                :

                 (i != 0)
                ?

                      $@", @{ColumnName[i]}"

                :

                     $@" (@{ColumnName[i]}";


            }
            return Query += (UpdateFlag) ? $@" Where {ConditionColumn} = @{ConditionColumn}" : ");Select Scope_Identity();";

        }
 */
#endregion