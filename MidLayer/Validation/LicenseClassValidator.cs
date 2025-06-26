using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;

namespace MidLayer.Validation
{
    public static class LicenseClassValidator
    {
        public static async Task<bool> IsLicenseClassExists(int ID) => await D_Check.Row("LicenseClasses", "LicenseClassID", ID, LicenseClass.map).ConfigureAwait(false);

        public static  async Task<bool> IsClassNameUnique(int ID, string Name) =>await Checks.Checked(ID, Name, "ClassName", "LicenseClasses", LicenseClass.map, "LicenseClassID").ConfigureAwait(false);

        public static async Task<bool> IsClassNameUnique(LicenseClass licenseClass) => await Checks.Checked(licenseClass, new HashSet<string> { "ClassName" }, "LicenseClasses", LicenseClass.map).ConfigureAwait(false);
    }
}
