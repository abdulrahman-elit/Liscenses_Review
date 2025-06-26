using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;

namespace Liscenses.Classes
{
    public class Handling
    {
        public async Task<(Form form, Control Custom, ICustmControl<object> control)> handler(Form form, DataGridViewRow dataGridView1, DataRow row, string Title, string hader, bool Enable, bool log)
        {
            ICustmControl<object> control=null;
            Control Custom=null;
            int ID = 0;
            if (log)
                ID = Convert.ToInt32(row["UserID"]);
            else if (dataGridView1 != null)
                ID = Convert.ToInt32(dataGridView1.Cells[0].Value);
            try
            {
                control =await GetCustomizedControls.Control(hader, dataGridView1, row);
                await control?.setInfo(ID, Enable, Title);
               Custom = control as Control;
                if (Custom is Tests spical)
                {
                    HandelSpaicalReturnData spicalfun = new HandelSpaicalReturnData(Convert.ToInt32(row["UserID"]), dataGridView1);
                    Showing.show(ref form,ref Custom, Title, hader,  spicalfun.spicalx);
                }
                else
                    Showing.show(ref form, ref Custom, Title, hader, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (form,Custom, control);
        }
    }
}
