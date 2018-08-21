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
            this.restClient = restClient ?? CreateRestClient();
        }

        public virtual IRestClient CreateRestClient()
           => new RestClient
           {
               BaseUrl = new Uri(AppSettings.AlphaServiceSettings.BaseUrl),
               Authenticator = new HmacAuthenticator(AppSettings.AlphaServiceSettings.ApiKey)
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