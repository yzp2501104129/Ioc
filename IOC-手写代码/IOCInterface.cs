using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioc
{
    public interface IOCInterface
    {
        void RegisterType<FORM, TO>() where TO : FORM;

        From Resolve<From>();
    }
}
