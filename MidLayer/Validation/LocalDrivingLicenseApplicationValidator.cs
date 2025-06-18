using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;

namespace MidLayer.Validation
{
    public static class LocalDrivingLicenseApplicationValidator
    {
        public static async Task<bool> IsLocalDrivingLicenseApplicationIDExists(int ID) =>await D_Check.Row("LocalDrivingLicenseApplications", "LocalDrivingLicenseApplicationID", ID, License.map).ConfigureAwait(false);
        //public static bool IsExceptedRequest(License _local)
        //{
        //    DataTable dt = null;
        //    dt = D_Fetch.Rows("LocalDrivingLicenseApplications", null, "", _local, License.map);
        //    if (dt == null || dt.Rows.Count == 0 || _local.rowsID.Count==0||_local.rowsID== null)
        //        return true;
        //    foreach (DataRow dr in dt.Rows)
        //       if (int.TryParse(dr["ApplicationID"].ToString(), out int currentTypeId))
        //            if (_local.rowsID.Contains(currentTypeId))
        //                if (int.TryParse(dr["LicenseClassID"].ToString(), out int Id) && Id == _local.LicenseClassID && _local.LocalDrivingLicenseApplicationID==-1)
        //                    throw new Exception("You Can Not Have Multi Application With Same License Class");
        //    return true;
        //}
        public static async Task<bool> IsExceptedRequest(License _local)
        {
            DataTable dt = null;
            dt =await D_Fetch.Rows("LocalDrivingLicenseApplications", null, "", _local, License.map).ConfigureAwait(false);
            if (dt == null || dt.Rows.Count == 0 || _local.rowsID.Count == 0 || _local.rowsID == null)
                return true;
            foreach (DataRow dr in dt.Rows)
                if (int.TryParse(dr["ApplicationID"].ToString(), out int currentTypeId))
                {
                    if (_local.rowsID.Contains(currentTypeId) && int.TryParse(dr["LicenseClassID"].ToString(), out int Id) && Id == _local.LicenseClassID && _local.LocalDrivingLicenseApplicationID == -1)
                    {
                        DataTable _table    =await D_Fetch.Rows("Applications", null, "ApplicationID", currentTypeId, MyApplication.map).ConfigureAwait(false);
                        DataRow drx =_table?.Rows[0];
                        if (drx != null)
                        {
                            if (int.TryParse(drx["ApplicationStatus"].ToString(), out int applicationTypeId))
                                if (applicationTypeId <= 3)
                                    throw new Exception("You Can Not Have Multi Application With Same License Class");
                        }
                    }
                    //    if ()
                    //        throw new Exception("You Can Not Have Multi Application With Same License Class");
                }
            return true;
        }

    }
}
/*
            //foreach (var row in _localrows)
            //for (int i = 0; i < ; i++)
            
            HashSet<int> result = new HashSet<int>();
            foreach (DataRow dr in dt.Rows) {
            {
                   result.Add( Convert.ToInt32(dr["ApplicationTypeID"]));
            }
            HashSet<int> list = new HashSet<int>();
            //for(int j = 0; j < result.Count; j++) 
            foreach (var row in _localrows)
            {
                if (result.Contains(row))
                    list.Add(row);
            }
            foreach(var row in list)
            {
                    DataRow r = dt.Rows.Contains(row);
                        if()
            }
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        if (Convert.ToInt32(row["ApplicationTypeID"]))
            //}
            // 4. Iterate through DataTable _localrows and check against the input HashSet
            }*/


