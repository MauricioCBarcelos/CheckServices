using System;
using System.Configuration;

namespace CheckServices.config
{
    class CustomConfigsGroup : ConfigurationSection
    {
        public static CustomConfigsGroup getConfig() => (CustomConfigsGroup)ConfigurationManager.GetSection("CustomConfig");

        public int ThreadInterval => Convert.ToInt32(ConfigurationManager.AppSettings["threadInterval"].ToString());

        public bool DisableAutoStart => Convert.ToBoolean(ConfigurationManager.AppSettings["DisableAutoStart"].ToString());

        [ConfigurationProperty("Services")]
        [ConfigurationCollection(typeof(ServicesElementCollection), AddItemName = "service")]
        public ServicesElementCollection Services => (ServicesElementCollection)this["Services"];

        [ConfigurationProperty("ParametersEmail")]
        [ConfigurationCollection(typeof(ParametersEmailElementCollection))]
        public ParametersEmailElementCollection ParametersEmail => (ParametersEmailElementCollection)this["ParametersEmail"];

    }
}
