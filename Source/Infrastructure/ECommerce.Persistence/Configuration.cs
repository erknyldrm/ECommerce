using Microsoft.Extensions.Configuration;

namespace ECommerce.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../Presentation/Api"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSql");
            }
        }
    }
}
