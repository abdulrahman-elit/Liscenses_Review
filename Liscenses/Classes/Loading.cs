using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MiddleLayer.B_CRUDOperation.B_Read;
using System.Windows.Forms;
using System.Data;
using MidLayer.B_CRUDOperation.B_Read;

namespace Liscenses.Classes
{
    public static class Loading
    {
        public static async Task<(DataTable dt,DataView dv)> Load(  DataGridView dataGridView1, string TableName, HashSet<string> columnFilter = null)
        {
            var obj = new B_Fetch();
            DataTable dt = await obj.Rows(TableName.ToLowerInvariant(), columnFilter);
             dt.Columns[0].AutoIncrement = false;
            DataView dv = dt.DefaultView;
            dataGridView1.DataSource = dv;
            return (dt,dv); 
        }
    }
}
