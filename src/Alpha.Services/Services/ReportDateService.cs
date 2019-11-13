namespace Alpha.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;

    public interface ReportDateService
    {
        Task<DateTime> GetBalanceUpDay();

        Task<DateTime> GetMoveBetsDay();

        Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDay();
    }

    public class ReportDateServiceImpl : ReportDateService
    {
        private const int CacheExpirationTimeInMinute = 5;
        private const string BalanceUpDayAndMoveBetsDayCacheKey = "Alpha.Services.ReportDateServiceImpl.BalanceUpDayAndMoveBetsDay";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;

        public ReportDateServiceImpl(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheService = cacheService;
        }

        public async Task<DateTime> GetBalanceUpDay()
        {
            var reportDates = await GetBalanceUpDayAndMoveBetsDay();

            return reportDates.BalanceUpDay;
        }

        public async Task<DateTime> GetMoveBetsDay()
        {
            var reportDates = await GetBalanceUpDayAndMoveBetsDay();

            return reportDates.MoveBetsDay;
        }

        public async Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDay()
         => await cacheService.GetOrSetAsync(
             BalanceUpDayAndMoveBetsDayCacheKey,
             () => dynamicRepository.Get<BalanceUpDayAndMoveBetsDay>(new GetBalanceUpDayAndMoveBetsDayCriteria()),
             new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationTimeInMinute)));
    }
}