using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liscenses.Classes
{
    public static class DeleteRow
    {
        public static async Task<DataTable> row( DataGridView dataGridView1,string message)
        {
            DataTable dt = null;
            int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show($"Are You Sure To Delete {ID}", "Management", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (await DeleteFun.fun(message, null, ID))
                {
                    dt.Rows.Remove(dt.Rows.Find(ID));
                    dataGridView1.Refresh();
                }
                else
                    MessageBox.Show("No Data Deleted");
            }
            return dt;
        }
    }
}
