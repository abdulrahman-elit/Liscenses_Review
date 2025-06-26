using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
//using EntityLayer.Classes;

namespace MidLayer.Validation
{
    public static class InternationalLicenseValidator
    {
        public static async Task<bool> IsInternationalLicenseExists(int ID) => await D_Check.Row("InternationalLicenses", "InternationalLicenseID", ID, InternationalLicense.map).ConfigureAwait(false);

    }
}
