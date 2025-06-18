using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liscenses.Classes
{
    internal class ShowedTable
    {
        internal DataTable InternationalLicense()
        {
            DataTable dtr = new DataTable("InternationalLicenses");

            dtr.Columns.Add(setColumn("GloablLicenseID", typeof(int), null, false, true));
            dtr.Columns.Add(setColumn("IssuedUsingLocalLicenseID", typeof(int), null, false, false));
            dtr.Columns.Add(setColumn("NationalNo", typeof(string), 50, false, false));
            dtr.Columns.Add(setColumn("FullName", typeof(string), 255, false, false));
            dtr.Columns.Add(setColumn("IssueDate", typeof(DateTime), null, false, false));
            dtr.Columns.Add(setColumn("ExpirationDate", typeof(DateTime), null, false, false));
            dtr.Columns.Add(setColumn("IsActive", typeof(bool), null, false, false));
            dtr.PrimaryKey = new DataColumn[] { dtr.Columns["LDLGloablLicenseID"] };
            return dtr;
        }
        internal DataTable LocalDrivingLicenseApplications()
        {
            DataTable dtr = new DataTable("LocalDrivingLicenseApplications");

            dtr.Columns.Add(setColumn("LDLAppID", typeof(int), null, false, true));
            dtr.Columns.Add(setColumn("DrivingClass", typeof(string), 100, false, false));
            dtr.Columns.Add(setColumn("NationalNo", typeof(string), 50, false, false));
            dtr.Columns.Add(setColumn("FullName", typeof(string), 255, false, false));
            dtr.Columns.Add(setColumn("ApplicationDate", typeof(DateTime), null, false, false));
            dtr.Columns.Add(setColumn("PassedTests", typeof(int), null, false, false));
            dtr.Columns.Add(setColumn("Status", typeof(string), 50, false, false));
            dtr.PrimaryKey = new DataColumn[] { dtr.Columns["LDLAppID"] };

            return dtr;
        }
        internal DataTable LocalLicense()
        {
            DataTable dtr = new DataTable("Licenses");

            dtr.Columns.Add(setColumn("LDLicenseID", typeof(int), null, false, true));
            dtr.Columns.Add(setColumn("DrivingClass", typeof(string), 100, false, false));
            dtr.Columns.Add(setColumn("NationalNo", typeof(string), 50, false, false));
            dtr.Columns.Add(setColumn("FullName", typeof(string), 255, false, false));
            dtr.Columns.Add(setColumn("IssueDate", typeof(DateTime), null, false, false));
            dtr.Columns.Add(setColumn("ExpirationDate", typeof(DateTime), null, false, false));
            dtr.Columns.Add(setColumn("IsActive", typeof(bool), null, false, false));
            dtr.Columns.Add(setColumn("Status", typeof(string), 50, false, false));
            dtr.PrimaryKey = new DataColumn[] { dtr.Columns["LDLicenseID"] };

            return dtr;
        }
        private DataColumn setColumn(string Name, Type type, int? MaxLength, bool AllowDBNull, bool Unique)
        {
            DataColumn column = new DataColumn();
            column.ColumnName = Name;
            column.DataType = type;
            if (MaxLength != null)
                column.MaxLength = MaxLength.Value;
            column.AllowDBNull = AllowDBNull;
            column.Unique = Unique;
            return column;
        }
    }
}
