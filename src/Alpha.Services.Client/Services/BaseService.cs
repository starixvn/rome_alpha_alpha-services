namespace Alpha.Services.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using RestSharp;

    public abstract class BaseService
    {
        private readonly IRestClient restClient;

        protected BaseService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

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