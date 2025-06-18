using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using MidLayer;
using MidLayer.Validation;

namespace EntityLayer.Classes
{
    public class TestType : ISupportedType<TestType>
    {
        #region Properties

        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "TestTypeID", (SqlDbType.Int, null) },
        { "TestTypeTitle", (SqlDbType.NVarChar, 100) },
        { "TestTypeDescription", (SqlDbType.NVarChar, 500) },
        { "TestTypeFees", (SqlDbType.SmallMoney, null) }
    };

        private string trim;
        public enStatus status { get;  set; }

        public int TestTypeID {  get; private set; }
        public string TestTypeTitle {  get; private set; }
        public string TestTypeDescription { get; private set; }
        public double TestTypeFees { get; private set; }

        private int _PrimaryKey { get; set; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                TestTypeID = _PrimaryKey = id;
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
                DataColumn pkColumn = dt.Columns["TestTypeID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(TestTypeID)}"] = _PrimaryKey;
            }
            else
                row = dt.Rows.Find(_PrimaryKey);

              row=await  ToDataRow(row);
            return (dt, row);
        }

        public Task<DataRow> ToDataRow(DataRow row)
        {
            row[$"{nameof(TestTypeTitle)}"] = this.TestTypeTitle;
            row[$"{nameof(TestTypeDescription)}"] = this.TestTypeDescription;
            row[$"{nameof(TestTypeFees)}"] = this.TestTypeFees;
            return Task.FromResult(row);
        }

        public void SetTestTypeTitle(string title)
        {
            trim = title.Trim();
            _ValidationService.Validate(trim, @"^[a-zA-Z0-9\s.,!?-]{1,100}$");
            this.TestTypeTitle = trim;
        }

        public void SetTestTypeDescription(string description)
        {
            trim = description.Trim();
            //_ValidationService.Validate(trim, @"^[a-zA-Z0-9\s.,!?-]{0,500}$");
            this.TestTypeDescription = trim;
        }

        public void SetTestTypeFees(double fees)
        {
            if (fees >= 0)
            this.TestTypeFees = fees;
        }

        public string PrimaryIntColumnIDName => "TestTypeID";

        #endregion

        #region Constructors

        public TestType(int testTypeId, string title, string description, double fees)
        {
            _ValidationService.validationID(testTypeId);
            SetTestTypeTitle(title);
            SetTestTypeDescription(description);
            SetTestTypeFees(fees);
            this.TestTypeID = _PrimaryKey = testTypeId;
            status = enStatus.Update;
        }

        public TestType(string title, string description, double fees)
        {
            SetTestTypeTitle(title);
            SetTestTypeDescription(description);
            SetTestTypeFees(fees);
            this.TestTypeID = _PrimaryKey = -1;
            status = enStatus.New;
        }

        public TestType(TestType testType)
        {
            _ValidationService.validationID(testType.TestTypeID);
            SetTestTypeTitle(testType.TestTypeTitle);
            SetTestTypeDescription(testType.TestTypeDescription);
            SetTestTypeFees(testType.TestTypeFees);
            this.TestTypeID = _PrimaryKey = testType.TestTypeID;
            status = enStatus.Update;
        }

        public TestType(DataRow testTypeRow)
        {
            if (testTypeRow == null)
                throw new ArgumentNullException(nameof(testTypeRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(testTypeRow["TestTypeID"] ?? -1));
            SetTestTypeTitle(testTypeRow["TestTypeTitle"].ToString());
            SetTestTypeDescription(testTypeRow["TestTypeDescription"].ToString());
            SetTestTypeFees(Convert.ToDouble(testTypeRow["TestTypeFees"]));
            TestTypeID = _PrimaryKey = Convert.ToInt32(testTypeRow["TestTypeID"] ?? -1);
            status = enStatus.Update;
        }

        public Task FromDataRow(DataRow testTypeRow)
        {
            if (testTypeRow == null)
                throw new ArgumentNullException(nameof(testTypeRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(testTypeRow["TestTypeID"] ?? -1));
            SetTestTypeTitle(testTypeRow["TestTypeTitle"].ToString());
            SetTestTypeDescription(testTypeRow["TestTypeDescription"].ToString());
            SetTestTypeFees(Convert.ToDouble(testTypeRow["TestTypeFees"]));
            TestTypeID = _PrimaryKey = Convert.ToInt32(testTypeRow["TestTypeID"] ?? -1);
            status = enStatus.Update;
            return Task.CompletedTask;
        }

        #endregion

        #region Validation

        public async Task<bool> Validation()
        {
            try
            {
                return await TestTypeValidator.IsTestTypeUnique(TestTypeID,TestTypeTitle)
                    &&await TestTypeValidator.IsValidFee(TestTypeID, TestTypeFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
