using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioc
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class IOCAttribute : Attribute
    {
    }
}
