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
    public static class TestTypeValidator
    {
        public static async Task<bool> IsTestTypeValid(int ID) => await D_Check.Row("TestTypes", "TestTypeID", ID, TestType.map).ConfigureAwait(false);
        public static async Task<bool> IsTestTypeUnique(int ID, string TestTitle) => await Checks.Checked(ID, TestTitle, "TestTypeTitle", "TestTypes", TestType.map, "TestTypeID").ConfigureAwait(false);
        public static async Task<bool> IsValidFee(int ID, double TestFees) => await Checks.Checked(ID, TestFees, "TestTypeFees", "TestTypes", TestType.map, "TestTypeID").ConfigureAwait(false);
        
    }
}
