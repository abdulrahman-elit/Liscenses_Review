using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Classes;
//using EntityLayer.Classes;
namespace MidLayer.Validation
{
    public static class _ValidationService 
    {

        //In the dictionary, the key is the table name and the value is another dictionary. 
        //In the inner dictionary, the key is the column name and the value is a tuple.
        //The Keys Must be in Lower Case.
        static Dictionary<string, HashSet<string>> ValidationList = new Dictionary<string, HashSet<string>>
        {
            { "people",Person.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "users",User.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "drivers",Driver.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "countries",Country.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "applicationtypes",ApplicationType.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "applications",MyApplication.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "licenseclasses",LicenseClass.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "localdrivinglicenseapplications",License.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "testappointments",TestAppointment.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "testtypes",TestType.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "tests",Test.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "licenses",MyLicense.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "internationallicenses",InternationalLicense.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },
            { "detainedlicenses",DetainedLicense.map.Keys.Select(key=> key.ToLowerInvariant()).ToHashSet() },

        //InternationalLicense
        };
        public static Dictionary<string, (SqlDbType type, int? size)> ParameterName(string TableName)
        {
            return TableName.ToLowerInvariant() switch
            {
                "people" => Person.map,
                "drivers" => Driver.map,
                "users" => User.map,
                "countries" => Country.map,
                "applicationtypes" => ApplicationType.map,
                "applications" => MyApplication.map,
                "licenseclasses" => LicenseClass.map,
                "localdrivinglicenseapplications" => License.map,
                "testappointments" => TestAppointment.map,
                "testtypes" => TestType.map,
                "tests" => Test.map,
                "licenses" => MyLicense.map,
                "internationallicenses" => InternationalLicense.map,
                "detainedlicenses" => DetainedLicense.map,
                _ => throw new KeyNotFoundException($"No parameters defined for table '{TableName}'.")
            };
        }
        public static bool Validation(string TableName)
        {
            if (string.IsNullOrWhiteSpace(TableName))
                throw new ArgumentException("Table name cannot be null or empty.");
            if (!ValidationList.ContainsKey(TableName.ToLower()))
                throw new ArgumentException("Table name not found in the validation list.");

            return true;

        }
        public static bool Validation(string TableName, HashSet<string> ColumnName)
        {
            string tableName = TableName.ToLowerInvariant();
            Validation(tableName);
            if (ColumnName == null || ColumnName.Count == 0 || ColumnName.Any(x => string.IsNullOrWhiteSpace(x) || !Validate(x, "^[A-Za-z_]+$")))
                throw new ArgumentException("Column names list cannot be null or empty.");
            if (!ValidationList.ContainsKey(tableName))
                throw new ArgumentException("Table name not found in the validation list.");

            if (ColumnName.Any(x => !ValidationList[tableName].Contains(x.ToLowerInvariant())))
                return false;
            return true;

        }
        public static bool Validate(string Value, string pattern = null)
        {

            if (string.IsNullOrEmpty(Value))
                throw new ArgumentNullException("You can Not send null or Empty Value");
            if (pattern == null)
                pattern = "^[A-Za-z_]+$";
            if (System.Text.RegularExpressions.Regex.IsMatch(Value, pattern))
                return true;
            else
                throw new ArgumentNullException("Wrong Format For The Value");

        }
        public static bool ValidateNote(string Value, string pattern = null)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(Value, pattern))
                return true;
            else
                throw new ArgumentNullException("Wrong Format For The Value");

        }
        public static bool IsAllowedType<T>()
        {
            Type type = typeof(T);
            bool Flag = type.IsPrimitive ||
                   type == typeof(string) ||
                   type == typeof(DateTime);
            if (!Flag)
                throw new ArgumentException($"Type {typeof(T)} is not allowed.");
            return Flag;
        }
        public static bool validationName(string Name) => Validate(Name, "^[A-Za-z]+([-_][A-Za-z]+)*$"/*"^[A-Za-z_-]+$"*/);

        public static bool validationAddress(string Address) => Validate(Address, @"^[A-Za-z0-9_]+( [A-Za-z0-9_]+)*$");
        public static bool validationNationalNo(string NationalNo) => Validate(NationalNo, @"^\w+$");
        public static bool validationPhone(string Phone) => Validate(Phone, @"^[0-9]{9}$");
        public static bool validationEmail(string Email) => Validate(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public static bool validationGendor(byte Gendor) => !(Gendor > 1);

        public static bool validationDateOfBirth(DateTime DateOfBirth) => !(DateOfBirth == null || ((DateTime.Today.Year - DateOfBirth.Year < 18) || (DateTime.Today.Year - DateOfBirth.Year > 120)));

        public static bool validationID(int NationalityCountryID) => !(NationalityCountryID <= 0);
        public static bool ValidationUserPassword(string Password) => _ValidationService.Validate(Password, @"^.{8,20}$");
        public static bool ValidationLisenasDateTime(DateTime date) => date < DateTime.Now.AddDays(1);



    }
}


#region tries
/*   public static bool Validate(Person person)
   {
       if (person == null)
           return false;
       try
       {
           return (
            ((Validate(person.FirstName, "^[A-Za-z]+$") && Validate(person.SecondName, "^[A-Za-z]+$") && Validate(person.ThirdName, "^[A-Za-z]+$") && Validate(person.LastName, "^[A-Za-z]+$"))) &&
            //return false;
            ((Validate(person.NationalNo, @"^\w+$") && (Validate(person.Address, @"^\w+$")))) &&
            //return false;
            ((Validate(person.Phone, @"^[0-9]{9}$") && Validate(person.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))) &&
            //return false;
            (!(person.Gendor > 1 || person.DateOfBirth == null || person.NationalityCountryID <= 0)))
                ? true : false;



       }
       catch (Exception ex)
       {
           throw ex;
       }
   }*/
#endregion
#region triesx
/*   public static bool Validate(Person person)
   {
       if (person == null)
           return false;
       try
       {
           return (
            ((Validate(person.FirstName, "^[A-Za-z]+$") && Validate(person.SecondName, "^[A-Za-z]+$") && Validate(person.ThirdName, "^[A-Za-z]+$") && Validate(person.LastName, "^[A-Za-z]+$"))) &&
            //return false;
            ((Validate(person.NationalNo, @"^\w+$") && (Validate(person.Address, @"^\w+$")))) &&
            //return false;
            ((Validate(person.Phone, @"^[0-9]{9}$") && Validate(person.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))) &&
            //return false;
            (!(person.Gendor > 1 || person.DateOfBirth == null || person.NationalityCountryID <= 0)))
                ? true : false;



       }
       catch (Exception ex)
       {
           throw ex;
       }
   }*/
#endregion