using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BaseOperation
{
    // static class _Reader
    //{
    //    internal static DataTable Read(SqlCommand cmd)
    //    {
    //        DataTable dt = new DataTable();
    //        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo))
    //        {
    //            if (reader.HasRows)
    //            {
    //                dt.Load(reader);
    //                return dt;
    //            }

    //        }
    //        return null;
    //    }
    //}
    static class _Reader
    {
        internal static async Task<DataTable> Read(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.KeyInfo).ConfigureAwait(false))
            {
                if (reader.HasRows)
                {
                    await Task.Run(() => dt.Load(reader)).ConfigureAwait(false);
                    return dt;
                }

            }
            return null;
        }
    }
}
