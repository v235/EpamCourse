using System;
using System.Configuration;


namespace BCL.Task.Configuration
{
    public class DefaultElement: ConfigurationElement
    {
        [ConfigurationProperty("path")]
        public string DefaultPath
        {
            get { return (String)this["path"]; }
        }

        [ConfigurationProperty("culture")]
        public string Culture
        {
            get { return (String)this["culture"]; }
        }
    }
}
