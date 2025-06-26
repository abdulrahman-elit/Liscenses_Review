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
    public class Test : ISupportedType<Test>
    {
        #region Properties

        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "TestID", (SqlDbType.Int, null) },
        { "TestAppointmentID", (SqlDbType.Int, null) },
        { "TestResult", (SqlDbType.Bit, null) },
        { "Notes", (SqlDbType.NVarChar, 500) },
        { "CreatedByUserID", (SqlDbType.Int, null) }
    };

        private string trim;
        public enStatus status { get; set; }

        public int TestID { get; private set; }
        public int TestAppointmentID { get; private set; }
        public bool TestResult { get; private set; }
        public string Notes { get; private set; }
        public int CreatedByUserID { get; private set; }

        private int _PrimaryKey { get; set; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                TestID = _PrimaryKey = id;
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
                DataColumn pkColumn = dt.Columns["TestID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(TestID)}"] = _PrimaryKey;
            }
            else
                row = dt.Rows.Find(_PrimaryKey);

               row =await  ToDataRow(row);
            return (dt, row);
        }

        public Task<DataRow> ToDataRow(DataRow row)
        {
            row[$"{nameof(TestAppointmentID)}"] = this.TestAppointmentID;
            row[$"{nameof(TestResult)}"] = this.TestResult;
            row[$"{nameof(Notes)}"] = this.Notes;
            row[$"{nameof(CreatedByUserID)}"] = this.CreatedByUserID;
            return Task.FromResult(row);
        }

        public void SetTestAppointmentID(int appointmentId)
        {
            _ValidationService.validationID(appointmentId);
            this.TestAppointmentID = appointmentId;
        }

        public void SetNotes(string notes)
        {
            if(notes != null)
            trim = notes.Trim();
            _ValidationService.ValidateNote(trim, @"^[a-zA-Z0-9\s.,!?-]{0,500}$");
            this.Notes = trim;
        }

        public void SetCreatedByUserID(int userId)
        {
            _ValidationService.validationID(userId);
            this.CreatedByUserID = userId;
        }
        public void setTestResult(bool  result)
        {
            this.TestResult = result;
        }
        public string PrimaryIntColumnIDName => "TestID";

        #endregion

        #region Constructors

        public Test(int testId, int appointmentId, bool result, string notes, int createdByUserId)
        {
            _ValidationService.validationID(testId);
            SetTestAppointmentID(appointmentId);
            SetNotes(notes);
            SetCreatedByUserID(createdByUserId);
            this.TestID = _PrimaryKey = testId;
            status = enStatus.Update;
        }

        public Test(int appointmentId, bool result, string notes, int createdByUserId)
        {
            SetTestAppointmentID(appointmentId);
            SetNotes(notes);
            SetCreatedByUserID(createdByUserId);
            this.TestID = _PrimaryKey = -1;
            status = enStatus.New;
        }

        public Test(Test test)
        {
            _ValidationService.validationID(test.TestID);
            SetTestAppointmentID(test.TestAppointmentID);
            SetNotes(test.Notes);
            SetCreatedByUserID(test.CreatedByUserID);
            this.TestID = _PrimaryKey = test.TestID;
            status = enStatus.Update;
        }

        public Test(DataRow testRow)
        {
            if (testRow == null)
                throw new ArgumentNullException(nameof(testRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(testRow["TestID"] ?? -1));
            SetTestAppointmentID(Convert.ToInt32(testRow["TestAppointmentID"] ?? -1));
            SetNotes(testRow["Notes"] as string);
            SetCreatedByUserID(Convert.ToInt32(testRow["CreatedByUserID"] ?? -1));
            TestID = _PrimaryKey = Convert.ToInt32(testRow["TestID"] ?? -1);
            status = enStatus.Update;
        }

        public Task FromDataRow(DataRow testRow)
        {
            if (testRow == null)
                throw new ArgumentNullException(nameof(testRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(testRow["TestID"] ?? -1));
            SetTestAppointmentID(Convert.ToInt32(testRow["TestAppointmentID"] ?? -1));
            SetNotes(testRow["Notes"] as string);
            SetCreatedByUserID(Convert.ToInt32(testRow["CreatedByUserID"] ?? -1));
            TestID = _PrimaryKey = Convert.ToInt32(testRow["TestID"] ?? -1);
            status = enStatus.Update;
            return Task.CompletedTask;
        }

        #endregion

        #region Validation

        public async Task<bool> Validation()
        {
            try
            {

                return await AppointmentValidator.IsTestAppointmentValid(TestAppointmentID) &&
                    await UserValidtaor.IsUserIDExists(CreatedByUserID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
