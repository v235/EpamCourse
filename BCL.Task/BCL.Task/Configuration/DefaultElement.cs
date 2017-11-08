using System;
using System.Configuration;


namespace BCL.Task.Configuration
{
    public class DefaultElement: ConfigurationElement
    {
        [ConfigurationProperty("Path")]
        public string DefaultPath
        {
            get { return (String)this["Path"]; }
        }

        [ConfigurationProperty("Culture")]
        public string Culture
        {
            get { return (String)this["Culture"]; }
        }
    }
}
