[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Alpha.Services.Tests")]

namespace Alpha.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.Customer;
    using Alpha.Services.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;

    public interface CustomerService
    {
        Task<Customer> GetCustomerInfo(int custId);

        Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUserName);

        Task<bool> IsAccountClosed(int custId, int subAccountId);
    }

    public class CustomerServiceImpl : CustomerService
    {
        private const string CustomerInfoCacheKey = "Alpha.Services.Services.CustomerServiceImpl.CustomerInfo";
        private const string AccountClosedCacheKey = "Alpha.Services.Services.CustomerServiceImpl.IsAccountActivated.{0}.{1}";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;

        public CustomerServiceImpl(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheService = cacheService;
        }

        public async Task<Customer> GetCustomerInfo(int custId)
            => await cacheService.GetOrSetAsync(
                $"{CustomerInfoCacheKey}.{custId}",
                () => dynamicRepository.Get<Customer>(new GetCustomerInfoCriteria(custId)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));

        public async Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUserName)
        {
            var command = new GetDownlineInfoCommand(custId, downlineUserName, 0);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            return new DownlineInfo(command.CustId, command.IsSubAcc, command.IsActive, (byte)command.RoleId);
        }

        public async Task<bool> IsAccountClosed(int custId, int subAccountId)
            => await cacheService.GetOrSetAsync(
                string.Format(AccountClosedCacheKey, custId, subAccountId),
                () => !IsAccountActive(custId, subAccountId),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));

        internal bool IsAccountActive(int custId, int subAccountId)
        {
            var command = new GetCustomerStatusCommand(custId, subAccountId);

            dynamicRepository.Execute(command);

            return command.IsActive;
        }
    }
}