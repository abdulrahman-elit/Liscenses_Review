using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using EntityLayer.Interfaces;
using MidLayer.Validation;
using MidLayer.Process;
using System.Threading.Tasks;
namespace EntityLayer.Classes
{
    public class Person : ISupportedType<Person>
    {
        #region Praporties
        public static readonly Dictionary<string, (SqlDbType type, int? size)> map = new Dictionary<string, (SqlDbType type, int? size)>
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
        private string trim;
        public enStatus status {  set; get; }
        public  string PrimaryIntColumnIDName { get; } = "PersonID";
        public int PersonID { get; private set; }
        public string NationalNo { private set; get; }
        public string FirstName { private set; get; }
        public string SecondName { private set; get; }
        public string ThirdName { private set; get; }
        public string LastName { private set; get; }
        public DateTime DateOfBirth { private set; get; }
        public byte Gendor { private set; get; }
        public string Address { private set; get; }
        public string Phone { private set; get; }
        public string Email { private set; get; }
        public int NationalityCountryID { private set; get; }
        public string ImagePath { private set; get; }
        private int _PrimaryKey {  set; get; }
        public int PrimaryKey(int id=0)
        {
            if (id > 0)
            {
                _PrimaryKey = PersonID = id;
                return _PrimaryKey;
            }
            else
                return _PrimaryKey;
        }
        public Task FromDataRow(DataRow personRow)
        {

            if (personRow == null)
                throw new ArgumentNullException(nameof(personRow) + " Empty");
            this.PersonID = _PrimaryKey = Convert.ToInt32(personRow["PersonID"] ?? -1); // Handle DBNull
            this.setNationalNo(personRow["NationalNo"] as string);
            this.setFirestName(personRow["FirstName"] as string);
            this.setSecondName(personRow["SecondName"] as string);
            this.setDateOfBirth(personRow["DateOfBirth"] as DateTime? ?? DateTime.MinValue);
            this.setGender(Convert.ToByte(personRow["Gendor"] ?? 0));
            this.setThirdName(personRow["ThirdName"] as string);
            this.setLastName(personRow["LastName"] as string);
            this.setAddress(personRow["Address"] as string);
            this.setPhone(personRow["Phone"] as string);
            this.setEmail(personRow["Email"] as string);
            this.setNationalityCountryID(Convert.ToInt32(personRow["NationalityCountryID"] ?? 0));
            this.setImage(personRow["ImagePath"] as string ?? string.Empty);
            status = enStatus.Update;
            return Task.CompletedTask;
        }
        public  Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool Add)
        {
            DataRow row;
            if (Add)
            {
                row = dt.NewRow();
                DataColumn pkColumn = dt.Columns["PersonID"];
                pkColumn.AutoIncrement = false;
                row[$"{nameof(PersonID)}"] = _PrimaryKey;
                //dt.Rows.Add(row);
            }
            else
                row = dt.Rows.Find(_PrimaryKey);
            row[$"{nameof(NationalNo)}"] = this.NationalNo;
            row[$"{nameof(FirstName)}"] = this.FirstName;
            row[$"{nameof(SecondName)}"] = this.SecondName;
            row[$"{nameof(ThirdName)}"] = this.ThirdName;
            row[$"{nameof(LastName)}"] = this.LastName;
            row[$"{nameof(DateOfBirth)}"] = this.DateOfBirth;
            row[$"{nameof(Gendor)}"] = this.Gendor;
            row[$"{nameof(Address)}"] = this.Address;
            row[$"{nameof(Phone)}"] = this.Phone;
            row[$"{nameof(Email)}"] = this.Email;
            row[$"{nameof(NationalityCountryID)}"] = this.NationalityCountryID;
            row[$"{nameof(ImagePath)}"] = this.ImagePath;
            return  Task.FromResult((dt,row));

        }

        public void setNationalNo(string NationalNo)
        {
             trim=NationalNo.Trim();
            _ValidationService.validationNationalNo(trim);
            this.NationalNo = trim;

        }
        public void setAddress(string Address)
        {
             trim=Address.Trim();
            _ValidationService.validationAddress(trim);
            this.Address = trim;

        }
        public void setFirestName(string FirstName)
        {
            trim = FirstName.Trim();
            _ValidationService.validationName(trim);
            this.FirstName = trim;
        }
        public void setSecondName(string SecondName)
        {
            trim = SecondName.Trim();
            _ValidationService.validationName(trim);
            this.SecondName = trim;
        }
        public void setThirdName(string ThirdName)
        {
            trim = ThirdName.Trim();
            _ValidationService.validationName(trim);
            this.ThirdName = trim;
        }
        public void setLastName(string LastName)
        {
            trim = LastName.Trim();
            _ValidationService.validationName(trim);
            this.LastName = trim;
        }
        public void setDateOfBirth(DateTime DateOfBirth)
        {
            _ValidationService.validationDateOfBirth(DateOfBirth);
            this.DateOfBirth = DateOfBirth;
        }
        public void setGender(byte Gender)
        {
            _ValidationService.validationGendor(Gender);
            this.Gendor = Gender;
        }
        public void setPhone(string Phone)
        {
            trim = Phone.Trim();
            _ValidationService.validationPhone(trim);
            this.Phone = trim;
        }
        public void setEmail(string Email)
        {
            trim = Email.Trim();
            _ValidationService.validationEmail(trim);
            this.Email = trim;
        }
        public void setNationalityCountryID(int NationalityCountryID)
        {
            _ValidationService.validationID(NationalityCountryID);
            this.NationalityCountryID = NationalityCountryID;
        }
        public void setImage(string  ImagePath)
        {
            if(ImagePath == null)
                this.ImagePath = string.Empty;
            else
                this.ImagePath = ImagePath.Trim();
        }
        #endregion
        #region Constractuers
        public Person(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID,
            string ImagePath)
        {
            _ValidationService.validationID(PersonID);
            this.PersonID=_PrimaryKey = PersonID ;
            setNationalNo(NationalNo);
            setFirestName(FirstName);
            setSecondName(SecondName);
            setThirdName(ThirdName);
            setLastName(LastName);
            setGender(Gendor);
            setDateOfBirth(DateOfBirth);
            setAddress(Address);
            setPhone(Phone);
            setEmail(Email);
            setNationalityCountryID(NationalityCountryID);
            setImage(ImagePath);
           
           
            status = enStatus.Update;
        }
        public Person(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                  DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID,
                  string ImagePath)
        {

            this.PersonID=_PrimaryKey = -1;
            setNationalNo(NationalNo);
            setFirestName(FirstName);
            setSecondName(SecondName);
            setThirdName(ThirdName);
            setLastName(LastName);
            setGender(Gendor);
            setDateOfBirth(DateOfBirth);
            setAddress(Address);
            setPhone(Phone);
            setEmail(Email);
            setNationalityCountryID(NationalityCountryID);
            setImage(ImagePath);
            status = enStatus.New;
        }
        public Person(Person person)
        {
           _ValidationService.validationID(person.PersonID);
            this.PersonID =_PrimaryKey= person.PersonID;
            setGender(person.Gendor);
            setDateOfBirth(person.DateOfBirth);
            setNationalNo(person.NationalNo);
            setFirestName(person.FirstName);
            setSecondName(person.SecondName);
            setThirdName(person.ThirdName);
            setLastName(person.LastName);
            setAddress(person.Address);
            setPhone(person.Phone);
            setEmail(person.Email);
            setNationalityCountryID(person.NationalityCountryID);
            setImage(person.ImagePath);
            status = enStatus.Update;
         
        }
        public Person(DataRow personRow)
        {

            if (personRow == null)
                throw new ArgumentNullException(nameof(personRow)+" Empty");
           this.PersonID =_PrimaryKey= Convert.ToInt32(personRow["PersonID"] ?? -1); // Handle DBNull
           this.setNationalNo(personRow["NationalNo"] as string);
           this.setFirestName(personRow["FirstName"] as string);
           this.setSecondName(personRow["SecondName"] as string);
           this.setDateOfBirth(personRow["DateOfBirth"] as DateTime? ?? DateTime.MinValue);
           this.setGender(Convert.ToByte(personRow["Gendor"] ?? 0));
           this.setThirdName(personRow["ThirdName"] as string);
           this.setLastName(personRow["LastName"] as string);
           this.setAddress(personRow["Address"] as string);
           this.setPhone(personRow["Phone"] as string);
           this.setEmail(personRow["Email"] as string);
           this.setNationalityCountryID(Convert.ToInt32(personRow["NationalityCountryID"] ?? 0));
           this.setImage(personRow["ImagePath"] as string ?? string.Empty);
            status = enStatus.Update;
         
        }
        #endregion
        #region Validation
        public   async Task<bool> Validation()
        {
            try
            {
                return await     PersonValidator.IsNationalNoExists(PersonID,NationalNo) && await PersonValidator.ISEmailExists(PersonID, Email);
            }
            catch 
            {
                throw ;
            }
            
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
//if( !_ValidationService.validationID(PersonID))
//     throw new Exception ($"Invalid {nameof(PersonID)}");
// if(!_ValidationService.validationNationalNoOrAddress(NationalNo))
//     throw new Exception($"Invalid {nameof(NationalNo)}");
// if(!_ValidationService.validationName(FirstName))
//     throw new Exception($"Invalid {nameof(FirstName)}");
// if (!_ValidationService.validationName(SecondName))
//     throw new Exception($"Invalid {nameof(SecondName)}");
// if(!_ValidationService.validationName(ThirdName))
//     throw new Exception($"Invalid {nameof(ThirdName)}");
// if(!_ValidationService.validationName(LastName))
//     throw new Exception($"Invalid {nameof(LastName)}");

//if(!_ValidationService.validationDateOfBirth(DateOfBirth))
//     throw new Exception($"Invalid {nameof(DateOfBirth)}");

// if (!_ValidationService.validationGendor(Gendor))
//     throw new Exception($"Invalid {nameof(Gendor)}");

// if (!_ValidationService.validationNationalNoOrAddress(Address))
//     throw new Exception($"Invalid {nameof(Address)}");

// if (!_ValidationService.validationPhone(Phone))
//     throw new Exception($"Invalid {nameof(Phone)}");

// if (!_ValidationService.validationEmail(Email))
//     throw new Exception($"Invalid {nameof(Email)}");

// if (!_ValidationService.validationID(NationalityCountryID))
//     throw new Exception($"Invalid {nameof(NationalityCountryID)}");
/*   public Task<DataRow> ToDataRow(DataTable dt)
{ 
   if (_PrimaryKey == -1)
   {
       DataRow row = dt.NewRow();
       // Populate newRow with default values
       row[$"{nameof(PersonID)}"] = this.PersonID;
       row[$"{nameof(NationalNo)}"] = this.NationalNo;
       row[$"{nameof(FirstName)}"] = this.FirstName;
       row[$"{nameof(SecondName)}"] = this.SecondName;
       row[$"{nameof(ThirdName)}"] = this.ThirdName;
       row[$"{nameof(LastName)}"] = this.LastName;
       row[$"{nameof(DateOfBirth)}"] = this.DateOfBirth;
       row[$"{nameof(Gendor)}"] = this.Gendor;
       row[$"{nameof(Address)}"] = this.Address;
       row[$"{nameof(Phone)}"] = this.Phone;
       row[$"{nameof(Email)}"] = this.Email;
       row[$"{nameof(NationalityCountryID)}"] = this.NationalityCountryID;
       row[$"{nameof(ImagePath)}"] = this.ImagePath;
       row[$"{nameof(status)}"] = this.status;
       return row;
   }
   else
   {
       DataRow row = dt.Rows.Find(_PrimaryKey);
       if (row == null)
       {
           throw new ArgumentException("Row with the specified primary key not found.");
       }
   row[$"{nameof(PersonID)}"] = this.PersonID;
   row[$"{nameof(NationalNo)}"] = this.NationalNo;
   row[$"{nameof(FirstName)}"] = this.FirstName;
   row[$"{nameof(SecondName)}"] = this.SecondName;
   row[$"{nameof(ThirdName)}"] = this.ThirdName;
   row[$"{nameof(LastName)}"] = this.LastName;
   row[$"{nameof(DateOfBirth)}"] = this.DateOfBirth;
   row[$"{nameof(Gendor)}"] = this.Gendor;
   row[$"{nameof(Address)}"] = this.Address;
   row[$"{nameof(Phone)}"] = this.Phone;
   row[$"{nameof(Email)}"] = this.Email;
   row[$"{nameof(NationalityCountryID)}"] = this.NationalityCountryID;
   row[$"{nameof(ImagePath)}"] = this.ImagePath;
   row[$"{nameof(status)}"] = this.status;
       return row;
   }


   //return row;

}*/
/*
//if (!_ValidationService.validationNationalNoOrAddress(NationalNo))
//    throw new Exception($"Invalid {nameof(NationalNo)}");
//if (!_ValidationService.validationName(FirstName))
//    throw new Exception($"Invalid {nameof(FirstName)}");
//if (!_ValidationService.validationName(SecondName))
//    throw new Exception($"Invalid {nameof(SecondName)}");
//if (!_ValidationService.validationName(ThirdName))
//    throw new Exception($"Invalid {nameof(ThirdName)}");
//if (!_ValidationService.validationName(LastName))
//    throw new Exception($"Invalid {nameof(LastName)}");

//if (!_ValidationService.validationDateOfBirth(DateOfBirth))
//    throw new Exception($"Invalid {nameof(DateOfBirth)}");

//if (!_ValidationService.validationGendor(Gendor))
//    throw new Exception($"Invalid {nameof(Gendor)}");

//if (!_ValidationService.validationNationalNoOrAddress(Address))
//    throw new Exception($"Invalid {nameof(Address)}");

//if (!_ValidationService.validationPhone(Phone))
//    throw new Exception($"Invalid {nameof(Phone)}");

//if (!_ValidationService.validationEmail(Email))
//    throw new Exception($"Invalid {nameof(Email)}");

//if (!_ValidationService.validationID(NationalityCountryID))
//    throw new Exception($"Invalid {nameof(NationalityCountryID)}");
*/

#endregion