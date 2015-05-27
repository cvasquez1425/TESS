using System.Configuration;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public static class DataContextFactory
    {
        static readonly string _connectionString;
        static DataContextFactory()
        {
            string connectionStringName = 
                ConfigurationManager.AppSettings.Get("ConnectionStringName");
            _connectionString = 
                ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public static TessEntities CreateContext()
        {
            return new TessEntities();
        }
    }
}