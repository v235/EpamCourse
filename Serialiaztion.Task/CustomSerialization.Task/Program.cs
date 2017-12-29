using System;
using CustomSerializationLib;


namespace CustomSerialization.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomSerializationProvider cp = new CustomSerializationProvider();

            //for test
            //cp.ISerializable();
            //cp.ISerializationSurrogate();
            //cp.SerializationCallbacks();
            //cp.IDataContractSurrogate(); 

            Console.ReadKey();
        }
    } 
}
