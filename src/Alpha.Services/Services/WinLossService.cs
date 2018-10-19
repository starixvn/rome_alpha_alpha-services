namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.WinLoss;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface WinLossService
    {
        Task<WinLossModel> GetWinLoss(WinLossParam param);
    }

    public class WinLossServiceImpl : WinLossService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReportDateService reportDateService;

        public WinLossServiceImpl(
            IDynamicRepository dynamicRepository,
            ReportDateService reportDateService)
        {
            this.dynamicRepository = dynamicRepository;
            this.reportDateService = reportDateService;
        }

        public async Task<WinLossModel> GetWinLoss(WinLossParam param)
        {
            var reportDate = await reportDateService.GetBalanceUpDayAndMoveBetsDay();

            var winLoss = await dynamicRepository.GetAsync<WinLossModel>(
                new GetWinLossCriteria(
                    param.CustLevel,
                    param.CustId,
                    param.Date,
                    param.Date,
                    reportDate.BalanceUpDay,
                    reportDate.MoveBetsDay,
                    param.ProductIds));

            return winLoss;
        }
    }
}