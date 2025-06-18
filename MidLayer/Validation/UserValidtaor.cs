using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
////using MiddleLayer.B_CRUDOperation.B_Read//;
using MidLayer.B_CRUDOperation;
namespace MidLayer.Validation
{
    public static class UserValidtaor
    {
        public static async Task<bool> IsUserIDExists(int ID) => await D_Check.Row("Users", "UserID", ID, User.map).ConfigureAwait(false);
        public static async Task<bool> IsUserNameExists(int id, string Name) => await Checks.Checked(id, Name, "UserName", "users", User.map, "UserID").ConfigureAwait(false);//D_Check.Row("Users", "UserName", Name, User.map);
        public static async Task<bool> IsUserNameExists(User user) => await Checks.Checked(user, new HashSet<string> { "UserName" }, "users", User.map).ConfigureAwait(false);//D_Check.Row("Users", "UserName", Name, User.map);
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower();
            }
        }
    }
}
#region Tries
/*  internal static bool checks(int PersonID, string value, string title)
  {
      DataTable dt = D_Fetch.Rows("Users", new HashSet<string> { "UserID", title }, title, value, User.map);

      if (dt == null || dt.Rows.Count == 0)
          return true;

      var row = dt?.Rows[0];
      int existingPersonID = Convert.ToInt32(row["UserID"]);

      if (existingPersonID == PersonID)
          return true;
      else
          throw new Exception("Alrady Been Used");
  }*/
//public bool ValidationUserPassword(string Password) => _ValidationService.Validate(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$");
#endregion