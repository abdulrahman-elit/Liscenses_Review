using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using EntityLayer.Interfaces;

using MidLayer.Validation;
using MidLayer.Process;
using System.Threading.Tasks;


namespace EntityLayer.Classes
{
    public class MyApplication : ISupportedType<MyApplication>
    {

        #region Praporties
      
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "ApplicationID", (SqlDbType.Int,null) },
        { "ApplicantPersonID", (SqlDbType.Int,null) },
        { "ApplicationDate", (SqlDbType.DateTime, null) },
        { "ApplicationTypeID", (SqlDbType.Int,null) },
        { "ApplicationStatus", (SqlDbType.TinyInt,null) },
        { "LastStatusDate", (SqlDbType.DateTime, null) },
        { "PaidFees", (SqlDbType.SmallMoney,null) },
        { "CreatedByUserID", (SqlDbType.Int, null) },
              };
        //private string trim;
        public enStatus status {  set; get; }
        public string PrimaryIntColumnIDName { get; } = "ApplicationID";
        public int ApplicationID { get; private set; }
        public DateTime ApplicationDate { private set; get; }
        public DateTime LastStatusDate { private set; get; }
        public short ApplicationTypeID { private set; get; }
        public short ApplicationStatus {  private set; get; }=0;
        public int ApplicantPersonID { private set; get; }
        public int CreatedByUserID { private set; get; }
        public decimal PaidFees { private set; get; }
        private int _PrimaryKey {  set; get; }
        public int PrimaryKey(int id=0)
        {
            if (id > 0)
            {
                _PrimaryKey = ApplicationID = id;
                return _PrimaryKey;
            }
            else
                return _PrimaryKey;
        }
        public Task FromDataRow(DataRow ApplicationRow)
        {

            if (ApplicationRow == null)
                throw new ArgumentNullException(nameof(ApplicationRow) + " Empty");
            this.ApplicationID = _PrimaryKey = Convert.ToInt32(ApplicationRow["ApplicationID"] ?? -1); // Handle DBNull
            this.setApplicantPersonID(Convert.ToInt32(ApplicationRow["ApplicantPersonID"] ?? -1));
            this.setApplicationTypeID(Convert.ToInt16(ApplicationRow["ApplicationTypeID"] ?? -1));
            this.setCreatedByUserID(Convert.ToInt32(ApplicationRow["CreatedByUserID"] ?? -1));
            this.setPaidFees(Convert.ToDecimal(ApplicationRow["PaidFees"] ?? 0));
            this.setApplicationDate(ApplicationRow["ApplicationDate"] as DateTime? ?? DateTime.MinValue);
            this.setLastStatusDate(ApplicationRow["LastStatusDate"] as DateTime? ?? DateTime.MinValue);
           
            status = enStatus.Update;
            return Task.CompletedTask;
        }
      
        public   Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {

            DataRow row=null;
            if (Add)
            {
                row = dt.NewRow();
                status = enStatus.New;
                DataColumn pkColumn = dt.Columns[dt.Columns[0].ColumnName];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(ApplicationID)}"] = _PrimaryKey;
             
            }
            else
                row = dt.Rows.Find(_PrimaryKey);
            row[$"{nameof(setApplicantPersonID)}"] = this.ApplicantPersonID;
            row[$"{nameof(ApplicationTypeID)}"] = this.ApplicationTypeID;
            row[$"{nameof(CreatedByUserID)}"] = this.CreatedByUserID;
            row[$"{nameof(PaidFees)}"] = this.PaidFees;
            row[$"{nameof(ApplicationDate)}"] = this.ApplicationDate;
            row[$"{nameof(LastStatusDate)}"] = this.LastStatusDate;
            //await Task.FromResult(row);
            return Task.FromResult((dt, row));

        }
        public void setApplicantPersonID(int ApplicantPersonID)
        {
            _ValidationService.validationID(ApplicantPersonID);
                this.ApplicantPersonID = ApplicantPersonID;
        }
        public void setApplicationTypeID(short ApplicationTypeID)
        {
            if (ApplicationTypeID >0)
            this.ApplicationTypeID = ApplicationTypeID;
        }
        public void setApplicationStatus(short ApplicationStatus)
        {
            if (ApplicationStatus <= 4 && ApplicationStatus >= 0)
                this.ApplicationStatus = ApplicationStatus;
        }
        public void setCreatedByUserID(int CreatedByUserID)
        {
            _ValidationService.validationID(CreatedByUserID);
            this.CreatedByUserID = CreatedByUserID;
        }
        public void setPaidFees(decimal PaidFees)
        {
           if (PaidFees > 0) 
            this.PaidFees = PaidFees;
        }
        public void setApplicationDate(DateTime ApplicationDate)
        {
           
            this.ApplicationDate = ApplicationDate;
        }
        public void setLastStatusDate(DateTime LastStatusDate)
        {
           
                this.LastStatusDate = LastStatusDate;
        }
        #endregion
        #region Constractuers
        public MyApplication(int ApplicationID,int ApplicantPersonID, int CreatedByUserID, short ApplicationTypeID,DateTime ApplicationDate,
            DateTime LastStatusDate,decimal PaidFees)
        {
            _ValidationService.validationID(ApplicationID);
            this.ApplicationID=_PrimaryKey = ApplicationID ;
            setApplicationDate(ApplicationDate);
            setApplicantPersonID(ApplicantPersonID);
            setApplicationTypeID(ApplicationTypeID);
            setCreatedByUserID(CreatedByUserID);
            setLastStatusDate(LastStatusDate);
            setPaidFees(PaidFees);
           
           
            status = enStatus.Update;
        }
        public MyApplication(int ApplicantPersonID, int CreatedByUserID, short ApplicationTypeID, DateTime ApplicationDate,
            DateTime LastStatusDate, decimal PaidFees)
        {

            this.ApplicationID=_PrimaryKey = -1;
            setApplicationDate(ApplicationDate);
            setApplicantPersonID(ApplicantPersonID);
            setApplicationTypeID(ApplicationTypeID);
            setCreatedByUserID(CreatedByUserID);
            setLastStatusDate(LastStatusDate);
            setPaidFees(PaidFees);
            status = enStatus.New;
        }
        public MyApplication(MyApplication Application)
        {
           _ValidationService.validationID(Application.ApplicationID);
            this.ApplicationID =_PrimaryKey= Application.ApplicationID;
          
            setApplicationDate(Application.ApplicationDate);
            setApplicantPersonID(Application.ApplicantPersonID);
            setApplicationTypeID(Application.ApplicationTypeID);
            setCreatedByUserID(Application.CreatedByUserID);
            setLastStatusDate(Application.LastStatusDate);
            setPaidFees(Application.PaidFees);

            status = enStatus.Update;
         
        }

        public MyApplication(DataRow ApplicationRow)
        {

            if (ApplicationRow == null)
                throw new ArgumentNullException(nameof(ApplicationRow)+" Empty");
            this.ApplicationID = _PrimaryKey = Convert.ToInt32(ApplicationRow["ApplicationID"] ?? -1); // Handle DBNull
            this.setApplicantPersonID(Convert.ToInt32(ApplicationRow["ApplicantPersonID"] ?? -1));
            this.setApplicationTypeID(Convert.ToInt16(ApplicationRow["ApplicationTypeID"] ?? -1));
            this.setCreatedByUserID(Convert.ToInt32(ApplicationRow["CreatedByUserID"] ?? -1));
            this.setApplicationStatus(Convert.ToInt16(ApplicationRow["ApplicationStatus"] ?? -1));
            this.setPaidFees(Convert.ToDecimal(ApplicationRow["PaidFees"] ?? 0));
            this.setApplicationDate(ApplicationRow["ApplicationDate"] as DateTime? ?? DateTime.MinValue);
            this.setLastStatusDate(ApplicationRow["LastStatusDate"] as DateTime? ?? DateTime.MinValue);
            this.ApplicationID =_PrimaryKey= Convert.ToInt32(ApplicationRow["ApplicationID"] ?? -1); // Handle DBNull
            status = enStatus.Update;
         
        }
        #endregion
        #region Validation
        public   async Task<bool> Validation()
        {
            try
            {
                return await PersonValidator.ISPersonExists(ApplicantPersonID) &&await UserValidtaor.IsUserIDExists(CreatedByUserID);
            }
            catch 
            {
                throw ;
            }
            
        }
        #endregion
    }
}
