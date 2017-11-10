using System;
using System.Reflection;

namespace IoCSample
{
    class Container
    {
        private Assembly asm;
        Assembly asm1 =Assembly.GetExecutingAssembly();
        public Container(string assemblyPath)
        {
            asm = Assembly.LoadFrom(assemblyPath);
        }
        public void CreateInstanceByConstructor()
        {
            
        }
        public void CreateInstanceByProperty()
        {

        }
    }
}
