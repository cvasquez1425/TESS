using System;

namespace Greenspoon.Tess
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Initialize logging facility
            //   InitializeLogger();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // NOTE: commented out because the site needs privileges to logging resources.
            // SingletonLogger.Instance.Error(ex.Message);
            //  Server.GetLastError().GetBaseException();
            // <customErrors ..> in web config will now redirect.
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        //private void InitializeLogger()
        //{
        //    // Read and assign application wide logging severity
        //    string severity = ConfigurationManager.AppSettings.Get("LogSeverity");
        //    SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

        //    // Send log messages to database (observer pattern)
        //    ILog log = new ObserverLogToDatabase();
        //    SingletonLogger.Instance.Attach(log);

        //    // Send log messages to email (observer pattern)
        //    string from = "notification@yourcompany.com";
        //    string to = "webmaster@yourcompany.com";
        //    string subject = "Webmaster: please review";
        //    string body = "email text";
        //    SmtpClient smtpClient = new SmtpClient("mail.yourcompany.com");

        //    log = new ObserverLogToEmail(from, to, subject, body, smtpClient);
        //    SingletonLogger.Instance.Attach(log);

        //    // Send log messages to a file
        //    log = new ObserverLogToFile(@"C:\Temp\DoFactory.log");
        //    SingletonLogger.Instance.Attach(log);

        //    // Send log message to event log
        //    log = new ObserverLogToEventlog();
        //    SingletonLogger.Instance.Attach(log);
        //}

    }
}
