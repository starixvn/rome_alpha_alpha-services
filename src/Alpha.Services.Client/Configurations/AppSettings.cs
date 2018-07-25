using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Alpha.Services.Client.Configurations
{
    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection settings;

        internal static NameValueCollection Settings
        {
            get { return settings ?? ConfigurationManager.AppSettings; }

            set { settings = value; }
        }

        public static string BaseUrl => Settings["AlphaServicesClient:BaseUrl"];

        public static string ApiKey => Settings["AlphaServicesClient:ApiKey"];
    }
}