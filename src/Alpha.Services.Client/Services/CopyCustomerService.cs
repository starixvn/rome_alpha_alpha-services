namespace Alpha.Services.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Configurations;
    using Alpha.Services.Client.Helpers;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface CopyCustomerService
    {
        Task<Result> ValidateMaxNewMember(int agentId, string language);

        Task<CustomerPTInfo> GetPT(int custId, byte custLevel);

        Task<CustomerPTInfo> GetPT(int custId, byte custLevel, string site);

        Task<CopyCustomerResult> Copy(ICopyCustomerData data);
    }

    public class CopyCustomerServiceImpl : BaseService, CopyCustomerService
    {
        private const byte ChatbotSystemId = 8;

        public CopyCustomerServiceImpl()
            : this(null)
        {
        }

        public CopyCustomerServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public override IRestClient CreateRestClient()
            => new RestClient
            {
                BaseUrl = new Uri(AppSettings.CopyCustomerServiceSettings.BaseUrl),
                Authenticator = new HmacAuthenticator(AppSettings.CopyCustomerServiceSettings.ApiKey)
            };

        public async Task<Result> ValidateMaxNewMember(int agentId, string language)
            => await GetAsync<Result>($"customer/{agentId}/ValidateMaxNewMember?language={language}");

        public async Task<CustomerPTInfo> GetPT(int custId, byte custLevel)
            => await GetAsync<CustomerPTInfo>($"customer/{custId}/{custLevel}/pt?site=");

        public async Task<CustomerPTInfo> GetPT(int custId, byte custLevel, string site)
            => await GetAsync<CustomerPTInfo>($"customer/{custId}/{custLevel}/pt?site={site}");

        public async Task<CopyCustomerResult> Copy(ICopyCustomerData data)
            => await PostAsync<CopyCustomerResult>("customer/copy",
                new
                {
                    data.ActorId,
                    data.ActorUserLevel,
                    data.UserName,
                    data.Credit,
                    data.MaxCredit,
                    data.SourceCustId,
                    data.SourceUsername,
                    data.Language,
                    data.IP,
                    systemId = ChatbotSystemId
                });
    }
}