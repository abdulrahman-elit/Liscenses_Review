using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
using EntityLayer.Interfaces;
using MidLayer.Validation;
//using MidLayer;
//using MidLayer;
//using Mid
namespace EntityLayer.Classes
{
    public class Country
    {
        #region Praporties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "CountryID", (SqlDbType.Int,null) },
        { "CountryName", (SqlDbType.NVarChar, 50) },
              };
       public int CountryID { private set; get; }
        public string CountryName { private set; get; }
             #endregion
        #region Constractuers
        public Country(int CountryID, string CountryName)
        {
            _ValidationService.validationID(CountryID);
            _ValidationService.validationName(CountryName);
            this.CountryID = CountryID;
            this.CountryName = CountryName;
       
        }
        public Country(Country Country)
        {
            _ValidationService.validationID(Country.CountryID);
            _ValidationService.validationName(Country.CountryName);
           this.CountryID = Country.CountryID;
            this.CountryName = Country.CountryName;
          
        }
        public Country(DataRow CountryRow)
        {


            if (CountryRow == null)
                throw new ArgumentNullException(nameof(CountryRow), "The DataRow cannot be null.");
            _ValidationService.validationID(Convert.ToInt32(CountryRow["CountryID"] ?? -1));
            _ValidationService.validationName((CountryRow["CountryName"] as string));
            CountryID = Convert.ToInt32(CountryRow["CountryID"] ?? -1); // Handle DBNull
            CountryName = CountryRow["CountryName"] as string; // Handle DBNull

        }
        #endregion
     
    }
}
