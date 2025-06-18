using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Classes;
using MidLayer.Process;
//using MiddleLayer.Process;

namespace Liscenses.Classes
{
    internal  class HandelSpaicalReturnData
    {
        DataGridViewRow _dataGridView1 ;
        int UserID;
        internal HandelSpaicalReturnData(int UserID, DataGridViewRow _dataGridView1)
        {
            this.UserID = UserID;
            this._dataGridView1 = _dataGridView1;
        }
        internal async Task spicalx(object arg1, TestAppointment testAppointment, Test _Test)
        {
            var manipulate = new DataProcessor();
            if (_Test != null && _Test.TestResult == true)
            {
                short val = Convert.ToInt16(_dataGridView1.Cells["PassedTests"].Value);
                val += 1;
                _dataGridView1.Cells["PassedTests"].Value = val;
                if (val == 3)
                    _dataGridView1.Cells["Status"].Value = "Completed";
                EntityLayer.Classes.License local = new EntityLayer.Classes.License(HandelSpaicalReturnData.GetDataRowFromDataGridViewRow(_dataGridView1));
                var app = local.GetApplicaion();
                app.setLastStatusDate(DateTime.Now);
                app.setApplicationStatus(val);
               await manipulate.ProcessData("Applications", MyApplication.map.Keys.ToHashSet(),  app, "ApplicationID");
            }
        }
        private static DataRow GetDataRowFromDataGridViewRow(DataGridViewRow dgvRow)
        {
            // Check if the row is bound to data
            if (dgvRow.DataBoundItem is DataRowView rowView)
            {
                return rowView.Row; // Returns the underlying DataRow
            }
            else if (dgvRow.DataBoundItem is DataRow row)
                return row;

            return null;
        }
    }
}
