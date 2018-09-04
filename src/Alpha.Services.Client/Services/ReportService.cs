namespace Alpha.Services.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ReportService
    {
        IDictionary<string, object> GetOutstanding(int custId, byte custLevel);

        IDictionary<string, object> GetBalanceInfo(int custId);

        IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date);

        Task<IDictionary<string, object>> GetOutstandingAsync(int custId, byte custLevel);

        Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId);

        Task<IDictionary<string, object>> GetWinLossAsync(int custId, byte custLevel, DateTimeOffset date);
    }

    public class ReportServiceImpl : BaseService, ReportService
    {
        public IDictionary<string, object> GetOutstanding(int custId, byte custLevel)
           => Get<Dictionary<string, object>>($"report/{custId}/{custLevel}/outstanding");

        public IDictionary<string, object> GetBalanceInfo(int custId)
            => Get<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date)
            => Get<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<IDictionary<string, object>> GetOutstandingAsync(int custId, byte custLevel)
           => await GetAsync<Dictionary<string, object>>($"report/{custId}/{custLevel}/outstanding");

        public async Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public async Task<IDictionary<string, object>> GetWinLossAsync(int custId, byte custLevel, DateTimeOffset date)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");
    }
}