namespace Alpha.Services.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface ReportService
    {
        OutstandingModel GetOutstanding(int custId);

        BalanceInfo GetBalanceInfo(int custId);

        WinLoss GetWinLoss(int custId, DateTimeOffset date);

        Task<OutstandingModel> GetOutstandingAsync(int custId);

        Task<BalanceInfo> GetBalanceInfoAsync(int custId);

        Task<WinLoss> GetWinLossAsync(int custId, DateTimeOffset date);
    }

    public class ReportServiceImpl : BaseService, ReportService
    {
        public ReportServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public OutstandingModel GetOutstanding(int custId)
           => Get<OutstandingModel>($"report/{custId}/outstanding");

        public BalanceInfo GetBalanceInfo(int custId)
            => Get<BalanceInfo>($"report/{custId}/balanceinfo");

        public WinLoss GetWinLoss(int custId, DateTimeOffset date)
            => Get<WinLoss>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");

        public async Task<OutstandingModel> GetOutstandingAsync(int custId)
           => await GetAsync<OutstandingModel>($"report/{custId}/outstanding");

        public async Task<BalanceInfo> GetBalanceInfoAsync(int custId)
            => await GetAsync<BalanceInfo>($"report/{custId}/balanceinfo");

        public async Task<WinLoss> GetWinLossAsync(int custId, DateTimeOffset date)
            => await GetAsync<WinLoss>($"report/{custId}/winloss/{date.ToUnixTimeMilliseconds()}");
    }
}