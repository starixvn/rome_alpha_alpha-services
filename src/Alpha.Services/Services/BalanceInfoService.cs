namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface BalanceInfoService
    {
        Task<BalanceInfo> GetBalanceInfo(BalanceInfoParam param);
    }

    public class BalanceInfoServiceImpl : BalanceInfoService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReportDateService reportDateService;

        public BalanceInfoServiceImpl(
            IDynamicRepository dynamicRepository,
            ReportDateService reportDateService)
        {
            this.dynamicRepository = dynamicRepository;
            this.reportDateService = reportDateService;
        }

        public async Task<BalanceInfo> GetBalanceInfo(BalanceInfoParam param)
        {
            var moveBetsDay = await reportDateService.GetMoveBetsDay();

            return await dynamicRepository.GetAsync<BalanceInfo>(
                new GetBalanceInfoCriteria(
                    param.CustLevel,
                    param.CustId,
                    moveBetsDay));
        }
    }
}