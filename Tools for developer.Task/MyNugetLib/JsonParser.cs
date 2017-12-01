using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;




namespace MyNugetLib
{
    public class JsonParser
    {
        public IEnumerable<string> ParseJson(string json)
        {
            return JArray.Parse(json).Select(s=>s.ToString());
        }
        public string GetFirstElement(string json)
        {
            return JArray.Parse(json).Select(s => s.ToString()).FirstOrDefault();
        }

        public string FindElementByKey(string json, string key)
        {
            return JArray.Parse(json).Select(s => s.ToString()).First(e=>e==key);
        }
    }
}
