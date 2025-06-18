using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;

namespace MidLayer.Validation
{
    public static class DetainedLicenseValidator
    {
        public static async Task<bool> IsDetainedLicenseExists(int ID) => await D_Check.Row("DetainedLicenses", "DriverID", ID, Driver.map).ConfigureAwait(false);

    }
}
