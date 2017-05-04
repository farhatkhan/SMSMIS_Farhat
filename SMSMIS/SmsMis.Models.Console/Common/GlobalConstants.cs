
namespace SmsMis.Models.Console.Common
{
    public class GlobalConstants
    {
        private static string _connectionString = null;

        public static string connectionString
        {
            get
            {
                if (_connectionString == null) readConfig();
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        private static void readConfig()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["ValencyConnection"] != null)
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings["ValencyConnection"];
            }
            
        }
    }
}
