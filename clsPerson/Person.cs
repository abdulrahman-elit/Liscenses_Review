using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsValidation.SupportedTypes;
using ISupportedType.SupportedTypes;
using MiddleLayer.B_CRUDOperation.B_Read;
using static System.Net.Mime.MediaTypeNames;
//using ISupportedtypes.SupportedTypes; 
namespace clsPerson.SupportedTypes
{
    public class Person : ISupportedType<Person>
    {
        #region Praporties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map =new Dictionary<string, (SqlDbType type, int? size)>
        {
        { "PersonID", (SqlDbType.Int,null) },
        { "NationalNo", (SqlDbType.NVarChar, 20) },
        { "FirstName", (SqlDbType.NVarChar, 20) },
        { "SecondName", (SqlDbType.NVarChar, 20) },
        { "ThirdName", (SqlDbType.NVarChar, 20) },
        { "LastName", (SqlDbType.NVarChar, 20) },
        { "DateOfBirth", (SqlDbType.DateTime, null) },
        { "Gendor", (SqlDbType.TinyInt, null) },
        { "Address", (SqlDbType.NVarChar, 500) },
        { "Phone", (SqlDbType.NVarChar, 20) },
        { "Email", (SqlDbType.NVarChar, 50) },
        { "NationalityCountryID", (SqlDbType.Int, null) },
        { "ImagePath", (SqlDbType.NVarChar, 250) } 
              };
             
     public static B_Check Exists = new B_Check();
     public enStatus status { set; get; }
     public string PrimaryIntColumnIDName { get; } = "PersonID";
     public  int PersonID { get; set; }
     public string NationalNo{set;get;}
     public string FirstName{set;get;}
     public string SecondName{set;get;}
     public string ThirdName{set;get;}
     public string LastName{set;get;}
     public DateTime DateOfBirth{set;get;}
     public byte Gendor{set;get;}
     public string Address{set;get;}
     public string Phone {set;get;}
     public string Email {set;get;}
     public int NationalityCountryID{set;get;}
     public string ImagePath { set; get; }
        #endregion
        #region Constractuers
        public Person(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID,
            string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            status = enStatus.Update;
            validationName(FirstName); validationName(SecondName); validationName(ThirdName); validationName(LastName);
            validationNationalNoOrAddress(NationalNo); validationNationalNoOrAddress(Address); validationPhone(Phone);
            validationEmail(Email); validationGendor(Gendor); validationDateOfBirth(DateOfBirth);
            validationNationalityCountryID(NationalityCountryID);
        }
        public Person(             string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                  DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID,
                  string ImagePath)
        {
            this.PersonID= -1;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            status = enStatus.New;
            validationName(FirstName); validationName(SecondName); validationName(ThirdName); validationName(LastName);
            validationNationalNoOrAddress(NationalNo); validationNationalNoOrAddress(Address); validationPhone(Phone);
            validationEmail(Email); validationGendor(Gendor); validationDateOfBirth(DateOfBirth);
            validationNationalityCountryID(NationalityCountryID);
        }
        public Person(Person person)
        {
            this.PersonID = person.PersonID;
            this.NationalNo = person.NationalNo;
            this.FirstName = person.FirstName;
            this.SecondName = person.SecondName;
            this.ThirdName = person.ThirdName;
            this.LastName = person.LastName;
            this.DateOfBirth = person.DateOfBirth;
            this.Gendor = person.Gendor;
            this.Address = person.Address;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.NationalityCountryID = person.NationalityCountryID;
            this.ImagePath = person.ImagePath;
            status = enStatus.Update;
            validationName(FirstName); validationName(SecondName); validationName(ThirdName); validationName(LastName);
            validationNationalNoOrAddress(NationalNo); validationNationalNoOrAddress(Address); validationPhone(Phone);
            validationEmail(Email); validationGendor(Gendor); validationDateOfBirth(DateOfBirth);
            validationNationalityCountryID(NationalityCountryID);
        }
      public Person (DataRow personRow)
        {

            if (personRow == null)
                throw new ArgumentNullException(nameof(personRow), "The DataRow cannot be null.");
            PersonID = Convert.ToInt32(personRow["ID"] ?? -1); // Handle DBNull
            NationalNo = personRow["NationalNo"] as string; // Handle DBNull
            FirstName = personRow["FirstName"] as string;
            SecondName = personRow["SecondName"] as string;
            ThirdName = personRow["ThirdName"] as string;
            LastName = personRow["LastName"] as string;
            DateOfBirth = personRow["DateOfBirth"] as DateTime? ?? DateTime.MinValue;
            Gendor = Convert.ToByte(personRow["Gendor"] ?? 0);
            Address = personRow["Address"] as string;
            Phone = personRow["Phone"] as string;
            Email = personRow["Email"] as string;
            NationalityCountryID = Convert.ToInt32(personRow["NationalityCountryID"] ?? 0); // Handle DBNull
            ImagePath = personRow["ImagePath"] as string ?? string.Empty;
            status = enStatus.Update;
            validationName(FirstName); validationName(SecondName); validationName(ThirdName); validationName(LastName);
            validationNationalNoOrAddress(NationalNo); validationNationalNoOrAddress(Address); validationPhone(Phone);
            validationEmail(Email); validationGendor(Gendor); validationDateOfBirth(DateOfBirth);
                    validationNationalityCountryID(NationalityCountryID);


        }
        #endregion
        #region Validation
        private bool Validation(Person person)
        {

        }
        #endregion
    }
}
#region tries

//((_ValidationService.Validate(person.FirstName, "^[A-Za-z]+$") && _ValidationService.Validate(person.SecondName, "^[A-Za-z]+$") && _ValidationService.Validate(person.ThirdName, "^[A-Za-z]+$") && _ValidationService.Validate(person.LastName, "^[A-Za-z]+$"))) &&
////return false;
//((_ValidationService.Validate(person.NationalNo, @"^\w+$") && (_ValidationService.Validate(person.Address, @"^\w+$")))) &&
////return false;
//((_ValidationService.Validate(person.Phone, @"^[0-9]{9}$") && _ValidationService.Validate(person.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))) &&
////return false;
//(!(person.Gendor > 1 || person.DateOfBirth == null || person.NationalityCountryID <= 0)))
#endregion