using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsPerson.SupportedTypes;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
using Liscenses;
using MidLayer.B_CRUDOperation.B_Read;

//using MiddleLayer.B_CRUDOperation.B_Read;
using static System.Net.Mime.MediaTypeNames;

namespace EntityLayer.Classes
{
    public partial class LocalLicense : UserControl, ICustmControl<MyLicense>
    {
        MyLicense _license;
        int year = 0;
        public LocalLicense()
        {
            InitializeComponent();
        }
        bool Result;
        public ISupportedType<MyLicense> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _license;
            }
            return null;
        }
        public int ApplicaionID() => _license.ApplicationID;

        public string PrimaryIntColumnIDName()
        {
            return _license.PrimaryIntColumnIDName;
        }
        public int maxumemYear() => year;
        public Task setInfo(DataRow Row, bool Show)
        {
            throw new NotImplementedException();
        }
        B_Fetch cmb = new B_Fetch();
        private void nullLicense() => _license = new MyLicense(1, 1, 1, DateTime.Now, DateTime.Now.AddYears(10), "", 1, true, 1, 1);
        public async Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible
        {
            try
            {
                if (_license == null)
                    nullLicense();
                DataTable dtl =await cmb.Rows("Licenses", null, _license.PrimaryIntColumnIDName, true, true, ID);
                if (dtl != null && dtl.Rows.Count > 0)
                {
                    MyLicense xx = new MyLicense(dtl?.Rows[0]);
                    _license = xx;

                }

               await Loadx();

                return _license.LicenseID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }
        }
        private string IssueReason(byte reason) => reason switch { 1 => "First Time", 2 => "Damged ", 3 => "Lost", 4 => "Renew", 0 => "Detaind", _ => "Unknown" };
        private async Task Loadx()
        {
            DataRow dtw = null;
            DataTable _tab=    await cmb.Rows("Applications", null, "ApplicationID", true, true, _license.ApplicationID);
            DataRow dty = _tab?.Rows[0];
             _tab=   await cmb.Rows("LicenseClasses", null, "LicenseClassID", true, true, _license.LicenseClass);
            DataRow dtz =_tab?.Rows[0];
			year = Convert.ToInt32(dtz["DefaultValidityLength"]);
            if (dty != null)
            {
                _tab = await cmb.Rows("People", null, "PersonID", true, true, Convert.ToInt32(dty["ApplicantPersonID"]));
                dtw = _tab?.Rows[0];
            }
			if (dtw != null && dtz != null)
            {
                lblLicenseID.Text = _license.LicenseID.ToString();
                lblClassName.Text = dtz["ClassName"].ToString();
                lblDateOfBirth.Text = dtw["DateOfBirth"].ToString();
                lblFullName.Text = dtw["FirstName"].ToString() + " " + dtw["SecondName"].ToString() + " " + dtw["ThirdName"].ToString() + " " + dtw["LastName"].ToString();
                if (Convert.ToInt32(dtw["Gendor"]) == 1)
                    lblGender.Text = "Female";
                else
                    lblGender.Text = "Male";
                lblNationalNo.Text = dtw["NationalNo"].ToString();
                lblDriverID.Text = _license.DriverID.ToString();
                lblExpiredDate.Text = _license.ExpirationDate.ToString();
                lblIssueDate.Text = _license.IssueDate.ToString();
                if (_license.IsActive)
                    lblIsActive.Text = "Yes";
                else
                    lblIsActive.Text = "No";
                lblNote.Text = _license.Notes.ToString();
                lblStatus.Text = IssueReason(Convert.ToByte(_license.IssueReason));
                string image = dtw["ImagePath"].ToString();
                if (!string.IsNullOrWhiteSpace(image) && File.Exists(image))
                    pictureBox1.ImageLocation = image;

            }
            else
            {
                MessageBox.Show("No Data To Present");
                this?.FindForm()?.Close();
            }

            //lblLicenseID.Text = _license.LicenseID.ToString();


        }
    }
}
