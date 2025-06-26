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
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;
//using EntityLayer.Classes;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;

namespace Liscenses.MyCustomControl
{
    public partial class ApplicationTypes : UserControl,ICustmControl<ApplicationType>
    {
        public ApplicationTypes()
        {
            InitializeComponent();
        }
        ApplicationType _ApplicationType;
        bool Result;

        public ISupportedType<ApplicationType> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _ApplicationType;
            }
            return null;
        }

        public Task setInfo(DataRow Row, bool Show)
        {
            throw new NotImplementedException();
        }

        public async Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible
        {
            try
            {
                B_Fetch b_Fetch = new B_Fetch();

                  DataTable _tab=  await b_Fetch.Rows("ApplicationTypes", null, "ApplicationTypeID", true, true, ID);
                DataRow r = _tab?.Rows[0];
                if (r != null)
                {
                    _ApplicationType = new ApplicationType(r);
                    loadx();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 1;
        }
        private void loadx()
        {
            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            //if (_ApplicationType.status == enStatus.New)
            //{
            //    this.Text = "New Application Type";
            //    btnSave.Text = "Add";
            //}
            //else
            //{
            //    this.Text = "Update Application Type";
            //    btnSave.Text = "Update";
            //}
        }
        public string PrimaryIntColumnIDName()
        {
            return _ApplicationType.PrimaryIntColumnIDName;
        }
        private bool Checks()
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Please Enter Application Type Title");
                return false;
            }
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                MessageBox.Show("Please Enter Application Fees");
                return false;
            }
            if (!decimal.TryParse(txtFees.Text, out decimal fees))
            {

                MessageBox.Show("Please Enter Valid Application Fees");
                return false;
            }
            _ApplicationType.SetApplicationTypeTitle (txtTitle.Text);
            _ApplicationType.SetApplicationFees (fees);
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)=>this.FindForm().Close();

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var manpulite = new DataProcessor();
                if (Checks())
                    Result = await manpulite.ProcessData("ApplicationTypes", ApplicationType.map.Keys.ToHashSet(),  _ApplicationType, "ApplicationTypeID");
                MessageBox.Show("SeccussFully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
