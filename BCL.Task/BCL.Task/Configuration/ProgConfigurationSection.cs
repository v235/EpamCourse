using System.Configuration;

namespace BCL.Task.Configuration
{
    public class ProgConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("default")]
        public DefaultElement Default => (DefaultElement) this["default"];

        [ConfigurationCollection(typeof(DirectoryElement),
            AddItemName = "directory")]
        [ConfigurationProperty("directories")]
        public DirectoryElementCollection Dirs => (DirectoryElementCollection) this["directories"];

        [ConfigurationCollection(typeof(RuleElement),
            AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public RuleElementCollection Rules => (RuleElementCollection)this["rules"];
    }
}
