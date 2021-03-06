﻿using System.Configuration;

namespace BCL.Task.Configuration
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("Path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string) base["Path"]; }
        }
    }
}
