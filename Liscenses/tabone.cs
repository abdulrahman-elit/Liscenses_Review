using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using EntityLayer.Classes;G
using Liscenses.Classes;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
using MidLayer.B_CRUDOperation;
using MidLayer.B_CRUDOperation.B_Read;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Liscenses
{
    public partial class tabone : Form
    {

        public tabone(DataRow Row)
        {
            InitializeComponent();
            if (Row != null)
                rowuser = Row;
            else
                throw new Exception("Invalid Row");
        }
        private Handling x = new Handling();
        private DataRow LogRow,rowuser;
        private DataTable dt,dty,dtz,dtw;
        private DataView dv;
       
        ShowedTable ShowedTable = new ShowedTable();
        SetColumnValue setColumnValue = new SetColumnValue();
        private static B_Fetch cmb = new B_Fetch();
        private static readonly HashSet<string> cmbPeopleFilter = new HashSet<string>() { "ImagePath", "NationalityCountryID" };
        private static readonly HashSet<string> cmbUsersFilter = new HashSet<string>() { "Password" };
        private static readonly HashSet<string> columnUsersFilter = new HashSet<string>() { "UserID", "PersonID", "UserName", "IsActive" };

        private System.Windows.Forms.Control Custom;
        ICustmControl<object> control;
        private Form form = new Form();
        private async void toolStripButton2_Click(object sender, EventArgs e) =>await tabone_Loadx("People",Properties.Resources.person_man, cmbPeopleFilter);
        private async void toolStripButton3_Click(object sender, EventArgs e) => await tabone_Loadx("Users",Properties.Resources.admin__1_, cmbUsersFilter, columnUsersFilter);
        private async void toolStripButton1_Click(object sender, EventArgs e) {await tabone_Loadx("Drivers",Properties.Resources.pilot); dataGridView1.ContextMenuStrip = null; btnAdd.Visible = false; }
        private async void toolStripMenuItem1_Click(object sender, EventArgs e) =>(form,Custom,control)=await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
        //x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
        private async void btnAdd_Click(object sender, EventArgs e) { dataGridView1.ClearSelection();(Custom,control,dt)=await AddEditOperation.operation(form,  dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false); }
        private async void toolStripMenuItem2_Click(object sender, EventArgs e) =>(Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
        private void textBox1_TextChanged(object sender, EventArgs e) => TextHandling.Changed(ref comboBox1, ref textBox1, ref dv, ref dataGridView1);
        private async void street_Click(object sender, EventArgs e) => (form,Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0],   LogRow, "show", "Test", true, false);
        //x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "show", "Test", true, false);

        private async void writen_Click(object sender, EventArgs e) => (form,Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Test", true, false);
        //x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "show", "Test", true, false);
        private async void showToolStripMenuItem_Click(object sender, EventArgs e) => (form,Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Local License", true, false);
        private async void replaceDamgedToolStripMenuItem_Click(object sender, EventArgs e) { string x = dataGridView1.SelectedRows[0].Cells["Status"].ToString();(Custom,control,dt)=await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", "Replace_Damged", true, true); handelISActive(x); }
        private void handelISActive(string x) { if (dataGridView1.SelectedRows[0].Cells["Status"].ToString() != x && dataGridView1.SelectedRows[0].Cells["Status"].ToString() != "Detaind") dataGridView1.SelectedRows[0].Cells["IsActive"].Value = false; }
        private async void relaseToolStripMenuItem_Click(object sender, EventArgs e) {(Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false); dataGridView1.SelectedRows[0].Cells["IsReleased"].Value = true; }
        private async void showToolStripMenuItem1_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e) =>(dt)=await DeleteRow.row( dataGridView1, lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')));
        private async void showInfoToolStripMenuItem_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
        private async void updateToolStripMenuItem_Click(object sender, EventArgs e) =>(Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
        private async void editToolStripMenuItem_Click(object sender, EventArgs e) => (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
        private void button4_Click(object sender, EventArgs e) => this.Close();
        private async void button1_Click(object sender, EventArgs e) { lblmanage.Text = "Users ";(form, Custom, control) =await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, true); }
        private async void button5_Click(object sender, EventArgs e) { lblmanage.Text = "Users ";(Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1,dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, true); }
        private void myPersonalInfoToolStripMenuItem_Click(object sender, EventArgs e) { toolStripButton2_Click(null, null); comboBox1.SelectedIndex = 1; textBox1.Text = LogRow["personID"].ToString(); }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e) { toolStripButton3_Click(null, null); comboBox1.SelectedIndex = 1; textBox1.Text = LogRow["UserID"].ToString(); }

        private void toolStripButton6_Click(object sender, EventArgs e) => this.FindForm().Close();

        private async void tabone_Load(object sender, EventArgs e) {
            await tabone_Loadx("People",Properties.Resources.person_man, cmbPeopleFilter);
            DataTable _tab = await cmb.Rows("Users", null, "UserID", true, true, Convert.ToInt32(rowuser["UserID"]));
            LogRow=_tab?.Rows[0]; dtw =await cmb.Rows("People");  dty =await cmb.Rows("Applications"); dtz =await cmb.Rows("LicenseClasses"); 
            comboBox2.Items.Add("All"); comboBox2.Items.Add("Yes"); comboBox2.Items.Add("No"); comboBox2.SelectedIndex = 0; comboBox2.Visible = false;
        }
        private async  void cancleToolStripMenuItem_Click(object sender, EventArgs e) => await ChecksOperation.Check( dataGridView1, "Cancel",
            async (ID, ApplicationID) =>
        {

            if (await CancelFun.Fun("Applications", "ApplicationID", ApplicationID))
            {
                DataRow dr = dt.Rows.Find(ID);
                dr["PassedTests"] = 0;
                dr["Status"] = "Cancel";
                dataGridView1.Refresh();
            }

        });


        private async void myPersonalInfoToolStripMenuItem_Click_1(object sender, EventArgs e) { await tabone_Loadx("ApplicationTypes", Properties.Resources.task_types); dataGridView1.ContextMenuStrip = contextMenuStrip5; btnAdd.Visible = false; }

        private async void userInfoToolStripMenuItem_Click_1(object sender, EventArgs e) {await tabone_Loadx("TestTypes", Properties.Resources.task_types); dataGridView1.ContextMenuStrip = contextMenuStrip5; btnAdd.Visible = false; }
        private async void deleteToolStripMenuItem1_Click(object sender, EventArgs e) =>await ChecksOperation.Check( dataGridView1, "Delete",async (ID, ApplicationID) =>
            {
                DataTable _tab=await cmb.Rows("Applications", null, "ApplicationID", true, true, ApplicationID);
                DataRow row =_tab?.Rows[0];
                if (row != null && Convert.ToInt32(row["ApplicationStatus"]) != 3)
                {

                    if (await handelapplicaionDeleation(ID) &&await DeleteFun.fun("LocalDrivingLicenseApplications", "LocalDrivingLicenseApplicationID", ID) &&await DeleteFun.fun("Applications", "ApplicationID", ApplicationID))
                    {
                        dt.Rows.Remove(dt.Rows.Find(ID));
                        dataGridView1.Refresh();
                    }
                }
                else
                    MessageBox.Show("You Can Not Delete Completed Applicaion");
            });
        private async void localLicense_Click(object sender, EventArgs e) {await handelall("Licenses",Properties.Resources.location__1_, contextMenuStrip3, ShowedTable.LocalLicense, setColumnValue.LocalLicense); dv.RowFilter = "IsActive = true"; }
        private async void gloablToolStripMenuItem_Click(object sender, EventArgs e) =>await handelall("InternationalLicenses",Properties.Resources.world__2_, null, ShowedTable.InternationalLicense, setColumnValue.InternationalLicense);

        private async void localToolStripMenuItem_Click(object sender, EventArgs e) { await handelall("LocalDrivingLicenseApplications" ,Properties.Resources.allow_list2, contextMenuStrip2, ShowedTable.LocalDrivingLicenseApplications, setColumnValue.LocalDrivingLicenseApplications); btnAdd.Visible = true; }
        private async void visonToolStripMenuItem_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Test", true, false);

        private void tests_MouseHover(object sender, EventArgs e) => handelmnue();

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) { if (dataGridView1.ContextMenuStrip != contextMenuStrip1) handelmnue(); }

        private async void issuingALicense_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0] , LogRow, "Show", "License", true, false);

        private void visblecolumn(string columnName) => dataGridView1.Columns[columnName].Visible = false;
        private async void toolStripButton5_Click(object sender, EventArgs e) { await tabone_Loadx("DetainedLicenses",Properties.Resources.id__2_); dataGridView1.ContextMenuStrip = contextMenuStrip4; btnAdd.Visible = false; }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { handleIsActiveCombobox(); ShowActiveOnly(); }
        private void handleIsActiveCombobox()
        {
            textBox1.Visible = (comboBox1.SelectedIndex != 0 && comboBox1.Text.ToLower() != "isactive");
            if (textBox1.Visible)
                textBox1.Focus();
            if (comboBox1.Text.ToLower() == "isactive")
            {
                comboBox2.Visible = true;
            }

        }
        private void ShowActiveOnly()
        {
            if (lblmanage.Text == "Licenses Managements") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsActive = true"; else dv.RowFilter = null; 
            if (lblmanage.Text == "DetainedLicenses Management") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsReleased = false"; else dv.RowFilter = null;
            if (lblmanage.Text == "InternationalLicenses Managements") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsActive = true"; else dv.RowFilter = null;
        }
    
        private async Task tabone_Loadx(string TableName, Image picturelocation, HashSet<string> cmbFilter = null, HashSet<string> columnFilter = null)
        {
            btnAdd.Visible = true;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            (dt,dv)=await Loading.Load(dataGridView1, TableName, columnFilter);
            //if (cmbFilter != null)
            lblmanage.Text = $"{TableName} Management";
                comboboxHandleing.comboboxHandle(comboBox1, dataGridView1, cmbFilter);
            if (picturelocation != null)
                pictureBox1.Image = picturelocation;


        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("Gendor"))
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Gendor")
                {
                    if (e.Value != null)
                    {
                        int genderValue = Convert.ToInt32(e.Value);
                        e.Value = genderValue == 0 ? "Male" : "Female";
                        e.FormattingApplied = true;
                    }
                }
            }
            else
                return;
        }


        private async Task<bool> handelapplicaionDeleation(int ID)
        {
            bool seccuss = false;
            DataTable tableTestAppointment =await cmb.Rows("TestAppointments", null, "LocalDrivingLicenseApplicationID", true, false, ID);
            if (tableTestAppointment != null)
            {
                foreach (DataRow rx in tableTestAppointment.Rows)
                {
                    seccuss =await DeleteFun.fun("Tests", "TestAppointmentID", Convert.ToInt32(rx["TestAppointmentID"]));
                    if (!seccuss)
                        break;
                }
                seccuss = await DeleteFun.fun("TestAppointments", "LocalDrivingLicenseApplicationID", ID);
            }
            else
                seccuss = true;
            return seccuss;
        }
        

        private async Task handelall(string tablename, Image imagepath, ContextMenuStrip menu, Func<DataTable> table,Action<List<DataTable>,int> fun)
        {
            await tabone_Loadx(tablename,imagepath);
            btnAdd.Visible = false;
            dataGridView1.ContextMenuStrip = menu;
            DataTable dtx = table();
            handelforloopdatatable(fun, dtx);
            handeldatatable(dtx, $"{tablename} Managements");
        }
       private void handeldatatable(DataTable dtx,string Message)
        {
            dt = dtx;
            dv = dt.DefaultView;
            dataGridView1.DataSource = dv;
            comboboxHandleing.comboboxHandle(comboBox1, dataGridView1, null);
            lblmanage.Text = Message;
        }
       
        private void handelforloopdatatable(Action<List<DataTable>,int> fun,DataTable dtx)
        {
            if(dt==null && dt.Rows.Count == 0)
            {
                MessageBox.Show("No Data Found");
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                fun(new List<DataTable> { dt, dtw, dty, dtz, dtx }, i);
        }

        private void handeltruefalsevalue(bool test, bool vison, bool street, bool writen)
        {
            ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["vison"].Enabled = vison;
            ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["street"].Enabled = street;
            ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["writen"].Enabled = writen;
            contextMenuStrip2.Items["tests"].Enabled = test;
        }
        private void handeloperationcontixtmnuestrip(bool seccuss)
        {
                    contextMenuStrip2.Items["delete"].Enabled = seccuss;
            contextMenuStrip2.Items["cancel"].Enabled = seccuss;
            contextMenuStrip2.Items["edit"].Enabled = seccuss;

        }
        private async void handelmnuestrip2()
        {
            DataRow rowz = null;
            int passed = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value);
            string state = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
               
             DataTable _tab=   await cmb.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", true, true, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LDLAppID"].Value));
            DataRow row = _tab?.Rows[0];

            if (row != null)
            {
                _tab = await cmb.Rows("Licenses", null, "ApplicationID", true, true, Convert.ToInt32(row["ApplicationID"]));
                rowz = _tab?.Rows[0];
            }
            bool seccuss = false;
            if (rowz == null)
            {
                if (state == "Completed")
                    seccuss = true;

                else
                    seccuss = false;
            }
            handeloperationcontixtmnuestrip(seccuss);

            contextMenuStrip2.Items["IssuingAlicense"].Enabled = seccuss;

            if ("Cancel" == state)
                contextMenuStrip2.Items["tests"].Enabled = contextMenuStrip2.Items["issuingALicense"].Enabled = false;
               
            else
            {
                switch (passed)
                {
                    case 0:
                        handeltruefalsevalue(true, true, false, false);
                        break;
                    case 1:
                        handeltruefalsevalue(true, false, true, false);
                        break;
                    case 2:
                        handeltruefalsevalue(true, false, false, true);
                        break;
                    case 3:
                        contextMenuStrip2.Items["tests"].Enabled = false;
                        break;
                    default:
                        contextMenuStrip2.Items["tests"].Enabled = false;
                        break;
                }
            }
        }
        private void handelmnuestrip3()
        {
            string state = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            bool IsActive = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["IsActive"].Value);
            if (state == "Detaind"|| !IsActive)
                contextMenuStrip3.Items["Services"].Enabled=false;
            else
                contextMenuStrip3.Items["Services"].Enabled = true;
        }
        private void handelmnuestrip4()
        {
            if (Convert.ToBoolean( dataGridView1.SelectedRows[0].Cells["IsReleased"].Value))
                contextMenuStrip4.Items["Release"].Enabled = false;
            else
                contextMenuStrip4.Items["Release"].Enabled = true;
        }
        private void handelmnue()
        {

            if ( dataGridView1 != null && dataGridView1.SelectedRows.Count > 0)
            {
                if (lblmanage.Text == "Licenses Managements")
                    handelmnuestrip3();
                else if (lblmanage.Text.Contains("Local"))
                    handelmnuestrip2();
                else if(lblmanage.Text.Contains("DetainedLicense"))
                    handelmnuestrip4();
              
            }
            else
                return;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dv == null )
                return;
            if (comboBox2.SelectedIndex == 0)
                dv.RowFilter = null;
            else if (comboBox2.SelectedIndex == 1)
                dv.RowFilter = (($@"{comboBox1.SelectedItem.ToString()} = true "));
            else
                dv.RowFilter = (($@"{comboBox1.SelectedItem.ToString()} = false "));
            dataGridView1.Refresh();
        }
    }
}
#region tries
//private DataTable GetDataTableFromDataSource()
//{
//    if (dataGridView1.DataSource is DataTable dataTable)
//    {
//        return dataTable;
//    }
//    else if (dataGridView1.DataSource is DataView dataView)
//    {
//        return dataView.Table;
//    }
//    else
//    {
//        throw new InvalidOperationException("DataGridView DataSource is not a DataTable or DataView.");
//    }
//}
/*private void ViewFilter(HashSet<string> columnFilter)
     {
         if (columnFilter != null)
             foreach (var column in columnFilter)
                 //dataGridView1.Columns[column].Visible = false;
                 dv.RowFilter = $"[{column}] = 0";
         dataGridView1.Refresh();
     }*/
//dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
//if (lblmanage.Text.Contains("People"))
//else if (lblmanage.Text.Contains("Users"))
//AddEditOperation("Add", true, false);
//else
//return;
//private static readonly HashSet<string> ShowedcolumnUsers = new HashSet<string>() { "Password" };
//private static readonly HashSet<string> DriversFilter = new HashSet<string>() { "ImagePath", "PersonID", "NationalityCountryID" };
//if(columnFilter != null)
//    //columnFilter.AsParallel().ForAll(column => dataGridView1.Columns[column].Visible = false);
//    foreach (var column in columnFilter)
//        dataGridView1.Columns[column].Visible = false;
//var filterSet = new HashSet<string>(cmbFilter, StringComparer.OrdinalIgnoreCase);
/* private T GetT<T>(int id) where T : ISupportedType<T>, new()
 {
     DataTable dataTable = GetDataTableFromDataSource();
     DataRow[] rows = dataTable.Select($"PersonID = {id}");
     if (rows.Length > 0)
     {
         T t = new T();
         foreach (DataColumn column in dataTable.Columns)
         {
             var propertyInfo = typeof(T).GetProperty(column.ColumnName);
             if (propertyInfo != null)
                 propertyInfo.SetValue(t, rows[0][column.ColumnName]);
         }
         return t;
     }
     return default;
 }*/
//foreach()
//else if(lblmanage.Text.Contains("Users"))
//User p = personInfo.ManipulatingResult();
/* private void EditDataGridViewRow<T>(int id, T newValue)where T : ISupportedType<T>
 {
     // Find the row in the DataTable
     if(newValue is ISupportedType<T> s)
     DataRow[] rows = ((DataTable)dataGridView1.DataSource).Select($"{s.PrimaryIntColumnIDName} = {newValue.GetProperty(s.PrimaryIntColumnIDName).GetValue(newValue)}");
     if (rows.Length > 0)
     {
         rows[0][$"{s.PrimaryIntColumnIDName}"] = newValue; // Edit the value
     }
 }*/
/*
 * private void EditDataGridViewRow<T>(int id, T newValue) where T : ISupportedType<T>
{
    // Validate the data source
    if (dataGridView1.DataSource is not DataTable dataTable)
    {
        throw new InvalidOperationException("DataGridView is not bound to a DataTable.");
    }

    // Get the primary column name from the interface
    var s = (ISupportedType<T>)newValue;
    string primaryColumn = s.PrimaryIntColumnIDName;

    // Get the property info for the primary column
    var propertyInfo = typeof(T).GetProperty(primaryColumn);
    if (propertyInfo == null)
    {
        throw new ArgumentException($"Property '{primaryColumn}' does not exist on type '{typeof(T).Name}'.");
    }

    // Get the primary key value from newValue
    object primaryKeyValue = propertyInfo.GetValue(newValue);

    // Build the filter expression safely (handle strings vs. numbers)
    string filter;
    if (primaryKeyValue is string stringValue)
    {
        filter = $"{primaryColumn} = '{stringValue}'"; // Enclose strings in quotes
    }
    else
    {
        filter = $"{primaryColumn} = {primaryKeyValue}"; // No quotes for numbers
    }

    // Find the row in the DataTable
    DataRow[] rows = dataTable.Select(filter);
    if (rows.Length > 0)
    {
        // Edit the row with the new value
        rows[0][primaryColumn] = primaryKeyValue;
    }
    else
    {
        throw new InvalidOperationException($"No row found with {primaryColumn} = {primaryKeyValue}.");
    }
}*/
/*private void EditDataGridViewRow<T>(int id, T newValue) where T : ISupportedType<T>
{
    // Ensure the DataGridView is bound to a DataTable
    if (dataGridView1.DataSource is not DataTable dataTable)
    {
        throw new InvalidOperationException("DataGridView DataSource is not a DataTable.");
    }

    // Get the supported type instance
    ISupportedType<T> supportedType = newValue;

    // Validate the primary key column name
    string primaryColumn = supportedType.PrimaryIntColumnIDName;

    // Get the property info for the primary key column
    var propertyInfo = typeof(T).GetProperty(primaryColumn);
    if (propertyInfo == null)
    {
        throw new InvalidOperationException($"Property '{primaryColumn}' not found on type '{typeof(T).Name}'.");
    }

    // Get the value of the primary key column from newValue
    object primaryKeyValue = propertyInfo.GetValue(newValue);

    // Build the filter expression safely
    string filter = $"{primaryColumn} = {FormatValueForFilter(primaryKeyValue)}";

    // Find the row in the DataTable
    DataRow[] rows = dataTable.Select(filter);
    if (rows.Length > 0)
    {
        // Edit the row with the new value
        rows[0][primaryColumn] = primaryKeyValue; // Edit the primary key column
    }
}

// Helper method to format values for the filter expression
private string FormatValueForFilter(object value)
{
    return value switch
    {
        string str => $"'{str.Replace("'", "''")}'", // Escape single quotes for strings
        DateTime dtr => $"'{dtr:yyyy-MM-dd HH:mm:ss}'", // Format dates
        _ => value.ToString() // Default for other types
    };
}*/
//private ICustmControl<T> Get<T>() where T : ISupportedType<T>
/*private void AddEditOperation<T>(string title ,bool enable,bool Edit,T back) where T : ICustmControl<T> 
{
    try
    {
        DataRow rowx;
        handler(title, enable);
        var xx = back.ManipulatingResult();
        if (xx is ISupportedType<T> t)
        {
            //if(xx is ISupportedType)
            if (xx == null)
                MessageBox.Show("No Data Editd");
            else
                t = (ISupportedType<T>)xx;
            if (Edit)
            {
                dtr.Rows.Find(dataGridView1?.Rows[0].Cells[0].Value);
                rowx = t.ToDataRow(dtr, false);
                rowx.AcceptChanges();
            }
            else
                dtr.Rows.Add(t.ToDataRow(dtr, true));
        }

        //if (xx != null)
        //ISupportedType<T> s = Value;
        //if (Value != null && Edit)
        //    dtr.Rows.Add(s);
        //else if (Value != null && !Edit)
        //   rowx=  dtr.Rows.Find(s.PrimaryKey());
        //if (xx != null && Edit)
        //{
        //    rowx=xx.ToDataRow(dtr); 
        //    rowx.AcceptChanges();
        //}
        //else if (xx != null && !Edit)

        //    dtr.Rows.Add(xx);
        //if (xx != null&&Edit)
        //    //myfun(xx);
        //    EditDataGridViewRow(xx);
        //else if(xx!=null &&!Edit)
        //dataGridView1.
        dataGridView1.Refresh();

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}*/

/*
if (lblmanage.Text.Contains("People"))
{
    form.Text = $"{Title} Person";
    //personInfo = new PersonInfo();//(form, Value,/*visable*//*Enable, Title);
    form.Size = personInfo.Size + new Size(12, 38);
    personInfo.Enabled = true;
    personInfo.setInfo(Value,false);
    form.Controls.Add(personInfo);
}
else
{ 
    form.Text = $"{Title} User";
     userInfo = new UserInfo();
    form.Size = userInfo.Size + new Size(12, 38);
    userInfo.Enabled = true;
    form.Controls.Add(userInfo);

}*/
/*    private void EditDataGridViewRow<T>(T newValue) where T : ISupportedType<T>
    {
        DataTable dataTable = GetDataTableFromDataSource();

        ISupportedType<T> supportedType = newValue;

        var primaryKeyProperty = typeof(T).GetProperty(supportedType.PrimaryIntColumnIDName);
        if (primaryKeyProperty == null)
            throw new InvalidOperationException($"Property '{supportedType.PrimaryIntColumnIDName}' not found on type '{typeof(T).Name}'.");

        var primaryKeyValue = primaryKeyProperty.GetValue(newValue);

        DataRow[] rows = dataTable.Select($"{supportedType.PrimaryIntColumnIDName} = {primaryKeyValue}");
        if (rows.Length > 0)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName == supportedType.PrimaryIntColumnIDName)
                    continue;
                var propertyInfo = typeof(T).GetProperty(column.ColumnName);
                if (propertyInfo != null)
                    rows[0][column.ColumnName] = propertyInfo.GetValue(newValue);
            }
        }
    }*/
/* private string FormatFilterValue(object value)
{
return value switch
{
 string str => $"'{str.Replace("'", "''")}'", 
 int or double or decimal => value.ToString(), 
 _ => throw new ArgumentException($"Unsupported filter value type: {value.GetType().Name}")
};
}*/
/* private T GetT<T>() //where T : ISupportedType<T>
{

    if (lblmanage.Text.Contains("People"))
    {
        //personInfo.ManipulatingResult();
        //return (T)Convert.ChangeType(p, typeof(T));
            ICustmControl<Person> p = (ICustmControl < Person > )(personInfo);
        return p;
    }
    else if (lblmanage.Text.Contains("Users"))
    {
        ICustmControl<User> u = (ICustmControl<User>)(userInfo);
        return u;
        //    User p = UserInfo.ManipulatingResult();
        //    return (T)Convert.ChangeType(p, typeof(T));
    }
    //else if (lblmanage.Text.Contains("Drivers"))
    //{
    //    Driver p = DriverInfo.ManipulatingResult();
    //    return (T)Convert.ChangeType(p, typeof(T));
    //}
    else
        return default;
}*/
//private void comboboxHandle(HashSet<string> cmbFilter)
//{
//    comboBox1.Items.Clear();
//    comboBox1.Items.Add("None");
//    foreach (DataGridViewColumn column in dataGridView1.Columns)
//    {
//        if (cmbFilter == null || !cmbFilter.Contains(column.Name))
//            comboBox1.Items.Add(column.Name);
//    }
//    comboBox1.SelectedIndex = 0;
//}
/*private Control GetControl()
      {
          if (lblmanage.Text.Contains("People"))
              return new PersonInfo();

          else if (lblmanage.Text.Contains("Users"))
              return new UserInfo();
          else
              return null;
      }*/
/*private void handler(string Title,bool Enable,bool log)
    {
        DataRow dataRow = LogRow;
        if (!log && dataGridView1.SelectedRows.Count <= 0 && tabControl1.SelectedTab == Managment)
        {
            MessageBox.Show("No row selected!");
            return;
        }
        else if (!log && dataGridView1.SelectedRows.Count > 0)
        {
            if (dataGridView1.SelectedRows[0].DataBoundItem is DataRowView rowView)
                dataRow = rowView.Row;
            else
                MessageBox.Show("Row is not data-bound!");
        }
            if (Title == "Add")
                dataRow = dtr.NewRow();
            try
            {
                control = GetCustomizedControl();
                control?.setInfo( dataRow, Enable);
                Custom = control as Control;
                show(Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    }*/
/*  private ICustmControl<object> GetCustomizedControl()
    {
        if (lblmanage.Text.Contains("People"))
            return new PersonInfo();

        else if (lblmanage.Text.Contains("Users"))
            return new UserInfo();
        else if (lblmanage.Text.Contains("Applications"))
            return new LocalLicenses(Convert.ToInt32(LogRow["UserID"] ?? -1), LogRow["UserName"].ToString(), false);
        else
            return null;
    }

    private void handler(string Title, bool Enable, bool log)
    {
        int ID = 0;
        if (log)
            ID = Convert.ToInt32(LogRow["UserID"]);
        else if (dataGridView1.SelectedRows.Count > 0)
            ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
        try
        {
            control = GetCustomizedControl();
            //(T ID, string ColumnName, bool visibility, string Title)
            control?.setInfo(ID, Enable, Title);
            Custom = control as Control;
            Showing.show(form, Custom, Title, lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }*/
//private void AddEditOperation(string title, bool Add, bool log)
//{
//    handler(title, false, log);
//    //var control = GetCustomizedControl();
//    if (control == null)
//    {
//        MessageBox.Show("Invalid control type.");
//        return;
//    }

//    try
//    {
//        var result = control.ManipulatingResult();

//        if (result == null)
//        {
//            MessageBox.Show("No Data Edited");
//            return;
//        }

//        DataRow row;

//        if (Add)
//        {
//            row = result.ToDataRow(dtr, Add);
//            dtr.Rows.Add(row);
//        }
//        else
//        {
//            if (!log)
//                row = result.ToDataRow(dtr, Add);
//            else if (result is User user)
//                row = user.ToDataRow(LogRow);
//            else
//                row = null;

//            if (row != null)
//                row.AcceptChanges();
//            //= dtr.Rows.Find(dataGridView1?.Rows[0].Cells[0].Value);
//        }
//        if (!log)
//            dataGridView1.Refresh();
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }
//}
/*private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
{
    if(int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int id))
    {
        if (MessageBox.Show($"Are Yue Shure To Delete {id}", "Management", MessageBoxButtons.OKCancel) == DialogResult.OK)
        {
            try
            {

                B_Delete.Row(lblmanage.Text.Substring(0,lblmanage.Text.IndexOf(' ')), GetCustomizedControl().PrimaryIntColumnIDName(), id);
                dtr.Rows.Remove(dtr.Rows.Find(id));
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } 
}
=============================================================================================================================================================================ط
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
//using EntityLayer.Classes;G
using Liscenses.Classes;
//using MiddleLayer.B_CRUDOperation.B_Read;
//using MiddleLayer.Process;
using MidLayer.B_CRUDOperation;
using MidLayer.B_CRUDOperation.B_Read;

namespace Liscenses
{
    public partial class tabone : Form
    {

        public tabone(DataRow Row)
        {
            InitializeComponent();
            if (Row != null)
                rowuser = Row;
            else
                throw new Exception("Invalid Row");
        }
        private Handling x = new Handling();
        private DataRow LogRow,rowuser;
        private DataTable dt,dty,dtz,dtw;
        private DataView dv;
       
        ShowedTable ShowedTable = new ShowedTable();
        SetColumnValue setColumnValue = new SetColumnValue();
        private static B_Fetch cmb = new B_Fetch();
        private static readonly HashSet<string> cmbPeopleFilter = new HashSet<string>() { "ImagePath", "NationalityCountryID" };
        private static readonly HashSet<string> cmbUsersFilter = new HashSet<string>() { "Password" };
        private static readonly HashSet<string> columnUsersFilter = new HashSet<string>() { "UserID", "PersonID", "UserName", "IsActive" };

        private Control Custom;
        ICustmControl<object> control;
        private Form form = new Form();
        private async void toolStripButton2_Click(object sender, EventArgs e) =>await tabone_Loadx("People",null/* @"C:\Users\dbhdh\OneDrive\Pictures\pic\person_man.png", cmbPeopleFilter);
/*private async void toolStripButton3_Click(object sender, EventArgs e) => await tabone_Loadx("Users", null/* @"C:\Users\dbhdh\Downloads\admin (1).png", cmbUsersFilter, columnUsersFilter);
private async void toolStripButton1_Click(object sender, EventArgs e) { await tabone_Loadx("Drivers", null/* @"C:\Users\dbhdh\OneDrive\Pictures\pic\pilot.png"); dataGridView1.ContextMenuStrip = null; btnAdd.Visible = false; }
private async void toolStripMenuItem1_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
//x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
private async void btnAdd_Click(object sender, EventArgs e) { dataGridView1.ClearSelection(); (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false); }
private async void toolStripMenuItem2_Click(object sender, EventArgs e) => (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
private void textBox1_TextChanged(object sender, EventArgs e) => TextHandling.Changed(ref comboBox1, ref textBox1, ref dv, ref dataGridView1);
private async void street_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Test", true, false);
//x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "show", "Test", true, false);

private async void writen_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Test", true, false);
//x.handler(form, dataGridView1.SelectedRows[0], ref Custom, ref control, LogRow, "show", "Test", true, false);
private async void showToolStripMenuItem_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Local License", true, false);
private async void replaceDamgedToolStripMenuItem_Click(object sender, EventArgs e) { string x = dataGridView1.SelectedRows[0].Cells["Status"].ToString(); (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", "Replace_Damged", true, true); handelISActive(x); }
private void handelISActive(string x) { if (dataGridView1.SelectedRows[0].Cells["Status"].ToString() != x && dataGridView1.SelectedRows[0].Cells["Status"].ToString() != "Detaind") dataGridView1.SelectedRows[0].Cells["IsActive"].Value = false; }
private async void relaseToolStripMenuItem_Click(object sender, EventArgs e) { (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false); dataGridView1.SelectedRows[0].Cells["IsReleased"].Value = true; }
private async void showToolStripMenuItem1_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
private async void deleteToolStripMenuItem_Click(object sender, EventArgs e) => (dt) = await DeleteRow.row(dataGridView1, lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')));
private async void showInfoToolStripMenuItem_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, false);
private async void updateToolStripMenuItem_Click(object sender, EventArgs e) => (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
private async void editToolStripMenuItem_Click(object sender, EventArgs e) => (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), false, false);
private void button4_Click(object sender, EventArgs e) => this.Close();
private async void button1_Click(object sender, EventArgs e) { lblmanage.Text = "Users "; (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, true); }
private async void button5_Click(object sender, EventArgs e) { lblmanage.Text = "Users "; (Custom, control, dt) = await AddEditOperation.operation(form, dataGridView1, dt, LogRow, "Edit", lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')), true, true); }
private void myPersonalInfoToolStripMenuItem_Click(object sender, EventArgs e) { toolStripButton2_Click(null, null); comboBox1.SelectedIndex = 1; textBox1.Text = LogRow["personID"].ToString(); }

private void userInfoToolStripMenuItem_Click(object sender, EventArgs e) { toolStripButton3_Click(null, null); comboBox1.SelectedIndex = 1; textBox1.Text = LogRow["UserID"].ToString(); }

private void toolStripButton6_Click(object sender, EventArgs e) => this.FindForm().Close();

private async void tabone_Load(object sender, EventArgs e)
{
    await tabone_Loadx("People", null/* @"C:\Users\dbhdh\OneDrive\Pictures\pic\person_man.png", cmbPeopleFilter);
    DataTable _tab = await cmb.Rows("Users", null, "UserID", true, true, Convert.ToInt32(rowuser["UserID"]));
    LogRow = _tab?.Rows[0]; dtw = await cmb.Rows("People"); dty = await cmb.Rows("Applications"); dtz = await cmb.Rows("LicenseClasses");
    comboBox2.Items.Add("All"); comboBox2.Items.Add("Yes"); comboBox2.Items.Add("No"); comboBox2.SelectedIndex = 0; comboBox2.Visible = false;
}
private async void cancleToolStripMenuItem_Click(object sender, EventArgs e) => await ChecksOperation.Check(dataGridView1, "Cancel",
    async (ID, ApplicationID) =>
    {

        if (await CancelFun.Fun("Applications", "ApplicationID", ApplicationID))
        {
            DataRow dr = dt.Rows.Find(ID);
            dr["PassedTests"] = 0;
            dr["Status"] = "Cancel";
            dataGridView1.Refresh();
        }

    });


private async void myPersonalInfoToolStripMenuItem_Click_1(object sender, EventArgs e) { await tabone_Loadx("ApplicationTypes", null/*@"C:\Users\dbhdh\Downloads\task_types.png"); dataGridView1.ContextMenuStrip = contextMenuStrip5; btnAdd.Visible = false; }

private async void userInfoToolStripMenuItem_Click_1(object sender, EventArgs e) { await tabone_Loadx("TestTypes", null/* @"C:\Users\dbhdh\Downloads\task_types.png"); dataGridView1.ContextMenuStrip = contextMenuStrip5; btnAdd.Visible = false; }
private async void deleteToolStripMenuItem1_Click(object sender, EventArgs e) => await ChecksOperation.Check(dataGridView1, "Delete", async (ID, ApplicationID) =>
{
    DataTable _tab = await cmb.Rows("Applications", null, "ApplicationID", true, true, ApplicationID);
    DataRow row = _tab?.Rows[0];
    if (row != null && Convert.ToInt32(row["ApplicationStatus"]) != 3)
    {

        if (await handelapplicaionDeleation(ID) && await DeleteFun.fun("LocalDrivingLicenseApplications", "LocalDrivingLicenseApplicationID", ID) && await DeleteFun.fun("Applications", "ApplicationID", ApplicationID))
        {
            dt.Rows.Remove(dt.Rows.Find(ID));
            dataGridView1.Refresh();
        }
    }
    else
        MessageBox.Show("You Can Not Delete Completed Applicaion");
});
private async void localLicense_Click(object sender, EventArgs e) { await handelall("Licenses", null/* @"C:\Users\dbhdh\Downloads\id (1).png", contextMenuStrip3, ShowedTable.LocalLicense, setColumnValue.LocalLicense); dv.RowFilter = "IsActive = true"; }
private async void gloablToolStripMenuItem_Click(object sender, EventArgs e) => await handelall("InternationalLicenses", null /*@"C:\Users\dbhdh\Downloads\world (2).png", null, ShowedTable.InternationalLicense, setColumnValue.InternationalLicense);

private async void localToolStripMenuItem_Click(object sender, EventArgs e) { await handelall("LocalDrivingLicenseApplications", null /*@"C:\Users\dbhdh\Downloads\location (1).png", contextMenuStrip2, ShowedTable.LocalDrivingLicenseApplications, setColumnValue.LocalDrivingLicenseApplications); btnAdd.Visible = true; }
private async void visonToolStripMenuItem_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "show", "Test", true, false);

private void tests_MouseHover(object sender, EventArgs e) => handelmnue();

private void dataGridView1_SelectionChanged(object sender, EventArgs e) { if (dataGridView1.ContextMenuStrip != contextMenuStrip1) handelmnue(); }

private async void issuingALicense_Click(object sender, EventArgs e) => (form, Custom, control) = await x.handler(form, dataGridView1.SelectedRows[0], LogRow, "Show", "License", true, false);

private void visblecolumn(string columnName) => dataGridView1.Columns[columnName].Visible = false;
private async void toolStripButton5_Click(object sender, EventArgs e) { await tabone_Loadx("DetainedLicenses", null/* @"C:\Users\dbhdh\Downloads\id (2).png"*); dataGridView1.ContextMenuStrip = contextMenuStrip4; btnAdd.Visible = false; }
private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { handleIsActiveCombobox(); ShowActiveOnly(); }
private void handleIsActiveCombobox()
{
    textBox1.Visible = (comboBox1.SelectedIndex != 0 && comboBox1.Text.ToLower() != "isactive");
    if (textBox1.Visible)
        textBox1.Focus();
    if (comboBox1.Text.ToLower() == "isactive")
    {
        comboBox2.Visible = true;
    }

}
private void ShowActiveOnly()
{
    if (lblmanage.Text == "Licenses Managements") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsActive = true"; else dv.RowFilter = null;
    if (lblmanage.Text == "DetainedLicenses Management") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsReleased = false"; else dv.RowFilter = null;
    if (lblmanage.Text == "InternationalLicenses Managements") if (comboBox1.SelectedIndex == 0) dv.RowFilter = "IsActive = true"; else dv.RowFilter = null;
}

private async Task tabone_Loadx(string TableName, string picturelocation, HashSet<string> cmbFilter = null, HashSet<string> columnFilter = null)
{
    btnAdd.Visible = true;
    dataGridView1.ContextMenuStrip = contextMenuStrip1;
    (dt, dv) = await Loading.Load(dataGridView1, TableName, columnFilter);
    //if (cmbFilter != null)
    lblmanage.Text = $"{TableName} Management";
    comboboxHandleing.comboboxHandle(comboBox1, dataGridView1, cmbFilter);
    if (picturelocation != null)
        pictureBox1.Image = Image.FromFile(picturelocation);


}
private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    if (dataGridView1.Columns.Contains("Gendor"))
    {
        if (dataGridView1.Columns[e.ColumnIndex].Name == "Gendor")
        {
            if (e.Value != null)
            {
                int genderValue = Convert.ToInt32(e.Value);
                e.Value = genderValue == 0 ? "Male" : "Female";
                e.FormattingApplied = true;
            }
        }
    }
    else
        return;
}


private async Task<bool> handelapplicaionDeleation(int ID)
{
    bool seccuss = false;
    DataTable tableTestAppointment = await cmb.Rows("TestAppointments", null, "LocalDrivingLicenseApplicationID", true, false, ID);
    if (tableTestAppointment != null)
    {
        foreach (DataRow rx in tableTestAppointment.Rows)
        {
            seccuss = await DeleteFun.fun("Tests", "TestAppointmentID", Convert.ToInt32(rx["TestAppointmentID"]));
            if (!seccuss)
                break;
        }
        seccuss = await DeleteFun.fun("TestAppointments", "LocalDrivingLicenseApplicationID", ID);
    }
    else
        seccuss = true;
    return seccuss;
}


private async Task handelall(string tablename, string imagepath, ContextMenuStrip menu, Func<DataTable> table, Action<List<DataTable>, int> fun)
{
    await tabone_Loadx(tablename, imagepath);
    btnAdd.Visible = false;
    dataGridView1.ContextMenuStrip = menu;
    DataTable dtx = table();
    handelforloopdatatable(fun, dtx);
    handeldatatable(dtx, $"{tablename} Managements");
}
private void handeldatatable(DataTable dtx, string Message)
{
    dt = dtx;
    dv = dt.DefaultView;
    dataGridView1.DataSource = dv;
    comboboxHandleing.comboboxHandle(comboBox1, dataGridView1, null);
    lblmanage.Text = Message;
}

private void handelforloopdatatable(Action<List<DataTable>, int> fun, DataTable dtx)
{
    if (dt == null && dt.Rows.Count == 0)
    {
        MessageBox.Show("No Data Found");
        return;
    }
    for (int i = 0; i < dt.Rows.Count; i++)
        fun(new List<DataTable> { dt, dtw, dty, dtz, dtx }, i);
}

private void handeltruefalsevalue(bool test, bool vison, bool street, bool writen)
{
    ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["vison"].Enabled = vison;
    ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["street"].Enabled = street;
    ((ToolStripMenuItem)contextMenuStrip2.Items["tests"]).DropDownItems["writen"].Enabled = writen;
    contextMenuStrip2.Items["tests"].Enabled = test;
}
private void handeloperationcontixtmnuestrip(bool seccuss)
{
    contextMenuStrip2.Items["delete"].Enabled = seccuss;
    contextMenuStrip2.Items["cancel"].Enabled = seccuss;
    contextMenuStrip2.Items["edit"].Enabled = seccuss;

}
private async void handelmnuestrip2()
{
    DataRow rowz = null;
    int passed = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value);
    string state = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();

    DataTable _tab = await cmb.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", true, true, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LDLAppID"].Value));
    DataRow row = _tab?.Rows[0];

    if (row != null)
    {
        _tab = await cmb.Rows("Licenses", null, "ApplicationID", true, true, Convert.ToInt32(row["ApplicationID"]));
        rowz = _tab?.Rows[0];
    }
    bool seccuss = false;
    if (rowz == null)
    {
        if (state == "Completed")
            seccuss = true;

        else
            seccuss = false;
    }
    handeloperationcontixtmnuestrip(seccuss);

    contextMenuStrip2.Items["IssuingAlicense"].Enabled = seccuss;

    if ("Cancel" == state)
        contextMenuStrip2.Items["tests"].Enabled = contextMenuStrip2.Items["issuingALicense"].Enabled = false;

    else
    {
        switch (passed)
        {
            case 0:
                handeltruefalsevalue(true, true, false, false);
                break;
            case 1:
                handeltruefalsevalue(true, false, true, false);
                break;
            case 2:
                handeltruefalsevalue(true, false, false, true);
                break;
            case 3:
                contextMenuStrip2.Items["tests"].Enabled = false;
                break;
            default:
                contextMenuStrip2.Items["tests"].Enabled = false;
                break;
        }
    }
}
private void handelmnuestrip3()
{
    string state = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
    bool IsActive = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["IsActive"].Value);
    if (state == "Detaind" || !IsActive)
        contextMenuStrip3.Items["Services"].Enabled = false;
    else
        contextMenuStrip3.Items["Services"].Enabled = true;
}
private void handelmnuestrip4()
{
    if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["IsReleased"].Value))
        contextMenuStrip4.Items["Release"].Enabled = false;
    else
        contextMenuStrip4.Items["Release"].Enabled = true;
}
private void handelmnue()
{

    if (dataGridView1 != null && dataGridView1.SelectedRows.Count > 0)
    {
        if (lblmanage.Text == "Licenses Managements")
            handelmnuestrip3();
        else if (lblmanage.Text.Contains("Local"))
            handelmnuestrip2();
        else if (lblmanage.Text.Contains("DetainedLicense"))
            handelmnuestrip4();

    }
    else
        return;
}

private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
{
    if (dv == null)
        return;
    if (comboBox2.SelectedIndex == 0)
        dv.RowFilter = null;
    else if (comboBox2.SelectedIndex == 1)
        dv.RowFilter = (($@"{comboBox1.SelectedItem.ToString()} = true "));
    else
        dv.RowFilter = (($@"{comboBox1.SelectedItem.ToString()} = false "));
    dataGridView1.Refresh();
}
    }
}*/
#region tries
//private DataTable GetDataTableFromDataSource()
//{
//    if (dataGridView1.DataSource is DataTable dataTable)
//    {
//        return dataTable;
//    }
//    else if (dataGridView1.DataSource is DataView dataView)
//    {
//        return dataView.Table;
//    }
//    else
//    {
//        throw new InvalidOperationException("DataGridView DataSource is not a DataTable or DataView.");
//    }
//}
/*private void ViewFilter(HashSet<string> columnFilter)
     {
         if (columnFilter != null)
             foreach (var column in columnFilter)
                 //dataGridView1.Columns[column].Visible = false;
                 dv.RowFilter = $"[{column}] = 0";
         dataGridView1.Refresh();
     }*/
//dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
//if (lblmanage.Text.Contains("People"))
//else if (lblmanage.Text.Contains("Users"))
//AddEditOperation("Add", true, false);
//else
//return;
//private static readonly HashSet<string> ShowedcolumnUsers = new HashSet<string>() { "Password" };
//private static readonly HashSet<string> DriversFilter = new HashSet<string>() { "ImagePath", "PersonID", "NationalityCountryID" };
//if(columnFilter != null)
//    //columnFilter.AsParallel().ForAll(column => dataGridView1.Columns[column].Visible = false);
//    foreach (var column in columnFilter)
//        dataGridView1.Columns[column].Visible = false;
//var filterSet = new HashSet<string>(cmbFilter, StringComparer.OrdinalIgnoreCase);
/* private T GetT<T>(int id) where T : ISupportedType<T>, new()
 {
     DataTable dataTable = GetDataTableFromDataSource();
     DataRow[] rows = dataTable.Select($"PersonID = {id}");
     if (rows.Length > 0)
     {
         T t = new T();
         foreach (DataColumn column in dataTable.Columns)
         {
             var propertyInfo = typeof(T).GetProperty(column.ColumnName);
             if (propertyInfo != null)
                 propertyInfo.SetValue(t, rows[0][column.ColumnName]);
         }
         return t;
     }
     return default;
 }*/
//foreach()
//else if(lblmanage.Text.Contains("Users"))
//User p = personInfo.ManipulatingResult();
/* private void EditDataGridViewRow<T>(int id, T newValue)where T : ISupportedType<T>
 {
     // Find the row in the DataTable
     if(newValue is ISupportedType<T> s)
     DataRow[] rows = ((DataTable)dataGridView1.DataSource).Select($"{s.PrimaryIntColumnIDName} = {newValue.GetProperty(s.PrimaryIntColumnIDName).GetValue(newValue)}");
     if (rows.Length > 0)
     {
         rows[0][$"{s.PrimaryIntColumnIDName}"] = newValue; // Edit the value
     }
 }*/
/*
 * private void EditDataGridViewRow<T>(int id, T newValue) where T : ISupportedType<T>
{
    // Validate the data source
    if (dataGridView1.DataSource is not DataTable dataTable)
    {
        throw new InvalidOperationException("DataGridView is not bound to a DataTable.");
    }

    // Get the primary column name from the interface
    var s = (ISupportedType<T>)newValue;
    string primaryColumn = s.PrimaryIntColumnIDName;

    // Get the property info for the primary column
    var propertyInfo = typeof(T).GetProperty(primaryColumn);
    if (propertyInfo == null)
    {
        throw new ArgumentException($"Property '{primaryColumn}' does not exist on type '{typeof(T).Name}'.");
    }

    // Get the primary key value from newValue
    object primaryKeyValue = propertyInfo.GetValue(newValue);

    // Build the filter expression safely (handle strings vs. numbers)
    string filter;
    if (primaryKeyValue is string stringValue)
    {
        filter = $"{primaryColumn} = '{stringValue}'"; // Enclose strings in quotes
    }
    else
    {
        filter = $"{primaryColumn} = {primaryKeyValue}"; // No quotes for numbers
    }

    // Find the row in the DataTable
    DataRow[] rows = dataTable.Select(filter);
    if (rows.Length > 0)
    {
        // Edit the row with the new value
        rows[0][primaryColumn] = primaryKeyValue;
    }
    else
    {
        throw new InvalidOperationException($"No row found with {primaryColumn} = {primaryKeyValue}.");
    }
}*/
/*private void EditDataGridViewRow<T>(int id, T newValue) where T : ISupportedType<T>
{
    // Ensure the DataGridView is bound to a DataTable
    if (dataGridView1.DataSource is not DataTable dataTable)
    {
        throw new InvalidOperationException("DataGridView DataSource is not a DataTable.");
    }

    // Get the supported type instance
    ISupportedType<T> supportedType = newValue;

    // Validate the primary key column name
    string primaryColumn = supportedType.PrimaryIntColumnIDName;

    // Get the property info for the primary key column
    var propertyInfo = typeof(T).GetProperty(primaryColumn);
    if (propertyInfo == null)
    {
        throw new InvalidOperationException($"Property '{primaryColumn}' not found on type '{typeof(T).Name}'.");
    }

    // Get the value of the primary key column from newValue
    object primaryKeyValue = propertyInfo.GetValue(newValue);

    // Build the filter expression safely
    string filter = $"{primaryColumn} = {FormatValueForFilter(primaryKeyValue)}";

    // Find the row in the DataTable
    DataRow[] rows = dataTable.Select(filter);
    if (rows.Length > 0)
    {
        // Edit the row with the new value
        rows[0][primaryColumn] = primaryKeyValue; // Edit the primary key column
    }
}

// Helper method to format values for the filter expression
private string FormatValueForFilter(object value)
{
    return value switch
    {
        string str => $"'{str.Replace("'", "''")}'", // Escape single quotes for strings
        DateTime dtr => $"'{dtr:yyyy-MM-dd HH:mm:ss}'", // Format dates
        _ => value.ToString() // Default for other types
    };
}*/
//private ICustmControl<T> Get<T>() where T : ISupportedType<T>
/*private void AddEditOperation<T>(string title ,bool enable,bool Edit,T back) where T : ICustmControl<T> 
{
    try
    {
        DataRow rowx;
        handler(title, enable);
        var xx = back.ManipulatingResult();
        if (xx is ISupportedType<T> t)
        {
            //if(xx is ISupportedType)
            if (xx == null)
                MessageBox.Show("No Data Editd");
            else
                t = (ISupportedType<T>)xx;
            if (Edit)
            {
                dtr.Rows.Find(dataGridView1?.Rows[0].Cells[0].Value);
                rowx = t.ToDataRow(dtr, false);
                rowx.AcceptChanges();
            }
            else
                dtr.Rows.Add(t.ToDataRow(dtr, true));
        }

        //if (xx != null)
        //ISupportedType<T> s = Value;
        //if (Value != null && Edit)
        //    dtr.Rows.Add(s);
        //else if (Value != null && !Edit)
        //   rowx=  dtr.Rows.Find(s.PrimaryKey());
        //if (xx != null && Edit)
        //{
        //    rowx=xx.ToDataRow(dtr); 
        //    rowx.AcceptChanges();
        //}
        //else if (xx != null && !Edit)

        //    dtr.Rows.Add(xx);
        //if (xx != null&&Edit)
        //    //myfun(xx);
        //    EditDataGridViewRow(xx);
        //else if(xx!=null &&!Edit)
        //dataGridView1.
        dataGridView1.Refresh();

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}*/

/*
if (lblmanage.Text.Contains("People"))
{
    form.Text = $"{Title} Person";
    //personInfo = new PersonInfo();//(form, Value,/*visable*//*Enable, Title);
    form.Size = personInfo.Size + new Size(12, 38);
    personInfo.Enabled = true;
    personInfo.setInfo(Value,false);
    form.Controls.Add(personInfo);
}
else
{ 
    form.Text = $"{Title} User";
     userInfo = new UserInfo();
    form.Size = userInfo.Size + new Size(12, 38);
    userInfo.Enabled = true;
    form.Controls.Add(userInfo);

}*/
/*    private void EditDataGridViewRow<T>(T newValue) where T : ISupportedType<T>
    {
        DataTable dataTable = GetDataTableFromDataSource();

        ISupportedType<T> supportedType = newValue;

        var primaryKeyProperty = typeof(T).GetProperty(supportedType.PrimaryIntColumnIDName);
        if (primaryKeyProperty == null)
            throw new InvalidOperationException($"Property '{supportedType.PrimaryIntColumnIDName}' not found on type '{typeof(T).Name}'.");

        var primaryKeyValue = primaryKeyProperty.GetValue(newValue);

        DataRow[] rows = dataTable.Select($"{supportedType.PrimaryIntColumnIDName} = {primaryKeyValue}");
        if (rows.Length > 0)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName == supportedType.PrimaryIntColumnIDName)
                    continue;
                var propertyInfo = typeof(T).GetProperty(column.ColumnName);
                if (propertyInfo != null)
                    rows[0][column.ColumnName] = propertyInfo.GetValue(newValue);
            }
        }
    }*/
/* private string FormatFilterValue(object value)
{
return value switch
{
 string str => $"'{str.Replace("'", "''")}'", 
 int or double or decimal => value.ToString(), 
 _ => throw new ArgumentException($"Unsupported filter value type: {value.GetType().Name}")
};
}*/
/* private T GetT<T>() //where T : ISupportedType<T>
{

    if (lblmanage.Text.Contains("People"))
    {
        //personInfo.ManipulatingResult();
        //return (T)Convert.ChangeType(p, typeof(T));
            ICustmControl<Person> p = (ICustmControl < Person > )(personInfo);
        return p;
    }
    else if (lblmanage.Text.Contains("Users"))
    {
        ICustmControl<User> u = (ICustmControl<User>)(userInfo);
        return u;
        //    User p = UserInfo.ManipulatingResult();
        //    return (T)Convert.ChangeType(p, typeof(T));
    }
    //else if (lblmanage.Text.Contains("Drivers"))
    //{
    //    Driver p = DriverInfo.ManipulatingResult();
    //    return (T)Convert.ChangeType(p, typeof(T));
    //}
    else
        return default;
}*/
//private void comboboxHandle(HashSet<string> cmbFilter)
//{
//    comboBox1.Items.Clear();
//    comboBox1.Items.Add("None");
//    foreach (DataGridViewColumn column in dataGridView1.Columns)
//    {
//        if (cmbFilter == null || !cmbFilter.Contains(column.Name))
//            comboBox1.Items.Add(column.Name);
//    }
//    comboBox1.SelectedIndex = 0;
//}
/*private Control GetControl()
      {
          if (lblmanage.Text.Contains("People"))
              return new PersonInfo();

          else if (lblmanage.Text.Contains("Users"))
              return new UserInfo();
          else
              return null;
      }*/
/*private void handler(string Title,bool Enable,bool log)
    {
        DataRow dataRow = LogRow;
        if (!log && dataGridView1.SelectedRows.Count <= 0 && tabControl1.SelectedTab == Managment)
        {
            MessageBox.Show("No row selected!");
            return;
        }
        else if (!log && dataGridView1.SelectedRows.Count > 0)
        {
            if (dataGridView1.SelectedRows[0].DataBoundItem is DataRowView rowView)
                dataRow = rowView.Row;
            else
                MessageBox.Show("Row is not data-bound!");
        }
            if (Title == "Add")
                dataRow = dtr.NewRow();
            try
            {
                control = GetCustomizedControl();
                control?.setInfo( dataRow, Enable);
                Custom = control as Control;
                show(Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    }*/
/*  private ICustmControl<object> GetCustomizedControl()
    {
        if (lblmanage.Text.Contains("People"))
            return new PersonInfo();

        else if (lblmanage.Text.Contains("Users"))
            return new UserInfo();
        else if (lblmanage.Text.Contains("Applications"))
            return new LocalLicenses(Convert.ToInt32(LogRow["UserID"] ?? -1), LogRow["UserName"].ToString(), false);
        else
            return null;
    }

    private void handler(string Title, bool Enable, bool log)
    {
        int ID = 0;
        if (log)
            ID = Convert.ToInt32(LogRow["UserID"]);
        else if (dataGridView1.SelectedRows.Count > 0)
            ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
        try
        {
            control = GetCustomizedControl();
            //(T ID, string ColumnName, bool visibility, string Title)
            control?.setInfo(ID, Enable, Title);
            Custom = control as Control;
            Showing.show(form, Custom, Title, lblmanage.Text.Substring(0, lblmanage.Text.IndexOf(' ')));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }*/
//private void AddEditOperation(string title, bool Add, bool log)
//{
//    handler(title, false, log);
//    //var control = GetCustomizedControl();
//    if (control == null)
//    {
//        MessageBox.Show("Invalid control type.");
//        return;
//    }

//    try
//    {
//        var result = control.ManipulatingResult();

//        if (result == null)
//        {
//            MessageBox.Show("No Data Edited");
//            return;
//        }

//        DataRow row;

//        if (Add)
//        {
//            row = result.ToDataRow(dtr, Add);
//            dtr.Rows.Add(row);
//        }
//        else
//        {
//            if (!log)
//                row = result.ToDataRow(dtr, Add);
//            else if (result is User user)
//                row = user.ToDataRow(LogRow);
//            else
//                row = null;

//            if (row != null)
//                row.AcceptChanges();
//            //= dtr.Rows.Find(dataGridView1?.Rows[0].Cells[0].Value);
//        }
//        if (!log)
//            dataGridView1.Refresh();
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }
//}
/*private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
{
    if(int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int id))
    {
        if (MessageBox.Show($"Are Yue Shure To Delete {id}", "Management", MessageBoxButtons.OKCancel) == DialogResult.OK)
        {
            try
            {

                B_Delete.Row(lblmanage.Text.Substring(0,lblmanage.Text.IndexOf(' ')), GetCustomizedControl().PrimaryIntColumnIDName(), id);
                dtr.Rows.Remove(dtr.Rows.Find(id));
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } 
}*/

#endregion

#endregion