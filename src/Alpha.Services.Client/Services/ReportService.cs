namespace Alpha.Services.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;

    public interface ReportService
    {
        IDictionary<string, object> GetOutstanding(int custId);

        IDictionary<string, object> GetBalanceInfo(int custId);

        IDictionary<string, object> GetWinLoss(int custId, DateTimeOffset date);

        Task<IDictionary<string, object>> GetOutstandingAsync(int custId);

        Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId);

        Task<IDictionary<string, object>> GetWinLossAsync(int custId, DateTimeOffset date);
    }

    public class ReportServiceImpl : BaseService, ReportService
    {
        public IDictionary<string, object> GetOutstanding(int custId)
           => Get<Dictionary<string, object>>($"report/{custId}/outstanding");

        public IDictionary<string, object> GetBalanceInfo(int custId)
            => Get<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public IDictionary<string, object> GetWinLoss(int custId, DateTimeOffset date)
            => Get<Dictionary<string, object>>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<IDictionary<string, object>> GetOutstandingAsync(int custId)
           => await GetAsync<Dictionary<string, object>>($"report/{custId}/outstanding");

        public async Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public async Task<IDictionary<string, object>> GetWinLossAsync(int custId, DateTimeOffset date)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");
    }
}