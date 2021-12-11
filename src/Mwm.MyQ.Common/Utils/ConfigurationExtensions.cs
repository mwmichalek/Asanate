using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mwm.MyQ.Common.Utils {
    public static class ConfigurationExtensions {

        public static IConfigurationRoot AddConfigurationWithUserSecrets(this IServiceCollection services, string userSecret = "7e91521f-8053-4ae3-b00a-8424cb20e0f6") {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(userSecret)
                .Build();
        }

        public static IConfigurationRoot AddConfigurations(this IServiceCollection services) {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
