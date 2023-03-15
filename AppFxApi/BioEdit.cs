using BBAppFxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFxApi
{
    public class BioEdit : WriteableDataFormBase
    {
        public BioEdit(Guid ContextId) : base(new Guid("788ab947-26ed-40c4-865e-8fe29577e593"), ContextId.ToString()) { }

    }
}
