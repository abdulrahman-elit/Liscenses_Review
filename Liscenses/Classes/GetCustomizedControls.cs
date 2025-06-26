using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using Licenses;
using Liscenses.MyCustomControl;
namespace Liscenses.Classes
{
    public static class GetCustomizedControls
    {
        public static async Task< ICustmControl<object>> Control(string Title, DataGridViewRow dataGridView1=null, DataRow row=null)
        {
            if (Title.Contains("People"))
                return  await PersonInfo.CreateAsync();

            else if (Title.Contains("Users"))
                return new UserInfo();
            else if (Title.Contains("Applications"))
                return await LocalLicenses.CreateAsync(Convert.ToInt32(row["UserID"] ?? -1), row["UserName"].ToString(), false);
            else if (Title.Contains("TestTypes"))
                return new TestTypes();
            else if (Title.Contains("Test"))
                return  await Tests.creatAsync(Convert.ToInt32(dataGridView1.Cells[0].Value), Convert.ToInt32(row["UserID"] ?? -1), row["UserName"].ToString(), false, (Convert.ToInt32(dataGridView1.Cells["PassedTests"].Value) + 1));
            else if (Title == "Local License")
                return new LocalLicense();
            else if (Title.Contains("Replace"))
                return new Replace_Damged(Convert.ToInt32(row["UserID"] ?? -1));
            else if (Title == "DetainedLicenses")
                return new DetaindLicensex(Convert.ToInt32(row["UserID"]), row["UserName"].ToString());
            else if (Title.Contains("License"))
                return new License(Convert.ToInt32(dataGridView1.Cells[0].Value), Convert.ToInt32(row["UserID"]), row["UserName"].ToString());
            else if (Title.Contains("ApplicationTypes"))
                return new ApplicationTypes(); 
            else
                return null;
        }
    }
}
