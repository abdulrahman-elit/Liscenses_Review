using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using EntityLayer.Interfaces;
////using MiddleLayer.B_CRUDOperation.B_Read;
using MidLayer;
using MidLayer.B_CRUDOperation.B_Read;
using MidLayer.Process;
using MidLayer.Validation;

namespace EntityLayer.Classes
{
    public class MyLicense  : ISupportedType<MyLicense> 
    {
        #region Properties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "LicenseID",        (SqlDbType.Int, null) },
        { "ApplicationID",    (SqlDbType.Int, null) },
        { "DriverID",         (SqlDbType.Int, null) },
        { "LicenseClass",     (SqlDbType.Int, null) },
        { "IssueDate",        (SqlDbType.DateTime, null) },
        { "ExpirationDate",   (SqlDbType.DateTime, null) },
        { "Notes",            (SqlDbType.NVarChar, 500) },
        { "PaidFees",         (SqlDbType.SmallMoney, null) },
        { "IsActive",         (SqlDbType.Bit, null) },
        { "IssueReason",      (SqlDbType.TinyInt, null) },
        { "CreatedByUserID",  (SqlDbType.Int, null) }
    };

        private string trim;
        public enStatus status { set; get; }
        public string PrimaryIntColumnIDName { get; } = "LicenseID";

        public int LicenseID { get; private set; }
        public int ApplicationID { get; private set; }
        public int DriverID { get; private set; }
        public int LicenseClass { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string Notes { get; private set; } 
        public decimal PaidFees { get; private set; } 
        public bool IsActive { get; private set; }
        public byte IssueReason { get; private set; }
        public int CreatedByUserID { get; private set; }

        private int _PrimaryKey { set; get; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                _PrimaryKey = LicenseID = id;
                return _PrimaryKey;
            }
            else
                return _PrimaryKey;
        }

        public Task FromDataRow(DataRow licenseRow)
        {
            if (licenseRow == null)
                throw new ArgumentNullException(nameof(licenseRow) + " Empty");

            this.LicenseID = _PrimaryKey = Convert.ToInt32(licenseRow["LicenseID"] ?? -1);
            this.setApplicationID(Convert.ToInt32(licenseRow["ApplicationID"] ?? -1));
            this.setDriverID(Convert.ToInt32(licenseRow["DriverID"] ?? -1));
            this.setLicenseClass(Convert.ToInt32(licenseRow["LicenseClass"] ?? -1));
            this.setExpirationDate(licenseRow["ExpirationDate"] as DateTime? ?? DateTime.MinValue);
            this.setIssueDate(licenseRow["IssueDate"] as DateTime? ?? DateTime.MinValue);
            this.setNotes(licenseRow["Notes"].ToString()); 
            this.setPaidFees(Convert.ToDecimal(licenseRow["PaidFees"] ?? 0m)); // Use decimal literal 0m
            this.setIsActive(Convert.ToBoolean(licenseRow["IsActive"] ?? false));
            this.setIssueReason(Convert.ToByte(licenseRow["IssueReason"] ?? 0));
            this.setCreatedByUserID(Convert.ToInt32(licenseRow["CreatedByUserID"] ?? -1));

            status = enStatus.Update;
            return Task.CompletedTask;
        }
        private async Task< DataRow> setLocalLicenseRow(DataRow newRow)
        {

            B_Fetch cmb = new B_Fetch();
            DataRow dtw = null;
            DataTable _tab=await cmb.Rows("Applications", null, "ApplicationID", true, true, ApplicationID);
            DataRow dty =_tab?.Rows[0];
               _tab=await cmb.Rows("LicenseClasses", null, "LicenseClassID", true, true, LicenseClass);
			DataRow dtz = _tab?.Rows[0];
            if (dty != null)
            {
                _tab=await cmb.Rows("People", null, "PersonID", true, true, Convert.ToInt32(dty["ApplicantPersonID"]));
                dtw =_tab?.Rows[0];
            }
            if (dtw != null && dtz != null)
            {
                newRow["LDLicenseID"] = Convert.ToInt32(LicenseID);
                newRow["DrivingClass"] = dtz["ClassName"].ToString();
                newRow["FullName"] = dtw["FirstName"].ToString() + " " + dtw["SecondName"].ToString() + " " + dtw["ThirdName"].ToString() + " " + dtw["LastName"].ToString();
                newRow["NationalNo"] = dtw["NationalNo"].ToString();
                newRow["ExpirationDate"] = Convert.ToDateTime(ExpirationDate);
                newRow["IssueDate"] = Convert.ToDateTime(IssueDate);
                if (IsActive)
                    newRow["IsActive"] = true;
                else
                    newRow["IsActive"] = false;

                newRow["Status"] = IssueReasoning();

                return newRow;

            }
            else
                return null;
            //return null;
        }
        private string IssueReasoning() => IssueReason switch {1=> "First Time",3=>"Damged ",2=>"Replace",4=>"Renew",0 =>"Detaind",_=>"Unknown" };
        public async Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns["LDLicenseID"];
                if (pkColumn != null) pkColumn.AutoIncrement = false; 
                row["LDLicenseID"] = _PrimaryKey;
                // dt.Rows.Add(row); // Usually added outside this method
            }
            else
            {
                row = dt.Rows.Find(_PrimaryKey);
                if (row == null)
                    throw new InvalidOperationException($"Row with Primary Key '{_PrimaryKey}' not found in the DataTable.");
            }

            if (row == null) 
                throw new InvalidOperationException($"Could not obtain a DataRow for LicenseID '{_PrimaryKey}'.");


            /* row[nameof(ApplicationID)] = this.ApplicationID;
             row[nameof(DriverID)] = this.DriverID;
             row[nameof(LicenseClass)] = this.LicenseClass;
             row[nameof(IssueDate)] = this.IssueDate;
             row[nameof(ExpirationDate)] = this.ExpirationDate;
             row[nameof(Notes)] = string.IsNullOrEmpty(this.Notes) ? (object)DBNull.Value : this.Notes;
             row[nameof(PaidFees)] = this.PaidFees;
             row[nameof(IsActive)] = this.IsActive;
             row[nameof(IssueReason)] = this.IssueReason;
             row[nameof(CreatedByUserID)] = this.CreatedByUserID;*/
              row=  await setLocalLicenseRow( row);
            return (dt, row);
        }

    
        public void setApplicationID(int applicationID)
        {
            _ValidationService.validationID(applicationID);
            this.ApplicationID = applicationID;
        }

        public void setDriverID(int driverID)
        {
            _ValidationService.validationID(driverID); 
            this.DriverID = driverID;
        }

        public void setLicenseClass(int licenseClass)
        {
            _ValidationService.validationID(licenseClass); 
            this.LicenseClass = licenseClass;
        }

        public void setIssueDate(DateTime issueDate)
        {
            if (ExpirationDate > DateTime.MinValue)
            {
                if (ExpirationDate < issueDate)
                    throw new ArgumentException("Expiration date cannot be before issue date.");
            }
            this.IssueDate = issueDate;
        }

        public void setExpirationDate(DateTime expirationDate)
        {
             if (expirationDate < IssueDate)
                throw new ArgumentException("Expiration date cannot be before issue date.");
            this.ExpirationDate = expirationDate;
        }

        public void setNotes(string notes)
        {
            if(notes != null)
            trim = notes.Trim();
            _ValidationService.ValidateNote(trim, @"^[a-zA-Z0-9\s.,!?-]{0,500}$");
            this.Notes = trim;
        }

        public void setPaidFees(decimal paidFees)
        {
            if(PaidFees >= 0)
            this.PaidFees = paidFees;
        }

        public void setIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }

        public void setIssueReason(byte issueReason)
        {
             if(issueReason>=0) 
            this.IssueReason = issueReason;
        }

        public void setCreatedByUserID(int createdByUserID)
        {
             _ValidationService.validationID(createdByUserID);
            this.CreatedByUserID = createdByUserID;
        }
        #endregion

        #region Constructors
        public MyLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate,
                       DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason,
                       int createdByUserID)
        {
            _ValidationService.validationID(licenseID);
            this.LicenseID = _PrimaryKey = licenseID;
            setApplicationID(applicationID);
            setDriverID(driverID);
            setLicenseClass(licenseClass);
            setExpirationDate(expirationDate);
            setIssueDate(issueDate);
            setNotes(notes);
            setPaidFees(paidFees);
            setIsActive(isActive);
            setIssueReason(issueReason);
            setCreatedByUserID(createdByUserID);

            status = enStatus.Update;
        }

       public MyLicense(int applicationID, int driverID, int licenseClass, DateTime issueDate,
                       DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason,
                       int createdByUserID)
        {
            this.LicenseID = _PrimaryKey = -1; 
            setApplicationID(applicationID);
            setDriverID(driverID);
            setLicenseClass(licenseClass);
            setExpirationDate(expirationDate);
            setIssueDate(issueDate);
            setNotes(notes);
            setPaidFees(paidFees);
            setIsActive(isActive);
            setIssueReason(issueReason);
            setCreatedByUserID(createdByUserID);

            status = enStatus.New;
        }

        // Copy constructor
        public MyLicense(MyLicense license)
        {
            _ValidationService.validationID(license.LicenseID);
            this.LicenseID = _PrimaryKey = license.LicenseID;
            setApplicationID(license.ApplicationID);
            setDriverID(license.DriverID);
            setLicenseClass(license.LicenseClass);
            setExpirationDate(license.ExpirationDate);
            setIssueDate(license.IssueDate);
            setNotes(license.Notes);
            setPaidFees(license.PaidFees);
            setIsActive(license.IsActive);
            setIssueReason(license.IssueReason);
            setCreatedByUserID(license.CreatedByUserID);

            status =enStatus.Update; 
        }

      
        public MyLicense(DataRow licenseRow)
        {
            if (licenseRow == null)
                throw new ArgumentNullException(nameof(licenseRow) + " Empty");

           
            FromDataRow(licenseRow);
          
        }

        #endregion

        #region Validation
        public async Task<bool> Validation()
        {
            try
            {
                if (this.ExpirationDate <= this.IssueDate)
                   throw new Exception($"Validation Error for LicenseID {_PrimaryKey}: Expiration date must be after Issue Date.");
                return await ApplicationValidator.IsApplicantExists(ApplicationID) && await LicenseClassValidator.IsLicenseClassExists(LicenseClass) &&
                  await  DriverValidtaor.IsDriverExists(DriverID)&&await UserValidtaor.IsUserIDExists(CreatedByUserID); 
                //return true;
            }
            catch 
            {
                throw; 
            }
        }
        #endregion
    }
}
