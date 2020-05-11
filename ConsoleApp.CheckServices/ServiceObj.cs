using System;
using System.ServiceProcess;

namespace ConsoleApp.CheckServices
{
    class ServiceObj
    {
        public ServiceController Service { get; set; }

        public bool AutoStart { get; set; }


        public string statusInString
        {
            get
            {
                switch (this.Service.Status)
                {
                    case ServiceControllerStatus.ContinuePending:
                        return "ContinuePending";

                    case ServiceControllerStatus.Paused:
                        return "Paused";

                    case ServiceControllerStatus.PausePending:
                        return "PausePending";

                    case ServiceControllerStatus.Running:
                        return "Running";

                    case ServiceControllerStatus.StartPending:
                        return "StartPending";

                    case ServiceControllerStatus.Stopped:
                        return "Stopped";

                    case ServiceControllerStatus.StopPending:
                        return "StopPending";

                }

                return "";
            }
        }

        public ServiceObj(string serviceName, bool autoStart)
        {
            this.Service = new ServiceController(serviceName);
        }

        public void StartService(int timeoutMilliseconds = 10000)
        {
            try
            {

                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                this.Service.Refresh();
                if (this.Service.Status == ServiceControllerStatus.Stopped)
                {
                    this.Service.Start();
                    Service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> already running.", Service.DisplayName));
                }
            }
            catch(Exception error)
            {

                throw error;
            }
        }

        public void StopService(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> already stopped.", service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }
        public void RestartService(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    // conta o resto do timeout
                    int millisec2 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    service.Start();
                    throw new Exception(string.Format("{0} --> was stopped and then started", service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
