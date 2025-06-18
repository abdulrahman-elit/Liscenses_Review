using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
//using EntityLayer.Classes;
using MidLayer.B_CRUDOperation.B_Read;


namespace MidLayer.Validation
{
    public static class LicenseValidator
    {
        public static async Task<bool> IsLicenseExists(int ID) => await D_Check.Row("Licenses", "LicenseID", ID, MyLicense.map).ConfigureAwait(false);
        public static async Task<bool> IsLicenseValidAndActive(int id)
        {
            DataTable table = await D_Fetch.Rows("Licenses", null, "ApplicationID", id, MyLicense.map, true).ConfigureAwait(false);
            if(table == null && table.Rows.Count>0)
             return   Convert.ToBoolean(table?.Rows[0]["IsActive"]);
                    else
             throw new Exception("You Do Not Has A license");
        }
    }
}

//DataRow row= D_Fetch.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", id, License.map, true)?.Rows[0];
// if(row == null) throw new Exception("The Local Driving License Applications Does Not Exsist ");