namespace Alpha.Services.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.Extensions.Configuration;

    public interface IAppSettings
    {
        bool IsLocalTest { get; }

        IEnumerable<int> GetParentSites(string level, string system);
    }

    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration configuration;

        public AppSettings(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            IsLocalTest = GetValue<bool>("IsLocalTest");
        }

        public bool IsLocalTest { get; }

        public IEnumerable<int> GetParentSites(string level, string system)
            => configuration.GetSection($"ParentSites:{level}:{system}").Get<List<int>>()
                ?? throw new InvalidOperationException($"Missing config for ParentSites:{level}:{system}");

        private T GetValue<T>(string key)
        {
            var value = configuration[key];

            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));

            return (T)typeConverter.ConvertFromString(value);
        }
    }
}