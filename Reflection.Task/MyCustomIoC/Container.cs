using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomIoC
{
    public class Container
    {
        private Assembly asm;
        private Dictionary<Type, Type> registryTypes = new Dictionary<Type, Type>();
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
                        registryTypes.Add(attr.Contract, type);
                    }
                    else
                    {
                        registryTypes.Add(type, type);
                    }
                }
            }
        }

        public void AddType(Type type)
        {
            registryTypes.Add(type, type);
        }

        public void AddType(Type type, Type typeConcrete)
        {
            registryTypes.Add(typeConcrete, type);
        }
        public object CreateInstance(Type instanceType)
        {
            return CreateInstanceByConstructor(instanceType);
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        private object CreateInstanceByProperty(Type instanceType, IEnumerable<PropertyInfo> prop)
        {
            try
            {
            var newnIstance = Activator.CreateInstance(instanceType);
            foreach (var p in prop)
            {
                if (registryTypes.Any(a => a.Key == p.PropertyType))
                {
                    p.SetValue(newnIstance, Activator.CreateInstance(registryTypes[p.PropertyType]), null);
                }
            }
            return newnIstance;
        }
        catch (System.MissingMethodException)
            {
                throw new IOException("Not all types are registry");
            }
        }

        private object CreateInstanceByConstructor(Type instanceType)
        {
                foreach (var c in instanceType.GetConstructors())
                {
                    var param = c.GetParameters();
                    if (param.Length > 0)
                    {

                        foreach (var p in param)
                        {
                            if (registryTypes.Any(a => a.Key == p.ParameterType))
                            {
                                paramToCreateInstance.Add(Activator.CreateInstance(registryTypes[p.ParameterType]));
                            }
                            else
                            {
                            throw new InvalidCostructorArgumentException(p.ParameterType.Name +" - parametr is not registred");
                            }
                        }
                    }
                }
                var prop = instanceType.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(ImportAttribute), false).Count() > 0);
                if (prop.Any())
                {
                    return CreateInstanceByProperty(instanceType, prop);
                }
                return Activator.CreateInstance(instanceType, paramToCreateInstance.ToArray());
        }
    }
}