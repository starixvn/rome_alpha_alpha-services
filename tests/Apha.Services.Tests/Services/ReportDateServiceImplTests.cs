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

    public class ReportDateServiceImplTests
    {
        private const string BalanceUpDayAndMoveBetsDayCacheKey = "Alpha.Services.ReportDateServiceImpl.BalanceUpDayAndMoveBetsDay";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;
        private readonly ReportDateServiceImpl reportDateService;
        private readonly BalanceUpDayAndMoveBetsDay balanceUpDayAndMoveBetsDay;

        public ReportDateServiceImplTests()
        {
            var balanceUpDay = new DateTime(2010, 1, 1);
            var moveBetsDay = new DateTime(2010, 1, 5);
            balanceUpDayAndMoveBetsDay = new BalanceUpDayAndMoveBetsDay(balanceUpDay, moveBetsDay);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            dynamicRepository.Get<BalanceUpDayAndMoveBetsDay>(Arg.Any<GetBalanceUpDayAndMoveBetsDayCriteria>())
                .Returns(balanceUpDayAndMoveBetsDay);

            cacheService = Substitute.For<ICacheService>();
            cacheService.GetOrSetAsync(
                BalanceUpDayAndMoveBetsDayCacheKey,
                Arg.Any<Func<BalanceUpDayAndMoveBetsDay>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(balanceUpDayAndMoveBetsDay));

            reportDateService = Substitute.ForPartsOf<ReportDateServiceImpl>(dynamicRepository, cacheService);
        }

        [Fact]
        public void GetBalanceUpDate_Always_CallsGetBalanceUpDayAndMoveBetsDay()
        {
            // Act
            reportDateService.GetBalanceUpDay().ConfigureAwait(false);

            // Assert
            reportDateService.Received().GetBalanceUpDayAndMoveBetsDay().ConfigureAwait(false);
        }

        [Fact]
        public void GetMoveBetsDay_Always_CallsGetBalanceUpDayAndMoveBetsDay()
        {
            // Act
            reportDateService.GetMoveBetsDay().ConfigureAwait(false);

            // Assert
            reportDateService.Received().GetBalanceUpDayAndMoveBetsDay().ConfigureAwait(false);
        }

        [Fact]
        public void GetBalanceUpDayAndMoveBetsDay_Always_ReturnsCorrectData()
        {
            // Act
            var result = reportDateService.GetBalanceUpDayAndMoveBetsDay().GetAwaiter().GetResult();

            // Assert
            Assert.True(result.BalanceUpDay == balanceUpDayAndMoveBetsDay.BalanceUpDay
                && result.MoveBetsDay == balanceUpDayAndMoveBetsDay.MoveBetsDay);
        }
    }
}