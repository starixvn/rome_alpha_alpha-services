namespace Alpha.Services.Tests.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.BalanceInfo;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class BalanceInfoServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReportDateService reportDateService;
        private readonly BalanceInfoServiceImpl balanceInfoService;

        public BalanceInfoServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            reportDateService = Substitute.For<ReportDateService>();
            balanceInfoService = new BalanceInfoServiceImpl(dynamicRepository, reportDateService);
        }

        [Fact]
        public void GetBalanceInfo_Always_CallsReportDateService()
        {
            // Act
            balanceInfoService.GetBalanceInfo(UserLevel.Agent, 1).GetAwaiter().GetResult();

            // Assert
            reportDateService.Received().GetMoveBetsDay();
        }

        [Fact]
        public void GetBalanceInfo_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var custLevel = UserLevel.Agent;

            // Act
            balanceInfoService.GetBalanceInfo(custLevel, custId).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().GetAsync<BalanceInfo>(
                Arg.Is<GetBalanceInfoCriteria>(c => c.CustLevel == custLevel && c.CustId == custId));
        }

        [Fact]
        public async Task GetBalances_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var custLevel = UserLevel.Agent;

            // Act
            await balanceInfoService.GetBalances(custLevel, custId, 50);

            // Assert
            await dynamicRepository
                .Received()
                .FetchAsync<BalanceItem>(Arg.Any<GetCreditBalanceCriteria>());
        }

        [Fact]
        public async Task GetBalances_Total_CalculateCorrectValue()
        {
            // Arrange
            var custId = 1;
            var custLevel = UserLevel.Agent;
            var balances = new List<BalanceItem>
            {
                new BalanceItem("master", 0, 0),
                new BalanceItem("master1", 5, 10),
                new BalanceItem("master2", 15, 20),
                new BalanceItem("master3", 25, 30)
            };
            dynamicRepository
                .FetchAsync<BalanceItem>(Arg.Any<GetCreditBalanceCriteria>())
                .Returns(balances);

            // Act
            var actualResult = await balanceInfoService.GetBalances(custLevel, custId, 50);

            // Assert
            Assert.True(
                actualResult.Total.Balance == 45
                && actualResult.Total.YesterdayBalance == 60);
        }
    }
}