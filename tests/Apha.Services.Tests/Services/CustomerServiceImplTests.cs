namespace Apha.Services.Tests.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class CustomerServiceImplTests
    {
        private const string CustomerInfoCacheKey = "Alpha.Services.Services.CustomerServiceImpl.CustomerInfo";
        private const string AccountClosedCacheKey = "Alpha.Services.Services.CustomerServiceImpl.IsAccountActivated";

        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerServiceImpl customerService;
        private readonly ICacheService cacheService;

        public CustomerServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheService = Substitute.For<ICacheService>();
            customerService = new CustomerServiceImpl(dynamicRepository, cacheService);
        }

        [Fact]
        public void GetCustomerInfo_ExistInCache_DoesNOTCallDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var customer = new Customer(custId, "Username", 2, "UUS", 1);
            cacheService.GetOrSetAsync(
                $"{CustomerInfoCacheKey}.{custId}",
                Arg.Any<Func<Customer>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(customer));

            // Act
            customerService.GetCustomerInfo(custId).ConfigureAwait(false);

            // Assert
            dynamicRepository.DidNotReceive().GetAsync<Customer>(
                Arg.Any<GetCustomerInfoCriteria>());
        }

        [Fact]
        public void GetCustomerInfo_ExistInCache_ReturnsCorrectResult()
        {
            // Arrange
            var custId = 1;
            var expectedResult = new Customer(custId, "Username", 2, "UUS", 1);
            cacheService.GetOrSetAsync(
                $"{CustomerInfoCacheKey}.{custId}",
                Arg.Any<Func<Customer>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = customerService.GetCustomerInfo(custId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsAccountClosed_ExistInCache_ReturnsCorrectResult()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;
            var expectedResult = false;
            cacheService.GetOrSetAsync(
                $"{AccountClosedCacheKey}.{custId}.{subAccountId}",
                Arg.Any<Func<bool>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = customerService.IsAccountClosed(custId, subAccountId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsAccountActive_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;

            // Act
            customerService.IsAccountActive(custId, subAccountId);

            // Assert
            dynamicRepository.Received().Execute(Arg.Is<GetCustomerStatusCommand>(
                c => c.CustId == custId
                    && c.SubAccId == subAccountId));
        }
    }
}