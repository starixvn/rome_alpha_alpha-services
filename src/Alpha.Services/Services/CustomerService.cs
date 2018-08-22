namespace Alpha.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;

    public interface CustomerService
    {
        Task<Customer> GetCustomerInfo(int custId);

        Task<bool> IsAccountClosed(int custId, int subAccountId);
    }

    public class CustomerServiceImpl : CustomerService
    {
        private const string AccountClosedCacheKey = "Alpha.Services.Services.CustomerServiceImpl.IsAccountActivated.{0}.{1}";
        private readonly IDynamicRepository dynamicRepository;

        public CustomerServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<Customer> GetCustomerInfo(int custId)
            => await dynamicRepository.GetAsync<Customer>(new GetCustomerInfoCriteria(custId));

        public async Task<bool> IsAccountClosed(int custId, int subAccountId)
            => await ConfiguredCacheService.GetOrSetAsync(
                string.Format(AccountClosedCacheKey, custId, subAccountId),
                () => !IsAccountActive(custId, subAccountId),
                new CacheItemOptions().SetAbsoluteExpiration(new TimeSpan(0, 0, 30)));

        private bool IsAccountActive(int custId, int subAccountId)
        {
            var command = new GetCustomerStatusCommand(custId, subAccountId);

            dynamicRepository.Execute(command);

            return command.IsActive;
        }
    }
}