namespace Alpha.Services.Tests.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.Customer;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class CustomerServiceImplTests
    {
        private const string CustomerInfoCacheKey = "Alpha.Services.Services.CustomerServiceImpl.CustomerInfo";
        private const string AccountStatusCacheKey = "Alpha.Services.Services.CustomerServiceImpl.AccountStatus";

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
            var customer = new Customer(custId, "Username", 2, "UUS", 1, true);
            cacheService.GetAsync<Customer>($"{CustomerInfoCacheKey}.{custId}")
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
            var expectedResult = new Customer(custId, "Username", 2, "UUS", 1, true);
            cacheService.GetAsync<Customer>($"{CustomerInfoCacheKey}.{custId}")
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = customerService.GetCustomerInfo(custId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetAccountStatus_ExistInCache_ReturnsCorrectResult()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;
            var expectedResult = new AccountStatus(true, 2, false);
            cacheService.GetOrSetAsync(
                $"{AccountStatusCacheKey}.{custId}.{subAccountId}",
                Arg.Any<Func<AccountStatus>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = customerService.GetAccountStatus(custId, subAccountId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.True(
                expectedResult.IsActive == actualResult.IsActive
                && expectedResult.Locked == actualResult.Locked);
        }

        [Fact]
        public void GetAccountStatusFromDB_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;

            // Act
            customerService.GetAccountStatusFromDB(custId, subAccountId);

            // Assert
            dynamicRepository.Received().Execute(Arg.Is<GetCustomerStatusCommand>(
                c => c.CustId == custId
                    && c.SubAccId == subAccountId));
        }

        [Fact]
        public void GetDownlineInfo_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var downlineUserName = "username";

            // Act
            customerService.GetDownlineInfo(custId, downlineUserName).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().ExecuteAsync(Arg.Is<GetDownlineInfoCommand>(
                command => command.UplineId == custId
                        && command.UserName == downlineUserName
                        && command.SubAccountId == 0));
        }

        [Fact]
        public void GetDownlineInfoCommand_DefaultProperties_HasValues()
        {
            var act = new GetDownlineInfoCommand(0, null, 0);
            Assert.True(act.CustId == 0);
            Assert.Null(act.UserName);
            Assert.True(act.SubAccountId == 0);
            Assert.False(act.IsActive);
            Assert.True(act.UplineId == 0);
            Assert.False(act.IsSubAcc);
            Assert.True(act.RoleId == 0);
        }

        [Fact]
        public void GetDownlineInfoCommand_GetSettingKey_ReturnCorrectValue()
        {
            var act = new GetDownlineInfoCommand(0, null, 0);
            Assert.True(act.GetSettingKey() == "Age_CustomerSelCheckBelonging_ByUsername");
        }

        [Fact]
        public void GetDownlineInfoCommand_IsValid_ReturnCorrectValue()
        {
            var act = new GetDownlineInfoCommand(1, "abc", 0);
            Assert.True(act.IsValid());
        }

        [Fact]
        public void Customer_DefaultCtor_ReturnDefaultValues()
        {
            var act = new Customer();
            Assert.True(act.CustId == 0);
            Assert.Null(act.UserName);
        }

        [Theory]
        [InlineData(1, "IBCBet")]
        [InlineData(7, "789Y")]
        [InlineData(8, "EU88")]
        [InlineData(9, "Y987")]
        [InlineData(11, "IBCIndo")]
        public void Customer_Ctor_ReturnDefaultSystemValues(int siteCode, string siteName)
        {
            var act = new Customer(0, null, 0, null, siteCode, true);
            Assert.True(act.System == siteName);
        }
    }
}