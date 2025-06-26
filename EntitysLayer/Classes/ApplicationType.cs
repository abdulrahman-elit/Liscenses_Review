using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidLayer.Validation;
//using 
using EntityLayer.Interfaces;


namespace EntityLayer.Classes
{

    /*  public class ApplicationType :ISupportedType<ApplicationType>
      {
          #region Praporties
          public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
          {
          { "ApplicationTypeID", (SqlDbType.Int,null) },
          { "ApplicationTypeTitle", (SqlDbType.NVarChar, 150) },
          { "ApplicationFees", (SqlDbType.SmallMoney, null) },
                };
          public int ApplicationTypeID { private set; get; }
          public string ApplicationTypeTitle { private set; get; }
          public double ApplicationFees { private set; get; } = 0;

          public string PrimaryIntColumnIDName => throw new NotImplementedException();

          public enStatus status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
          #endregion
          #region Constractuers
          public ApplicationType(int ApplicationTypeID, string ApplicationTypeTitle,double ApplicationFees)
          {
              _ValidationService.validationID(ApplicationTypeID);
              _ValidationService.validationName(ApplicationTypeTitle);
              this.ApplicationTypeID = ApplicationTypeID;
              this.ApplicationTypeTitle = ApplicationTypeTitle;
              if(ApplicationFees > 0)
                  this.ApplicationFees = ApplicationFees; 

          }
          public ApplicationType(ApplicationType ApplicationType)
          {
              _ValidationService.validationID(ApplicationType.ApplicationTypeID);
              _ValidationService.validationName(ApplicationType.ApplicationTypeTitle);
              this.ApplicationTypeID = ApplicationType.ApplicationTypeID;
              this.ApplicationTypeTitle = ApplicationType.ApplicationTypeTitle;
              if (ApplicationType.ApplicationFees > 0)
                  this.ApplicationFees = ApplicationType.ApplicationFees;


          }
          public ApplicationType(DataRow ApplicationTypeRow)
          {


              if (ApplicationTypeRow == null)
                  throw new ArgumentNullException(nameof(ApplicationTypeRow), "The DataRow cannot be null.");
              _ValidationService.validationID(Convert.ToInt32(ApplicationTypeRow["ApplicationTypeID"] ?? -1));
              _ValidationService.validationName((ApplicationTypeRow["ApplicationTypeTitle"] as string));
              ApplicationTypeID = Convert.ToInt32(ApplicationTypeRow["ApplicationTypeID"] ?? -1); 
              ApplicationTypeTitle = ApplicationTypeRow["ApplicationTypeTitle"] as string;
              if (ApplicationTypeRow["ApplicationFees"] != DBNull.Value)
              {
                  ApplicationFees = Convert.ToDouble(ApplicationTypeRow["ApplicationFees"]);
              }
              else
              {
                  ApplicationFees = 0;
              }

          }

          public async Task<bool> Validation()
          {
              throw new NotImplementedException();
          }

          public int PrimaryKey(int id = 0)
          {
              throw new NotImplementedException();
          }

          public Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
          {
              throw new NotImplementedException();
          }

          public Task FromDataRow(DataRow dr)
          {
              throw new NotImplementedException();
          }
          #endregion


      }*/
    public class ApplicationType : ISupportedType<ApplicationType>
    {
        #region Properties

        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
    {
        { "ApplicationTypeID", (SqlDbType.Int, null) },
        { "ApplicationTypeTitle", (SqlDbType.NVarChar, 150) },
        { "ApplicationFees", (SqlDbType.SmallMoney, null) }
    };

        private string trim;
        public enStatus status { get; set; }

        public int ApplicationTypeID { get; private set; }
        public string ApplicationTypeTitle { get; private set; }
        public decimal ApplicationFees { get; private set; }

        private int _PrimaryKey { get; set; }

        public int PrimaryKey(int id = 0)
        {
            if (id > 0)
            {
                ApplicationTypeID = _PrimaryKey = id;
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
                DataColumn pkColumn = dt.Columns["ApplicationTypeID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(ApplicationTypeID)}"] = _PrimaryKey;
            }
            else
                row = dt.Rows.Find(_PrimaryKey);

               row= await _ToDataRow(row);
            return (dt, row);
        }

        public Task<DataRow> _ToDataRow(DataRow row)
        {
            row[$"{nameof(ApplicationTypeTitle)}"] = this.ApplicationTypeTitle;
            row[$"{nameof(ApplicationFees)}"] = this.ApplicationFees;
            return Task.FromResult(row);
        }

        public void SetApplicationTypeTitle(string title)
        {
            trim = title.Trim();
            _ValidationService.Validate(trim, @"^[a-zA-Z0-9\s.,!?-]{1,150}$");
            this.ApplicationTypeTitle = trim;
        }

        public void SetApplicationFees(decimal fees)
        {
            if (fees >= 0)
                this.ApplicationFees = fees;
        }

        public string PrimaryIntColumnIDName => "ApplicationTypeID";

        #endregion

        #region Constructors
        ////using MidLayer
        public ApplicationType(int applicationTypeId, string title, decimal fees)
        {
            _ValidationService.validationID(applicationTypeId);
            SetApplicationTypeTitle(title);
            SetApplicationFees(fees);
            this.ApplicationTypeID = _PrimaryKey = applicationTypeId;
            status = enStatus.Update;
        }

        public ApplicationType(string title, decimal fees)
        {
            SetApplicationTypeTitle(title);
            SetApplicationFees(fees);
            this.ApplicationTypeID = _PrimaryKey = -1;
            status = enStatus.New;
        }

        public ApplicationType(ApplicationType applicationType)
        {
            _ValidationService.validationID(applicationType.ApplicationTypeID);
            SetApplicationTypeTitle(applicationType.ApplicationTypeTitle);
            SetApplicationFees(applicationType.ApplicationFees);
            this.ApplicationTypeID = _PrimaryKey = applicationType.ApplicationTypeID;
            status = enStatus.Update;
        }

        public ApplicationType(DataRow applicationTypeRow)
        {
            if (applicationTypeRow == null)
                throw new ArgumentNullException(nameof(applicationTypeRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(applicationTypeRow["ApplicationTypeID"] ?? -1));
            SetApplicationTypeTitle(applicationTypeRow["ApplicationTypeTitle"].ToString());
            SetApplicationFees(Convert.ToDecimal(applicationTypeRow["ApplicationFees"]));
            ApplicationTypeID = _PrimaryKey = Convert.ToInt32(applicationTypeRow["ApplicationTypeID"] ?? -1);
            status = enStatus.Update;
        }

        public Task FromDataRow(DataRow applicationTypeRow)
        {
            if (applicationTypeRow == null)
                throw new ArgumentNullException(nameof(applicationTypeRow), "The DataRow cannot be null.");

            _ValidationService.validationID(Convert.ToInt32(applicationTypeRow["ApplicationTypeID"] ?? -1));
            SetApplicationTypeTitle(applicationTypeRow["ApplicationTypeTitle"].ToString());
            SetApplicationFees(Convert.ToDecimal(applicationTypeRow["ApplicationFees"]));
            ApplicationTypeID = _PrimaryKey = Convert.ToInt32(applicationTypeRow["ApplicationTypeID"] ?? -1);
            status = enStatus.Update;
            return Task.CompletedTask;
        }

        #endregion

        #region Validation

        public async Task<bool> Validation()
        {
            try
            {
                return await ApplicationTypeValidator.IsApplicationTypeUnique(ApplicationTypeID, ApplicationTypeTitle)
                    && await ApplicationTypeValidator.IsValidFee(ApplicationTypeID, ApplicationFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
