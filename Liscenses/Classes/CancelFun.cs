using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
using MidLayer.Process;
using System.Windows.Forms;
using MidLayer.B_CRUDOperation.B_Read;
using System.Data;
//using MiddleLayer.B_CRUDOperation.B_Read;

namespace Liscenses.Classes
{
    public class CancelFun
    {
        public static async Task<bool> Fun( string title, string ColumnName, int ID)
        {
            var cmb = new B_Fetch();
            if (ColumnName == null)
            {
                var j = await GetCustomizedControls.Control(title);
                ColumnName = j.PrimaryIntColumnIDName();
            }
            try
            {
                DataProcessor dp = new DataProcessor();
                DataTable dt = await cmb.Rows(title, null, ColumnName, true, true, ID);
                MyApplication val = new MyApplication(dt.Rows?[0]);
                if (val != null && val.ApplicationStatus < 3)
                {
                    val.setApplicationStatus(4);
                    await dp.ProcessData(title, new HashSet<string> { "ApplicationStatus" },  val, ColumnName);
                    return true;
                }
                else
                {
                    MessageBox.Show("You Can Not Cancel this Application");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
