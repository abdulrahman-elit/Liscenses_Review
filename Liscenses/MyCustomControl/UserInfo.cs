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
using MidLayer.Validation;



//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Liscenses
{
    public partial class UserInfo : UserControl, ICustmControl<User>
    {
        private User _user;
        public static B_Fetch cmb = new B_Fetch();
        public static DataProcessor manipulate = new DataProcessor();
        //private TabPage _page;
        //private Form _frm;
        public UserInfo()
        {
            InitializeComponent();
            personInfo1.Enabled = false;
            personInfo1.btnVisible(false);
            nullUser();
            comboboxHandle();
        }
        private bool exposed;
        public bool Result { get; private set; }
        private int FilleterResult {  get; set; } 
        public ISupportedType<User> ManipulatingResult()
        {
            if (Result)
            {
                Result = false;
                return _user ;
            }
            return null;
        }
        public string PrimaryIntColumnIDName()
        {
            //if (_user == null)
            //    nullUser();
            return _user.PrimaryIntColumnIDName;
        }
        private void nullUser() => _user = new User(1, "xxx", "12345678", true);
        private void btnCancle_Click(object sender, EventArgs e) => this.FindForm()?.Close(); //_frm.Close();
        private void button2_Click(object sender, EventArgs e) {
            FilleterResult=_user.PersonID;
            tabControl1.SelectedIndex = 0;
        }
        public async Task setInfo(/*Form frm,*/ DataRow Row, bool Show)
        {
            panel1.Visible = panel3.Visible = panel4.Visible = panel2.Enabled = !Show;
            if (int.TryParse(Row?[0].ToString() ?? "Null", out int result))
            {

                //btnAddEdit.Text = "Edit";
                User x = new User(Row);
                _user = x;
                //setTabPage(Show);
                //_user.FromDataRow(Row);
               await personInfo1.setInfo(_user.PersonID, personInfo1.PrimaryIntColumnIDName(), !Show, "Show");
            }
            else
                Clear();

            bool operation = (!Show && result > 0);
            setPanel(Show);
            setControlText(operation);
            setControlVisible(operation);
            //_frm = frm;
            exposed = (operation || Show);
            if (exposed) 
            {
                await set();
                Loadx();
            }
        }
        public async Task<int> setInfo<T>(T ID, bool Show, string Title) where T : IConvertible
        {
            panel1.Visible = panel3.Visible = panel4.Visible = panel2.Enabled = !Show;

            try
            {
                nullUser();
                DataTable dtx =await cmb.Rows("users", null,_user.PrimaryIntColumnIDName, true, true, ID);
                if (dtx != null && dtx.Rows.Count > 0)
                {
                    User x = new User(dtx?.Rows[0]);
                    _user = x;
                }
                bool operation = (!Show &&_user.UserID!=-1);
                setPanel(Show);
                setControlText(operation);
                setControlVisible(operation);
                exposed = (operation || Show);
                if (exposed)
                {
                  await personInfo1.setInfo(_user.PersonID, personInfo1.PrimaryIntColumnIDName(), !Show, "Show");
                    await set();
                    Loadx();
                }
                return _user.UserID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }
            finally
            {
                //setPanel(visibility);
                //btnAddEdit.Visible = btnCancle.Visible = visibility;
                //btnAddEdit.Text = Title;
                //if(visibility) 
                //Loadx();
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
            lblUpdateConfierm.Text = operation ? "Updated" : "Confirm";
            
        }
        private void setControlVisible(bool visible)
        {
            tpPersonInfo.Visible=lblNote.Visible = false;
            if (visible)
            {
                linkLabel1.Visible = true;
                linkLabel2.Visible = panel1.Visible = false;
            }
            else
                linkLabel1.Visible = linkLabel2.Visible = txtaddional.Visible = lblConfiermPassword.Visible = pb3.Visible = lblNote.Visible = visible;

        }
        //private bool setConfirmPassword()
        //{
        //    //if (txtPassword.Text.Length < 8)
        //    //    throw new Exception("Password must be at least 8 characters");
        //    if (btnAddEdit.Text == "Add")
        //    {
        //        if (txtPassword.Text != txtConfirmPassword.Text)
        //        {
        //            errorProvider1.SetError(txtPassword, "Password and Confirm Password must be the same");
        //            return false;
        //        }

        //    }
        //    else
        //    {
        //        if (txtaddional.Text != txtConfirmPassword.Text)
        //        {
        //            errorProvider1.SetError(txtPassword, "Password and Confirm Password must be the same");
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        public void Clear()
        {
            personInfo1.Clear();
            txtUserID.Text = "???";
            txtUserName.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
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
        private async Task set() {
            if (/*btnAddEdit.Text == "Edit"*/exposed)
            {
                DataTable _tab = await Fetch.Rows("users", new HashSet<string> { "Password" }, "UserID", true, true, _user.UserID);//?.Rows[0].ItemArray.GetValue(0).ToString();
                _password =_tab?.Rows[0].ItemArray.GetValue(0).ToString();
                if(_password.Length<64)
                    {
                    _password = UserValidtaor.ComputeHash(_password);
                    _user.setPassword(_password);
                     }
            }
            else
                _password = string.Empty;
                }
        private bool setConfirmPassword()
        {
            if (txtPassword.Text.Length < 8)
                throw new Exception("Password must be at least 8 characters");
            if (btnAddEdit.Text == "Add")
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                    throw new ArgumentException("Password and Confirm Password must be the same");
            }
            else
            {
                if (txtaddional.Text != txtConfirmPassword.Text)
                    throw new ArgumentException("Password and Confirm Password must be the same");
            }
            return true;
        }
      
        private bool LengthPassword()
        {
            //if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            bool success=true;
            
            if (txtConfirmPassword.Text.Length < 8)
            {
                errorProvider1.SetError(txtConfirmPassword, "Password must be at least 8 characters");
                success = false;
            }
            if (txtPassword.Text.Length < 8)
            {
                errorProvider1.SetError(txtPassword, "Password must be at least 8 characters");
                success = false;
            }
            if (btnAddEdit.Text == "Edit")
            {
                if (txtaddional.Text.Length < 8)
                {
                    errorProvider1.SetError(txtaddional, "Password must be at least 8 characters");
                    success = false;
                }
            }

            return success;

        }
        private async void textBox1_TextChanged(object sender, EventArgs e)
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
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {


            if (btnAddEdit.Text == "Edit")
            {


                if (_password ==UserValidtaor.ComputeHash(txtPassword.Text))
                {
                    txtConfirmPassword.Enabled = txtaddional.Enabled = true;
                    lblNote.Visible = false;
                    errorProvider1.SetError(txtPassword, "");

                }
                else
                {
                    txtConfirmPassword.Enabled = txtaddional.Enabled = false;
                    lblNote.Visible = true;
                    txtaddional.Clear(); txtConfirmPassword.Clear();
                }
            }

        }
        private bool checks()
        {
            bool success = true;
            bool operation = true;
            if (txtUserName.Text != (_user.UserName))
            {
                operation = handler(txtUserName, () => _user.setUserName(txtUserName.Text));
                success = (operation && success) ? true : false;
            }

               if (linkLabel2.Visible||btnAddEdit.Text=="Add")
                {

                    if (LengthPassword())
                    {
                        operation = handler(txtPassword, () => setConfirmPassword()) ? handler(txtPassword, () => _user.setPassword(txtConfirmPassword.Text)) : false;
                        success = (operation && success) ? true : false;
                    }
                    else
                    {
                        errorProvider1.SetError(txtPassword, "Password must be at least 8 characters");
                        success = false;
                    }
                }
                else
                    _user.setPassword(_password);

             if (checkBox1.Checked != (_user.IsActive))
            {
                _user.setIsActive(checkBox1.Checked);
            }
            if((int.TryParse( txtFilter.Text,out int result))&&result !=_user.PersonID )
            {
                _user.setpersonID(result);
            }

            return success;
        }
        private void Loadx()
        {

            if (/*btnAddEdit.Text == "Edit"*/exposed)
            {
                txtUserID.Text = (_user.UserID).ToString();
                txtUserName.Text = (_user.UserName).ToString();
                //txtPassword.Text = (_user.Password);
                FilleterResult = _user.PersonID; //personInfo1.setInfo(_user.PersonID, "PersonID", false, "Show");
                txtFilter.Text = FilleterResult.ToString();
                checkBox1.Checked = (_user.IsActive);
            }
            else
            {
                FilleterResult = -1;
                Clear();
            }
        }
        private void  comboboxHandle()
        {

            cmbFilter.Items.Add("PersonID");
            cmbFilter.Items.Add("NationalNo");
            cmbFilter.SelectedIndex = 0;
        }
       private void UserInfo_Load(object sender, EventArgs e)
        {
        }
      
        private B_Check check = new B_Check();
        private  B_Fetch Fetch = new B_Fetch();
        private   string _password;
   
        private async void btnAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var hash = User.map.Keys.ToHashSet();
                hash.Remove("UserID");
                //if (_user == null)
                //    nullUser();
                if (checks())
                {
                    errorProvider1.Clear();
                    Result =await manipulate.ProcessData("users", hash,   _user);
                    if (Result && btnAddEdit.Text == "Add")
                    {
                        //_user.setpersonID((int)manipulate.Rows("people", new HashSet<string> { "PersonID" }, "NationalNo", true, true, personInfo1.NationalNo)?.Rows[0].ItemArray.GetValue(0));
                        //personInfo1.setInfo(_user.PersonID, "PersonID", true, "Add");
                        txtUserID.Text = _user.PrimaryKey().ToString();
                        btnAddEdit.Text = "Edit";
                    }
                    MessageBox.Show("Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to Add/Edit \n{ex.Message}");
            }

        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.Visible=panel1.Visible = false;
            linkLabel1.Visible=true;
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblNote.Visible=panel1.Visible=linkLabel2.Visible=txtaddional.Visible=pb3.Visible=lblConfiermPassword.Visible=true;
            txtaddional.Enabled=txtConfirmPassword.Enabled= linkLabel1.Visible = false;
            txtPassword.Focus();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtaddional.Clear();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            B_Check b_Check = new B_Check();
            if (FilleterResult >0)
            {
                if (await b_Check.Row("Users", "PersonID", FilleterResult, true)&&_user.status==enStatus.New)
                    MessageBox.Show("This Person Already in The System");
                else
                {
                    //if(b_Check.Row("Users", null,"PersonID",true,true, FilleterResult).Rows)
                    tabControl1.SelectedIndex = 1;
                    _user.setpersonID(FilleterResult);
                    FilleterResult = -1;
                }
            }
            else
                MessageBox.Show("Not Found");
        }
        private async Task cmbHandleID()
        {
   
                if (int.TryParse(txtFilter.Text, out int PersonID))
                    try
                    {
                        FilleterResult =await personInfo1.setInfo(PersonID, "PersonID", false, "show");
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
                FilleterResult =await personInfo1.setInfo(txtFilter.Text, "NationalNo", false, "show");
                errorProvider1.SetError(txtFilter, "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)=>this.FindForm()?.Close();

       
    }
}

#region tries
/* private void setTabPage(bool show)
 {
     //if (tabControl1 != null && tabControl1.TabPages.ContainsKey("UserInfo"))
     if(!show)
     {
             _page = tpUserInfo;
             tabControl1.TabPages.Remove(_page);
     }
     //else
     //{
     //    if(!tabControl1.TabPages.ContainsKey("tpUserInfo"))
     //        tabControl1.TabPages.Add(tpUserInfo);

     //}
 }*/

/*//if(tabControl1.TabPages.Count < 2)
     //{

     //    //if (tabControl1.TabPages.ContainsKey("tpUserInfo")&&_page!=null)
     //    //    tabControl1.TabPages.Insert(0, tpPersonInfo);
     //    //else if ( _page!=null)
     //    //    tabControl1.TabPages.Add(tpUserInfo);
     //}*/

/* private void cmbHandleID()
 {
     if (!int.TryParse(cmbFilter.SelectedItem.ToString(), out int value))
         errorProvider1.SetError(txtFilter, "Invalid Value Except Numbers");
     else
     {
         errorProvider1.SetError(txtFilter, "");
         try
         {
             if(check.Row("people", cmbFilter.Text, value, true))
             tabControl1.SelectedIndex = 1;
             else
                 MessageBox.Show(txtFilter,"Not Found")
         }
         catch (Exception ex)
         {
             errorProvider1.SetError(txtFilter, ex.Message);

         }
     }

 }
 private void cmbHandleNationalNo()
 {
     try
     {
         if (check.Row("people", cmbFilter.Text, txtFilter.Text, true))
             tabControl1.SelectedIndex = 1;
         else
             MessageBox.Show(txtFilter, "Not Found");
     }
     catch (Exception ex)
     {
         errorProvider1.SetError(txtFilter, ex.Message);

     }
 }*/
//{
//if (tabControl1.TabPages.Count > 1)
//}
//btnAddEdit.Visible = btnCancle.Visible = txtConfirmPassword.Visible = txtPassword.Visible = lblFilter.Visible = pb1.Visible =
//lblConfiermPassword.Visible = lblpassword.Visible = txtFilter.Visible = cmbFilter.Visible = tabPage2.Enabled = pb2.Visible  =
// txtaddional.Visible = lblUpdateConfierm.Visible = pb3.Visible = !Show;
//txtConfirmPassword.Visible=txtPassword.Visible=lblpassword.Visible=lblConfiermPassword.Visible=tabPage2.Enabled =pb1.Visible=pb2.Visible= !Show;
//txtPassword.Text= txtConfirmPassword.Text = (_user.Password).ToString().Length;
/*private bool setpass()
{

    return 
}*/
//check.Row("users", "Password", txtPassword.Text)
/*if (txtPassword.Text.Length < 8)
else
}
}
}
}
//comboBox1.Items.Clear();
//comboBox1.Items.Add("None");
//foreach (var column in Person.map.Keys)
//{
//    if (cmbFilter == null || !cmbFilter.Contains(column))
//        comboBox1.Items.Add(column);
//}
//else
//    MessageBox.Show("Please enter a valid NationalNo");
//personInfo1= new PersonInfo();
/*HashSet<string> cmbFilter*/
//private static readonly HashSet<string> cmbPeopleFilter = new HashSet<string>() { "ImagePath", "PersonID", "NationalityCountryID" };

#endregion