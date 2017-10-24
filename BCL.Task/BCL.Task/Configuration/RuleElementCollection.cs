using System.Configuration;

namespace BCL.Task.Configuration
{
    public class RuleElementCollection: ConfigurationElementCollection
{
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).DestDir;
        }
    }
}
