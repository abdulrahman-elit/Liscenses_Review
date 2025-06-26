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
//using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Runtime.Remoting.Lifetime;
using MidLayer.Process;
using MidLayer.B_CRUDOperation.B_Read;
namespace Liscenses.MyCustomControl
{
    public partial class DetaindLicensex : UserControl, ICustmControl<DetainedLicense>
    {
        int UserID;
        string UserName;
        public DetaindLicensex(int UserID, string UserName)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            InitializeComponent();
        }
        DetainedLicense detaind;
        bool Result;
        public ISupportedType<DetainedLicense> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return detaind;
            }
            return null;
        }

        public string PrimaryIntColumnIDName()
        {
            return detaind.PrimaryIntColumnIDName;
        }

        public Task setInfo(DataRow Row, bool Show)
        {
            throw new NotImplementedException();
        }
        B_Fetch cmb = new B_Fetch();
        private void nullLDetaind() => detaind = new DetainedLicense(1, DateTime.Now, 0, 1);
        public async Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible
        {
            try
            {
                if (detaind == null)
                    nullLDetaind();
                    DataTable _tab=await cmb.Rows("DetainedLicenses", null, detaind.PrimaryIntColumnIDName, true, true, ID);
                DataRow dr =  _tab?.Rows[0];
				if (dr != null)
                {
                    detaind = new DetainedLicense(dr);
                    await localLicense1.setInfo(detaind.LicenseID, true, "show");
                }

                Loadx(Title);
                if (Title == "Show")
                    btnRelase.Visible = false;
                return detaind.DetainID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }
        }
        private void Loadx(string Title)
        {
            lblCreatedByUserID.Text = detaind.CreatedByUserID.ToString();
            lblDetainDate.Text = detaind.DetainDate.ToString();
            lblDetainID.Text = detaind.DetainID.ToString();
            lblFineFees.Text = detaind.FineFees.ToString();
            if (Title == "Show")
            {
                label9.Visible = false;
            }
            else
                lblReleasedByUserID.Text = UserName;
        }

        private void DetaindLicense_Load(object sender, EventArgs e)
        {

        }
        private async Task<int> relaseApplicaionId()
        {
            B_Fetch cmb = new B_Fetch();
            DataTable table = await cmb.Rows("ApplicationTypes");
            if (table != null)
            {
                foreach (DataRow dr in table.Rows)
                    if (dr["ApplicationTypeTitle"].ToString().Contains("Release"))
                        return Convert.ToInt32(dr["ApplicationTypeID"]);
            }

            return -1;
        }
        private async void btnRelase_Click(object sender, EventArgs e)
        {
            try
            {
                var manpiulate = new DataProcessor();
                detaind.setIsReleased(true);
                detaind.setReleasedByUserID(UserID);
                detaind.setReleaseDate(DateTime.Now);
                detaind.setReleaseApplicationID(localLicense1.ApplicaionID());
                Result = await manpiulate.ProcessData("DetainedLicenses", DetainedLicense.map.Keys.ToHashSet(),  detaind, "DetainID");
                MessageBox.Show("SeccussFully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Data Creation" + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e) => this.FindForm().Close();
    }
}
