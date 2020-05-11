using ConsoleApp.CheckServices.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.CheckServices
{
    class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            log.Info("Starting service");

            CustomConfigsGroup sConfig = CustomConfigsGroup.getConfig();

            List<ServiceObj> servicesStoppedList = new List<ServiceObj>();

            for (int i = 0; i < sConfig.Services.Count; i++)
            {
                try
                {

                    ServiceObj sc = new ServiceObj(sConfig.Services[i].Name, sConfig.Services[i].AutoStart);

                    if (!sc.statusInString.Equals("Running"))
                    {
                        servicesStoppedList.Add(sc);
                    }

                }
                catch (Exception error)
                {
                    log.Error("Error when to search service " + ((sConfig.Services[i].Name == "") ? "(empty)" : sConfig.Services[i].Name) + ":", error);
                }
            }

            if (servicesStoppedList.Count > 0)
            {
                int totalservicesRecovery = 0;
                log.Info("List of services stopped: " + servicesFormatter(servicesStoppedList));

                if (sConfig.DisableAutoStart)
                {
                    log.Debug("Auto start is disabled");
                }
                else
                {
                    for (int i = 0; i < servicesStoppedList.Count; i++)
                    {
                        try
                        {
                            servicesStoppedList[i].StartService();
                            totalservicesRecovery++;
                        }
                        catch (Exception error)
                        {

                            log.Error("Error to start the service " + servicesStoppedList[i].Service.ServiceName + ":", error);

                        }
                    }
                }
                string serviceFormater = servicesFormatter(servicesStoppedList);

                log.Info("List of services stopped(updated): " + serviceFormater);

                if (sConfig.ParametersEmail[0].Enable)
                {
                    string bodyFormatter = "";
                    string subjectFormatter = "";

                    if (sConfig.ParametersEmail[0].IsHTML)
                    {
                        try
                        {
                            string html = System.IO.File.ReadAllText(sConfig.ParametersEmail[0].DirHtml, Encoding.UTF8);
                            bodyFormatter = html.Replace("##msg##", sConfig.ParametersEmail[0].Body);
                            bodyFormatter = bodyFormatter.Replace("##insertTable##", servicesFormaterTableHTML(servicesStoppedList));
                        }
                        catch (Exception error)
                        {
                            log.Error("Error to get html file!", error);

                            bodyFormatter = "|" + sConfig.ParametersEmail[0].Body + "\n" + serviceFormater + "|";

                        }
                    }
                    else
                    {
                        bodyFormatter = sConfig.ParametersEmail[0].Body + "\n" + serviceFormater;
                    }

                    bodyFormatter = bodyFormatter.Replace("##TotalStopped##", servicesStoppedList.Count + "");
                    bodyFormatter = bodyFormatter.Replace("##TotalRecovered##", totalservicesRecovery + "");

                    subjectFormatter = sConfig.ParametersEmail[0].Subject.Replace("##TotalStopped##", servicesStoppedList.Count + "");
                    subjectFormatter = subjectFormatter.Replace("##TotalRecovered##", totalservicesRecovery + "");

                    try
                    {
                        EmailObj sd = new EmailObj()
                        {
                            Ssl = sConfig.ParametersEmail[0].Ssl,
                            SMTPServer = sConfig.ParametersEmail[0].SMTPServer,
                            SMTPPort = Convert.ToInt32(sConfig.ParametersEmail[0].SMTPPort),
                            From = sConfig.ParametersEmail[0].From,
                            Password = sConfig.ParametersEmail[0].Password,
                            To = sConfig.ParametersEmail[0].To,
                            Cc = sConfig.ParametersEmail[0].Cc,
                            Attachments = sConfig.ParametersEmail[0].Attachments.Split(','),
                            Subject = subjectFormatter,
                            Body = bodyFormatter,
                            IsHTML = sConfig.ParametersEmail[0].IsHTML

                        };

                        sd.SendEmail();
                    }
                    catch (Exception error)
                    {
                        log.Error("Error to sent Email: ", error);
                    }


                }
                else
                {
                    log.Debug("Email Not enable");
                }

            }

            // Prevent the Console from closing immediately afterwards
            Console.ReadKey();
        }

        private static string servicesFormatter(List<ServiceObj> servicesStoppedList)
        {
            string listServicesLog = "";
            for (int i = 0; i < servicesStoppedList.Count; i++)
            {
                listServicesLog += "\n";
                listServicesLog += "Service: " + servicesStoppedList[i].Service.ServiceName + ";";
                listServicesLog += "Status:" + servicesStoppedList[i].statusInString + ";";
                listServicesLog += "AutoStart:" + servicesStoppedList[i].AutoStart + ";";
            }
            listServicesLog = listServicesLog.Substring(0, listServicesLog.Length - 1);
            return listServicesLog;
        }

        private static string servicesFormaterTableHTML(List<ServiceObj> servicesStoppedList)
        {
            string listServicesLog = "";
            for (int i = 0; i < servicesStoppedList.Count; i++)
            {
                listServicesLog += "\n";
                listServicesLog += "\n<tr>";
                listServicesLog += "\n<th class=\"ServicesTable\">" + servicesStoppedList[i].Service.ServiceName + "</th>";
                listServicesLog += "\n<th class=\"ServicesTable\">" + servicesStoppedList[i].statusInString + "</th>";
                listServicesLog += "\n<th class=\"ServicesTable\">" + servicesStoppedList[i].AutoStart + "</th>";
                listServicesLog += "\n</tr>";
            }
            listServicesLog = listServicesLog.Substring(0, listServicesLog.Length - 1);
            return listServicesLog;
        }
    }
}
