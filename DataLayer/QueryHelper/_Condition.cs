using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using clsPerson;

namespace DataLayer.QueryHelper
{
    internal static class _Condition
    {
        internal static SqlParameter SetParameter<T>(string query, Dictionary<string, (SqlDbType type, int? size)> Map, T ParameterVal)
        {
            if (Map == null || !Map.TryGetValue(query, out var tup))
                throw new ArgumentException($"The query string '{query}' is not valid.", nameof(query));

            object result;

            if (typeof(T).IsPrimitive || typeof(T) == typeof(string))
                result = ParameterVal;

            else
            {
                var val = typeof(T).GetProperty(query);

                if (val == null)
                    throw new ArgumentException($"Property '{query}' does not exist in the provided object.", nameof(query));

                result = val.GetValue(ParameterVal);

                //if (result == null)
                //    throw new ArgumentException($"Property '{query}' is null in the provided object.", nameof(query));

            }

            var para = new SqlParameter("@" + query, tup.type) { Value = result ?? DBNull.Value };

            if (tup.size.HasValue)
                para.Size = tup.size.Value;

            return para;
        }
    }
}
    /*    internal static SqlParameter SetParameter(string query, Dictionary<string, (SqlDbType type, int? size)> Map, object ParameterVal)
        {
            if (Map == null || !Map.TryGetValue(query, out var tup))
                throw new ArgumentException($"The query string '{query}' is not valid.", nameof(query));

            //object result;

            //if (typeof(T).IsPrimitive || typeof(T) == typeof(string))
            //    result = ParameterVal;

            //else
            //{
            //    var val = typeof(T).GetProperty(query);

            //    if (val == null)
            //        throw new ArgumentException($"Property '{query}' does not exist in the provided object.", nameof(query));

            //    result = val.GetValue(ParameterVal);

                //if (result == null)
                //    throw new ArgumentException($"Property '{query}' is null in the provided object.", nameof(query));

            //}
            var para = new SqlParameter("@" + query, tup.type) { Value = ParameterVal ?? DBNull.Value };

            if (tup.size.HasValue)
                para.Size = tup.size.Value;

            return para;
        }
    }
}

*/
        #region tries
        /*
        internal static SqlParameter Condtiones(string query,Person person) 
        {
            return query switch
            {
                
                nameof(person.NationalNo)=>           new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.NationalNo} ,
                nameof(person.FirstName)=>            new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.FirstName} ,
                nameof(person.SecondName)=>           new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.SecondName} ,
                nameof(person.ThirdName)=>            new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.ThirdName} ,
                nameof(person.LastName)=>             new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.LastName} ,
                nameof(person.DateOfBirth)=>          new SqlParameter("@"+query, SqlDbType.DateTime)     {Value= person.DateOfBirth} ,
                nameof(person.Gendor)=>               new SqlParameter("@"+query, SqlDbType.TinyInt)      {Value= person.Gendor} ,
                nameof(person.Address)=>              new SqlParameter("@"+query, SqlDbType.NVarChar,500) {Value= person.Address} ,
                nameof(person.Phone)=>                new SqlParameter("@"+query, SqlDbType.NVarChar,20)  {Value= person.Phone} ,
                nameof(person.Email)=>                new SqlParameter("@"+query, SqlDbType.NVarChar,50)  {Value= person.Email} ,
                nameof(person.NationalityCountryID)=> new SqlParameter("@"+query, SqlDbType.Int)          {Value= person.NationalityCountryID} ,
                nameof(person.ImagePath)=>            new SqlParameter("@"+query, SqlDbType.NVarChar,250) {Value = person.ImagePath },
                _ => throw                            new ArgumentException("The query string is not valid.", nameof(query)),

            };
        }
        internal static SqlParameter Condtiones<T>(string query, T person)
        {
            return query switch
            {

               "NationalNo" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "FirstName" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "SecondName" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "ThirdName" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "LastName" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "DateOfBirth" => new SqlParameter("@" + query, SqlDbType.DateTime) { Value = person },
               "Gendor" => new SqlParameter("@" + query, SqlDbType.TinyInt) { Value = person },
               "Address" => new SqlParameter("@" + query, SqlDbType.NVarChar, 500) { Value = person },
               "Phone" => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person },
               "Email" => new SqlParameter("@" + query, SqlDbType.NVarChar, 50) { Value = person },
               "NationalityCountryID" => new SqlParameter("@" + query, SqlDbType.Int) { Value = person },
               "ImagePath" => new SqlParameter("@" + query, SqlDbType.NVarChar, 250) { Value = person },
                _ => throw new ArgumentException("The query string is not valid.", nameof(query)),

            };
        }
        */
        /* internal static SqlParameter CreateParameter<T>(string query, Dictionary<string, (SqlDbType type, int? size)> map, T obj)
         {
             // Define a dictionary to map property names to their SQL data types and sizes


             // Validate the query
             if (!parameterMap.TryGetValue(query, out var sqlInfo))
             {
                 throw new ArgumentException($"The query string '{query}' is not valid.", nameof(query));
             }

             // Use reflection to get the property value
             var property = typeof(T).GetProperty(query);
             if (property == null || property.GetValue(obj) == null)
             {
                 throw new ArgumentException($"Property '{query}' does not exist or is null in the provided object.", nameof(query));
             }

             // Create the SqlParameter
             var parameter = new SqlParameter("@" + query, sqlInfo.Type)
             {
                 Value = property.GetValue(obj)
             };

             // Set the size if applicable
             if (sqlInfo.Size.HasValue)
             {
                 parameter.Size = sqlInfo.Size.Value;
             }

             return parameter;
         }
     }*/
        #endregion  