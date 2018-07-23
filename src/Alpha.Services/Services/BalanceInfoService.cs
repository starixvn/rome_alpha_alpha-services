namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Fanex.Data.Repository;

    public interface BalanceInfoService
    {
        Task<BalanceInfo> GetBalanceInfo(UserLevel custLevel, int custId);
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

        public async Task<BalanceInfo> GetBalanceInfo(UserLevel custLevel, int custId)
        {
            var moveBetsDay = await reportDateService.GetMoveBetsDay();

            return await dynamicRepository.GetAsync<BalanceInfo>(
                new GetBalanceInfoCriteria(
                    custLevel,
                    custId,
                    moveBetsDay));
        }
    }
}