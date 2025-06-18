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
using EntityLayer.Interfaces;
//using EntityLayer.Classes;
using MidLayer.Process;
using MidLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;

namespace Liscenses.MyCustomControl
{
    public partial class Replace_Damged : UserControl, ICustmControl<MyLicense>
    {
        decimal fees;
        int _UserID;
        byte status ;
        public Replace_Damged(int UserID)
        {
            _UserID = UserID;   
            InitializeComponent();
        }
        static B_Fetch  cmb = new B_Fetch();

        private DataTable dt;
        private void datatable(int status) 
        {
            fees = Convert.ToDecimal(dt.Rows[status-1]["ApplicationFees"]);
            
        }
        private void checksdetaicon()
        {
            if (rbReplace.Checked)
            {
                status = 2;
                datatable(3);
            }
            else if (rbDamged.Checked)
            {
                status = 3;
                datatable(4);
            }
            else if (rbReNew.Checked)
            {
                datatable(1);
                status = 4;
            }
            else if(rbDetaind.Checked) 
            {
                status = 0;
                datatable(5);
            }
            else
            {
                status=_license.IssueReason;
                datatable(6);
            }
                lblCost.Text = fees.ToString() + " $";
        }
        private void checedrb()
        {
            checksdetaicon();
        }
        private void rbReplace_CheckedChanged(object sender, EventArgs e)
        {
            checedrb();
        }

        private void rbDamged_CheckedChanged(object sender, EventArgs e)
        {
            checedrb();
        }
        bool Result;
        MyLicense _license;
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
        private void nullLicense() => _license = new MyLicense(1, 1, 1, DateTime.Now, DateTime.Now.AddYears(10), "", 1, true, 1, 1);
         public async Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible
        {
            try
            {
                if (_license == null)
                    nullLicense();
                DataTable dtl = await cmb.Rows("Licenses", null, _license.PrimaryIntColumnIDName, true, true, ID);
                if (dtl != null && dtl.Rows.Count > 0)
                {
                    MyLicense xx = new MyLicense(dtl?.Rows[0]);
                    _license = xx;

                }
                await localLicense1.setInfo(ID, visibility, Title);
                if (_license.ExpirationDate <= DateTime.Now)
                {
                    rbDamged.Enabled=rbReplace.Enabled = rbGloabl.Enabled = false;
                    rbReNew.Checked = true;
                }
                else
                    rbReNew.Enabled = false;
                    DataTable _tab=await cmb.Rows("InternationalLicenses", null, "ApplicationID", true, true, _license.ApplicationID);
                    DataRow rox=_tab?.Rows[0];
                if (_license.LicenseClass !=3||rox!=null)
                    rbGloabl.Enabled = false;


                return _license.LicenseID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }


        }
        private void btnCancel_Click(object sender, EventArgs e)=>this.FindForm().Close();

         DetainedLicense detained=null;
         InternationalLicense gloabl=null;
         MyLicense x=null;
         DataProcessor manpiulate = new DataProcessor();
        private async Task handle()
        {
                DataTable _tab=await cmb.Rows("Applications", null, "ApplicationID", true, true, _license.ApplicationID);
            DataRow dataRow=_tab?.Rows[0];
            MyApplication application = new MyApplication(dataRow);
            MyApplication newapplication=new MyApplication(application.ApplicantPersonID,_UserID,status,DateTime.Now,DateTime.Now,fees);
          await  manpiulate.ProcessData("Applications", MyApplication.map.Keys.ToHashSet(), newapplication, "ApplicationID");

            if (x != null)
            {
                x.setApplicationID(newapplication.ApplicationID);
                Result = await manpiulate.ProcessData("Licenses", MyLicense.map.Keys.ToHashSet(),  x, "LicenseID");
                _license = x;
            }
            else if (detained != null)
            {
                detained.setReleaseApplicationID(newapplication.ApplicationID);
                Result = false;
                await manpiulate.ProcessData("DetainedLicenses", DetainedLicense.map.Keys.ToHashSet(),  detained, "DetainedLicenseID");
            }
            else if (gloabl != null)
            {
                gloabl.setApplicationID(newapplication.ApplicationID);
                Result = false;
                await manpiulate.ProcessData("InternationalLicenses", InternationalLicense.map.Keys.ToHashSet(),  gloabl, "InternationalLicenseID");
            }
        }
        private async void btnCtreat_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expired = _license.ExpirationDate;
                if (status == 0)
                    _license.setIssueReason(status);
                else
                    _license.setIsActive(false);

                if(!rbGloabl.Checked)
                  await   manpiulate.ProcessData("Licenses", MyLicense.map.Keys.ToHashSet(),  _license, "LicenseID");

                if (status == 4)
                    expired = DateTime.Now.AddYears(localLicense1.maxumemYear());

                if (status == 0)
                    detained = new DetainedLicense(_license.LicenseID, DateTime.Now, fees, _UserID);
                else if(rbGloabl.Checked)
                        gloabl=new InternationalLicense(_license.ApplicationID,_license.DriverID,_license.LicenseID,DateTime.Now,DateTime.Now.AddYears(1),true,_UserID);
                else
                    x = new MyLicense(_license.ApplicationID, _license.DriverID, _license.LicenseClass, _license.IssueDate, expired, _license.Notes, fees, true, status, _license.CreatedByUserID);

               await handle();
                MessageBox.Show("SeccussFully");
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Data Creation" + ex);
            }
        }

        private void rbReNew_CheckedChanged(object sender, EventArgs e)
        {
            checedrb();
        }

        private void rbDetaind_CheckedChanged(object sender, EventArgs e)
        {
            checedrb();
        }

        private async void Replace_Damged_Load(object sender, EventArgs e)
        {
            dt = await cmb.Rows("ApplicationTypes");//cmb.FillComboBox(cmbLicenseClass, "LicenseClasses", "LicenseClassID", "LicenseClassName", true);
            datatable(3);
            lblCost.Text=fees.ToString()+" $";
        }

        private void rbGloabl_CheckedChanged(object sender, EventArgs e)
        {
            checedrb();
        }
    }
}
