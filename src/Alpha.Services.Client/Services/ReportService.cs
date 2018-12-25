namespace Alpha.Services.Client.Services
{
    using Alpha.Services.Client.Models;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ReportService
    {
        OutstandingDetail GetOutstandings(int custId, byte custLevel, int top);

        OutstandingDetail GetDownlineOutstanding(int custId, byte custLevel, int downlineId, string downlineName, string language);

        IDictionary<string, object> GetBalanceInfo(int custId);

        BalanceTotal GetBalances(int custId, byte custLevel, int top);

        IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date);

        Task<OutstandingDetail> GetOutstandingsAsync(int custId, byte custLevel, int top);

        Task<OutstandingDetail> GetDownlineOutstandingAsync(int custId, byte custLevel, int downlineId, string downlineName, string language);

        Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId);

        Task<BalanceTotal> GetBalancesAsync(int custId, byte custLevel, int top);

        Task<IDictionary<string, object>> GetWinLossAsync(int custId, byte custLevel, DateTimeOffset date);
    }

    public class ReportServiceImpl : BaseService, ReportService
    {
        public ReportServiceImpl()
            : this(null)
        {
        }

        internal ReportServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public IDictionary<string, object> GetBalanceInfo(int custId)
            => Get<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public BalanceTotal GetBalances(int custId, byte custLevel, int top)
            => Get<BalanceTotal>($"report/{custId}/{custLevel}/balances/{top}");

        public IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date)
            => Get<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public async Task<BalanceTotal> GetBalancesAsync(int custId, byte custLevel, int top)
            => await GetAsync<BalanceTotal>($"report/{custId}/{custLevel}/balances/{top}");

        public async Task<IDictionary<string, object>> GetWinLossAsync(int custId, byte custLevel, DateTimeOffset date)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");

        public OutstandingDetail GetOutstandings(int custId, byte custLevel, int top)
            => Get<OutstandingDetail>($"report/{custId}/{custLevel}/outstandings/{top}");

        public async Task<OutstandingDetail> GetOutstandingsAsync(int custId, byte custLevel, int top)
            => await GetAsync<OutstandingDetail>($"report/{custId}/{custLevel}/outstandings/{top}");

        public OutstandingDetail GetDownlineOutstanding(int custId, byte custLevel, int downlineId, string downlineName, string language)
            => Get<OutstandingDetail>($"report/{custId}/{custLevel}/outstanding/{downlineId}/{downlineName}/{language}");

        public async Task<OutstandingDetail> GetDownlineOutstandingAsync(int custId, byte custLevel, int downlineId, string downlineName, string language)
            => await GetAsync<OutstandingDetail>($"report/{custId}/{custLevel}/outstanding/{downlineId}/{downlineName}/{language}");
    }
}