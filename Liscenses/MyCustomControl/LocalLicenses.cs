using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using clsPerson.SupportedTypes;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;

//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
using MidLayer.Validation;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Liscenses
{

    public partial class LocalLicenses : UserControl, ICustmControl<EntityLayer.Classes.License>
    {
        private decimal fees;
        private MyApplication _application;
        private DataTable _table;
        private short selectedLicenseId;
        public static B_Fetch cmb = new B_Fetch();
        private EntityLayer.Classes.License _localApplicaion;
        public static DataProcessor manipulate = new DataProcessor();
        //private TabPage _page;
        private int _ID { get; set; } = -1;
        private int _age { get; set; } = -1;
        private string _UserName { get; set; } = string.Empty;
        //private Form _frm;
        //public LocalLicenses()
        //{
        //    InitializeComponent();
        //    //tabControl1.Enabled = false;
        //    personInfo1.Enabled = false;
        //    personInfo1.btnVisible(false);
        //}
        private void setShowed(bool Show)
        {
            panel1.Enabled = panel3.Visible = panel4.Visible = panel2.Enabled = !Show;
        }
        public LocalLicenses(int ID, string UserName, bool Show)
        {
             InitializeComponent();
            setShowed(Show);
            personInfo1.Enabled = false;
            personInfo1.btnVisible(false);
            nullApplication();
            comboboxHandle();
            
            _ID = ID;
            _localApplicaion = new EntityLayer.Classes.License(1, 1);
            txtUserName.Text = UserName;
            dateTimePicker1.Text = DateTime.Now.ToString();
        }
        public static async Task<LocalLicenses> CreateAsync(int ID, string UserName, bool Show)
        {
             LocalLicenses localLicenses = new LocalLicenses(ID, UserName, Show);
            PersonInfo personInfo1 = await PersonInfo.CreateAsync();
            await localLicenses.setcmb();
            await localLicenses.getApplicationFees();
            return localLicenses;
        }
        public  LocalLicenses()
        {
            InitializeComponent();
            //setShowed(Show);
            personInfo1.Enabled = false;
            personInfo1.btnVisible(false);
            nullApplication();
            comboboxHandle();
            //getApplicationFees();
            //_ID = ID;
            _localApplicaion = new EntityLayer.Classes.License(1, 1);
            //txtUserName.Text = UserName;
            dateTimePicker1.Text = DateTime.Now.ToString();
            //setcmb();
        }
        private async Task setcmb()//txtApplicationFees
        {
            _table =await cmb.Rows("LicenseClasses");
            cmbLicenseClass.DataSource = _table.DefaultView;
            //cmbLicenseClass.DataSource = cmb.Rows("ApplicationTypes").DefaultView;
            cmbLicenseClass.DisplayMember = "ClassName";
            cmbLicenseClass.ValueMember = "LicenseClassID";
            //txtApplicationFees.Text=cmbLicenseClass.SelectedValue.;
            cmbLicenseClass.SelectedIndex = 0;
            txtClassFees.Text = _table?.Rows[0]["ClassFees"].ToString();
            _age = Convert.ToInt32(_table?.Rows[0]["MinimumAllowedAge"] ?? 0);

        }
        private short _typeID;
        private bool exposed;
        public bool Result { get; private set; }
        private int FilleterResult { get; set; }
        public ISupportedType<EntityLayer.Classes.License> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _localApplicaion;
            }
            return null;
        }
        private async Task getApplicationFees()
        {
            DataTable dt =await cmb.Rows("ApplicationTypes");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ApplicationTypeTitle"].ToString() == "New Local Driving License Service")
                {
                    txtApplicaionFees.Text = dr["ApplicationFees"].ToString();
                    _typeID = Convert.ToInt16(dr["ApplicationTypeID"] ?? -1);
                    fees = Convert.ToDecimal(dr["ApplicationFees"] ?? -1);
                }
            }
        }
        public string PrimaryIntColumnIDName()
        {
            return _application.PrimaryIntColumnIDName;
        }
        private void nullApplication() => _application = new MyApplication(1, 1, 0, DateTime.Now, DateTime.Now, 1);
        private void nullLocalApplication() => _localApplicaion = new EntityLayer.Classes.License(1, 1);
        public async Task setInfo(DataRow Row, bool Show)
        {
            //panel1.Visible = panel3.Visible = panel4.Visible = panel2.Enabled = !Show;
            if (int.TryParse(Row?[0].ToString() ?? "Null", out int result))
            {

                //btnAddEdit.Text = "Edit";
                MyApplication x = new MyApplication(Row);
                _application = x;

               await personInfo1.setInfo(_application.ApplicantPersonID, !Show, "Show");
            }
            else
                Clear();

            bool operation = (!Show && result > 0);
            setPanel(Show);
            setControlText(operation);
            //setControlVisible(operation);
            exposed = (operation || Show);
            if (exposed)
            {
                Loadx();
            }
        }
        public async Task<int> setInfo<U>(U ID, bool Show, string Title) where U : IConvertible
        {
            try
            {
                panel1.Enabled = panel3.Visible = panel4.Visible = panel2.Enabled = !Show;
                if (_application == null)
                    nullApplication();
                if (_localApplicaion == null)
                    nullLocalApplication();
                DataTable dtl =await cmb.Rows("LocalDrivingLicenseApplications", null, _localApplicaion.PrimaryIntColumnIDName, true, true, ID);
                if (dtl != null && dtl.Rows.Count > 0)
                {
                    EntityLayer.Classes.License xx =await  EntityLayer.Classes.License.CreateAsync(dtl?.Rows[0]);
                    _localApplicaion = xx;
                    dtl =await cmb.Rows("Applications", null, _application.PrimaryIntColumnIDName, true, true, _localApplicaion.ApplicationID);
                    MyApplication x = new MyApplication(dtl?.Rows[0]);
                    _application = x;
                }
                //exposed = false;
                //exposed = true;
                bool operation = (!Show && _application.ApplicationID != -1);
                setPanel(Show);
                setControlText(operation);

                exposed = (operation || Show);
                if (exposed)
                {
                    Loadx();
                }

                return _application.ApplicationID;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }

        }

        private void setPanel(bool show)
        {

            if (!show)
            {
                tabControl1.Location = new Point(0, -25);
                tabControl1.Height = panel5.Height + 25;
            }
            else
            {
                tabControl1.Location = new Point(0, 0);
                tabControl1.Height = panel5.ClientSize.Height;
            }

            if (tabControl1.TabPages.Count > 0)
                tabControl1.SelectedIndex = 0;
        }
        private void setControlText(bool operation)
        {
            btnAddEdit.Text = operation ? "Edit" : "Add";
        }
        public void Clear()
        {
            personInfo1.Clear();
            txtApplicaionID.Text = "???";
            //txtUserName.Text="???";
            //txtApplicationFees.Text = "???";
            //txtApplicaionDate.Clear();
            txtFilter.Clear();
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
        private bool checks()
        {
            bool success = true;
            if ((int.TryParse(txtFilter.Text, out int result)) && result != _application.ApplicantPersonID)
            {
                _application.setApplicantPersonID(result);
            }
            //if (selectedLicenseId != _application.ApplicationTypeID)
            //{
            //    _application.setApplicationTypeID(selectedLicenseId);
            //}
            _application.setApplicationTypeID(_typeID);
            _application.setApplicationDate(DateTime.Now);
            _application.setLastStatusDate(DateTime.Now);
            if (personInfo1.age() < _age)
            {
                MessageBox.Show($"You Under Age The Age Required {_age}");
                success = false;
            }
            _application.setPaidFees(fees);
            _application.setCreatedByUserID(_ID);
            _application.setApplicationStatus(0);
            if (_localApplicaion.LicenseClassID != selectedLicenseId)
                _localApplicaion.setLicenseClassID(selectedLicenseId);
            _localApplicaion.setApplication(_application);
            return success;
        }
        private void Loadx()
        {

            if (exposed)
            {
                txtApplicaionID.Text = (_application.ApplicationID).ToString();
                txtUserName.Text = _UserName;
                dateTimePicker1.Text = _application.ApplicationDate.ToString();
                txtClassFees.Text = (_application.PaidFees.ToString());
                FilleterResult = (_application.ApplicantPersonID); //personInfo1.setInfo(_applicaion.PersonID, "PersonID", false, "Show");
                txtFilter.Text = FilleterResult.ToString();
                cmbLicenseClass.SelectedIndex = _localApplicaion.LicenseClassID - 1;
            }
            else
            {
                FilleterResult = -1;
                Clear();
            }
        }
        private void comboboxHandle()
        {

            cmbFilter.Items.Add("PersonID");
            cmbFilter.Items.Add("NationalNo");
            cmbFilter.SelectedIndex = 0;
        }
        private async Task  cmbHandleID()
        {

            if (int.TryParse(txtFilter.Text, out int PersonID))
                try
                {
                    FilleterResult = await personInfo1.setInfo(PersonID, "PersonID", false, "show");
                    errorProvider1.SetError(txtFilter, "");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            else
                errorProvider1.SetError(txtFilter, "Please enter a valid number");
        }
        private async Task cmbHandleNationalNo()
        {
            try
            {
                FilleterResult = await personInfo1.setInfo(txtFilter.Text, "NationalNo", false, "show");
                errorProvider1.SetError(txtFilter, "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (FilleterResult > 0)
            {
                tabControl1.SelectedIndex = 1;
                _application.setApplicantPersonID(FilleterResult);
                FilleterResult = -1;
            }
            else
                MessageBox.Show("Not Found");
        }

        private void button3_Click_1(object sender, EventArgs e) => this.FindForm()?.Close();

        private async void txtFilter_TextChanged(object sender, EventArgs e)
        {
            personInfo1.Clear();
            if (!(string.IsNullOrWhiteSpace(txtFilter.Text)))
            {
                if (cmbFilter.SelectedIndex == 0)
                   await cmbHandleID();
                else
                   await cmbHandleNationalNo();
            }
            else
                errorProvider1.SetError(txtFilter, "Please enter a Value ");
        }
        private async Task setRowsApplicationID()
        {
            _localApplicaion.rowsID.Clear();
            DataTable dtID = await cmb.Rows("Applications", null, "ApplicantPersonID", true, false, _application.ApplicantPersonID);
            if (dtID != null)
                foreach (DataRow dr in dtID.Rows)
                    _localApplicaion.rowsID.Add(Convert.ToInt32(dr["ApplicationID"]));
        }
        public async Task<bool> validate(EntityLayer.Classes.License _local) =>await LocalDrivingLicenseApplicationValidator.IsExceptedRequest(_local);

        private async void btnAddEdit_Click_1(object sender, EventArgs e)
        {

            try
            {
                var hash = MyApplication.map.Keys.ToHashSet();
                hash.Remove("ApplicationID");
                //if (_application == null)
                //    nullApplication();
                if (checks())
                {
                    errorProvider1.Clear();
                    await setRowsApplicationID();
                    //_localApplicaion.setLicenseClassID(selectedLicenseId);
                    if (await validate(_localApplicaion))
                    {
                        Result = await manipulate.ProcessData("Applications", hash,  _application);
                        _localApplicaion.setApplicationID(_application.ApplicationID);
                        if (Result)
                        {
                          await  manipulate.ProcessData("LocalDrivingLicenseApplications", EntityLayer.Classes.License.map.Keys.ToHashSet(),  _localApplicaion);
                            txtApplicaionID.Text = _application.PrimaryKey().ToString();
                            if (btnAddEdit.Text == "Add")
                                btnAddEdit.Text = "Edit";
                        }
                        MessageBox.Show("Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to Add/Edit \n{ex.Message}");
            }

        }
        //private void btnAddEdit_Click_1(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        var hash = MyApplication.map.Keys.ToHashSet();
        //        hash.Remove("ApplicationID");
        //        //if (_application == null)
        //        //    nullApplication();
        //        if (checksTestAppointments())
        //        {
        //            errorProvider1.Clear();
        //            Result = manipulate.ProcessData("Applications", hash, _application);
        //            if (Result && btnAddEdit.Text == "Add")
        //            {
        //                //_applicaion.setpersonID((int)manipulate.Rows("people", new HashSet<string> { "PersonID" }, "NationalNo", true, true, personInfo1.NationalNo)?.Rows[0].ItemArray.GetValue(0));
        //                //personInfo1.setInfo(_applicaion.PersonID, "PersonID", true, "Add");
        //                txtApplicaionID.Text = _application.PrimaryKey().ToString();
        //                btnAddEdit.Text = "Edit";
        //            }
        //            MessageBox.Show("Successfully");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to Add/Edit \n{ex.Message}");
        //    }

        //}

        private void btnCancle_Click_1(object sender, EventArgs e) => this.FindForm()?.Close();

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtFilter.Focus();
            FilleterResult = _application.ApplicantPersonID;
            tabControl1.SelectedIndex = 0;
        }

        private void cmbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLicenseClass.SelectedValue != null)
            {
                selectedLicenseId = Convert.ToInt16(cmbLicenseClass.SelectedIndex + 1);

                foreach (DataRow item in _table.Rows)
                {
                    if (int.TryParse(item["LicenseClassID"]?.ToString(), out int rowLicenseId))
                    {
                        if (rowLicenseId == selectedLicenseId)
                        {
                            txtClassFees.Text = item["ClassFees"].ToString();
                            _age = Convert.ToInt32(item["MinimumAllowedAge"] ?? 0);
                        }
                    }
                }
            }
        }

        private async void LocalLicenses_Load_1(object sender, EventArgs e)
        {
            await getApplicationFees();
            await setcmb();
            txtFilter.Focus();
        }

    
    }
}
