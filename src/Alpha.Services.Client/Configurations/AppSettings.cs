namespace Alpha.Services.Client.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public interface IServiceSettings
    {
        string BaseUrl { get; }

        string ApiKey { get; }
    }

    public class ServiceSettings : IServiceSettings
    {
        public ServiceSettings(string baseUrl, string apiKey)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
        }

        public string BaseUrl { get; }

        public string ApiKey { get; }
    }

    public static class AppSettings
    {
        private readonly static IConfigurationRoot configurationRoot;

        static AppSettings()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var configFileName = "appsettings.json";

            if (File.Exists(Path.Combine(currentDirectory, configFileName)))
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(currentDirectory)
                    .AddJsonFile(configFileName);

                configurationRoot = builder.Build();
            }

            var alphaServicesBaseUrl = GetConfigs("AlphaServices:BaseUrl");
            var alphaServicesApiKey = GetConfigs("AlphaServices:ApiKey");
            AlphaServiceSettings = new ServiceSettings(alphaServicesBaseUrl, alphaServicesApiKey);

            var announcementServiceBaseUrl = GetConfigs("AnnouncementService:BaseUrl");
            var announcementServiceApiKey = GetConfigs("AnnouncementService:ApiKey");
            AnnouncementServiceSettings = new ServiceSettings(announcementServiceBaseUrl, announcementServiceApiKey);

            var copyCustomerServiceBaseUrl = GetConfigs("CopyCustomerService:BaseUrl");
            var copyCustomerServiceApiKey = GetConfigs("CopyCustomerService:ApiKey");
            CopyCustomerServiceSettings = new ServiceSettings(copyCustomerServiceBaseUrl, copyCustomerServiceApiKey);
        }

        internal static NameValueCollection Settings => ConfigurationManager.AppSettings;

        public static IServiceSettings AlphaServiceSettings { get; private set; }

        public static IServiceSettings AnnouncementServiceSettings { get; private set; }

        public static IServiceSettings CopyCustomerServiceSettings { get; private set; }

        private static string GetConfigs(string configName)
        {
            var configValue = configurationRoot == null
                ? Settings[configName]
                : configurationRoot[configName];

            return configValue ?? throw new InvalidOperationException($"Missing config for {configName}");
        }
    }
}