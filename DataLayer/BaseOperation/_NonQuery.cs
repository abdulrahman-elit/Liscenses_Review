using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BaseOperation
{
    //class _NonQuery
    //{
    //    internal static int NonQuery(SqlCommand cmd)
    //    {
    //        return cmd.ExecuteNonQuery();
    //    }
    //}
    class _NonQuery
    {
        internal static async Task<int> NonQuery(SqlCommand cmd)
        {
            return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
