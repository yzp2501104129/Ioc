using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioc
{
    public class IOCclass : IOCInterface
    {
        private static Dictionary<string, object> keyValues = new Dictionary<string, object>();

        public void RegisterType<From, TO>() where From : TO
        {
            keyValues.Add(typeof(From).FullName, typeof(From));
        }

        public From Resolve<From>()
        {
            Object OBJ = keyValues[typeof(From).FullName];
            return (From)Resolve2(OBJ);

        }
        private object Resolve2(object OBJ)
        {
            var ConstructorsList = OBJ.GetType().GetConstructors();
            var Ctor = ConstructorsList.FirstOrDefault(x => x.IsDefined(typeof(IOCAttribute), true));
            if (Ctor == null)
            {
                Ctor = ConstructorsList.OrderByDescending(x => x.GetParameters().Length).First();
            }
            List<object> listobj = new List<object>();
            foreach (var item in Ctor.GetParameters())
            {
                var param = keyValues[item.ParameterType.FullName];
                Resolve2(param);
                listobj.Add(param);
            }
            return Activator.CreateInstance(OBJ.GetType(), listobj.ToArray());
        }
    }
}
