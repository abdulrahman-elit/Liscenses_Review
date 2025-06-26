using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using System.Data;
using MidLayer.Validation;
namespace EntityLayer.Classes
{


    public class DetainedLicense : ISupportedType<DetainedLicense>
    {
        #region Properties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "DetainID",             (SqlDbType.Int, null) },
        { "LicenseID",            (SqlDbType.Int, null) },
        { "DetainDate",           (SqlDbType.SmallDateTime, null) },
        { "FineFees",             (SqlDbType.SmallMoney, null) },
        { "CreatedByUserID",      (SqlDbType.Int, null) },
        { "IsReleased",           (SqlDbType.Bit, null) },
        { "ReleaseDate",          (SqlDbType.SmallDateTime, null) },
        { "ReleasedByUserID",     (SqlDbType.Int, null) },          
        { "ReleaseApplicationID", (SqlDbType.Int, null) }           
    };

        public enStatus status { set; get; }
        public string PrimaryIntColumnIDName { get; } = "DetainID";

        public int DetainID { get; private set; }
        public int LicenseID { get; private set; }
        public DateTime DetainDate { get; private set; }
        public decimal FineFees { get; private set; } 
        public int CreatedByUserID { get; private set; }
        public bool IsReleased { get; private set; }
        public DateTime? ReleaseDate { get; private set; } 
        public int? ReleasedByUserID { get; private set; } 
        public int? ReleaseApplicationID { get; private set; } 
        private int _PrimaryKey { set; get; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                _PrimaryKey = DetainID = id;
                return _PrimaryKey;
            }
            else
                return _PrimaryKey;
        }

        public Task FromDataRow(DataRow detainedLicenseRow)
        {
            if (detainedLicenseRow == null)
                throw new ArgumentNullException(nameof(detainedLicenseRow) + " Empty");

            this.DetainID = _PrimaryKey = Convert.ToInt32(detainedLicenseRow["DetainID"] ?? -1);
            this.setLicenseID(Convert.ToInt32(detainedLicenseRow["LicenseID"] ?? -1));
            this.setDetainDate(Convert.ToDateTime(detainedLicenseRow["DetainDate"] ?? DateTime.MinValue));
            this.setFineFees(Convert.ToDecimal(detainedLicenseRow["FineFees"] ?? 0m));
            this.setCreatedByUserID(Convert.ToInt32(detainedLicenseRow["CreatedByUserID"] ?? -1));
            this.setIsReleased(Convert.ToBoolean(detainedLicenseRow["IsReleased"] ?? false));

            // Handle nullable fields
            this.setReleaseDate(detainedLicenseRow["ReleaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(detainedLicenseRow["ReleaseDate"]));
            this.setReleasedByUserID(detainedLicenseRow["ReleasedByUserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(detainedLicenseRow["ReleasedByUserID"]));
            this.setReleaseApplicationID(detainedLicenseRow["ReleaseApplicationID"] == DBNull.Value ? (int?)null : Convert.ToInt32(detainedLicenseRow["ReleaseApplicationID"]));

            status = enStatus.Update;
            return Task.CompletedTask;
        }

        public  Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row;
            if (Add)
            {
                row = dt.NewRow();
                  DataColumn pkColumn = dt.Columns[PrimaryIntColumnIDName];
                if (pkColumn != null) pkColumn.AutoIncrement = false; // Only if column exists and needed
                row[nameof(DetainID)] = _PrimaryKey;
                // dt.Rows.Add(row); // Usually added outside this method
            }
            else
            {
                row = dt.Rows.Find(_PrimaryKey);
                if (row == null)
                {
                    throw new InvalidOperationException($"Row with Primary Key '{_PrimaryKey}' not found in the DataTable for DetainedLicenses.");
                }
            }

            if (row == null) 
                throw new InvalidOperationException($"Could not obtain a DataRow for DetainID '{_PrimaryKey}'.");


            row[nameof(LicenseID)] = this.LicenseID;
            row[nameof(DetainDate)] = this.DetainDate;
            row[nameof(FineFees)] = this.FineFees;
            row[nameof(CreatedByUserID)] = this.CreatedByUserID;
            row[nameof(IsReleased)] = this.IsReleased;

            row[nameof(ReleaseDate)] = this.ReleaseDate.HasValue ? (object)this.ReleaseDate.Value : DBNull.Value;
            row[nameof(ReleasedByUserID)] = this.ReleasedByUserID.HasValue ? (object)this.ReleasedByUserID.Value : DBNull.Value;
            row[nameof(ReleaseApplicationID)] = this.ReleaseApplicationID.HasValue ? (object)this.ReleaseApplicationID.Value : DBNull.Value;

              
            return Task.FromResult((dt, row));   
        }


        public void setLicenseID(int licenseID)
        {
             _ValidationService.validationID(licenseID); 
            this.LicenseID = licenseID;
        }

        public void setDetainDate(DateTime detainDate)
        {
            this.DetainDate = detainDate;
        }

        public void setFineFees(decimal fineFees)
        {
          if (fineFees >= 0)
            this.FineFees = fineFees;
        }

        public void setCreatedByUserID(int createdByUserID)
        {
            _ValidationService.validationID(createdByUserID);
            this.CreatedByUserID = createdByUserID;
        }

        public void setIsReleased(bool isReleased)
        {
            this.IsReleased = isReleased;
        }

        public void setReleaseDate(DateTime? releaseDate)
        {
            if (releaseDate.HasValue && releaseDate.Value < DetainDate)
                throw new ArgumentException("Release date cannot be before detain date.");
            this.ReleaseDate = releaseDate;
        }


        public void setReleasedByUserID(int? releasedByUserID)
        {
            if (releasedByUserID.HasValue)
                _ValidationService.validationID(releasedByUserID.Value);
                this.ReleasedByUserID = releasedByUserID;
        }

        public void setReleaseApplicationID(int? releaseApplicationID)
        {
            if (releaseApplicationID.HasValue)
                _ValidationService.validationID(releaseApplicationID.Value);
                this.ReleaseApplicationID = releaseApplicationID;
        }
        #endregion

        #region Constructors
         public DetainedLicense(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID,
                               bool isReleased, DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            _ValidationService.validationID(detainID);
            this.DetainID = _PrimaryKey = detainID;
            setLicenseID(licenseID);
            setDetainDate(detainDate);
            setFineFees(fineFees);
            setCreatedByUserID(createdByUserID);
            setIsReleased(isReleased);
            setReleaseDate(releaseDate); 
            setReleasedByUserID(releasedByUserID); 
            setReleaseApplicationID(releaseApplicationID); 

            status = enStatus.Update;
        }
     public DetainedLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {
            this.DetainID = _PrimaryKey = -1; 
            setLicenseID(licenseID);
            setDetainDate(detainDate);
            setFineFees(fineFees);
            setCreatedByUserID(createdByUserID);
            setIsReleased(false); 
            setReleaseDate(null); 
            setReleasedByUserID(null); 
            setReleaseApplicationID(null); 

            status = enStatus.New;
        }

        public DetainedLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID,
                               bool isReleased, DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            this.DetainID = _PrimaryKey = -1; 
            setLicenseID(licenseID);
            setDetainDate(detainDate);
            setFineFees(fineFees);
            setCreatedByUserID(createdByUserID);
            setIsReleased(isReleased);
            setReleaseDate(releaseDate);
            setReleasedByUserID(releasedByUserID);
            setReleaseApplicationID(releaseApplicationID);

            status = enStatus.New;
        }


        // Copy constructor
        public DetainedLicense(DetainedLicense detainedLicense)
        {
            _ValidationService.validationID(detainedLicense.DetainID);
            this.DetainID = _PrimaryKey = detainedLicense.DetainID;
            setLicenseID(detainedLicense.LicenseID);
            setDetainDate(detainedLicense.DetainDate);
            setFineFees(detainedLicense.FineFees);
            setCreatedByUserID(detainedLicense.CreatedByUserID);
            setIsReleased(detainedLicense.IsReleased);
            setReleaseDate(detainedLicense.ReleaseDate);
            setReleasedByUserID(detainedLicense.ReleasedByUserID);
            setReleaseApplicationID(detainedLicense.ReleaseApplicationID);

            status = enStatus.Update; 
        }
        public DetainedLicense(DataRow detainedLicenseRow)
        {
            if (detainedLicenseRow == null)
                throw new ArgumentNullException(nameof(detainedLicenseRow) + " Empty");

         
            FromDataRow(detainedLicenseRow);
          
        }
        #endregion

        #region Validation
        public async Task<bool> Validation()
        {
            try
            {
                if (this.IsReleased)
                {
                    if (!this.ReleaseDate.HasValue)
                       throw new Exception($"Validation Error for DetainID {_PrimaryKey}: ReleaseDate cannot be null if IsReleased is true.");
           
                    if (!this.ReleasedByUserID.HasValue)
                        throw new Exception($"Validation Error for DetainID {_PrimaryKey}: ReleasedByUserID cannot be null if IsReleased is true.");
                    if (this.ReleaseDate.Value < this.DetainDate)
                        throw new Exception($"Validation Error for DetainID {_PrimaryKey}: ReleaseDate cannot be before DetainDate.");
                }
                else 
                {
                    if (this.ReleaseDate.HasValue || this.ReleasedByUserID.HasValue || this.ReleaseApplicationID.HasValue)
                    {

                        throw new Exception($"Validation Warning for DetainID {_PrimaryKey}: Release information is present but IsReleased is false.");
                    }
                }

                return await LicenseValidator.IsLicenseExists(LicenseID)&& await UserValidtaor.IsUserIDExists(CreatedByUserID); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during DetainedLicense validation for ID {_PrimaryKey}: {ex.Message}");
            }
        }
        #endregion

       /* #region Business Logic Methods (Examples)

        public void Release(DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            if (IsReleased)
            {
                throw new InvalidOperationException($"License (DetainID: {DetainID}) is already released.");
            }
            if (releaseDate < DetainDate)
            {
                throw new ArgumentException("Release date cannot be before detain date.", nameof(releaseDate));
            }

            setReleaseDate(releaseDate);
            setReleasedByUserID(releasedByUserID);
            setReleaseApplicationID(releaseApplicationID);
            setIsReleased(true);

            // Consider changing status if this object is tracked for updates
            if (status != enStatus.New) // Don't change status if it's a new object being defined
            {
                status = enStatus.Update;
            }
        }

        #endregion*/
    }
}
