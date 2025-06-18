using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EventLoger;

namespace DataLayer.BaseOperation
{
    public static class Base
    {

        //public static T ExecuteOperation<T>(string Query, Func<SqlCommand, T> myopration, SqlParameter[] parameters = null)
        //{
        //    try

        //    {
        //        using (SqlConnection conn = new SqlConnection(_clsSqlConnection.connection))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(Query, conn))
        //            {
        //                if (parameters != null)
        //                    cmd.Parameters.AddRange(parameters);
        //                return myopration(cmd);
        //            }
        //        }
        //    }
        //    catch 
        //    {
        //        throw ;
        //    }

        //}
        public static async Task<T> ExecuteOperation<T>(string Query,Func<SqlCommand,Task<T>> myopration, SqlParameter[] parameters = null)
        {
            try

            {
                using (SqlConnection conn = new SqlConnection(_clsSqlConnection.connection))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    using (SqlCommand cmd = new SqlCommand(Query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        return await myopration(cmd).ConfigureAwait(false);
                    }
                }
            }
            catch(Exception ex)
            {
                EventLogger.LogError($"Error during database operation: {Query}", ex, 3001);
                throw;
            }

        }
    }
}

//----------------------------------------------------------------
//internal static class Base
//{

//    internal static T opreation<T>(string Query, Func<SqlCommand, T> myopration, SqlParameter[] parameters = null)
//    {
//        try

//        {
//            using (SqlConnection conn = new SqlConnection(clsSqlConnction.connction))
//            {
//                conn.Open();
//                using (SqlCommand cmd = new SqlCommand(Query, conn))
//                {
//                    if (parameters != null)
//                        cmd.Parameters.AddRange(parameters);
//                    return myopration(cmd);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }

//    }
//    internal static class _Reader
//    {
//        public static DataTable Read(SqlCommand cmd)
//        {
//            DataTable dt = new DataTable();
//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                if (reader.HasRows)
//                {
//                    dt.Load(reader);
//                    return dt;
//                }

//            }
//            return null;
//        }

//    }
//    internal static class PepoleCondtion
//    {
//        internal static SqlParameter Condtiones(string query, Person person)
//        {
//            return query switch
//            {

//                nameof(person.NationalNo) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.NationalNo },
//                nameof(person.FirstName) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.FirstName },
//                nameof(person.SecondName) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.SecondName },
//                nameof(person.ThirdName) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.ThirdName },
//                nameof(person.LastName) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.LastName },
//                nameof(person.DateOfBirth) => new SqlParameter("@" + query, SqlDbType.DateTime) { Value = person.DateOfBirth },
//                nameof(person.Gendor) => new SqlParameter("@" + query, SqlDbType.TinyInt) { Value = person.Gendor },
//                nameof(person.Address) => new SqlParameter("@" + query, SqlDbType.NVarChar, 500) { Value = person.Address },
//                nameof(person.Phone) => new SqlParameter("@" + query, SqlDbType.NVarChar, 20) { Value = person.Phone },
//                nameof(person.Email) => new SqlParameter("@" + query, SqlDbType.NVarChar, 50) { Value = person.Email },
//                nameof(person.NationalityCountryID) => new SqlParameter("@" + query, SqlDbType.Int) { Value = person.NationalityCountryID },
//                nameof(person.ImagePath) => new SqlParameter("@" + query, SqlDbType.NVarChar, 250) { Value = person.ImagePath },
//                _ => throw new ArgumentException("The query string is not valid.", nameof(query)),

//            };
//        }
//    }
//    public static class RtriveAllPepoleRows
//    {
//        public static DataTable Rows()
//        {
//            string querey = @"Select * From Pepole";
//            try
//            {
//                return Base.opreation(Query: querey, myopration: _Reader.Read, parameters: Fillter.CreateParameters(querey, PepoleCondtion.Condtiones));

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }

//    }