using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using MiddleLayer.Process;
//using MiddleLayer.B_CRUDOperation.B_Read;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Liscenses.Classes;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;
namespace Liscenses
{
    public partial class PersonInfo: UserControl,ICustmControl<Person>
    {
           public static B_Fetch cmb= new B_Fetch();
           public static DataProcessor manipulate= new DataProcessor();
           private Person _person;
        private bool flag=false;
        //private Form _frm;
        string newSavedImagePath;
        public PersonInfo()
        {
            InitializeComponent();
            //setcmb();
            groupBox1.Enabled = false;
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
        }
      
        public static async Task<PersonInfo> CreateAsync()
        {
         PersonInfo personInfo = new PersonInfo();
            await personInfo.setcmb();
            return personInfo;  
        }
        public int age()=>(DateTime.Now.Year- _person.DateOfBirth.Year);
        public void btnVisible(bool show)
        {
            btnAddEdit.Visible =btnCancle.Visible=lblAddEdit.Visible=lblRemove.Visible =show;
        }
        public async Task setInfo(/*Form frm,*/DataRow Row,  bool Show)
        {
            groupBox1.Enabled = lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = !Show;
            if (int.TryParse(Row?[0].ToString() ?? "Null", out int result))
            {
                Person x  = new Person(Row);
                _person = x;
                    //_person.FromDataRow(Row);
                    await Loadx();
                
            }
            else 
                Clear();
                btnAddEdit.Text = (!Show && result > 0) ? "Edit" : "Add";
            //   _frm = frm;
        //    return Task.CompletedTask;

        }
     
        public async Task<int> setInfo<U>(U ID, bool Show, string Title) where U : IConvertible
        {
            try
            {
                groupBox1.Enabled = lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = !Show;
                if(_person==null)
                nullPerson();
                DataTable dt = await cmb.Rows("people", null,_person.PrimaryIntColumnIDName, true, true, ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Person x = new Person(dt?.Rows[0]);
                    _person = x;
                }
                    await Loadx();
                btnAddEdit.Text = (!Show && _person.PersonID!=-1) ? "Edit" : "Add";
                return _person.PersonID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }
            finally
            {
                //lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = visibility;
                //btnAddEdit.Text = Title;
            }
        }
        public async Task<int> setInfo<U>(U ID,string ColumnName, bool visibility, string Title) where U : IConvertible
        {
            try
            {
                DataTable dt = await cmb.Rows("people", null, ColumnName, true, true, ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Person x = new Person(dt?.Rows[0]);
                    _person = x;
                }
                else
                    nullPerson();
                //_person.FromDataRow(dt?.Rows[0]);
                await Loadx();
                return _person.PersonID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
                return -1;
            }
            finally
            {
                lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = visibility;
                btnAddEdit.Text = Title;
            }
        }
        public void Clear()
        {

            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
                else if (control is ComboBox comboBox)
                    if(flag)
                    comboBox.SelectedIndex = 0;
            }
            txtID.Text = "???";
            //dateTimePicker1.Value= DateTime.Now;
            pictureBox12.Image = null;
        }
        private async Task Loadx()
        {

            if (_person.PersonID >0)
            {
                txtID.Text = (_person.PersonID).ToString();
                txtNational.Text = (_person.NationalNo);
                txtFirst.Text = (_person.FirstName);
                txtSeconed.Text = (_person.SecondName);
                txtThired.Text = (_person.ThirdName);
                txtLast.Text = (_person.LastName);
                dateTimePicker1.Value = _person.DateOfBirth;
                txtAddress.Text = (_person.Address);
                txtEmail.Text = (_person.Email);
                txtPhone.Text = (_person.Phone);
                if(!flag)
                    await setcmb();
                cmbNachonalty.SelectedIndex = (_person.NationalityCountryID);
                cmbGender.SelectedIndex = (_person.Gendor);
                if (!string.IsNullOrWhiteSpace(_person.ImagePath) && File.Exists(_person.ImagePath))
                    pictureBox12.ImageLocation = _person.ImagePath;
                if (!string.IsNullOrWhiteSpace(_person.ImagePath))
                {
                    lblAddEdit.Text = "Edite Image";
                    lblRemove.Visible = true; 
                }
            }
        }
        private bool handler<T>(T Sender,Action myfun) where T : Control
        {
            bool success ;
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
            bool operation = true;
                if (txtNational.Text != (_person.NationalNo))
                {
                    operation = handler(txtNational, () => _person.setNationalNo(txtNational.Text));
                    success = (operation && success) ? true : false;
                }

                if (txtFirst.Text != (_person.FirstName))
                {

                    operation = handler(txtFirst, () => _person.setFirestName(txtFirst.Text));
                    success = (operation && success) ? true : false;
                }

                if (txtSeconed.Text != (_person.SecondName))
                {
                    operation = handler(txtSeconed, () => _person.setSecondName(txtSeconed.Text));
                    success = (operation && success) ? true : false;
                }
                if (txtThired.Text != (_person.ThirdName))
                {
                    operation = handler(txtThired, () => _person.setThirdName(txtThired.Text));
                    success = (operation && success) ? true : false;
                }
                if (txtLast.Text != (_person.LastName))
                {
                    operation = handler(txtLast, () => _person.setLastName(txtLast.Text));
                    success = (operation && success) ? true : false;
                }
                if (txtAddress.Text != (_person.Address))
                {
                    operation = handler(txtAddress, () => _person.setAddress(txtAddress.Text));
                    success = (operation && success) ? true : false;
                }

                if (txtEmail.Text != (_person.Email))
                {
                    operation = handler(txtEmail, () => _person.setEmail(txtEmail.Text));
                    success = (operation && success) ? true : false;

                }
                if (txtPhone.Text != (_person.Phone))
                {
                    operation = handler(txtPhone, () => _person.setPhone(txtPhone.Text));
                    success = (operation && success) ? true : false;
                }
                if (dateTimePicker1.Value != _person.DateOfBirth)
                {
                    operation = handler(dateTimePicker1, () => _person.setDateOfBirth(dateTimePicker1.Value));
                    success = (operation && success) ? true : false;
                }
                if ((byte)cmbGender.SelectedIndex != _person.Gendor)
                {
                    operation = handler(cmbGender, () => _person.setGender((byte)cmbGender.SelectedIndex));
                    success = (operation && success) ? true : false;
                }
                if (cmbNachonalty.SelectedIndex != _person.NationalityCountryID)
                {
                    operation = handler(cmbNachonalty, () => _person.setNationalityCountryID(cmbNachonalty.SelectedIndex+1));
                    success = (operation && success) ? true : false;
                }
            if (newImage() &&!string.IsNullOrWhiteSpace(newSavedImagePath)&& newSavedImagePath != _person.ImagePath)
            {   
                _person.setImage(newSavedImagePath);
            }
          
                return success;
        }
        public string PrimaryIntColumnIDName()
        {
            if (_person == null)
                nullPerson();
            return _person.PrimaryIntColumnIDName;
        } 
        private async Task  setcmb()
        {
            cmbNachonalty.DataSource = await cmb.Rows("countries");
            cmbNachonalty.DisplayMember = "CountryName";
            cmbNachonalty.ValueMember = "CountryID";
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.SelectedIndex = 0;
            flag = true;
        }
        private async void PersonInfo_Load(object sender, EventArgs e)
        {
         
            pictureBox7.Image =   ((cmbGender.SelectedIndex == 0) ? Properties.Resources.masculine :Properties.Resources.femenine);
            lblAddEdit.Text = (pictureBox12.Image == null) ? "Add Image" : "Edit Image";
            if (lblAddEdit.Text == "Add Image")
                lblRemove.Visible = false;
            if (_person != null)
                await Loadx();
          
        }
        private void lblAddEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                    openFileDialog1.Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox12.ImageLocation = openFileDialog1 .FileName;
                lblRemove.Visible = true;
                lblAddEdit.Text = "Edit Image";
            }
                
        }
        private bool newImage()
        {
            string selectedImagePath = openFileDialog1.FileName;

            string storeFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MyApp",
                "ImageStore.txt"
            );

            string targetDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MyAppImages"
            );

            string oldImagePath = _person.ImagePath;

            newSavedImagePath = ImageManager.UpdateStoredImage(
             storeFilePath,
             oldImagePath,
             selectedImagePath,
             targetDirectory
         );
            if (newSavedImagePath != null)
            {
            pictureBox12.ImageLocation = newSavedImagePath;
                //MessageBox.Show("Image saved!");
                return true;
            }
            else
            {
                MessageBox.Show("Failed to save image.");
                return false;
            }
        }
        private void btnGetPath_Click(object sender, EventArgs e)=>MessageBox.Show($"Image path: {pictureBox12.ImageLocation}");
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)=> pictureBox7.Image =  ((cmbGender.SelectedIndex == 0) ? Properties.Resources.masculine :Properties.Resources.femenine);
        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox12.ImageLocation = null;
            lblAddEdit.Text = "Add Image";
            lblRemove.Visible=false;
        }
        public bool Result {  get; private set; }   
        public ISupportedType<Person> ManipulatingResult()
        { 
            if (Result)
            {
                Result = false;
                return _person ;
            }
            return null;
        }
        private async  void btnAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var hash = Person.map.Keys.ToHashSet();
                hash.Remove("PersonID");
                if (_person == null)
                    nullPerson();
                if (checks())
                {
                    errorProvider1.Clear();
                    Result =await manipulate.ProcessData("people", hash,  _person);
                    if (Result && btnAddEdit.Text == "Add")
                    {
                        txtID.Text =_person.PrimaryKey().ToString();
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
        //private string primary() { nullPerson();_person. }
        private void nullPerson()=>_person = new Person("0","a","b","c","d",DateTime.Now,0,"0","123456789","z@gmail.com",1,null);
        private void btnCancle_Click(object sender, EventArgs e) => this.FindForm()?.Close();//_frm.Close();   

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

#region tries
// Subscribe to MouseDown for all TextBoxes
/*public PersonInfo()
{
    InitializeComponent();
    this.textBox1.Enter += new EventHandler(TextBox_Enter);
    this.textBox2.Enter += new EventHandler(TextBox_Enter);
    this.textBox3.Enter += new EventHandler(TextBox_Enter);
    this.textBox4.Enter += new EventHandler(TextBox_Enter);
    this.textBox5.Enter += new EventHandler(TextBox_Enter);
    this.textBox6.Enter += new EventHandler(TextBox_Enter);
    this.textBox7.Enter += new EventHandler(TextBox_Enter);
    this.textBox8.Enter += new EventHandler(TextBox_Enter);
}

private void PersonInfo_Load(object sender, EventArgs e)
{

}
private void TextBox_Enter(object sender, EventArgs e)
{
    // Prevent the TextBox from gaining focus
    this.ReadOnly
    this.BackColor = SystemColors.Control;
    this.BorderStyle= BorderStyle.None;
    this.ActiveControl = null; // This will remove focus from the TextBox
}
*/
/*public PersonInfo()
{
    InitializeComponent();
    this.textBox1.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox2.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox3.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox4.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox5.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox6.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox7.MouseDown += new MouseEventHandler(TextBox_MouseDown);
    this.textBox8.MouseDown += new MouseEventHandler(TextBox_MouseDown);
}



private void TextBox_MouseDown(object sender, MouseEventArgs e)
{
    // Prevent the TextBox from gaining focus
     sender.ReadOnly = true;
    this.BackColor = SystemColors.Control;
    this.BorderStyle = BorderStyle.None;
    this.ActiveControl = null; // This will remove focus from the TextBox
}
*/
//public PersonInfo()
//{
//    InitializeComponent();

//}
// Any initialization code can go here
//private void TextBox_MouseDown(object sender, MouseEventArgs e)
// Prevent the TextBox from gaining focus
//textBox.ReadOnly = true; // Make it read-only
//textBox.BackColor = SystemColors.Control; // Match background
//textBox.BorderStyle = BorderStyle.None; // Remove border
//comboBox.TabStop = false;
//textBox.TabStop = false;
        /*public PersonInfo(Form frm, int ID, bool visibility, string Title)
        {
            InitializeComponent();
            Person x= new Person(cmb.Rows("people", null, "PersonID", true, true, ID).NewRow()) ;
            _person = x;
            lblAddEdit.Visible = lblRemove.Visible = btnAddEdit.Visible = btnCancle.Visible = visibility;
            btnAddEdit.Text = Title;
            _frm = frm;
        }*/
       /*public PersonInfo(Form frm,DataRow person, bool visibility,string Title)
        {
            InitializeComponent();
            nullPerson();
            if(person != null)
            _person.FromDataRow(person);
            lblAddEdit.Visible = lblRemove.Visible=btnAddEdit.Visible=btnCancle.Visible = visibility;
            btnAddEdit.Text = Title;
            _frm = frm;
        }
          public PersonInfo(DataRow Row, bool Edit,bool Show)
        {
            InitializeComponent();
            groupBox1.Enabled= lblAddEdit.Visible = lblRemove.Visible = Show;
            btnAddEdit.Visible = btnCancle.Visible = Edit;
            btnAddEdit.Text = (!Edit&&Show)?"Add":(Edit)?"Edit":"Show";
            if (Edit|| Show)
            {
                Person x = new Person(Row);
                _person = x;
                 Loadx();
            }
            else
            {
              
            }
        }*/
//textBox.Enter += TextBox_Enter;
//comboBox.Enter += TextBox_Enter
//private void TextBox_Enter(object sender, EventArgs e)
//{
//    if (sender is TextBox textBox)
//        this.ActiveControl = null; 
//}
//if (pictureBox12.Image!= null) 
//    lblAddEdit.Text = "Add Image";
//else
//lblAddEdit.Text=
//private void show(bool Details)
//{
//    //groupBox1.Enabled = Details;
//        //groupBox1.Enabled=ShowDetails;
//        foreach (Control control in groupBox1.Controls)
//        {
//            if (control is TextBox textBox)
//                textBox.Enabled = Details;
//            else if (control is ComboBox comboBox)
//                comboBox.Visible = Details;
//        }
//}
/*private Person Add_Edit()
{
    return (_person != null) ?
     new Person(
        int.Parse(txtID.Text),
        txtNational.Text,
        txtFirst.Text,
        txtSeconed.Text,
        txtThired.Text,
        txtLast.Text,
        dateTimePicker1.Value,
        (byte)cmbGender.SelectedIndex,
        txtAddress.Text,
        txtPhone.Text,
        txtEmail.Text,
        cmbNachonalty.SelectedIndex,
        pictureBox12.ImageLocation
        ) :
         new Person(
        txtNational.Text,
        txtFirst.Text,
        txtSeconed.Text,
        txtThired.Text,
        txtLast.Text,
        dateTimePicker1.Value,
        (byte)cmbGender.SelectedIndex,
        txtAddress.Text,
        txtPhone.Text,
        txtEmail.Text,
        cmbNachonalty.SelectedIndex,
        pictureBox12.ImageLocation
        );

}*/
#endregion