using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Interfaces;
using EntityLayer.Classes;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;

namespace Liscenses.MyCustomControl
{
    public partial class TestTypes : UserControl, ICustmControl<TestType>
    {
        public TestTypes()
        {
            InitializeComponent();
        }
        bool Result;
        TestType _TestType;
        public ISupportedType<TestType> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _TestType;
            }
            return null;
        }

        public string PrimaryIntColumnIDName()
        {
            return _TestType.PrimaryIntColumnIDName;
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
                  DataTable _tab=  await b_Fetch.Rows("TestTypes", null, "TestTypeID", true, true, ID);
                DataRow r = _tab?.Rows[0];
                if (r != null)
                {
                    _TestType = new TestType(r);
                    loadx();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 1;
        }
        private void loadx()
        {
            lblID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle.ToString();
            txtFees.Text = _TestType.TestTypeFees.ToString();
            txtDescription.Text = _TestType.TestTypeDescription.ToString();
        }
        private bool Checks()
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtFees.Text))
            {
                MessageBox.Show("Please Fill All Required Fields");
                return false;
            }
            _TestType.SetTestTypeTitle(txtTitle.Text);
            _TestType.SetTestTypeDescription(txtDescription.Text);
            _TestType.SetTestTypeFees(Convert.ToDouble(txtFees.Text));
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e) => this.FindForm().Close();

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var manpulite = new DataProcessor();
                if (Checks())
                    Result =await manpulite.ProcessData("TestTypes", TestType.map.Keys.ToHashSet(),  _TestType, "TestTypeID");
                MessageBox.Show("SeccussFully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
