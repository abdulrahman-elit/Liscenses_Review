using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Data;
    using EntityLayer.Interfaces;
using MidLayer.Validation;
using MidLayer.Process;
using MidLayer;

namespace EntityLayer.Classes
{

    public class LicenseClass  : ISupportedType<LicenseClass> 
    {
        #region Properties

       
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "LicenseClassID",        (SqlDbType.Int,       null) }, 
        { "ClassName",             (SqlDbType.NVarChar,  50  ) },
        { "ClassDescription",      (SqlDbType.NVarChar,  500  ) },
        { "MinimumAllowedAge",     (SqlDbType.TinyInt,   null ) },
        { "DefaultValidityLength", (SqlDbType.TinyInt,   null ) },
        { "ClassFees",             (SqlDbType.SmallMoney,   null) }  
    };

        private string _trim; 
        public enStatus status { set; get; } 
        public int LicenseClassID { private set; get; }
        public string ClassName { private set; get; }
        public string ClassDescription { private set; get; }
        public byte MinimumAllowedAge { private set; get; } 
        public byte DefaultValidityLength { private set; get; } 
        public decimal ClassFees { private set; get; }

        private int _PrimaryKey { set; get; }
        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                LicenseClassID = _PrimaryKey = id;
                return _PrimaryKey;
            }
            return _PrimaryKey;
        }
        public string PrimaryIntColumnIDName { get; } = "LicenseClassID";

        #endregion

        #region DataRow 

        public async Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row = null;
            if (Add)
            {
                row = dt.NewRow();
                // DataColumn pkColumn = dt.Columns["LicenseClassID"];
                // pkColumn.AutoIncrement = false; // Temporarily disable auto-increment if needed
                // row[nameof(LicenseClassID)] = _PrimaryKey;
            }
            else
            {
                //if (dt.PrimaryKey == null || dt.PrimaryKey.Length == 0)
                //{
                //    // Set the Primary Key if not already set (essential for Find)
                //    dt.PrimaryKey = new DataColumn[] { dt.Columns[PrimaryIntColumnIDName] };
                //}
                row = dt.Rows.Find(_PrimaryKey);
            }
         
            if (row == null && Add) 
                row = dt.NewRow();
            else if (row == null && !Add) 
                throw new Exception($"Could not find LicenseClass with ID {_PrimaryKey} in the DataTable.");


              row= await ToDataRow(row); 
            return (dt, row);
        }

        public Task<DataRow> ToDataRow(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

          
            if (status == enStatus.New && _PrimaryKey <= 0) 
            {
                // Often, for new rows with identity PK, you don't set the ID column value here.
                // row[nameof(LicenseClassID)] = DBNull.Value; // Or omit setting it
            }
            else if (_PrimaryKey > 0)
            {
                row[nameof(LicenseClassID)] = this.LicenseClassID;
            }


            row[nameof(ClassName)] = this.ClassName;
            row[nameof(ClassDescription)] = this.ClassDescription;
            row[nameof(MinimumAllowedAge)] = this.MinimumAllowedAge;
            row[nameof(DefaultValidityLength)] = this.DefaultValidityLength;
            row[nameof(ClassFees)] = this.ClassFees;
            return Task.FromResult(row);
        }

        #endregion

        #region Setters with Validation 

        public void setClassName(string className)
        {
            _trim = className?.Trim();
            if (string.IsNullOrEmpty(_trim))
            {
                throw new ArgumentException("Class Name cannot be empty.", nameof(className));
            }
            if (_trim.Length > (map[nameof(ClassName)].size ?? 50)) 
            {
                throw new ArgumentException($"Class Name cannot exceed {map[nameof(ClassName)].size} characters.", nameof(className));
            }
             _ValidationService.Validate(_trim);
            this.ClassName = _trim;
        }

        public void setClassDescription(string classDescription)
        {
            _trim = classDescription?.Trim();
            if (_trim != null && _trim.Length > (map[nameof(ClassDescription)].size ?? 500))
            {
                throw new ArgumentException($"Class Description cannot exceed {map[nameof(ClassDescription)].size} characters.", nameof(classDescription));
            }
            this.ClassDescription = _trim ?? string.Empty; 
        }

        public void setMinimumAllowedAge(byte minimumAllowedAge)
        {
         
            if (minimumAllowedAge <= 16) 
            {
                throw new ArgumentException("Minimum Allowed Age must be 16.", nameof(minimumAllowedAge));
            }
             if(minimumAllowedAge>= 16 && minimumAllowedAge<=  120)
            this.MinimumAllowedAge = minimumAllowedAge;
        }

        public void setDefaultValidityLength(byte defaultValidityLength)
        {
          
            if (defaultValidityLength <= 0)
            {
                throw new ArgumentException("Default Validity Length must be positive.", nameof(defaultValidityLength));
            }

            if (DefaultValidityLength >= 1 && DefaultValidityLength <= 20) 
            this.DefaultValidityLength = defaultValidityLength;
        }

        public void setClassFees(decimal classFees)
        {

            if (classFees < 0)
            {
                throw new ArgumentException("Class Fees cannot be negative.", nameof(classFees));
            }
           this.ClassFees = classFees;
        }

        #endregion

        #region Constructors

       
        public LicenseClass(int licenseClassID, string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
           
             _ValidationService.validationID(licenseClassID); 
            //if (licenseClassID <= 0)
                //throw new ArgumentException("License Class ID must be positive for an existing record.", nameof(licenseClassID));

            setClassName(className);
            setClassDescription(classDescription);
            setMinimumAllowedAge(minimumAllowedAge);
            setDefaultValidityLength(defaultValidityLength);
            setClassFees(classFees);

            this.LicenseClassID = _PrimaryKey = licenseClassID;
            this.status = enStatus.Update; 
        }

        public LicenseClass(string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            setClassName(className);
            setClassDescription(classDescription);
            setMinimumAllowedAge(minimumAllowedAge);
            setDefaultValidityLength(defaultValidityLength);
            setClassFees(classFees);

            this.LicenseClassID = _PrimaryKey = -1; 
            this.status = enStatus.New;
        }

        public LicenseClass(LicenseClass other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            setClassName(other.ClassName);
            setClassDescription(other.ClassDescription);
            setMinimumAllowedAge(other.MinimumAllowedAge);
            setDefaultValidityLength(other.DefaultValidityLength);
            setClassFees(other.ClassFees);

            this.LicenseClassID = _PrimaryKey = other.LicenseClassID;
            this.status = other.status; 
        }

        public LicenseClass(DataRow classRow)
        {
            if (classRow == null)
                throw new ArgumentNullException(nameof(classRow), "The DataRow cannot be null.");
            FromDataRow(classRow);
        }

        public Task FromDataRow(DataRow classRow)
        {
            if (classRow == null)
                throw new ArgumentNullException(nameof(classRow), "The DataRow cannot be null.");


            int id = Convert.ToInt32(classRow[nameof(LicenseClassID)] ?? -1);
            if (id <= 0)
                throw new InvalidOperationException($"Invalid {nameof(LicenseClassID)} found in DataRow.");
            this.LicenseClassID = _PrimaryKey = id;



            setClassName(classRow[nameof(ClassName)] as string);
            setClassDescription(classRow[nameof(ClassDescription)] as string);
            setMinimumAllowedAge(Convert.ToByte(classRow[nameof(MinimumAllowedAge)] ?? 0));
            setDefaultValidityLength(Convert.ToByte(classRow[nameof(DefaultValidityLength)] ?? 0));
            setClassFees(Convert.ToDecimal(classRow[nameof(ClassFees)] ?? 0m));
            this.status = enStatus.Update; 
            return Task.CompletedTask;
        }

        #endregion

        #region Validation 

        
        public async Task<bool> Validation()
        {
            try
            {
                return await LicenseClassValidator.IsClassNameUnique(LicenseClassID,ClassName);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }

}
