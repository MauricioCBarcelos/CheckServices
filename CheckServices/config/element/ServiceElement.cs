using System.Configuration;

namespace CheckServices.config
{
    class ServiceElement : ConfigurationElement
    {

        [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["Name"];

        [ConfigurationProperty("autoStart", IsRequired = true)]
        public bool AutoStart => (bool)base["autoStart"];

    }
}
