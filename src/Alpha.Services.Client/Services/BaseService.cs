namespace Alpha.Services.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Configurations;
    using Alpha.Services.Client.Helpers;
    using RestSharp;

    public class BaseService
    {
        private readonly IRestClient restClient;

        protected BaseService() : this(null)
        {
        }

        internal BaseService(IRestClient restClient)
        {
            this.restClient = restClient ?? CreateDefaultClient();
        }

        public static IRestClient CreateDefaultClient()
           => new RestClient
           {
               BaseUrl = new Uri(AppSettings.ServiceSettings.BaseUrl),
               Authenticator = new HmacAuthenticator()
           };

        protected T Get<T>(string resource) where T : new()
        {
            var request = new RestRequest(resource, Method.GET);
            var response = restClient.Execute<T>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException(response.ErrorMessage);
        }

        protected async Task<T> GetAsync<T>(string resource) where T : new()
        {
            var request = new RestRequest(resource, Method.GET);
            var response = await restClient.ExecuteTaskAsync<T>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException(response.ErrorMessage);
        }
    }
}