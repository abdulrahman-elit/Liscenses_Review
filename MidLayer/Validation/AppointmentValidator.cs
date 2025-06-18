using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.D_CRUDOperation.D_Read;
using EntityLayer.Classes;

namespace MidLayer.Validation
{
    public static class AppointmentValidator
    {
        public static async Task<bool> IsTestAppointmentValid(int ID) => await D_Check.Row("TestAppointments", "TestAppointmentID", ID, TestAppointment.map).ConfigureAwait(false);

    }
}
