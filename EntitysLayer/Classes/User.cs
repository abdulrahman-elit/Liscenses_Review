using System;
using System.Collections.Generic;
using System.Data;
////using MiddleLayer.Process;
using EntityLayer.Interfaces;
using MidLayer.Validation;
using System.Net;
using System.Security.Policy;
using MidLayer;
using System.Threading.Tasks;
namespace EntityLayer.Classes
{
    public class User : ISupportedType<User>
    {
        #region Praporties
     
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "UserID", (SqlDbType.Int,null) },
        { "PersonID", (SqlDbType.Int,null) },
        { "UserName", (SqlDbType.NVarChar, 20) },
        { "Password", (SqlDbType.NVarChar, 70) },
        { "IsActive", (SqlDbType.Bit,null) }
        };
        private string trim;
        public enStatus status {  set; get; }
        public int UserID { private set; get; }
        public int PersonID { private set; get; }
        public string UserName { private set; get; }
        public string Password { private set; get; }
        public bool IsActive { private set; get; }
        private int _PrimaryKey {  set; get; }
        public int PrimaryKey(int id = 0)
        {
            if(id > 0)
            {
                UserID = _PrimaryKey = id;
                return _PrimaryKey;
            }
            return    _PrimaryKey;
        }
        public async Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row = null;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns["UserID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(UserID)}"] = _PrimaryKey;
                //dt.Rows.Add(row);
            }
            else
                row = dt.Rows.Find(_PrimaryKey);
            row =await ToDataRow(row);
            return (dt, row);
        }
        public Task<DataRow> ToDataRow(DataRow row)
        {
            row[$"{nameof(PersonID)}"] = this.PersonID;
            row[$"{nameof(UserName)}"] = this.UserName;
            //row[$"{nameof(Password)}"] = this.Password;
            row[$"{nameof(IsActive)}"] = this.IsActive;
            return Task.FromResult( row);

        }
        public void setpersonID(int PersonID)
        {

            _ValidationService.validationID(PersonID);
            this.PersonID = PersonID;
        }
        public void setUserName(string Name)
        {
             trim=Name.Trim();
            _ValidationService.Validate(trim, "^[a-zA-Z0-9_.-]{3,20}$");
            this.UserName = trim;
        }
        public void setPassword(string Password)
        {
            trim = Password.Trim();
            //_ValidationService.ValidationUserPassword(trim);
            this.Password = trim;
        }
        public void setIsActive(bool IsActive)=>this.IsActive = IsActive;
        public string PrimaryIntColumnIDName { get; } = "UserID";
        #endregion
        #region Constractuers
        public User(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            _ValidationService.validationID(UserID);
           setpersonID(PersonID);
            setUserName(UserName);
            setPassword(Password);
            setIsActive(IsActive);  
            this.UserID =_PrimaryKey= UserID;
          
            status = enStatus.Update;
        }
        public User(int PersonID, string UserName, string Password, bool IsActive)
        {
            setpersonID(PersonID);
            setUserName(UserName);
            setPassword(Password);
            setIsActive(IsActive);
            this.UserID =_PrimaryKey= -1;
            status = enStatus.New;
        }
       public  User(User user)
        {
            _ValidationService.validationID(user.UserID);
            setpersonID(user.PersonID);
            setUserName(user.UserName);
            setPassword(user.Password);
            setIsActive(user.IsActive);
         
            this.UserID =_PrimaryKey= user.UserID;
    
            status = enStatus.Update;
        }
        public User(DataRow UserRow)
        {

            if (UserRow == null)
                throw new ArgumentNullException(nameof(UserRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(UserRow["UserID"] ?? -1));
            setpersonID(Convert.ToInt32(UserRow["PersonID"] ?? -1));
            setUserName(UserRow["UserName"] as string);
            //setPassword(UserRow["Password"] as string);
            //this.Password = UserRow["Password"] as string;
            setIsActive(UserRow["IsActive"] as bool? ?? false);
            UserID =_PrimaryKey = Convert.ToInt32(UserRow["UserID"] ?? -1);
            status = enStatus.Update;
        }

        public Task FromDataRow(DataRow UserRow)
        {
            if (UserRow == null)
                throw new ArgumentNullException(nameof(UserRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(UserRow["UserID"] ?? -1));
            setpersonID(Convert.ToInt32(UserRow["PersonID"] ?? -1));
            setUserName(UserRow["UserName"] as string);
            setPassword(UserRow["Password"] as string);
            setIsActive(UserRow["IsActive"] as bool? ?? false);
            UserID = _PrimaryKey = Convert.ToInt32(UserRow["UserID"] ?? -1);
            status = enStatus.Update;
            return Task.CompletedTask;
        }
        #endregion
        #region Validation
          public async  Task<bool> Validation()
        {
            try
            {
                this.Password= UserValidtaor.ComputeHash(Password);
                return  await PersonValidator.ISPersonExists(PersonID) &&await UserValidtaor.IsUserNameExists(UserID,UserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}


     /* if(!_ValidationService.validationID(UserID))
                 throw new Exception($"Invalid {nameof(UserID)}");

             if (!_ValidationService.validationID(PersonID))
                 throw new Exception($"Invalid {nameof(PersonID)}");

             if (!_ValidationService.validationName(UserName))
                 throw new Exception($"Invalid {nameof(UserName)}");

             if (!_ValidationService.ValidationUserPassword(Password))

                 throw new Exception($"Invalid {nameof(Password)}");*/