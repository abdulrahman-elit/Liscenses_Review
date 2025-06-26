using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using MidLayer.Process;
using MidLayer.B_CRUDOperation;
namespace Licenses
{
    public partial class Form2: Form
    {
            public DataProcessor obj1 = new MidLayer.Process.DataProcessor();
            public Person p = new Person
              (
              //PersonID: 1028,
              NationalNo: "n54",
              FirstName: "Ali",
              SecondName: "Ali",
              ThirdName: "Kmal",
              LastName: "Salem",
              DateOfBirth: DateTime.Today.AddYears(-25),
              Gendor: 1,
              Address: "Baghdad",
              Phone: "123456789",
              Email: "d@gmail.com",
              NationalityCountryID: 1,
              ImagePath: null
              );
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = DataLayer.GetAllRows.FetchAllRows.Rows("Countries","CountryName","C",Person.person);
            //dataGridView1.DataSource = DataLayer.CRUDOpreation.Read.Rows("People", "SecondName", "Mohammed"+"", Person.person);
            //dataGridView1.DataSource = DataLayer.CRUDOpreation.D_Read.Rows("People");
            //dataGridView1.DataSource = DataLayer.GetAllRows.FetchAllRows.Rows("People");
            var obj = new MidLayer.B_CRUDOperation.B_Read.B_Fetch();
            //dataGridView1.DataSource = obj.Rows("People");
            dataGridView1.DataSource = obj.Rows<int>("People");
            //dataGridView1.DataSource = obj.Rows("People", "SecondName", true,true,"Mohammed");

        }

        private  void button1_Click(object sender, EventArgs e)
        {
            List<string> ColumnName = Person.map.Keys.ToList();
            ColumnName.Remove("PersonID"); 
            Person p = new Person
                (
                NationalNo: "n53",
                FirstName: "Mohammed",
                SecondName: "Ali",
                ThirdName: "Kmal",
                LastName: "Salem",
                DateOfBirth: DateTime.Today.AddYears(-20),
                Gendor: 1,
                Address: "Baghdad",
                Phone: "123456789",
                Email: "d@gmail.com",
                NationalityCountryID: 1,
                ImagePath: null
                );

            //obj1.ProcessData("people", Person.map.Keys.ToHashSet(), p, "PersonID");
           
            //MessageBox.Show(await obj1.ProcessData("people", Person.map.Keys.ToHashSet(),  p, "PersonID").ToString());
            //MessageBox.Show(B_Delete.Row("people", "PersonID", 1028).ToString());
            //MessageBox.Show(v ? "True":"False");
            //MessageBox.Show(obj1.Row("people", "SecondName", "M", true).ToString());
            //ColumnName.Remove("PersonID");
            //Person p = new Person
            //    (
            //    NationalNo: "n53",
            //    FirstName: "Mohammed",
            //    SecondName: "Ali",
            //    ThirdName: "Kmal",
            //    LastName: "Salem",
            //    DateOfBirth: DateTime.Now,
            //    Gendor: 1,
            //    Address: "Baghdad",
            //    Phone: "07779777777",
            //    Email: "d@gmail.com",
            //    NationalityCountryID: 1,
            //    ImagePath: null
            //    );
            //dataGridView1.DataSource = 
            //int ID = 1027;
            //DataLayer.CRUDOpreation.Update.Row("People", ColumnName,p,"PersonID",Person.person).ToString();
            //MessageBox.Show( DataLayer.CRUDOpreation.Create.Row("People", ColumnName,p,Person.person).ToString());
            //MessageBox.Show( DataLayer.CRUDOpreation.Delete.Row("People", "PersonID",ID,Person.person)?"Successfully":"Not"+"Deleted");
            //dataGridView1.Refresh();
            //dataGridView1.DataSource = DataLayer.CRUDOpreation.Read.Rows("People", "SecondName", textBox1.Text, Person.person);
            //if (int.TryParse(textBox1.Text, out int result))
            //bool val= (DataLayer.CRUDOpreation.Check.Row("People", "FirstName", textBox1.Text+"%",Person.person))>0;
            //MessageBox.Show((val?"":"Not ") + "Exists");
            //MessageBox.Show(string.IsNullOrWhiteSpace(x) ? x : "Not" + " Exists ");
            //if (int.TryParse(textBox1.Text, out int result))
            //    MessageBox.Show(DataLayer.ExistsRow.ExistsPeopleRow.Row<int>(result) ? "" : "Not " + "Exists");
        }
    }
}
