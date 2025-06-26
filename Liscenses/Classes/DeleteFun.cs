using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
using MidLayer.Validation;
using System.Windows.Forms;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.B_CRUDOperation;
////using MiddleLayer.B_CRUDOperation.B_Read;

namespace Liscenses.Classes
{
    public static class DeleteFun
    {
        public static async Task< bool >fun(string title, string ColumnName, int ID)
        {
            var cmb = new B_Fetch();
            if (ColumnName == null)
            {
                var j = await GetCustomizedControls.Control(title);
                ColumnName = j.PrimaryIntColumnIDName();
            }
            try
            {
               await  B_Delete.Row(title, ColumnName, ID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
