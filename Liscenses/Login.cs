using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MidLayer.B_CRUDOperation.B_Read;
using Microsoft.Win32;
using System.Diagnostics;
using MidLayer.Validation;
namespace Liscenses
{
    public partial class Login : Form
    {
        //string lastinfo;
        string UserName;
        string pass;
        public Login()
        {
            InitializeComponent();
            GetLastUserInfo();
        }
        private async void DataBaseChecks()
        {
            DataTable dt = await fetch.Rows("Users", null /*new HashSet<string> { "UserName", "Password" }*/, "UserName", true, true, textBox1.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt?.Rows[0]["Password"].ToString() ==UserValidtaor.ComputeHash( textBox2.Text))
                {
                    if (Convert.ToBoolean(dt?.Rows[0]["IsActive"]))
                    {
                        MessageBox.Show("Login Success");
                        Form form = new tabone(dt?.Rows[0]);
                        //this.Hide();
                        form.ShowDialog();
                    }
                    else
                        MessageBox.Show("The User Is Not Active");

                }
                else
                {
                    MessageBox.Show("Invalid Password");
                    textBox2.Focus();
                }
            }
            else
            {
                try
                {
                    //string ProgName = "Licenses";
                    //if (!EventLog.SourceExists(ProgName))
                    //{
                    //    EventLog.CreateEventSource(ProgName, "Application");
                    //}
                    //EventLog.WriteEntry(ProgName, textBox1.Text + " Does Not Exists", EventLogEntryType.Error);
                    //EventLog[] el = EventLog.GetEventLogs(".");
                    //if(el!=null)
                    //foreach (var ex in el)
                    //{
                    //    MessageBox.Show("Container : " + ex.Container + "EnableRaisingEvents :" + ex.EnableRaisingEvents
                    //        + "Log :" + ex.Log + ex.LogDisplayName + ex.MachineName + ex.Source + ex.Entries + ex.ToString());
                    //}
                }
                catch (Exception ex)
                {
                    {
                        MessageBox.Show("ERROR : " + ex.Message);
                    }
                }



                MessageBox.Show("Invalid UserName");
                textBox1.Focus();
            }
        }
        private bool txtChecks()
        {
            bool success = true;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Can not Be Empty Try Agin");
                success = false;
            }
            else
                errorProvider1.SetError(textBox1, "");

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Can not Be Empty Try Agin");
                success = false;
            }
            else
                errorProvider1.SetError(textBox2, "");
            return success;
        }
        private B_Fetch fetch = new B_Fetch();
        private void GetLastUserInfo()
        {
            try
            {
                string path = @"HKEY_CURRENT_USER\SOFTWARE\MyInfo";
                //RegistryKey key = Registry.CurrentUser.OpenSubKey(path);
                string name = "UserName";
                string password = "Password";
                //string Value = ;
                UserName = Convert.ToString(Registry.GetValue(path, name, null));
                if (UserName != null)
                    textBox1.Text = UserName;

                pass = Convert.ToString(Registry.GetValue(path, password, null));
                if (pass != null)
                    textBox2.Text = pass;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}");

            }
        }
        private void savedfileinfo()
        {
            try
            {
                string path = @"HKEY_CURRENT_USER\SOFTWARE\MyInfo";
                //RegistryKey key = Registry.CurrentUser.CreateSubKey(path);
                //key.SetValue("UserName", textBox1.Text);
                //key.SetValue("Password", textBox2.Text);

                //key.Close();
                Registry.SetValue(path, "UserName", textBox1.Text, RegistryValueKind.String);
                Registry.SetValue(path, "Password", textBox2.Text, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool success = true;// txtChecks();
            if (success)
            {
                if (rbRemember.Checked)
                    savedfileinfo();
                DataBaseChecks();
            }
        }


        private void Login_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pass) && !string.IsNullOrWhiteSpace(UserName))
            {
                textBox1.Text = UserName/*lastinfo.Substring(1, lastinfo.IndexOf(')')-1)*/;
                textBox2.Text = pass;//lastinfo.Substring(lastinfo.IndexOf(')')+1).TrimEnd('.',' ');
            }
        }

    }
}
//private void savedfileinfo()
//{
//    string myfile = @" @C:\Remeber\";
//       if(! File.Exists(myfile))
//        File.Create(myfile);
//       string writetofile= "("+textBox1 .Text+")"+textBox2.Text+".";
//    File.WriteAllText(myfile, writetofile);
//}

/*private string GetLastUserInfo()
{
    try
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string directoryPath = Path.Combine(appDataPath, "Remember");
        string filePath = Path.Combine(directoryPath, "UserInfo.txt");

        if (!File.Exists(filePath))
        {
            //MessageBox.Show("User info file does not exist.");
            return null;
        }

        var lines = File.ReadLines(filePath);
        string lastLine = lines.LastOrDefault(line => !string.IsNullOrWhiteSpace(line));

        if (string.IsNullOrWhiteSpace(lastLine))
        {
            //MessageBox.Show("File is empty or contains only blank lines.");
            return null;
        }

        return lastLine;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
        return null;
    }
}*/
/*   private void savedfileinfo()
   {
       try
       {

           string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
           string directoryPath = Path.Combine(appDataPath, "Remember");
           string filePath = Path.Combine(directoryPath, "UserInfo.txt");

           if (!Directory.Exists(directoryPath))
           {
               Directory.CreateDirectory(directoryPath);
           }
           if (!File.Exists(filePath))
           {
               File.Create(filePath);
           }

           string writetofile = $"({textBox1.Text}){textBox2.Text}.\n";
           bool seccuss = true;
           var lines = File.ReadLines(filePath);
           foreach (var line in lines)

               if (line == writetofile)
               {
                   seccuss = false;
                   break;
               }
           if (seccuss)
               File.AppendAllText(filePath, writetofile);

       }
       catch (Exception ex)
       {
           MessageBox.Show($"An error occurred while saving the file: {ex.Message}");
       }
   }*/
//private void rbRemember_CheckedChanged(object sender, EventArgs e)
//{
//    if(rbRemember.Checked)
//        savedfileinfo();
//}

/* private void Login_Load(object sender, EventArgs e)
 {
     this.ActiveControl = textBox1;
     errorProvider1.SetError(textBox1, "Enter Your UserName");
     errorProvider1.SetError(textBox2, "Enter Your Password");
 }
 private void textBox1_TextChanged(object sender, EventArgs e)
 {
     errorProvider1.SetError(textBox1, "");
 }*/
/*private void textBox2_TextChanged(object sender, EventArgs e)
{
    errorProvider1.SetError(textBox2, "");
}
private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
{
    if (e.KeyChar == (char)Keys.Enter)
    {
        textBox2.Focus();
    }
}
private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
{
    if (e.KeyChar == (char)Keys.Enter)
    {
        button1_Click(sender, e);
    }
}
private void textBox1_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(textBox1.Text))
    {
        errorProvider1.SetError(textBox1, "Enter Your UserName");
    }
    else
    {
        errorProvider1.SetError(textBox1, "");
    }
}
private void textBox2_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(textBox2.Text))
    {
        errorProvider1.SetError(textBox2, "Enter Your Password");
    }
    else
    {
        errorProvider1.SetError(textBox2, "");
    }
}
private void button1_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(textBox1.Text))
    {
        errorProvider1.SetError(textBox1, "Enter Your UserName");
    }
    else
    {
        errorProvider1.SetError(textBox1, "");
    }
    if (string.IsNullOrEmpty(textBox2.Text))
    {
        errorProvider1.SetError(textBox2, "Enter Your Password");
    }
    else
    {
        errorProvider1.SetError(textBox2, "");
    }
}
private void button1_MouseEnter(object sender, EventArgs e)
{
    button1.BackColor = Color.LightBlue;
}
private void button1_MouseLeave(object sender, EventArgs e)
{
    button1.BackColor = Color.LightGray;
}
private void button1_MouseHover(object sender, EventArgs e)
{
    button1.BackColor = Color.LightBlue;
}
private void button1_MouseMove(object sender, MouseEventArgs e)
{
    button1.BackColor = Color.LightBlue;
}
private void button1_MouseClick(object sender, MouseEventArgs e)
{
    button1.BackColor = Color.LightGray;
}
private void button1_MouseDown(object sender, MouseEventArgs e)
{
    button1.BackColor = Color.LightGray;
}
private void button1_MouseUp(object sender, MouseEventArgs e)
{
    button1.BackColor = Color.LightBlue;
}
private void button1_MouseCaptureChanged(object sender, EventArgs e)
{
    button1.BackColor = Color.LightGray;
}
private void button1_MouseLeave_1(object sender, EventArgs e)
{
    button1.BackColor = Color.LightGray;
}
private void button1_MouseEnter_1(object sender, EventArgs e)
{
    button1.BackColor = Color.LightBlue;
}
private void button1_MouseHover_1(object sender, EventArgs e)
{
    button1.BackColor = Color.LightBlue;
}*/