using System.Configuration;

namespace BCL.Task.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("filename", IsRequired = true)]
        public string FileName
        {
            get { return (string)base["filename"]; }
        }
        [ConfigurationProperty("destdir", IsKey = true, IsRequired = true)]
        public string DestDir
        {
            get { return (string)base["destdir"]; }
        }
        [ConfigurationProperty("fileaddnumber")]
        public bool FileAddNumber
        {
            get { return (bool)base["fileaddnumber"]; }
        }
        [ConfigurationProperty("fileadddate")]
        public bool FileAddDate
        {
            get { return (bool)base["fileadddate"]; }
        }
    }
}
