namespace Alpha.Services.Configurations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public interface IAppSettings
    {
        bool IsLocalTest { get; }

        bool DisableAuthentication { get; }

        IEnumerable<int> GetParentSites(string level, string system);
    }

    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration configuration;

        public AppSettings(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public bool IsLocalTest => configuration.GetValue<bool>("IsLocalTest");

        public bool DisableAuthentication => configuration.GetValue<bool>("DisableAuthentication");

        public IEnumerable<int> GetParentSites(string level, string system)
            => configuration.GetSection($"ParentSites:{level}:{system}").Get<List<int>>()
                ?? throw new InvalidOperationException($"Missing config for ParentSites:{level}:{system}");
    }
}