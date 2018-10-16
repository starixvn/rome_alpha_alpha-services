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

        protected BaseService(IRestClient restClient)
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
            => Execute<T>(resource, Method.GET);

        protected async Task<T> GetAsync<T>(string resource) where T : new()
            => await ExecuteAsync<T>(resource, Method.GET);

        protected T Post<T>(string resource, object data) where T : new()
            => Execute<T>(resource, Method.POST, data);

        protected async Task<T> PostAsync<T>(string resource, object data) where T : new()
            => await ExecuteAsync<T>(resource, Method.POST, data);

        protected async Task PostAsync(string resource, object data)
            => await ExecuteAsync(resource, Method.POST, data);

        private T Execute<T>(string resource, Method method, object data = null) where T : new()
        {
            var request = new RestRequest(resource, method);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            var response = restClient.Execute<T>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException(
                string.Join(
                    "\r\n",
                    $"Uri: {response.ResponseUri}",
                    $"Status: {response.StatusCode}",
                    $"Content: {response.Content}",
                    $"ErrorMessage: {response.ErrorMessage}"));
        }

        private async Task<T> ExecuteAsync<T>(string resource, Method method, object data = null) where T : new()
        {
            var request = new RestRequest(resource, method);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            var response = await restClient.ExecuteTaskAsync<T>(request).ConfigureAwait(false);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException(
                string.Join(
                    "\r\n",
                    $"Uri: {response.ResponseUri}",
                    $"Status: {response.StatusCode}",
                    $"Content: {response.Content}",
                    $"ErrorMessage: {response.ErrorMessage}"));
        }

        private async Task ExecuteAsync(string resource, Method method, object data = null)
        {
            var request = new RestRequest(resource, method);

            if (data != null)
            {
                request.AddJsonBody(data);
            }

            var response = await restClient.ExecuteTaskAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new InvalidOperationException(
                    string.Join(
                        "\r\n",
                        $"Uri: {response.ResponseUri}",
                        $"Status: {response.StatusCode}",
                        $"Content: {response.Content}",
                        $"ErrorMessage: {response.ErrorMessage}"));
            }
        }
    }
}