namespace Apha.Services.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class WinLossServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReportDateService reportDateService;
        private readonly WinLossServiceImpl winLossService;

        public WinLossServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            dynamicRepository.FetchAsync<WinLossDTO>(Arg.Any<GetWinLossCriteria>()).Returns(new List<WinLossDTO>());

            reportDateService = Substitute.For<ReportDateService>();
            reportDateService.GetBalanceUpDayAndMoveBetsDay()
                .Returns(Task.FromResult(new BalanceUpDayAndMoveBetsDay(DateTime.Now, DateTime.Now)));

            winLossService = new WinLossServiceImpl(dynamicRepository, reportDateService);
        }

        [Fact]
        public void GetWinLoss_Always_CallsReportDateService()
        {
            // Arrange
            var param = new WinLossParam(UserLevel.Agent, 1, DateTime.Now, DateTime.Now, new List<int> { 1, 2 });

            // Act
            winLossService.GetWinLoss(param).GetAwaiter().GetResult();

            // Assert
            reportDateService.Received().GetBalanceUpDayAndMoveBetsDay();
        }

        [Fact]
        public void GetWinLoss_Always_CallsDynamicRepository()
        {
            // Arrange
            var param = new WinLossParam(UserLevel.Agent, 1, DateTime.Now, DateTime.Now, new List<int> { 1, 2 });

            // Act
            winLossService.GetWinLoss(param).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().FetchAsync<WinLossDTO>(Arg.Any<GetWinLossCriteria>());
        }
    }
}