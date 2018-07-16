using System;
using System.Threading.Tasks;
using Alpha.Services.Criteria;
using Alpha.Services.Models;
using Fanex.Data.Repository;

namespace Alpha.Services.Services
{
    public interface ReportDateService
    {
        Task<DateTime> GetMoveBetsDay();
    }

    public class ReportDateServiceImpl : ReportDateService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ReportDateServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<DateTime> GetMoveBetsDay()
        {
            var data = await GetBalanceUpDayAndMoveBetsDay();

            return data.MoveBetsDay;
        }

        private async Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDay()
            => await dynamicRepository.GetAsync<BalanceUpDayAndMoveBetsDay>(new GetBalanceUpDayAndMoveBetsDayCriteria());
    }
}