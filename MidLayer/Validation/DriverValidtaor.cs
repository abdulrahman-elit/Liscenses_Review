using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;
using   MidLayer.B_CRUDOperation.B_Read;

namespace MidLayer.Validation
{
    public static class DriverValidtaor
    {
        public static async Task<bool> IsDriverExists(int ID) => await D_Check.Row("Drivers", "DriverID", ID, Driver.map).ConfigureAwait(false);
    }
}
