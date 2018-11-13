namespace Alpha.Services.Tests.Services
{
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
    }
}