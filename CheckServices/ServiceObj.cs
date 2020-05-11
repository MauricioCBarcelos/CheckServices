using System;
using System.ServiceProcess;

namespace CheckServices
{
    class ServiceObj
    {
        public ServiceController Service { get; set; }

        public bool AutoStart { get; set; }


        public string statusInString
        {
            get
            {
                switch (Service.Status)
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
            Service = new ServiceController(serviceName);
            AutoStart = autoStart;
        }

        public void StartService(int timeoutMilliseconds = 10000)
        {
            try
            {

                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                Service.Refresh();
                if (Service.Status == ServiceControllerStatus.Stopped)
                {
                    Service.Start();
                    Service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> already running.", Service.DisplayName));
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public void StopService(int timeoutMilliseconds = 10000)
        {

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                Service.Refresh();
                if (Service.Status == ServiceControllerStatus.Running)
                {
                    Service.Stop();
                    Service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> already stopped.", Service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }
        public void RestartService(int timeoutMilliseconds = 10000)
        {

            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                Service.Refresh();
                if (Service.Status != ServiceControllerStatus.Stopped)
                {
                    Service.Stop();
                    Service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    // conta o resto do timeout
                    int millisec2 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                    Service.Start();
                    Service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    Service.Start();
                    throw new Exception(string.Format("{0} --> was stopped and then started", Service.ServiceName));
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
