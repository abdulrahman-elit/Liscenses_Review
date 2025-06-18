using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Interfaces;
using MidLayer.B_CRUDOperation;
namespace MidLayer.Process
{
    public class DataProcessor
    {
        public async Task <bool> ProcessData<T>(string TableName, HashSet<string> ColumnName,  T Value, string ConditionColumn = null) where T : ISupportedType<T>
        {
            try
            {
                if (Value != null && Value is ISupportedType<T> supported)
                    switch (supported.status)
                    {
                        case enStatus.New:
                            supported.status = enStatus.Update;
                            return await(_B_Create.Row(TableName, ColumnName, Value).ConfigureAwait(false));
                        case enStatus.Update:
                            return await (_B_Update.Row(TableName, ColumnName, Value, ConditionColumn).ConfigureAwait(false));
                        default:
                            return false;
                    }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
