using System.Configuration;

namespace Greenspoon.Tess.Classes
{
    public sealed class DocumentConfig
    {
        private static DocumentConfig instance;
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Domain { get; private set; }

        private DocumentConfig() { }

        public static DocumentConfig Instance
        {
            get
            {
                return instance ?? 
                    (instance = new DocumentConfig {
                        UserName = ConfigurationManager.AppSettings["Doc.UserName"],
                        Password = ConfigurationManager.AppSettings["Doc.Password"],
                        Domain = ConfigurationManager.AppSettings["Doc.Domain"]
                    });
            }
        }
    }
}