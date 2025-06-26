using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BaseOperation
{
     class _Scaler
    {
        //internal static object Scaler(SqlCommand cmd)
        //{
        //    return cmd.ExecuteScalar();
        //}
        internal static async Task <object> Scaler(SqlCommand cmd)
        {
            return await cmd.ExecuteScalarAsync().ConfigureAwait(false);
        }

    }
}
        #region tries
   //public static T Scaler<T>(SqlCommand cmd)
        //{
           
        //    return cmd.ExecuteScalar() as T;
        //}
        /* internal static T Scaler<T>(SqlCommand cmd)
         {
             object result = cmd.ExecuteScalar();
             if (result == null || result == DBNull.Value)
             {
                 if (typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
                     throw new InvalidOperationException("The query returned null, but the expected type is a non-nullable value type.");
                 return default;
             }
             try
             {
                 return (T)Convert.ChangeType(result, typeof(T));
             }
             catch (InvalidCastException ex)
             {
                 throw new InvalidCastException($"Cannot cast the query result to type {typeof(T)}.", ex);
             }
         }*/
        #endregion