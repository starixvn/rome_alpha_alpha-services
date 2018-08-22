namespace Alpha.Services.Client.Services
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Client.Configurations;
    using Alpha.Services.Client.Helpers;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface AnnouncementService
    {
        IEnumerable<Message> GetAnnouncements(int custId, string language, DateTimeOffset fromDate, DateTimeOffset toDate);
    }

    public class AnnouncementServiceImpl : BaseService, AnnouncementService
    {
        public override IRestClient CreateRestClient()
            => new RestClient
            {
                BaseUrl = new Uri(AppSettings.AnnouncementServiceSettings.BaseUrl),
                Authenticator = new HmacAuthenticator(AppSettings.AnnouncementServiceSettings.ApiKey)
            };

        public IEnumerable<Message> GetAnnouncements(int custId, string language, DateTimeOffset fromDate, DateTimeOffset toDate)
            => Get<List<Message>>($"announcements/{custId}/{fromDate.ToUnixTimeMilliseconds()}/{toDate.ToUnixTimeMilliseconds()}/{language}");
    }
}