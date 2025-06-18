using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liscenses.Classes
{
    internal class SetColumnValue
    {
        internal void InternationalLicense(List<DataTable> dt, int i)
        {
            DataRow newRow = dt[4].NewRow();
            newRow["GloablLicenseID"] = Convert.ToInt32(dt[0].Rows[i]["InternationalLicenseID"]);
            newRow["IssuedUsingLocalLicenseID"] = Convert.ToInt32(dt[0].Rows[i]["IssuedUsingLocalLicenseID"]);
            var row = getApplicaionInfo(dt[2], Convert.ToInt32(dt[0].Rows[i]["ApplicationID"]));
            var list = getPersonFullName(dt[1], Convert.ToInt32(row["ApplicantPersonID"]));
            newRow["NationalNo"] = list[0];
            newRow["FullName"] = list[1];
            newRow["IssueDate"] = Convert.ToDateTime(dt[0].Rows[i]["IssueDate"]);
            newRow["ExpirationDate"] = Convert.ToDateTime(dt[0].Rows[i]["ExpirationDate"]);
            newRow["IsActive"] = Convert.ToBoolean(dt[0].Rows[i]["IsActive"]);
            dt[4].Rows.Add(newRow);
        }

        internal void LocalLicense(List<DataTable> dt, int i)
        {
            DataRow newRow = dt[4].NewRow();
            newRow["LDLicenseID"] = Convert.ToInt32(dt[0].Rows[i]["LicenseID"]);
            newRow["DrivingClass"] = getDrivingClass(dt[3], Convert.ToInt32(dt[0].Rows[i]["LicenseClass"]));
            var row = getApplicaionInfo(dt[2], Convert.ToInt32(dt[0].Rows[i]["ApplicationID"]));
            var list = getPersonFullName(dt[1], Convert.ToInt32(row["ApplicantPersonID"]));
            newRow["NationalNo"] = list[0];
            newRow["FullName"] = list[1];
            newRow["IssueDate"] = Convert.ToDateTime(dt[0].Rows[i]["IssueDate"]);
            newRow["ExpirationDate"] = Convert.ToDateTime(dt[0].Rows[i]["ExpirationDate"]);
            byte status = Convert.ToByte(dt[0].Rows[i]["IssueReason"]);
            newRow["IsActive"] = Convert.ToBoolean(dt[0].Rows[i]["IsActive"]);
            newRow["Status"] = IssueReason(status);
            dt[4].Rows.Add(newRow);
        }

        internal void LocalDrivingLicenseApplications(List<DataTable> dt, int i)
        {
            DataRow newRow = dt[4].NewRow();
            newRow["LDLAppID"] = Convert.ToInt32(dt[0].Rows[i]["LocalDrivingLicenseApplicationID"]);
            newRow["DrivingClass"] = getDrivingClass(dt[3], Convert.ToInt32(dt[0].Rows[i]["LicenseClassID"]));
            var row = getApplicaionInfo(dt[2], Convert.ToInt32(dt[0].Rows[i]["ApplicationID"]));
            var list = getPersonFullName(dt[1], Convert.ToInt32(row["ApplicantPersonID"]));
            newRow["NationalNo"] = list[0];
            newRow["FullName"] = list[1];
            newRow["ApplicationDate"] = row["ApplicationDate"];
            int status = Convert.ToInt32(row["ApplicationStatus"]);
            if (status == 4)
                newRow["PassedTests"] = 0;
            else
                newRow["PassedTests"] = status;
            newRow["Status"] = getStatus(status);
            dt[4].Rows.Add(newRow);
        }
        private DataRow getApplicaionInfo(DataTable dtc, int ID)
        {
            foreach (DataRow dr in dtc.Rows)
            {
                if (Convert.ToInt32(dr["ApplicationID"]) == ID)
                    return dr;
            }
            return null;
        }
        private List<string> getPersonFullName(DataTable dtc, int ID)
        {
            List<string> list = new List<string>();
            foreach (DataRow dr in dtc.Rows)
            {
                if (Convert.ToInt32(dr["PersonID"]) == ID)
                {
                    list.Add(dr["NationalNo"].ToString());
                    list.Add(dr["FirstName"].ToString() + " " + dr["SecondName"].ToString() + " " + dr["ThirdName"].ToString() + " " + dr["LastName"].ToString());
                    return list;
                }
            }
            return null;
        }
        private string getDrivingClass(DataTable dtc, int ID)
        {
            foreach (DataRow dr in dtc.Rows)
            {
                if (Convert.ToInt32(dr["LicenseClassID"]) == ID)
                    return dr["ClassName"].ToString();
            }
            return null;
        }
        private string IssueReason(byte reason) => reason switch { 1 => "First Time", 3 => "Damged ", 2 => "Replace", 4 => "Renew", 0 => "Detaind", _ => "Unknown" };
        private string getStatus(int status) => status switch { 3 => "Completed", 4 => "Cancel", _ => "New" };

    }
}
