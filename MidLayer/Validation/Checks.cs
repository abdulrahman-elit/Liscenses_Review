using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
using EntityLayer.Interfaces;

namespace MidLayer.Validation
{
    static internal class Checks
    {
        static internal async Task<bool> Checked<T>(T value, HashSet<string> title, string tableName, Dictionary<string, (SqlDbType type, int? size)> map) where T : ISupportedType<T>
        {
            DataTable dt = null;
            dt = await D_Fetch.Rows(tableName, null, title, value, map).ConfigureAwait(false);
            if (dt == null || dt.Rows.Count == 0)
                return true;
            int x = Convert.ToInt32(dt?.Rows[0][value.PrimaryIntColumnIDName] ?? 0);
            if (dt.Rows.Count > 1 || x != value.PrimaryKey())
                throw new Exception("Already Been Used");
            else
                return true;
        }
        static internal async Task<bool> Checked<T>(int ID, T value, string title, string tableName, Dictionary<string, (SqlDbType type, int? size)> map, string PrimaryIntColumnIDName) where T : IConvertible
        {
            DataTable dt = await D_Fetch.Rows(tableName, new HashSet<string> { PrimaryIntColumnIDName, title }, title, value, map).ConfigureAwait(false);

            if (dt == null || dt.Rows.Count == 0)
                return true;

            var row = dt?.Rows[0];
            int existingPersonID = Convert.ToInt32(row[PrimaryIntColumnIDName]);

            if (existingPersonID == ID)
                return true;
            else
                throw new Exception("Already Been Used");
        }
        //static internal  bool Checked<T>(int ID, T value, string title,string tableName, Dictionary<string, (SqlDbType type, int? size)> map,string PrimaryIntColumnIDName) where T :IConvertible
        //static internal bool Checked<T>(T value, HashSet<string> title, string tableName, Dictionary<string, (SqlDbType type, int? size)> map) where T : ISupportedType<T>
        //{
        //    DataTable dt = null;
        //    dt = D_Fetch.Rows(tableName, null, title, value, map);
        //    if (dt == null || dt.Rows.Count == 0)
        //        return true;
        //    int x = Convert.ToInt32(dt?.Rows[0][value.PrimaryIntColumnIDName] ?? 0);
        //    if (dt.Rows.Count > 1 || x != value.PrimaryKey())
        //        throw new Exception("Already Been Used");
        //    else
        //        return true;
        //}
        //static internal bool Checked<T>(int ID, T value, string title, string tableName, Dictionary<string, (SqlDbType type, int? size)> map, string PrimaryIntColumnIDName) where T : IConvertible
        //{
        //    DataTable dt = D_Fetch.Rows(tableName, new HashSet<string> { PrimaryIntColumnIDName, title }, title, value, map);

        //    if (dt == null || dt.Rows.Count == 0)
        //        return true;

        //    var row = dt?.Rows[0];
        //    int existingPersonID = Convert.ToInt32(row[PrimaryIntColumnIDName]);

        //    if (existingPersonID == ID)
        //        return true;
        //    else
        //        throw new Exception("Already Been Used");
        //}

    }
}
/* 
static internal bool Checked(int ID, Dictionary<string,IConvertible> val, string tableName, Dictionary<string, (SqlDbType type, int? size)> map) 
      {
          DataTable dt = D_Fetch.Rows(tableName, null, val.Keys.First(), map);

          if (dt == null || dt.Rows.Count == 0)
              return true;

          //var row = dt?.Rows[0];
          foreach (DataRow dr in dt.Rows)
          {
              for (int i = 0;i<val.Count;i++)
              {
                  if(val.Values.ElementAt(i) is int x)

              }
          }
          int existingPersonID = Convert.ToInt32(row[map.Keys.First()]);
          if (existingPersonID == ID)
              return true;
          else
              throw new Exception("Already Been Used");
      }
*/
/*
static internal bool Checked(int ID, string value, string title, string tableName, Dictionary<string, (SqlDbType type, int? size)> map, string PrimaryIntColumnIDName)
{
    DataTable dt = D_Fetch.Rows(tableName, new HashSet<string> { PrimaryIntColumnIDName, title }, title, value, map);

    if (dt == null || dt.Rows.Count == 0)
        return true;

    var row = dt?.Rows[0];
    int existingPersonID = Convert.ToInt32(row[PrimaryIntColumnIDName]);

    if (existingPersonID == ID)
        return true;
    else
        throw new Exception("Alrady Been Used");
}
 //dt = D_Fetch.Rows(tableName, null, new HashSet<string> { "setApplicantPersonID", "ApplicationTypeID" }, A, MyApplication.map);
            //DataTable dt = D_Fetch.Rows(tableName,null, title, value, map);

            //if (dt == null || dt.Rows.Count == 0)
            //    return true;
            //if(dt.Rows.Count > 1 && IsNew)
            //    return false;
            //var row = dt?.Rows[0];
            //int existingPersonID = Convert.ToInt32(row[map.Keys.First()]);
            //if (existingPersonID == ID)
            //    return true;
            //else
            //    throw new Exception("Already Been Used");
*/