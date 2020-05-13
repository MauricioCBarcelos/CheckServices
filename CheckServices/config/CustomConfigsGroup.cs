using System;
using System.Configuration;

namespace CheckServices.config
{
    class CustomConfigsGroup : ConfigurationSection
    {
        public static CustomConfigsGroup getConfig() => (CustomConfigsGroup)ConfigurationManager.GetSection("CustomConfig");

        public int[] ThreadInterval
        {
            get
            {
                int[] times = new int[3];

                for (int i = 0; i < times.Length; i++)
                {
                    times[i] = Convert.ToInt32(ConfigurationManager.AppSettings["threadInterval"].ToString().Split(':')[i]);
                }

                return times;
            }
        }

        public bool DisableAutoStart => Convert.ToBoolean(ConfigurationManager.AppSettings["DisableAutoStart"].ToString());

        [ConfigurationProperty("Services")]
        [ConfigurationCollection(typeof(ServicesElementCollection), AddItemName = "service")]
        public ServicesElementCollection Services => (ServicesElementCollection)this["Services"];

        [ConfigurationProperty("ParametersEmail")]
        [ConfigurationCollection(typeof(ParametersEmailElementCollection))]
        public ParametersEmailElementCollection ParametersEmail => (ParametersEmailElementCollection)this["ParametersEmail"];

    }
}
