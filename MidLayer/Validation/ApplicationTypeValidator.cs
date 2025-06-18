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
    public static class ApplicationTypeValidator
    {
        public static async Task<bool> IsApplicationTypeValid(int ID) => await D_Check.Row("ApplicationTypes", "ApplicationTypeID", ID, ApplicationType.map).ConfigureAwait(false);
        public static async Task<bool> IsApplicationTypeUnique(int ID, string ApplicationTitle) =>await Checks.Checked(ID, ApplicationTitle, "ApplicationTypeTitle", "ApplicationTypes", ApplicationType.map, "ApplicationTypeID").ConfigureAwait(false);
        public static async Task<bool> IsValidFee(int ID, decimal ApplicationFees) => await Checks.Checked(ID, ApplicationFees, "ApplicationFees", "ApplicationTypes", ApplicationType.map, "ApplicationTypeID").ConfigureAwait(false);
    }
}
