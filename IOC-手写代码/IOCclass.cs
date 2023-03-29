using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioc
{
    public class IOCclass : IOCInterface
    {
        private static Dictionary<string, Type> keyValues = new Dictionary<string, Type>();

        public void RegisterType<From, TO>() where TO : From
        {
            keyValues.Add(typeof(From).FullName, typeof(TO));
        }

        public From Resolve<From>()
        {
            Type OBJ = keyValues[typeof(From).FullName];
            return (From)Resolve2(OBJ);

        }
        private object Resolve2(Type OBJ)
        {

            var ConstructorsList = OBJ.GetConstructors();
            var Ctor = ConstructorsList.FirstOrDefault(x => x.IsDefined(typeof(IOCAttribute), true));
            if (Ctor == null)
            {
                Ctor = ConstructorsList.OrderByDescending(x => x.GetParameters().Length).First();
            }
            List<object> listobj = new List<object>();
            foreach (var item in Ctor.GetParameters())
            {
                var param = keyValues[item.ParameterType.FullName];
                var SonObj = Resolve2(param);
                listobj.Add(SonObj);
            }
            return Activator.CreateInstance(OBJ, listobj.ToArray());
        }
    }
}
