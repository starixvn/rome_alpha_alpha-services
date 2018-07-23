namespace Alpha.Services.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public class AlphaServicesClient : IAlphaServicesClient
    {
        private readonly IRestClient restClient;

        public AlphaServicesClient(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public OutstandingModel GetOutstanding(int custId)
           => Get<OutstandingModel>($"report/{custId}/outstanding");

        public BalanceInfo GetBalanceInfo(int custId)
            => Get<BalanceInfo>($"report/{custId}/balanceinfo");

        public WinLoss GetWinLoss(int custId, DateTimeOffset date)
            => Get<WinLoss>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");

        public IEnumerable<Message> GetMessages(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime)
            => Get<List<Message>>($"message/{custId}/messages/{messageType}/{language}/{fromTime.ToUnixTimeMilliseconds()}/{toTime.ToUnixTimeMilliseconds()}");

        public async Task<OutstandingModel> GetOutstandingAsync(int custId)
            => await GetAsync<OutstandingModel>($"report/{custId}/outstanding");

        public async Task<BalanceInfo> GetBalanceInfoAsync(int custId)
            => await GetAsync<BalanceInfo>($"report/{custId}/balanceinfo");

        public async Task<WinLoss> GetWinLossAsync(int custId, DateTimeOffset date)
            => await GetAsync<WinLoss>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<IEnumerable<Message>> GetMessagesAsync(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime)
            => await GetAsync<List<Message>>($"message/{custId}/messages/{messageType}/{language}/{fromTime.ToUnixTimeMilliseconds()}/{toTime.ToUnixTimeMilliseconds()}");

        private T Get<T>(string resource) where T : new()
        {
            var request = new RestRequest(resource, Method.GET);
            var response = restClient.Execute<T>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new InvalidOperationException(response.ErrorMessage);
        }

        private async Task<T> GetAsync<T>(string resource) where T : new()
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