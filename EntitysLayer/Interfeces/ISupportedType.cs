using System.Data;
using System.Threading.Tasks;

namespace EntityLayer.Interfaces
{
    public enum enStatus
    {
        Update,
        New,
        Active,
        Inactive,
        Pending
    }
    public interface ISupportedType<out T>
    {
        // This Also Must Implemented in the Inherited Class
        //public static readonly Dictionary<string, (SqlDbType type, int? size)> map { set; get; }
         Task<bool> Validation();
        //bool Validation(T Value = default);
         int PrimaryKey(int id=0);
        Task<(DataTable dtr,DataRow r)> ToDataRow(DataTable dt,bool add);
        Task FromDataRow(DataRow dr);
        string PrimaryIntColumnIDName { get; }
        enStatus status { set; get; }
    }
}
