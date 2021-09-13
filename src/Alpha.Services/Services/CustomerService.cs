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

        Task<AccountStatus> GetAccountStatus(int custId, int subAccountId);
    }

    public class CustomerServiceImpl : CustomerService
    {
        private const int CacheExpirationTimeInSecond = 30;
        private const string CustomerInfoCacheKey = "Alpha.Services.Services.CustomerServiceImpl.CustomerInfo";
        private const string AccountStatusCacheKey = "Alpha.Services.Services.CustomerServiceImpl.AccountStatus.{0}.{1}";
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
        {
            var cacheKey = $"{CustomerInfoCacheKey}.{custId}";
            var customer = await cacheService.GetAsync<Customer>(cacheKey);

            if (customer == null)
            {
                customer = await dynamicRepository.GetAsync<Customer>(new GetCustomerInfoCriteria(custId));

                if (customer != null)
                {
                    await cacheService.SetAsync(cacheKey, customer, new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
                }
            }

            return customer;
        }

        public async Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUserName)
        {
            var command = new GetDownlineInfoCommand(custId, downlineUserName, 0);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            return new DownlineInfo(command.CustId, command.IsSubAcc, command.IsActive, (byte)command.RoleId);
        }

        public async Task<AccountStatus> GetAccountStatus(int custId, int subAccountId)
            => await cacheService.GetOrSetAsync(
                string.Format(AccountStatusCacheKey, custId, subAccountId),
                () => GetAccountStatusFromDB(custId, subAccountId),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTimeInSecond)));

        internal AccountStatus GetAccountStatusFromDB(int custId, int subAccountId)
        {
            var command = new GetCustomerStatusCommand(custId, subAccountId);

            dynamicRepository.Execute(command);

            return new AccountStatus(command.IsActive, command.Locked, command.IsProtectedMode);
        }
    }
}