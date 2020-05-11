using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CheckServices.Config
{
    class ServiceElement : ConfigurationElement
    {

        [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["Name"];

        [ConfigurationProperty("autoStart", IsRequired = true)]
        public bool AutoStart => (bool)base["autoStart"];

    }
}
