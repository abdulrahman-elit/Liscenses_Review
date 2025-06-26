using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liscenses.Classes
{
    public static class TextHandling
    {
        public static void Changed(ref ComboBox comboBox1, ref TextBox textBox1, ref DataView dv, ref DataGridView dataGridView1)
        {
            if (comboBox1.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dv.RowFilter = "";
                return;
            }
            string oprateor;
            if (int.TryParse(textBox1.Text, out int r)) oprateor = " = "; else { oprateor = " Like "; r = -1; }
            try
            {
                dv.RowFilter = (($@"{comboBox1.SelectedItem.ToString()} {oprateor} " + ((r == -1) ? $@"'{textBox1.Text}%'" : $@"{r}")));
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid Input"+ex.Message);//{ex.Message}");
                textBox1.Text = "";
                textBox1.Focus();
            }
        }
    }
}
