using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MidLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.B_CRUDOperation.B_Read;

namespace Liscenses.Classes
{
    public static class ChecksOperation
    {
        public static async Task Check( DataGridView dataGridView1, string message, Action<int, int> action)
        {
            var cmb = new B_Fetch();
            int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show($"Are You Sure To {message} {ID}", "Management", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataTable dt    =await cmb.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", true, true, ID);
                DataRow dr = dt?.Rows[0];
                if (dr != null)
                {
                    action(ID, Convert.ToInt32(dr["ApplicationID"] ?? 0));
                }
            }
        }
    }
}
