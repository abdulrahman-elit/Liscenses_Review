using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using   MidLayer.B_CRUDOperation.B_Read;

namespace Liscenses
{
    public partial class Users: Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            var obj = new B_Fetch();
            dataGridView1.DataSource = obj.Rows("users");
            dataGridView1.Columns["Password"].Visible = false;
            comboBox1.Items.Add("None");
            comboBox1.SelectedIndex = 0;
            //comboBox1.SelectedText.PadRight(10);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                //column.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (column.HeaderText != "Password" )//&& column.HeaderText != "PersonID" && column.HeaderText != "NationalityCountryID")
                    comboBox1.Items.Add(column.HeaderText);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                textBox1.Visible = false;
            else
                textBox1.Visible = true;
        }
    }
}
