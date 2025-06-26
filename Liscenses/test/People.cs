using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MidLayer.B_CRUDOperation.B_Read;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Liscenses
{
    public partial class People: Form
    {
        public People()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public DataView dv;

        private async void People_Load(object sender, EventArgs e)
        {
            var obj = new B_Fetch();
            DataTable _tab=    await obj.Rows("people");
            dv = _tab.DefaultView;
            dataGridView1.DataSource = dv;

            // Initialize ComboBox
            comboBox1.Items.Add("None");
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "ImagePath" &&
                    column.Name != "PersonID" &&
                    column.Name != "NationalityCountryID")
                {
                    comboBox1.Items.Add(column.Name); // Use column.Name
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (comboBox1.SelectedIndex != 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0)
            {
                dv.RowFilter = ""; // Clear filter
                return;
            }

            // Get the actual column name
            string columnName = dataGridView1.Columns[comboBox1.SelectedIndex].Name;

            // Apply the filter
            dv.RowFilter = $"{columnName} LIKE '{textBox1.Text}%'";
            dataGridView1.Refresh();
        }
    }
}


#region tries
//comboBox1.SelectedText.PadRight(10);
//dv.Sort = "LastName ASC";
// Bind the DataView to a BindingSource
//dataGridView1.Columns[0].Visible = false;
//    try { 
//    comboBox1.DataSource = obj.Rows("countryies");
//}
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }
//column.SortMode = DataGridViewColumnSortMode.NotSortable;
//if(comboBox1.SelectedIndex == 0)
//{
//    dataGridView1.DataSource = new B_Fetch().Rows("people");
//}
//else
//{
//    dataGridView1.DataSource = new B_Fetch().Rows("people", comboBox1.SelectedItem.ToString(), textBox1.Text);
//}   
// Get the search term
//string searchTerm = textBox1.Text.Trim().ToLower();
//dv.RowFilter = "Age > 30";
// Bind to DataGridView

// Iterate backward through the rows to avoid collection modification issues
//for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
//{
//    DataGridViewRow row = dataGridView1.Rows[i];

//    // Skip the new row (empty row at the bottom)
//    if (row.IsNewRow)
//    {
//        continue;
//    }

//    // Get the cell value safely
//    var cellValue = row.Cells[comboBox1.SelectedIndex].Value;
//    if (cellValue == null)
//    {
//        row.Visible = false; // Hide rows with null values
//        continue;
//    }

//    // Check if the cell value contains the search term
//    if (cellValue.ToString().ToLower().Contains(searchTerm))
//    {
//        row.Visible = true;
//    }
//    else
//    {
//        row.Visible = false;
//    }
// Assuming your data is bound to a BindingSource
//BindingSource bindingSource = (BindingSource)dataGridView1.DataSource;

//// Get the column name from the ComboBox
//string columnName = comboBox1.SelectedItem.ToString();

//// Build the filter expression
//string filter = $"{columnName} LIKE '%{textBox1.Text}%'";
//bindingSource.Filter = filter;

//// Refresh the DataGridView
//dataGridView1.Refresh();
// Create a DataView for advanced filtering/sorting
#endregion