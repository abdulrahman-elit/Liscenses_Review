using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Liscenses.Classes
{
    public static class comboboxHandleing
    {
        public  static void comboboxHandle(ComboBox comboBox1,DataGridView dataGridView1, HashSet<string> cmbFilter)
        {
           
                comboBox1.Items.Clear();
                comboBox1.Items.Add("None");
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (cmbFilter == null || !cmbFilter.Contains(column.Name))
                        comboBox1.Items.Add(column.Name);
                }
                comboBox1.SelectedIndex = 0;
            }
        }
    
}
