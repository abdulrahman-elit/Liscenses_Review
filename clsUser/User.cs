using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using clsPerson.SupportedTypes;
using clsValidation.SupportedTypes;
using ISupportedType.SupportedTypes;
using MiddleLayer.B_CRUDOperation.B_Read;
namespace clsUser.SupportedTypes
{
    public class User:ISupportedType<User>
    {
        #region Praporties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "UserID", (SqlDbType.Int,null) },
        { "PersonID", (SqlDbType.Int,null) },
        { "UserName", (SqlDbType.NVarChar, 20) },
        { "Password", (SqlDbType.NVarChar, 20) },
        { "IsActive", (SqlDbType.Bit,null) }
        };


        public enStatus status{ set;get;}
      public int UserID{set;get;} 
      public int PersonID{ set;get;} 
      public string UserName{set;get;} 
      public string Password{set;get;} 
      public bool IsActive { set; get; }


      public string PrimaryIntColumnIDName { get; } = "UserID";
        public static B_Check Exists = new B_Check();
        #endregion
        #region Constractuers
        User(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID   = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            status = enStatus.Update;
            ValidationUserID(PersonID);
            ValidationUserName(UserName);
            _ValidationService.Validate(UserName, @"^\w+$");
            ValidationUserPassword(Password);
        }
        User(int PersonID,string UserName, string Password, bool IsActive)
        {
            this.UserID   = -1;
            this.PersonID = PersonID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            status = enStatus.New;
            ValidationUserID(PersonID);
            ValidationUserName(UserName);
            _ValidationService.Validate(UserName, @"^\w+$");
            ValidationUserPassword(Password);
        }
        User(User user)
        {
            this.PersonID = user.PersonID;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.IsActive = user.IsActive;
            status = enStatus.Update;
            ValidationUserID(PersonID);
            ValidationUserName(UserName);
            _ValidationService.Validate(UserName, @"^\w+$");
            ValidationUserPassword(Password);
        }
        public User(DataRow UserRow)
        {

            if (UserRow == null)
             throw new ArgumentNullException(nameof(UserRow), "The DataRow cannot be null.");
            PersonID = Convert.ToInt32(UserRow["ID"] ?? -1); // Handle DBNull
            UserName = UserRow["UserName"] as string; // Handle DBNull
            Password = UserRow["Password"] as string;
            IsActive = UserRow["IsActive"] as bool? ??false;
            status = enStatus.Update;
            ValidationUserID(PersonID); 
            ValidationUserName(UserName);
            _ValidationService.Validate(UserName, @"^\w+$"); 
            ValidationUserPassword(Password);



        }
        #endregion
        #region Validation
        public static bool IsUserExists(int ID)=> Exists.Row("Users", "UserID", ID, true);
        public bool ValidationUserID(int ID) => Person.ISPersonExists(ID);
        public bool ValidationUserName(string Name) => Exists.Row("Users","UserName",Name,true);
        public bool ValidationUserPassword(string Password) => _ValidationService.Validate(Password, @"^.{8,20}$");
        //public bool ValidationUserPassword(string Password) => _ValidationService.Validate(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$");
        public bool Validation (User user)
        {
            try
            {
                return ValidationUserID(user.PersonID) && ValidationUserName(user.UserName) && _ValidationService.Validate(user.UserName, @"^\w+$") && ValidationUserPassword(user.Password);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

