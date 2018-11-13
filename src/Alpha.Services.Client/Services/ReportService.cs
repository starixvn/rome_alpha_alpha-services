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

        IDictionary<string, object> GetDownlineOutstanding(int custId, byte custLevel, int downlineId);

        IDictionary<string, object> GetBalanceInfo(int custId);

        IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date);

        Task<OutstandingDetail> GetOutstandingsAsync(int custId, byte custLevel, int top);

        Task<IDictionary<string, object>> GetDownlineOutstandingAsync(int custId, byte custLevel, int downlineId);

        Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId);

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

        public IDictionary<string, object> GetWinLoss(int custId, byte custLevel, DateTimeOffset date)
            => Get<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<IDictionary<string, object>> GetBalanceInfoAsync(int custId)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/balanceinfo");

        public async Task<IDictionary<string, object>> GetWinLossAsync(int custId, byte custLevel, DateTimeOffset date)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}");

        public OutstandingDetail GetOutstandings(int custId, byte custLevel, int top)
            => Get<OutstandingDetail>($"report/{custId}/{custLevel}/outstandings/{top}");

        public async Task<OutstandingDetail> GetOutstandingsAsync(int custId, byte custLevel, int top)
            => await GetAsync<OutstandingDetail>($"report/{custId}/{custLevel}/outstandings/{top}");

        public IDictionary<string, object> GetDownlineOutstanding(int custId, byte custLevel, int downlineId)
            => Get<Dictionary<string, object>>($"report/{custId}/{custLevel}/outstanding/{downlineId}");

        public async Task<IDictionary<string, object>> GetDownlineOutstandingAsync(int custId, byte custLevel, int downlineId)
            => await GetAsync<Dictionary<string, object>>($"report/{custId}/{custLevel}/outstanding/{downlineId}");
    }
}