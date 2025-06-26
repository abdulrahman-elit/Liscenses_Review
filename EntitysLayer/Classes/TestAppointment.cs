using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using MidLayer.Process;
using MidLayer;
using MidLayer.Validation;

namespace EntityLayer.Classes
{
    public class TestAppointment : ISupportedType<TestAppointment>
    {
        #region Properties

        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "TestAppointmentID", (SqlDbType.Int, null) },
        { "TestTypeID", (SqlDbType.Int, null) },
        { "LocalDrivingLicenseApplicationID", (SqlDbType.Int, null) },
        { "AppointmentDate", (SqlDbType.SmallDateTime, null) },
        { "PaidFees", (SqlDbType.SmallMoney, null) },
        { "CreatedByUserID", (SqlDbType.Int, null) },
        { "IsLocked", (SqlDbType.Bit, null) }
    };

        //private string trim;
        public enStatus status { get; set; }

        public int TestAppointmentID {  get; private set; }
        public int TestTypeID {  get; private set; }
        public int LocalDrivingLicenseApplicationID {  get; private set; }
        public DateTime AppointmentDate {  get; private set; }
        public decimal PaidFees {  get; private set; }
        public int CreatedByUserID {  get; private set; }
        public bool IsLocked {  get; private set; }

        private int _PrimaryKey { get;  set; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                TestAppointmentID = _PrimaryKey = id;
                return _PrimaryKey;
            }
            return _PrimaryKey;
        }

        public async Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row = null;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns["TestAppointmentID"];
                if(pkColumn!=null)
                pkColumn.AutoIncrement = false;

                row[$"{nameof(TestAppointmentID)}"] = _PrimaryKey;
            }
            else
                row = dt.Rows.Find(_PrimaryKey);

               row =await ToDataRow(row);
            return (dt, row);   
        }

        public Task<DataRow> ToDataRow(DataRow row)
        {
            row[$"{nameof(TestTypeID)}"] = this.TestTypeID;
            row[$"{nameof(LocalDrivingLicenseApplicationID)}"] = this.LocalDrivingLicenseApplicationID;
            row[$"{nameof(AppointmentDate)}"] = this.AppointmentDate;
            row[$"{nameof(PaidFees)}"] = this.PaidFees;
            row[$"{nameof(CreatedByUserID)}"] = this.CreatedByUserID;
            row[$"{nameof(IsLocked)}"] = this.IsLocked;
            return Task.FromResult(row);
        }

        public void SetTestTypeID(int typeId)
        {
            _ValidationService.validationID(typeId);
            this.TestTypeID = typeId;
        }

        public void SetLocalDrivingLicenseApplicationID(int applicationId)
        {
            _ValidationService.validationID(applicationId);
            this.LocalDrivingLicenseApplicationID = applicationId;
        }

        public void SetAppointmentDate(DateTime date)
        {
            //_ValidationService.v(date, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(30));
            
            this.AppointmentDate = date;
    
        }

        public void SetPaidFees(decimal fees)
        {
                if (fees > 0)
                this.PaidFees = fees;
        }

        public void SetCreatedByUserID(int userId)
        {
            _ValidationService.validationID(userId);
            this.CreatedByUserID = userId;
        }

        public void SetIsLocked(bool locked)
        {
            this.IsLocked = locked;
        }

        public string PrimaryIntColumnIDName => "TestAppointmentID";

        #endregion

        #region Constructors

        public TestAppointment(int appointmentId, int typeId, int applicationId, DateTime date, decimal fees, int createdByUserId, bool isLocked)
        {
            _ValidationService.validationID(appointmentId);
            SetTestTypeID(typeId);
            SetLocalDrivingLicenseApplicationID(applicationId);
            SetAppointmentDate(date);
            SetPaidFees(fees);
            SetCreatedByUserID(createdByUserId);
            SetIsLocked(isLocked);
            this.TestAppointmentID = _PrimaryKey = appointmentId;
            status = enStatus.Update;
        }

        public TestAppointment(int typeId, int applicationId, DateTime date, decimal fees, int createdByUserId, bool isLocked)
        {
            SetTestTypeID(typeId);
            SetLocalDrivingLicenseApplicationID(applicationId);
            SetAppointmentDate(date);
            SetPaidFees(fees);
            SetCreatedByUserID(createdByUserId);
            SetIsLocked(isLocked);
            this.TestAppointmentID = _PrimaryKey = -1;
            status = enStatus.New;
        }

        public TestAppointment(TestAppointment appointment)
        {
            _ValidationService.validationID(appointment.TestAppointmentID);
            SetTestTypeID(appointment.TestTypeID);
            SetLocalDrivingLicenseApplicationID(appointment.LocalDrivingLicenseApplicationID);
            SetAppointmentDate(appointment.AppointmentDate);
            SetPaidFees(appointment.PaidFees);
            SetCreatedByUserID(appointment.CreatedByUserID);
            SetIsLocked(appointment.IsLocked);
            this.TestAppointmentID = _PrimaryKey = appointment.TestAppointmentID;
            status = enStatus.Update;
        }

        public TestAppointment(DataRow appointmentRow)
        {
            if (appointmentRow == null)
                throw new ArgumentNullException(nameof(appointmentRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(appointmentRow["TestAppointmentID"] ?? -1));
            SetTestTypeID(Convert.ToInt32(appointmentRow["TestTypeID"] ?? -1));
            SetLocalDrivingLicenseApplicationID(Convert.ToInt32(appointmentRow["LocalDrivingLicenseApplicationID"] ?? -1));
            SetAppointmentDate(Convert.ToDateTime(appointmentRow["AppointmentDate"]));
            SetPaidFees(Convert.ToDecimal(appointmentRow["PaidFees"]));
            SetCreatedByUserID(Convert.ToInt32(appointmentRow["CreatedByUserID"] ?? -1));
            SetIsLocked(Convert.ToBoolean(appointmentRow["IsLocked"] ?? false));
            TestAppointmentID = _PrimaryKey = Convert.ToInt32(appointmentRow["TestAppointmentID"] ?? -1);
            status = enStatus.Update;
        }

        public Task FromDataRow(DataRow appointmentRow)
        {
            if (appointmentRow == null)
                throw new ArgumentNullException(nameof(appointmentRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(appointmentRow["TestAppointmentID"] ?? -1));
            SetTestTypeID(Convert.ToInt32(appointmentRow["TestTypeID"] ?? -1));
            SetLocalDrivingLicenseApplicationID(Convert.ToInt32(appointmentRow["LocalDrivingLicenseApplicationID"] ?? -1));
            SetAppointmentDate(Convert.ToDateTime(appointmentRow["AppointmentDate"]));
            SetPaidFees(Convert.ToDecimal(appointmentRow["PaidFees"]));
            SetCreatedByUserID(Convert.ToInt32(appointmentRow["CreatedByUserID"] ?? -1));
            SetIsLocked(Convert.ToBoolean(appointmentRow["IsLocked"] ?? false));
            TestAppointmentID = _PrimaryKey = Convert.ToInt32(appointmentRow["TestAppointmentID"] ?? -1);
            status = enStatus.Update;
            return Task.CompletedTask;
        }

        #endregion

        #region Validation

        public async Task<bool> Validation()
        {
            try
            {
                return await TestTypeValidator.IsTestTypeValid(TestTypeID) &&
                    await LocalDrivingLicenseApplicationValidator.IsLocalDrivingLicenseApplicationIDExists(LocalDrivingLicenseApplicationID)
                    && await UserValidtaor.IsUserIDExists(CreatedByUserID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
