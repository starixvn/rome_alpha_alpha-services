namespace Alpha.Services.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.BalanceInfo;
    using Alpha.Services.Models;
    using Fanex.Data.Repository;

    public interface BalanceInfoService
    {
        Task<BalanceInfo> GetBalanceInfo(UserLevel custLevel, int custId);

        Task<BalanceTotal> GetBalances(UserLevel custLevel, int custId, int top);
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
                new GetBalanceInfoCriteria(custLevel, custId, moveBetsDay));
        }

        public async Task<BalanceTotal> GetBalances(UserLevel custLevel, int custId, int top)
        {
            var criteria = new GetCreditBalanceCriteria(custLevel, custId);
            var balances = (await dynamicRepository.FetchAsync<BalanceItem>(criteria))
                .OrderByDescending(b => b.Balance)
                .ToList();

            var total = new BalanceItem(0, 0);

            foreach (var item in balances)
            {
                total += item;
            }

            return criteria.TotalItems <= top
                ? new BalanceTotal(custLevel.Value, 0, total, balances)
                : new BalanceTotal(custLevel.Value, criteria.TotalItems - top, total, balances.Take(top));
        }
    }
}