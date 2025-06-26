using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using System.Data.SqlClient;
using MidLayer;
using MidLayer.Validation;
using MidLayer.Process;

namespace EntityLayer.Classes
{


    public class InternationalLicense : ISupportedType<InternationalLicense>
    {
        #region Properties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "InternationalLicenseID",     (SqlDbType.Int, null) },
        { "ApplicationID",            (SqlDbType.Int, null) },
        { "DriverID",                 (SqlDbType.Int, null) },
        { "IssuedUsingLocalLicenseID", (SqlDbType.Int, null) },
        { "IssueDate",                (SqlDbType.SmallDateTime, null) },
        { "ExpirationDate",           (SqlDbType.SmallDateTime, null) },
        { "IsActive",                 (SqlDbType.Bit, null) },
        { "CreatedByUserID",          (SqlDbType.Int, null) }
    };
        public enStatus status { set; get; }
        public string PrimaryIntColumnIDName { get; } = "InternationalLicenseID";

        public int InternationalLicenseID { get; private set; }
        public int ApplicationID { get; private set; }
        public int DriverID { get; private set; }
        public int IssuedUsingLocalLicenseID { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActive { get; private set; }
        public int CreatedByUserID { get; private set; }

        private int _PrimaryKey { set; get; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                _PrimaryKey = InternationalLicenseID = id;
                return _PrimaryKey;
            }
            else
                return _PrimaryKey;
        }

        public Task FromDataRow(DataRow intlLicenseRow)
        {
            if (intlLicenseRow == null)
                throw new ArgumentNullException(nameof(intlLicenseRow) + " Empty");

            this.InternationalLicenseID = _PrimaryKey = Convert.ToInt32(intlLicenseRow["InternationalLicenseID"] ?? -1);
            this.setApplicationID(Convert.ToInt32(intlLicenseRow["ApplicationID"] ?? -1));
            this.setDriverID(Convert.ToInt32(intlLicenseRow["DriverID"] ?? -1));
            this.setIssuedUsingLocalLicenseID(Convert.ToInt32(intlLicenseRow["IssuedUsingLocalLicenseID"] ?? -1));
            this.setIssueDate(Convert.ToDateTime(intlLicenseRow["IssueDate"] ?? DateTime.MinValue));
            this.setExpirationDate(Convert.ToDateTime(intlLicenseRow["ExpirationDate"] ?? DateTime.MinValue));
            this.setIsActive(Convert.ToBoolean(intlLicenseRow["IsActive"] ?? false));
            this.setCreatedByUserID(Convert.ToInt32(intlLicenseRow["CreatedByUserID"] ?? -1));

            status = enStatus.Update;
            return Task.CompletedTask;
        }

        public Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns[PrimaryIntColumnIDName];
                if (pkColumn != null) pkColumn.AutoIncrement = false;
                row[nameof(InternationalLicenseID)] = _PrimaryKey;
                // dt.Rows.Add(row); // Usually added outside this method
            }
            else
            {
                row = dt.Rows.Find(_PrimaryKey);
                if (row == null)
                    throw new InvalidOperationException($"Row with Primary Key '{_PrimaryKey}' not found in the DataTable for InternationalLicenses.");
                
            }

            if (row == null) 
                throw new InvalidOperationException($"Could not obtain a DataRow for InternationalLicenseID '{_PrimaryKey}'.");


            row[nameof(ApplicationID)] = this.ApplicationID;
            row[nameof(DriverID)] = this.DriverID;
            row[nameof(IssuedUsingLocalLicenseID)] = this.IssuedUsingLocalLicenseID;
            row[nameof(IssueDate)] = this.IssueDate;
            row[nameof(ExpirationDate)] = this.ExpirationDate;
            row[nameof(IsActive)] = this.IsActive;
            row[nameof(CreatedByUserID)] = this.CreatedByUserID;
            //return Task.FromResult(row);
            return Task.FromResult((dt, row));
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

        public void setIssuedUsingLocalLicenseID(int issuedUsingLocalLicenseID)
        {
             _ValidationService.validationID(issuedUsingLocalLicenseID);
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
        }

        public void setIssueDate(DateTime issueDate)
        {
            if (ExpirationDate > DateTime.MinValue)
            {
                if (ExpirationDate < issueDate)
                    throw new ArgumentException("Expiration date cannot be before issue date.");

                this.IssueDate = issueDate;
            }
              
        }

        public void setExpirationDate(DateTime expirationDate)
        {
           
            if (expirationDate < IssueDate) 
                throw new ArgumentException("Expiration date cannot be before issue date.");
            
            this.ExpirationDate = expirationDate;
        }

        public void setIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }

        public void setCreatedByUserID(int createdByUserID)
        {
            _ValidationService.validationID(createdByUserID); 
            this.CreatedByUserID = createdByUserID;
        }
        #endregion

        #region Constructors
        public InternationalLicense(int InternationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID,
                                   DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            _ValidationService.validationID(InternationalLicenseID);
            this.InternationalLicenseID = _PrimaryKey = InternationalLicenseID;
            setApplicationID(applicationID);
            setDriverID(driverID);
            setIssuedUsingLocalLicenseID(issuedUsingLocalLicenseID);
            setExpirationDate(expirationDate); 
            setIssueDate(issueDate);
            setIsActive(isActive);
            setCreatedByUserID(createdByUserID);

            status = enStatus.Update;
        }
        public InternationalLicense(int applicationID, int driverID, int issuedUsingLocalLicenseID,
                                   DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            this.InternationalLicenseID = _PrimaryKey = -1; 
            setApplicationID(applicationID);
            setDriverID(driverID);
            setIssuedUsingLocalLicenseID(issuedUsingLocalLicenseID);
            setExpirationDate(expirationDate);
            setIssueDate(issueDate);
            setIsActive(isActive);
            setCreatedByUserID(createdByUserID);

            status = enStatus.New;
        }

        public InternationalLicense(InternationalLicense intlLicense)
        {
            _ValidationService.validationID(intlLicense.InternationalLicenseID);
            this.InternationalLicenseID = _PrimaryKey = intlLicense.InternationalLicenseID;
            setApplicationID(intlLicense.ApplicationID);
            setDriverID(intlLicense.DriverID);
            setIssuedUsingLocalLicenseID(intlLicense.IssuedUsingLocalLicenseID);
            setIssueDate(intlLicense.IssueDate);
            setExpirationDate(intlLicense.ExpirationDate);
            setIsActive(intlLicense.IsActive);
            setCreatedByUserID(intlLicense.CreatedByUserID);

            status =enStatus.Update; 
        }

        public InternationalLicense(DataRow intlLicenseRow)
        {
            if (intlLicenseRow == null)
                throw new ArgumentNullException(nameof(intlLicenseRow) + " Empty");

      
            FromDataRow(intlLicenseRow);
        }

        #endregion

        #region Validation
        public async Task<bool> Validation()
        {
            try
            {
              
                if (this.ExpirationDate <= this.IssueDate)
                    throw new Exception($"Validation Error for InternationalLicenseID {_PrimaryKey}: Expiration date must be after Issue Date.");

                // Example: Check if the referenced Local License exists and is active
                if ( ! await LicenseValidator.IsLicenseValidAndActive(ApplicationID))
                    throw new Exception($"Validation Error for InternationalLicenseID {_PrimaryKey}: Issued Local License ID {this.IssuedUsingLocalLicenseID} is invalid or inactive.");

                return
                   await DriverValidtaor.IsDriverExists(DriverID) &&
                   await UserValidtaor.IsUserIDExists(CreatedByUserID) &&
                   await LicenseValidator.IsLicenseExists(IssuedUsingLocalLicenseID);
                //return true;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error during InternationalLicense validation for ID {_PrimaryKey}: {ex.Message}");
            }
        }
        #endregion
    }
}
