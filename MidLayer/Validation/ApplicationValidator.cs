using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
//using EntityLayer.Classes;
using MidLayer.Process;

namespace MidLayer.Validation
{
    public class ApplicationValidator
    {
        public static async Task<bool> IsApplicantExists(int ID) => await D_Check.Row("Applications", "ApplicationID", ID, MyApplication.map).ConfigureAwait(false);//D_Check.Row("Users", "UserName", Name, User.map);
        public static async Task<bool> IsApplicantPersonExists(MyApplication A) =>await Checks.Checked(A, new HashSet<string> { "ApplicantPersonID", "ApplicationTypeID" }, "Applications", MyApplication.map).ConfigureAwait(false);
        //{
        //    string tableName = "Applications";
        //    DataTable dt = null;

        //    dt = D_Fetch.Rows(tableName, null, new HashSet<string> {"setApplicantPersonID", "ApplicationTypeID" },A, MyApplication.map);

        //    if (dt == null || dt.Rows.Count == 0)
        //        return true;
        //        int x = Convert.ToInt32(dt?.Rows[0][MyApplication.map.Keys.First()] ?? -1);
        //        if (dt.Rows.Count > 1&& x != A.PrimaryKey())
        //                throw new Exception("Already Been Used");
        //        else
        //            return true;
        //    }
        //}
    }
}
