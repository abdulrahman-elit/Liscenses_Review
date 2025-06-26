using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
using DataLayer.D_CRUDOperation.D_Read;
//using MidLayer.B_CRUDOperation.;
using System.Data;
using System.Runtime.CompilerServices;


namespace MidLayer.Validation
{
    public static class PersonValidator
    {
        #region Validation
        public static async Task<bool> ISPersonExists(int ID) =>await D_Check.Row("People", "PersonID", ID, Person.map).ConfigureAwait(false);
        //internal static bool checks
        public static async Task< bool> IsNationalNoExists(int PersonID, string nationalNo) => await Checks.Checked(PersonID, nationalNo, "NationalNo", "people", Person.map, "PersonID").ConfigureAwait(false);
        public static  async Task<bool> IsNationalNoExists(Person person) => await Checks.Checked(person, new HashSet<string> { "NationalNo" }, "people", Person.map).ConfigureAwait(false);
        public static async Task<bool> ISEmailExists(Person person) => await Checks.Checked(person, new HashSet<string> { "Email" }, "people", Person.map).ConfigureAwait(false);
        public static async Task<bool> ISEmailExists(int PersonID, string Email) => await Checks.Checked(PersonID, Email, "Email", "people", Person.map, "PersonID").ConfigureAwait(false);

        //public static bool ISEmailExists(string ID) => D_Check.Row("People", "Email", ID,Person.map);
        /*    public bool Validation(Person person)
            {

                try
                {
                    return
                         validationName(person.FirstName) && validationName(person.SecondName) && validationName(person.ThirdName) && validationName(person.LastName) &&
                         validationNationalNoOrAddress(person.NationalNo) && validationNationalNoOrAddress(person.Address) && validationPhone(person.Phone) &&
                         validationEmail(person.Email) && validationGendor(person.Gendor) && validationDateOfBirth(person.DateOfBirth) &&
                         validationNationalityCountryID(person.NationalityCountryID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
         */
        #endregion
    }
}