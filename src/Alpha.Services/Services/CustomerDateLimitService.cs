namespace Alpha.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Fanex.Data.Repository;

    public interface CustomerDateLimitService
    {
        Task<DateTime> GetCustomerDateLimit(int custId);
    }

    public class CustomerDateLimitServiceImpl : CustomerDateLimitService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CustomerDateLimitServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<DateTime> GetCustomerDateLimit(int custId)
            => await dynamicRepository.GetAsync<DateTime>(new GetCustomerDateLimitCriteria(custId));
    }
}