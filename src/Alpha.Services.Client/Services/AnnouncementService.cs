using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Alpha.Services.Client.Configurations;
using Alpha.Services.Client.Helpers;
using Alpha.Services.Client.Models;
using RestSharp;

[assembly: InternalsVisibleTo("Alpha.Services.Client.Tests")]

namespace Alpha.Services.Client.Services
{
    public interface AnnouncementService
    {
        IEnumerable<Message> GetAnnouncements(DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers);
    }

    public class AnnouncementServiceImpl : BaseService, AnnouncementService
    {
        public AnnouncementServiceImpl()
            : this(null)
        {
        }

        public AnnouncementServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public override IRestClient CreateRestClient()
            => new RestClient
            {
                BaseUrl = new Uri(AppSettings.AnnouncementServiceSettings.BaseUrl),
                Authenticator = new HmacAuthenticator(AppSettings.AnnouncementServiceSettings.ApiKey)
            };

        public IEnumerable<Message> GetAnnouncements(DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers)
            => Post<List<Message>>("announcements",
                new
                {
                    fromTime = fromTime.ToUnixTimeMilliseconds(),
                    toTime = toTime.ToUnixTimeMilliseconds(),
                    customers
                });
    }
}