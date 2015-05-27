using System.Configuration;
using System.Web;

namespace Greenspoon.Tess.Classes
{
    // Singleton implementation. Not thread safe.
    public sealed class CrystalReportConfig
    {
        private static CrystalReportConfig instance;
        public string CrystalReportBasePath { get; private set; }
        public string DSNFileLocation { get; private set; }
        public string DatabaseName { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }

        private CrystalReportConfig() { }

        public static CrystalReportConfig Instance
        {
            get {
                if (instance == null) {
                    instance = new CrystalReportConfig() {
                        CrystalReportBasePath = ConfigurationManager.AppSettings["CrystalReportBasePath"],
                        DSNFileLocation       = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DSNFileFullPath"]),
                        DatabaseName          = ConfigurationManager.AppSettings["DatabaseName"],
                        UserID                = ConfigurationManager.AppSettings["UserID"],
                        Password              = ConfigurationManager.AppSettings["Password"]
                    };
                }
                return instance;
            }
        }
    }
}