using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CheckServices.Config
{
    
    class ParametersEmailElement : ConfigurationElement
    {

        [ConfigurationProperty("enable", IsRequired = true)]
        public bool Enable => (bool)base["enable"];

        [ConfigurationProperty("ssl", IsRequired = true)]
        public bool Ssl => (bool)base["ssl"];

        [ConfigurationProperty("isHTML", IsRequired = true)]
        public bool IsHTML => (bool)base["isHTML"];

        [ConfigurationProperty("SMTPServer", IsRequired = true)]
        public string SMTPServer => ((string)base["SMTPServer"]).Trim();

        [ConfigurationProperty("SMTPPort", IsRequired = true)]
        public string SMTPPort => ((string)base["SMTPPort"]).Trim();

        [ConfigurationProperty("dirHtml", IsRequired = true)]
        public string DirHtml => ((string)base["dirHtml"]).Trim();

        [ConfigurationProperty("from", IsRequired = true)]
        public string From => ((string)base["from"]).Trim();

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => ((string)base["password"]).Trim();

        [ConfigurationProperty("Subject", IsRequired = true)]
        public string Subject => ((string)base["Subject"]).Trim();

        [ConfigurationProperty("body", IsRequired = true)]
        public string Body => ((string)base["body"]).Trim();

        [ConfigurationProperty("to", IsRequired = true)]
        public string To => ((string)base["to"]).Trim();

        [ConfigurationProperty("cc", IsRequired = true)]
        public string Cc => ((string)base["cc"]).Trim();

        [ConfigurationProperty("attachments", IsRequired = false)]
        public string Attachments => ((string)base["attachments"]).Trim();


    }
}
