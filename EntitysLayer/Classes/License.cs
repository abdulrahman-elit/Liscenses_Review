using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
//using MidLayer.Va;
//using MidLayer;
////using MiddleLayer.B_CRUDOperation.B_Read;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Messaging;
using MidLayer.Validation;
using MidLayer.B_CRUDOperation.B_Read;

namespace EntityLayer.Classes
{


    public class License : ISupportedType<License>
    {
        #region Properties
        private MyApplication _application;
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "LocalDrivingLicenseApplicationID", (SqlDbType.Int, null) },
        { "ApplicationID", (SqlDbType.Int, null) },
        { "LicenseClassID", (SqlDbType.Int, null) }
    };
        public HashSet<int> rowsID { set; get; }=new HashSet<int>();
       public enStatus status { set; get; }

        public int LocalDrivingLicenseApplicationID { private set; get; }
        public int ApplicationID { private set; get; }
        public int LicenseClassID { private set; get; }

         private int _PrimaryKey { set; get; }
        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                LocalDrivingLicenseApplicationID = _PrimaryKey = id;
                return _PrimaryKey;
            }
            return _PrimaryKey;
        }
        public MyApplication GetApplicaion()=>_application; 
        public void setApplicaionStatus(short status)
        {
            _application.setApplicationStatus(status);
        }
        public void setApplicationID(int applicationID)
        {
            //_ValidationService.validationID(applicationID);
            if(_application != null&&_application.ApplicationID==applicationID)
             this.ApplicationID = applicationID;
        }

        public void setLicenseClassID(int licenseClassID)
        {
            _ValidationService.validationID(licenseClassID);
                                                            
            this.LicenseClassID = licenseClassID;
        }
        private List<string> getPersonFullName(DataRow dtc)
        {
            List<string> list = new List<string>();


            list.Add(dtc["NationalNo"].ToString());
            list.Add(dtc["FirstName"].ToString() + " " + dtc["SecondName"].ToString() + " " + dtc["ThirdName"].ToString() + " " + dtc["LastName"].ToString());
            return list;
        }
        private string getStatus(int status)
        {
            return status switch
            {
                4 => "Cancel",
                3 => "Completed",
                _ => "New",
            };
        }
        private async Task<DataRow> setLocalLicenseRow(DataRow newRow)
        {
            B_Fetch cmb = new B_Fetch();
             DataTable _tab=   await cmb.Rows("People", null, "PersonID", true, true, _application.ApplicantPersonID);
            DataRow row =_tab?.Rows[0];

			var list = getPersonFullName(row);
            newRow["NationalNo"] = list[0];
            newRow["FullName"] = list[1];
            newRow["ApplicationDate"] = _application.ApplicationDate;
            if (_application.ApplicationStatus == 4)
                newRow["PassedTests"] = 0;
            else
                newRow["PassedTests"] = _application.ApplicationStatus;
            newRow["Status"] = getStatus(_application.ApplicationStatus);
            return newRow;
        }
        public void setApplication(MyApplication application)
        {
            if (application != null)
                this._application = application;
            else
                throw new Exception("The Application Can Not Be Null");
        }
        public  async Task setApplication(int ID=0)
        {
            B_Fetch cmb = new B_Fetch();
            DataRow row = null;
            if (ID > 0)
            {
                   DataTable _tab= await cmb.Rows("Applications", null, "ApplicationID", true, true, ApplicationID);
                row =_tab?.Rows[0]; 
			}
            else
            {
				DataTable _tab =    await cmb.Rows("LocalDrivingLicenseApplications", null, "LocalDrivingLicenseApplicationID", true, true, LocalDrivingLicenseApplicationID);
                row =_tab?.Rows[0];
				ApplicationID = Convert.ToInt32(row["ApplicationID"]);
                _tab = await cmb.Rows("Applications", null, "ApplicationID", true, true, ApplicationID);
                row = _tab?.Rows[0];

            }
            _application = new MyApplication(row);
        }
        public string PrimaryIntColumnIDName { get; } = "LocalDrivingLicenseApplicationID";

        #endregion

        #region Constructors

       private License(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            _ValidationService.validationID(localDrivingLicenseApplicationID);
           
            setApplicationID(applicationID);
            setLicenseClassID(licenseClassID);
            this.LocalDrivingLicenseApplicationID = _PrimaryKey = localDrivingLicenseApplicationID;
            this.status = enStatus.Update;
        }
        public static async  Task<License> CreateAsync(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            License license = new License(localDrivingLicenseApplicationID,applicationID,licenseClassID);
              await license.setApplication();
            return license;

        }
           public License(int applicationID, int licenseClassID)
        {
         
            setApplicationID(applicationID);
            setLicenseClassID(licenseClassID);
            this.LocalDrivingLicenseApplicationID = _PrimaryKey = -1; // Default for new records
            this.status = enStatus.New;
        }

       public License(License application)
        {
            if (application.LocalDrivingLicenseApplicationID > 0)
            {
                _ValidationService.validationID(application.LocalDrivingLicenseApplicationID);
                this.LocalDrivingLicenseApplicationID = _PrimaryKey = application.LocalDrivingLicenseApplicationID;
                this.status = enStatus.Update;
            }
            else
            {
                this.LocalDrivingLicenseApplicationID = _PrimaryKey = -1;
                this.status = enStatus.New;
            }
            setApplication(application._application);
            setApplicationID(application.ApplicationID);
            setLicenseClassID(application.LicenseClassID);

        }

        public License(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row), "The DataRow cannot be null.");
            this.status = enStatus.Update;
        }
        public static async Task<License> CreateAsync(DataRow row)
        {
            License license = new License(row);
            if(row.Table.Columns.Count>4)
            await license. FromDataRow2(row);
            else
             await license.  FromDataRow(row); 
            return license;

        }

        #endregion

        #region DataRow Handling

        public async Task FromDataRow2(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row), "The DataRow cannot be null.");

            int pkValue = Convert.ToInt32(row["LDLAppID"] ?? -1);
            this.LocalDrivingLicenseApplicationID = _PrimaryKey = pkValue;
            await setApplication(); 
     
            setLicenseClassID(Convert.ToInt32(getDrivingClass(row["DrivingClass"].ToString())));
            this.status = enStatus.Update;
            
        }
        public async Task FromDataRow(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row), "The DataRow cannot be null.");

            int pkValue = Convert.ToInt32(row["LocalDrivingLicenseApplicationID"] ?? -1);
        
            this.ApplicationID=Convert.ToInt32(row["ApplicationID"] ?? -1);
            await setApplication(1);
            setLicenseClassID(Convert.ToInt32(row["LicenseClassID"] ?? -1));
            this.LocalDrivingLicenseApplicationID = _PrimaryKey = pkValue;
            this.status = enStatus.Update;
        }

        public async Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row = null;
            
            if (Add)
            {
                row = dt.NewRow();
              
                if (_PrimaryKey > 0) 
                {
                    DataColumn pkColumn = dt.Columns["LDLAppID"];
                    if (pkColumn != null) 
                    {
                        //bool originalAutoIncrement = pkColumn.AutoIncrement;
                        pkColumn.AutoIncrement = false;
                        //row[nameof(LocalDrivingLicenseApplicationID)] = _PrimaryKey;
                        //pkColumn.AutoIncrement = originalAutoIncrement; // Restore setting
                    }
                        row["LDLAppID"] = _PrimaryKey;
                }
              
            }
            else 
            {
                if (_PrimaryKey <= 0)
                    throw new InvalidOperationException("Cannot find a DataRow without a valid Primary Key for update.");
                row = dt.Rows.Find(_PrimaryKey);
                if (row == null)
                    throw new InvalidOperationException($"Cannot find DataRow with Primary Key {_PrimaryKey} in the DataTable for update.");
            }
           row= await setLocalLicenseRow(row);
            row =    await ToDataRow(row);
            return (dt, row);
        }
        private async Task< string> getDrivingClass(string message=null)
        {

            B_Fetch cmb = new B_Fetch();
            DataTable _table = await cmb.Rows("LicenseClasses");

            //DataRow dr = cmb.Rows("LicenseClass",null, "LicenseClassID",true,true,LicenseClassID)?.Rows[0];
            foreach (DataRow dr in _table.Rows)
            {
                if (message == null)
                {
                    if (Convert.ToInt32(dr["LicenseClassID"]) == LicenseClassID)
                        return dr["ClassName"].ToString();
                }
                else
                {
                    if (message == dr["ClassName"].ToString())
                        return dr["LicenseClassID"].ToString();
                }

            }
            return null;

        }
        public Task<DataRow> ToDataRow(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            row["DrivingClass"] = getDrivingClass();
            //row[nameof(ApplicationID)] = this.ApplicationID;
            //row[nameof(LicenseClassID)] = this.LicenseClassID;
            return Task.FromResult(row);
        }

        #endregion

        #region Validation

      public async Task<bool> Validation()
        {
            try
            {
                return await ApplicationValidator.IsApplicantExists(ApplicationID)&& await LicenseClassValidator.IsLicenseClassExists(LicenseClassID); 
            }
            catch 
            {
                throw; 
            }
              /*  // Validate that the referenced Application and LicenseClass exist.
                // Assumes existence of static validator classes/methods.
                // Replace with your actual validation logic.
                bool isAppValid = ApplicationValidator.ISApplicationExists(this.ApplicationID);
                if (!isAppValid)
                {
                    // Consider logging or specific exception handling
                    Console.WriteLine($"Validation Failed: Application with ID {this.ApplicationID} does not exist.");
                    return false;
                }

                bool isClassValid = LicenseClassValidator.ISLicenseClassExists(this.LicenseClassID);
                if (!isClassValid)
                {
                    // Consider logging or specific exception handling
                    Console.WriteLine($"Validation Failed: License Class with ID {this.LicenseClassID} does not exist.");
                    return false;
                }
                */
        }
        #endregion
    }
}
#region tries
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EntityLayer.Interfaces;
//using MidLayer.Process;
//using MidLayer;
//using EntityLayer.Classes;

//namespace EntityLayer.Classes
//{
//    /*internal class LocalLicenseApplication
//    {

//    }
//    */
//    public class LocalLicenseApplication : ISupportedType<LocalLicenseApplication>
//    {
//        #region Praporties

//        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
//        {
//        { "LocalDrivingLicenseApplicationID", (SqlDbType.Int,null) },
//        { "ApplicationID", (SqlDbType.Int,null) },
//        { "LicenseClassID", (SqlDbType.Int,null) },

//        };
//        //private string trim;
//        public enStatus status { set; get; }
//        public int LocalDrivingLicenseApplicationID { private set; get; }
//        public int ApplicationID { private set; get; }
//        public int LicenseClassID { private set; get; } 
//        private int _PrimaryKey { set; get; }
//        public int PrimaryKey(int id = 0)
//        {
//            if (id > 0)
//            {
//                LocalDrivingLicenseApplicationID = _PrimaryKey = id;
//                return _PrimaryKey;
//            }
//            return _PrimaryKey;
//        }
//        public Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
//        {
//            DataRow row = null;
//            if (Add)
//            {
//                row = dt.NewRow();
//                DataColumn pkColumn = dt.Columns["LocalDrivingLicenseApplicationID"];
//                pkColumn.AutoIncrement = false;
//                row[$"{nameof(LocalDrivingLicenseApplicationID)}"] = _PrimaryKey;
//            }
//            else
//                row = dt.Rows.Find(_PrimaryKey);
//            return ToDataRow(row);
//        }
//        public Task<DataRow> ToDataRow(DataRow row)
//        {
//            row[$"{nameof(ApplicationID)}"] = this.ApplicationID;
//            row[$"{nameof(LicenseClassID)}"] = this.LicenseClassID;
//            return row;

//        }

//        public void setApplicationID(int ApplicationID)
//        {

//            _ValidationService.validationID(ApplicationID);
//            this.ApplicationID = ApplicationID;
//        }
//        public void setLicenseClassID(int LicenseClassID)
//        {

//            _ValidationService.validationID(LicenseClassID);
//            this.LicenseClassID = LicenseClassID;
//        }
//        public string PrimaryIntColumnIDName { get; } = "LocalLicenseApplicationID";
//        #endregion
//        #region Constractuers
//        public LocalLicenseApplication(int LocalLicenseApplicationID, int ApplicationID,int LicenseClassID)
//        {
//            _ValidationService.validationID(LocalLicenseApplicationID);
//            setApplicationID(ApplicationID);
//            setApplicationID(LicenseClassID);
//            this.LocalDrivingLicenseApplicationID = _PrimaryKey = LocalLicenseApplicationID;

//            status = enStatus.Update;
//        }
//        public LocalLicenseApplication(int ApplicationID, int LicenseClassID)
//        {
//            setApplicationID(ApplicationID);
//            setApplicationID(LicenseClassID);
//            this.LocalDrivingLicenseApplicationID = _PrimaryKey = -1;
//            status = enStatus.New;
//        }
//        public LocalLicenseApplication(LocalLicenseApplication LocalLicenseApplication)
//        {
//            _ValidationService.validationID(LocalLicenseApplication.LocalDrivingLicenseApplicationID);
//            setApplicationID(LocalLicenseApplication.ApplicationID);
//            setApplicationID(LocalLicenseApplication.LicenseClassID);

//            this.LocalDrivingLicenseApplicationID = _PrimaryKey = LocalLicenseApplication.LocalDrivingLicenseApplicationID;

//            status = enStatus.Update;
//        }
//        public LocalLicenseApplication(DataRow LocalLicenseApplicationRow)
//        {

//            if (LocalLicenseApplicationRow == null)
//                throw new ArgumentNullException(nameof(LocalLicenseApplicationRow), "The DataRow cannot be null.");

//            _ValidationService.validationID(Convert.ToInt32(LocalLicenseApplicationRow["LocalDrivingLicenseApplicationID"] ?? -1));
//            setApplicationID(Convert.ToInt32(LocalLicenseApplicationRow["ApplicationID"] ?? -1));
//            setLicenseClassID(Convert.ToInt32(LocalLicenseApplicationRow["LicenseClassID"] ?? -1));
//            LocalDrivingLicenseApplicationID = _PrimaryKey = Convert.ToInt32(LocalLicenseApplicationRow["LocalDrivingLicenseApplicationID"] ?? -1);
//            status = enStatus.Update;
//        }

//        public Task FromDataRow(DataRow LocalLicenseApplicationRow)
//        {
//            if (LocalLicenseApplicationRow == null)
//                throw new ArgumentNullException(nameof(LocalLicenseApplicationRow), "The DataRow cannot be null.");

//            _ValidationService.validationID(Convert.ToInt32(LocalLicenseApplicationRow["LocalDrivingLicenseApplicationID"] ?? -1));
//            setApplicationID(Convert.ToInt32(LocalLicenseApplicationRow["ApplicationID"] ?? -1));
//            setLicenseClassID(Convert.ToInt32(LocalLicenseApplicationRow["LicenseClassID"] ?? -1));
//            LocalDrivingLicenseApplicationID = _PrimaryKey = Convert.ToInt32(LocalLicenseApplicationRow["LocalDrivingLicenseApplicationID"] ?? -1);
//            status = enStatus.Update;
//        }
//        #endregion
//        #region Validation
//        public async Task<bool> Validation()
//        {
//            try
//            {
//                return true; //PersonValidator  LocalLicenseApplicationValidtor.IsLocalLicenseApplicationNameExists(LocalLicenseApplicationID, LocalLicenseApplicationName);
//                //return PersonValidator.ISPersonExists(LocalLicenseApplication.ApplicationID) && !LocalLicenseApplicationValidtor.IsLocalLicenseApplicationNameExists(LocalLicenseApplication.LocalLicenseApplicationName);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        #endregion

//    }
//}

#endregion