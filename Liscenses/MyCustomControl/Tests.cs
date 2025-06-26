using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsPerson.SupportedTypes;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using EntityLayer.Classes;
using Liscenses.Classes;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Liscenses
{
    public partial class Tests : UserControl, ICustmControl<TestAppointment>
    {
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        B_Fetch cmb = new B_Fetch();
        private int _testNum;
        DataRow drx;
        private int _ID;
        private int _UserID;
        public static DataProcessor manipulate = new DataProcessor();
        private TestAppointment _TestAppointment;
        private Test _Test;
        public event Func<object, TestAppointment, Test, Task> HandelData;
        private Tests(int ID, int UserID, string UserName, bool Show, int Testnum)
        {
            InitializeComponent();
            _testNum = Testnum;
            _ID = ID;
            _UserID = UserID;
        }
        public static async Task<Tests> creatAsync(int ID, int UserID, string UserName, bool Show, int Testnum)
        {
            Tests test = new Tests(ID, UserID, UserName, Show, Testnum);
            await test.setcontrol(ID, UserID, UserName, Show);
            return test;
        }
        public bool Result { get; private set; }

        public ISupportedType<TestAppointment> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _TestAppointment;
            }
            return null;
        }
        public void Clear()
        {

        }
        private void Loadx()
        {

            //if (_TestAppointment.AppointmentDate > 0)
            //{
            //    txtID.Text = (_person.PersonID).ToString();
            //    txtNational.Text = (_person.NationalNo);
            //    txtFirst.Text = (_person.FirstName);
            //    txtSeconed.Text = (_person.SecondName);
            //    txtThired.Text = (_person.ThirdName);
            //    txtLast.Text = (_person.LastName);
            //    dateTimePicker1.Value = _person.DateOfBirth;
            //    txtAddress.Text = (_person.Address);
            //    txtEmail.Text = (_person.Email);
            //    txtPhone.Text = (_person.Phone);
            //    cmbNachonalty.SelectedIndex = (_person.NationalityCountryID);
            //    cmbGender.SelectedIndex = (_person.Gendor);
            //    if (!string.IsNullOrWhiteSpace(_person.ImagePath) && File.Exists(_person.ImagePath))
            //        pictureBox12.ImageLocation = _person.ImagePath;
            //}
        }
        private bool handler<T>(T Sender, Action myfun) where T : Control
        {
            bool success;
            try
            {
                myfun();
                success = true;
                errorProvider1.SetError(Sender, "");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(Sender, ex.Message);
                success = false;
            }
            return success;
        }
        private async Task<bool> checksTestAppointments()
        {


            _TestAppointment.SetTestTypeID(_testNum);
            _TestAppointment.SetLocalDrivingLicenseApplicationID(_ID);
            _TestAppointment.SetCreatedByUserID(_UserID);
                DataTable _tab=await cmb.Rows("TestTypes", null, "TestTypeID", true, true, _testNum);
            DataRow dr =_tab?.Rows[0];
            _TestAppointment.SetIsLocked(false);
            _TestAppointment.SetPaidFees(Convert.ToDecimal(dr["TestTypeFees"]));
            _TestAppointment.SetAppointmentDate(dateTimePicker1.Value);

            return true;
        }
        private Task<bool> checksTest()
        {


           _Test.SetTestAppointmentID(_TestAppointment.TestAppointmentID);
            _Test.SetCreatedByUserID (_UserID);
                _Test.SetNotes(textBox1.Text);
            if(rbpass.Checked)
            _Test.setTestResult(true);
            else
                _Test.setTestResult(false);
            return Task.FromResult(true);
        }

        public string PrimaryIntColumnIDName()
        {
            if (_TestAppointment == null)
                nullTestAppointment();
            return _TestAppointment.PrimaryIntColumnIDName;
        }

        private void nullTestAppointment() => _TestAppointment = new TestAppointment(1, 1, DateTime.Now, 1, 1, false);
        private void nullTest() =>_Test = new Test(72, false, "", 1);
        private void btnCancle_Click(object sender, EventArgs e) => this.FindForm()?.Close();//_frm.Close();   

        public Task setInfo(/*Form frm,*/DataRow Row, bool Show)
        {
            //groupBox1.Enabled = lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = !Show;
            //if (int.TryParse(Row?[0].ToString() ?? "Null", out int result))
            //{
            //    TestAppointment x = new TestAppointment(Row);
            //    _TestAppointment = x;
            //    setDataGridView(x.LocalDrivingLicenseApplicationID);
            //    //_person.FromDataRow(Row);
            //    Loadx();

            //}
            //else
            //    Clear();
            //btnAddEdit.Text = (!Show && result > 0) ? "Edit" : "Add";
            //   _frm = frm;
            throw new NotImplementedException("This method is not implemented. Use setInfo<U> instead.");

        }

        public async Task<int> setInfo<U>(U ID, bool Show, string Title) where U : IConvertible
        {
            try
            {
                //groupBox1.Enabled = lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = !Show;
                if (_TestAppointment == null || _TestAppointment.IsLocked == false)
                    nullTestAppointment();
                DataTable dt =await cmb.Rows("TestAppointments", null, "LocalDrivingLicenseApplicationID", true, false, ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TestAppointment x = new TestAppointment(dt.Rows[dt.Rows.Count - 1]);
                    _TestAppointment = x;
                   await setDataGridView(_TestAppointment.LocalDrivingLicenseApplicationID);
                }
                Loadx();
                //btnAddEdit.Text = (!Show && _person.PersonID != -1) ? "Edit" : "Add";
                return _TestAppointment.TestAppointmentID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }

        }

        private async Task setcontrol(int ID, int UserID, string UserName, bool Show)
        {
            LocalLicenses crl = new LocalLicenses(UserID, UserName, Show);
            await crl.setInfo(ID, true, "show");
            tabControl1.TabPages[0].Controls.Add(crl);
            crl.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, -25);
            tabControl1.Height = panel5.Height + 25;
           await setDataGridView(ID);
        }
        private async Task setDataGridView(int ID)
        {
            dt =await cmb.Rows("TestAppointments", null, "LocalDrivingLicenseApplicationID", true, false, ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                refrech();
            }
        }
        public void CreateTestAppointmentTable()
        {
            // Create a new DataTable
            // Add columns to the DataTable based on the provided schema
            if (dt == null)
                dt = new DataTable("TestAppointment");

            dt.Columns.Add("TestAppointmentID", typeof(int)).AllowDBNull = true;
            dt.PrimaryKey = new[] { dt.Columns["TestAppointmentID"] };
            dt.Columns.Add("TestTypeID", typeof(int)).AllowDBNull = true;
            dt.Columns.Add("LocalDrivingLicenseApplicationID", typeof(int)).AllowDBNull = true;
            dt.Columns.Add("AppointmentDate", typeof(DateTime)).AllowDBNull = true; // SMALLDATETIME maps to DateTime in C#
            dt.Columns.Add("PaidFees", typeof(decimal)).AllowDBNull = true; // SMALLMONEY maps to decimal in C#
            dt.Columns.Add("CreatedByUserID", typeof(int)).AllowDBNull = true;
            dt.Columns.Add("IsLocked", typeof(bool)).AllowDBNull = true; // BIT maps to bool in C#
            refrech();
            // Return the created DataTable

        }
        private async Task setnullTest()
        {
             DataTable _tab=   await cmb.Rows("Tests", null, "TestAppointmentID", true, false, _TestAppointment.TestAppointmentID);
            DataRow r = _tab?.Rows[0];
            if (r != null)
                _Test = new Test(r);
        }
        private void refrech()
        {
            dv = dt.DefaultView;
            dv.RowFilter = $"TestTypeID = {_testNum}";
            dataGridView1.DataSource = dv;
            List<int> f = new List<int> { 1, 2, 5 };
            for (int i = 0; i < f.Count; i++)
                dataGridView1.Columns[f[i]].Visible = false;
        }
        private async Task<bool> checkesTest_TestAppointment()
        {
                bool secusse = false;
            if(_Test!=null)
            {
                if (_Test.TestResult == true)
                {
                    MessageBox.Show("You Can Not Take The Test Again");
                    return secusse;
                }
            }
            else if(_TestAppointment.IsLocked==true)
            {
               await setnullTest();
                return !_Test.TestResult;
            }
            if (_TestAppointment != null)
            {
                if (_TestAppointment.IsLocked == true)
                {
                     DataTable _tab= await cmb.Rows("Tests", null, "TestAppointmentID", true, true, _TestAppointment.TestAppointmentID);
                    DataRow dr = _tab?.Rows[0];
                    if (dr != null)
                    {
                        Test x = new Test(dr);
                        _Test = x;
                        secusse = _Test.TestResult;
                        return !secusse;
                    }
                }
                if (dataGridView1.Rows.Count < 1)
                    secusse = true;

            }
            return secusse;
        }
        private async Task AddEditOp<T>( T val, HashSet<string> hash, Func<Task<bool>> checkesTest, Func<Task<bool>> checkes, Action action, string TableName, Action<bool> action1) where T : ISupportedType<T>
        {
            try
            {
                //var hash = TestAppointment.map.Keys.ToHashSet();

                if (val == null ||await checkesTest())
                    //nullTestAppointment();
                    action();
                if (await checkes())//checksTestAppointments())
                {
                    errorProvider1.Clear();
                    Result = await manipulate.ProcessData(TableName/*"TestAppointments"*/, hash, val/*_TestAppointment*/);
                    action1(Result);
                  
                }

                MessageBox.Show("Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"You Can Not Add Anless You already Take The Test"+ex.Message);// \n{ex.Message}");
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (await checkesTest_TestAppointment())
            {
                await AddEditOp( _TestAppointment, TestAppointment.map.Keys.ToHashSet(), checkesTest_TestAppointment, checksTestAppointments, nullTestAppointment, "TestAppointments", (result) =>
            {
                if (result)
                {

                    if (dt == null)
                    { CreateTestAppointmentTable(); }
                    dt.Rows.Add(_TestAppointment.ToDataRow(dt, true));
                    dataGridView1.Refresh();

                }
            }

            );
            }
        }

        private async void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
            var hash = TestAppointment.map.Keys.ToHashSet();
            DataRow dr= dt.Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value);
            await _TestAppointment.FromDataRow(dr);
            if (_TestAppointment.IsLocked == false)
            {
                _TestAppointment.SetAppointmentDate(dateTimePicker1.Value);
                dr["AppointmentDate"] = _TestAppointment.AppointmentDate;
                //AddEditOperation.operation(ref dataGridView1,ref this,ref dt, null, false, false);
             await   manipulate.ProcessData("TestAppointments", hash,  _TestAppointment);
                dr.AcceptChanges();
            }
            else
                MessageBox.Show("You Can Not Change Taken Test Date");

        }
        private void setTabpage2()
        {
            if (_testNum == 1)
            {
                pictureBox1.Image =Properties.Resources.eye_open;
                lblTest.Text = "Viosn Test";
            }
            else if (_testNum == 2)
            {
                pictureBox1.Image = Properties.Resources.traffic_light;
                lblTest.Text = "Street Test";
            }
            else
            {
                pictureBox1.Image =Properties.Resources.exam;
                lblTest.Text = "Writen Test";
            }
        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_TestAppointment.IsLocked == false)
            {
                setTabpage2();
            
                tabControl1.SelectedIndex = 1;


            }        
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
      
               await AddEditOp( _Test, Test.map.Keys.ToHashSet(),async () =>await Task.FromResult(true), checksTest, nullTest, "Tests", async (result) =>
            {
                if (result)
                {

                    drx = dt.Rows?.Find(_TestAppointment.TestAppointmentID);
                    if (drx == null)
                    {
                        dt.Rows.Add(_TestAppointment.ToDataRow(dt, true));
                        drx = dt.Rows[dt.Rows.Count - 1];
                    }
                    _TestAppointment.SetIsLocked(true);
                    drx["IsLocked"] = true;
                    //AddEditOperation.operation(ref dataGridView1,ref this,ref dt, null, false, false);
                   await manipulate.ProcessData("TestAppointments", TestAppointment.map.Keys.ToHashSet(),  _TestAppointment);
                    drx.AcceptChanges();
                    dataGridView1.Refresh();
                }
            });
                tabControl1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HandelData?.Invoke(this, _TestAppointment, _Test);
            this.FindForm().Close();
        }
    }
}
