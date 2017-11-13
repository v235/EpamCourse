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
        private Dictionary<Type,Type> addTypes=new Dictionary<Type, Type>();
        private List<Object> paramToCreateInstance = new List<Object>();
        public void AddAssembly(Assembly asm)
        {
            this.asm = asm;
        }

        public void AddType(Type type)
        {
            addTypes.Add(type,type);
        }
        public void AddType(Type type, Type typeConcrete)
        {
            addTypes.Add(typeConcrete, type);
        }

        public object CreateInstance(Type instance)
        {
            var prop = instance.GetProperties();
            if (prop.Length > 0)
            {
                var newnIstance = Activator.CreateInstance(instance);
                foreach (var p in prop)
                {
                    if (addTypes[p.PropertyType] != null)
                    {
                        p.SetValue(newnIstance, Activator.CreateInstance(addTypes[p.PropertyType]), null);
                    }
                }
                return newnIstance;
            }
            foreach (var c in instance.GetConstructors())
            {
                var param = c.GetParameters();
                if (param.Length > 0)
                {
                    foreach (var p in param)
                    {
                        if (addTypes[p.ParameterType] != null)
                        {
                            paramToCreateInstance.Add(Activator.CreateInstance(addTypes[p.ParameterType]));
                        }
                    }
                }
            }
            return Activator.CreateInstance(instance, paramToCreateInstance.ToArray());
        }
    }
}
