using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;

namespace Liscenses.Classes
{
    public static class Showing
    {
        public static  void show(ref Form form,ref Control Custom, string Title,string hader,Func<object,TestAppointment,Test,Task> Spical_HandelData)
        {
            Form frm = new Form();
            form = frm;
            //var Info = GetCustomizedControl()as Control;
            Custom.Enabled = true;
            form.Text = $"{Title} {hader}";
            form.Size = Custom.Size + new Size(12, 38);
            form.Controls.Add(Custom);
            if (Custom is Tests spical)
                spical.HandelData += Spical_HandelData;//async (o,ta,t){
                //await Spical_HandelData(o, ta, t)};
            form.ShowDialog();
        }

    }
    
}
