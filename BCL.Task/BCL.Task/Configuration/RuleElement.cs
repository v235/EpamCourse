using System.Configuration;

namespace BCL.Task.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("FileName", IsRequired = true)]
        public string FileName
        {
            get { return (string)base["FileName"]; }
        }
        [ConfigurationProperty("DestDir", IsKey = true, IsRequired = true)]
        public string DestDir
        {
            get { return (string)base["DestDir"]; }
        }
        [ConfigurationProperty("FileAddNumber")]
        public bool FileAddNumber
        {
            get { return (bool)base["FileAddNumber"]; }
        }
        [ConfigurationProperty("FileAddDate")]
        public bool FileAddDate
        {
            get { return (bool)base["FileAddDate"]; }
        }
    }
}
