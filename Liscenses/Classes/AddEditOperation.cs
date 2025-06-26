using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
using System.Windows.Forms;

namespace Liscenses.Classes
{
    public static class AddEditOperation
    {
        public static async Task<(Control Custom, ICustmControl<object> control,DataTable dt)> operation(Form form, DataGridView dataGridView1,DataTable dt, DataRow row, string title,string hader, bool Add, bool log)
        {
         
            DataGridViewRow dgv=null;
            Control Custom = null;
            ICustmControl<object> control = null;
            Handling x=new Handling();
            if(dataGridView1 != null&&dataGridView1.SelectedRows.Count>0 ) 
                dgv = dataGridView1.SelectedRows[0];
            (form ,Custom,control)=await    x.handler(form, dgv, row, title, hader, false, log);
            if (control == null)
            {
                MessageBox.Show("Invalid control type.");
                return (null, null,  null);
            }

            try
            {

                var result = control.ManipulatingResult();

                if (result == null)
                {
                    //MessageBox.Show("No Data Edited");
                    return (null, null, null);
                }

                DataRow row1;

                if (Add)
                {
                    (dt,row1) = await result.ToDataRow(dt, Add);
                    dt.Rows.Add(row1);
                }
                else
                {
                    if (!log)
                        (dt, row1) = await result.ToDataRow(dt, Add);
                    else if (result is User user)
                        row1 =await user.ToDataRow(row);
                    else
                        row1= null;

                    if (row1 != null)
                        row1.AcceptChanges();
                }
                if (!log)
                    dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return(Custom,control,dt);
        }
       /* public static async Task operation( DataGridView dataGridView1,  ICustmControl<object> control,  DataTable dt, DataRow row, bool Add, bool log)
        {
            if (control == null)
            {
                MessageBox.Show("Invalid control type.");
                return;
            }

            try
            {
                var result = control.ManipulatingResult();

                if (result == null)
                {
                    MessageBox.Show("No Data Edited");
                    return;
                }

                DataRow row1;

                if (Add)
                {
                    row1 = await result.ToDataRow(dt, Add);
                    dt.Rows.Add(row);
                }
                else
                {
                    if (!log)
                        row1 =await result.ToDataRow(dt, Add);
                    else if (result is User user)
                        row1 =await user.ToDataRow(row);
                    else
                        row1 = null;

                    if (row1 != null)
                        row1.AcceptChanges();
                  
                }
                if (!log)
                    dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
