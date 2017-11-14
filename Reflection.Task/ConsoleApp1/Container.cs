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
        private Dictionary<Type, Type> addTypes = new Dictionary<Type, Type>();
        private List<Object> paramToCreateInstance = new List<Object>();

        public void AddAssembly(Assembly asm)
        {
            this.asm = asm;
            foreach (var type in asm.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(ExportAttribute), false).Count() > 0))
            {
                foreach (var attr in type.GetCustomAttributes(false).Cast<ExportAttribute>())
                {
                    if (attr.Contract != null)
                    {
                        addTypes.Add(attr.Contract,type);
                    }
                    else
                    {
                        addTypes.Add(type,type);
                    }
                }
            }
        }

        public void AddType(Type type)
        {
            addTypes.Add(type, type);
        }

        public void AddType(Type type, Type typeConcrete)
        {
            addTypes.Add(typeConcrete, type);
        }

        private object CreateInstanceByConstructor(Type instance, IEnumerable<PropertyInfo> prop)
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

        private object CreateInstanceByProperty(Type instance)
        {
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

        public object CreateInstance(Type instance)
        {
            if (instance.GetCustomAttributes(typeof(ImportConstructorAttribute), false).Length > 0)
            {
                return CreateInstanceByProperty(instance);
            }
            var prop = instance.GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(ImportAttribute), false).Count() > 0);
            return CreateInstanceByConstructor(instance, prop);
        }

        public T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }
    }
}
