using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using MidLayer;
using MidLayer.Validation;
// Ensure that the namespace 'MidLayer.Validation' exists and is correctly referenced in your project.
// If the namespace does not exist, you need to create it or reference the correct assembly.

//using MidLayer.Validation; // This line assumes that the 'Validation' namespace exists within 'MidLayer'.

// If the 'Validation' namespace is missing, you can either:
// 1. Add the missing assembly reference to your project.
// 2. Verify that the 'Validation' namespace is correctly defined in the 'MidLayer' project or library.
// 3. If 'Validation' is part of another namespace, update the using directive to the correct namespace.
//using static MidLayer._ser;
//using MidLayer.Process;
//using MidLayer.Validation
namespace EntityLayer.Classes
{
    public class Driver : ISupportedType<Driver>
    {
        #region Praporties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "DriverID", (SqlDbType.Int,null) },
        { "PersonID", (SqlDbType.Int, null) },
        { "CreatedByUserID", (SqlDbType.Int, null) },
        { "CreatedDate", (SqlDbType.SmallDateTime, null) },
              };
        public string PrimaryIntColumnIDName { get; } = "DriverID";
        public enStatus status { set; get; }
        public int DriverID { private set; get; }
        public int PersonID { private set; get; }
        public int CreatedByUserID { private set; get; }
        public DateTime CreatedDate { private set; get; }
        private int _PrimaryKey { set; get; }
        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                DriverID = _PrimaryKey = id;
                return _PrimaryKey;
            }
            return _PrimaryKey;
        }
        public  Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row = null;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns["DriverID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(DriverID)}"] = _PrimaryKey;
            }
            else
                row = dt.Rows.Find(_PrimaryKey);
            row[$"{nameof(PersonID)}"] = this.PersonID;
            row[$"{nameof(CreatedByUserID)}"] = this.CreatedByUserID;
            row[$"{nameof(CreatedDate)}"] = this.CreatedDate;
            return Task.FromResult((dt, row));
            //Task.FromResult(row);
        }
        public void setpersonID(int ID)
        {
            //////_ValidationService.validationID(ID);
            this.PersonID = ID;
        }
        public void setCreatedByUserID(int ID)
        {
            //_ValidationService.validationID(ID);
            this.CreatedByUserID = ID;
        }
        public void setCreatedDate(DateTime CreatedDate)
        {
            //_ValidationService.ValidationLisenasDateTime(CreatedDate);
            this.CreatedDate = CreatedDate;
        }
        #endregion
        #region Constractuers
        public Driver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            //_ValidationService.validationID(DriverID);
            this.DriverID = _PrimaryKey = DriverID;
            setpersonID(PersonID);
            setCreatedByUserID(CreatedByUserID);
            setCreatedDate(CreatedDate);
            this.status = enStatus.Update;
        }
        public Driver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = _PrimaryKey = -1;
            setpersonID(PersonID);
            setCreatedByUserID(CreatedByUserID);
            setCreatedDate(CreatedDate);
            this.status = enStatus.New;

        }
        public Driver(Driver driver)
        {
            //_ValidationService.validationID(driver.DriverID);

            this.DriverID = _PrimaryKey = driver.DriverID;
            setpersonID(driver.PersonID);
            setCreatedByUserID(driver.CreatedByUserID);
            setCreatedDate(driver.CreatedDate);
            this.status = enStatus.Update;

        }
        public Driver(DataRow DriverRow)
        {


            if (DriverRow == null)
                throw new ArgumentNullException(nameof(DriverRow), "The DataRow cannot be null.");
            //_ValidationService.validationID(Convert.ToInt32(DriverRow["DriverID"] ?? -1));
            setpersonID(Convert.ToInt32(DriverRow["PersonID"] ?? -1));
            setCreatedByUserID(Convert.ToInt32(DriverRow["CreatedByUserID"] ?? -1));
            setCreatedDate(DriverRow["CreatedDate"] as DateTime? ?? DateTime.MinValue);
            DriverID = _PrimaryKey = Convert.ToInt32(DriverRow["DriverID"] ?? -1); // Handle DBNull
            status = enStatus.Update;

        }
        public Task FromDataRow(DataRow DriverRow)
        {


            if (DriverRow == null)
                throw new ArgumentNullException(nameof(DriverRow), "The DataRow cannot be null.");
            //_ValidationService.validationID(Convert.ToInt32(DriverRow["DriverID"] ?? -1));
            setpersonID(Convert.ToInt32(DriverRow["PersonID"] ?? -1));
            setCreatedByUserID(Convert.ToInt32(DriverRow["CreatedByUserID"] ?? -1));
            setCreatedDate(DriverRow["CreatedDate"] as DateTime? ?? DateTime.MinValue);
            DriverID = _PrimaryKey = Convert.ToInt32(DriverRow["DriverID"] ?? -1); // Handle DBNull
            status = enStatus.Update;
            return Task.CompletedTask;
        }
        #endregion
        #region Validation

        public async Task<bool> Validation()
        {

            try
            {
                return await PersonValidator.ISPersonExists(this.PersonID) && await UserValidtaor.IsUserIDExists(this.CreatedByUserID);
            }
            catch (Exception ex)
            {
                throw ex;

                //}
            }
            #endregion
        }
    }
}
