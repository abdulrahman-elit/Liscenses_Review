using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer.Interfaces;

namespace Liscenses
{
    public interface ICustmControl<out T>
    {
        ISupportedType<T> ManipulatingResult();
        public Task setInfo(/*Form frm,*/ DataRow Row, bool Show);
        public  Task<int> setInfo<U>(U ID, bool visibility, string Title) where U : IConvertible;
        public string PrimaryIntColumnIDName();

    }
}
