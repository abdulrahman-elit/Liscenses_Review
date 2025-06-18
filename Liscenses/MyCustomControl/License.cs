using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using EntityLayer.Classes;
using Liscenses.Classes;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;

namespace Liscenses
{
    public partial class License : UserControl, ICustmControl<MyLicense>
    {
        LocalLicenses _local;
        MyLicense _license;
        DataRow row;
        int _userid;
        int PersonID;
        public License(int ID, int UserID, string UserName)
        {
            _userid = UserID;
            InitializeComponent();
            _local = new LocalLicenses(ID, UserName, true);
            _local.Dock = DockStyle.Top;
            this.Controls.Add(_local);
        }
        bool Result;
        //private void nulllicense() => _license = new MyLicense(1,1,1,);

        public ISupportedType<MyLicense> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _license;
            }
            return null;
        }

        public string PrimaryIntColumnIDName()
        {
            return _license.PrimaryIntColumnIDName;
        }

        public Task setInfo(DataRow Row, bool Show)
        {

            throw new NotImplementedException();
        }
        public async Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible
        {
            B_Fetch cmb = new B_Fetch();
           await  _local.setInfo(ID, visibility, Title);
               DataTable _tab= await cmb.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", true, true, ID);
            row =_tab?.Rows[0];
			return 1;
            //throw new NotImplementedException();
        }
        //private void setDriver(ref int PersonID) =>driver= new Driver(PersonID,_userid, DateTime.Now);
        private async Task<int> MakeDriver()
        {
            var manpuilte = new DataProcessor();
            Driver driver=new Driver(PersonID,_userid,DateTime.Now);
            //setDriver(ref PersonID);
           await manpuilte.ProcessData("Drivers", Driver.map.Keys.ToHashSet(),  driver, "DriverID");
            return driver.DriverID;
        }

        internal async Task<int> AddDrivers()
        {
            B_Fetch fetch = new B_Fetch();
              DataTable _tab  = await fetch.Rows("Applications", null, "ApplicationID", true, true, Convert.ToInt32(row["ApplicationID"]));
            DataRow rowx = _tab?.Rows[0];
			if (rowx != null)
            {
                 
                PersonID = Convert.ToInt32(rowx["ApplicantPersonID"]);
                _tab =await fetch.Rows("Drivers", null, "PersonID", true, true, PersonID);
                rowx =_tab?.Rows[0];
				if (rowx == null)
                    return await MakeDriver();
                return Convert.ToInt32(rowx["DriverID"]);
            }
            else
                throw new Exception("This Person Is Not Exists in the system");
        }
        bool cheackintable(DataTable dtl)
        {
            if (dtl != null)
            {
                foreach (DataRow r in dtl.Rows)
                    if (Convert.ToInt32(row["LicenseClassID"]) == Convert.ToInt32(r["LicenseClass"]))
                        return false;
            }
            else
                return false;
                return true;
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int DriverID = await AddDrivers();
                B_Fetch fetch = new B_Fetch();
                DataTable tablelicense = await fetch.Rows("Licenses", null, "DriverID", true, true, Convert.ToInt32(row["ApplicationID"]));
                if (tablelicense == null||cheackintable(tablelicense))
                {
                    DataTable _tab=await fetch.Rows("LicenseClasses", null, "LicenseClassID", true, true, Convert.ToInt32(row["LicenseClassID"]));
                    DataRow rowx =_tab?.Rows[0];
					if (rowx != null)
                    {
                        _license = new MyLicense(Convert.ToInt32(row["ApplicationID"]), DriverID, Convert.ToInt32(rowx["LicenseClassID"]), DateTime.Now
                            , DateTime.Now.AddYears(Convert.ToInt32(rowx["DefaultValidityLength"])), textBox1.Text, Convert.ToDecimal(rowx["ClassFees"]),
                            true, 1, _userid);
                        var manpuilte = new DataProcessor();
                       await manpuilte.ProcessData("Licenses", MyLicense.map.Keys.ToHashSet(),  _license, "LicenseID");
                        MessageBox.Show("Successfully");
                    }
                }
                else
                    MessageBox.Show("You Alrady Has A License From This Applicaion");

            }

            catch (Exception ex)
            {
                MessageBox.Show($"Failed to Add/Edit \n{ex.Message}");
            }
        }
        private void button1_Click(object sender, EventArgs e)=>this.FindForm().Close();
    }
}
