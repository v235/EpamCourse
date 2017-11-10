using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Container
    {
        private Assembly asm;
        private List<Type> addTypes=new List<Type>();
        private List<Object> paramToCreateInstance = new List<Object>();
        public void AddAssembly(Assembly asm)
        {
            this.asm = asm;
        }

        public void AddType(Type type)
        {
            addTypes.Add(type);
        }
        public void AddType(Type type, Type typeConcrete)
        {
            addTypes.Add(type);
        }

        public object CreateInstance(Type instance)
        {
            foreach (var c in instance.GetConstructors())
            {
                var param = c.GetParameters();
                if (param.Length > 0)
                {
                    //Activator.CreateInstance(instance,param);
                    foreach (var p in param)
                    {
                        var r = addTypes.FirstOrDefault(e => e == p.ParameterType);
                        if (r != null)
                        {
                            paramToCreateInstance.Add(Activator.CreateInstance(r));
                        }
                    }
                }
            }
            return Activator.CreateInstance(instance, paramToCreateInstance.ToArray());
        }

        //public Type CreateInstanceByProperty(Type instance)
        //{
        //    var result = instance;
        //    return result;
        //}
    }
}
