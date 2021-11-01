namespace Alpha.Services.API.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class BalanceInfoHandlerTests
    {
        private readonly CustomerService customerService;
        private readonly BalanceInfoService balanceInfoService;
        private readonly OutstandingService outstandingService;
        private readonly BalanceInfoHandler balanceInfoHandler;

        public BalanceInfoHandlerTests()
        {
            customerService = Substitute.For<CustomerService>();
            balanceInfoService = Substitute.For<BalanceInfoService>();
            outstandingService = Substitute.For<OutstandingService>();
            balanceInfoHandler = new BalanceInfoHandler(customerService, balanceInfoService, outstandingService);
        }

        [Fact]
        public void Handle_Always_CallsGetCustomerInfo()
        {
            // Arrange
            var request = new BalanceInfoQuery(1);

            // Act
            balanceInfoHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().GetCustomerInfo(Arg.Is(request.CustId));
        }

        [Fact]
        public void Handle_CustomerIsNotNull_CallsGetBalanceInfo()
        {
            // Arrange
            var request = new BalanceInfoQuery(1);
            var customer = new Customer(1, "username", CustLevel.Agent, "UUS", 1, true, false);
            customerService.GetCustomerInfo(Arg.Any<int>())
                .Returns(Task.FromResult(customer));

            // Act
            balanceInfoHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            balanceInfoService.Received().GetBalanceInfo(Arg.Is(customer.CustLevel), Arg.Is(request.CustId));
        }

        [Fact]
        public void Handle_CustomerIsNotNull_ReturnsCorrectResult()
        {
            // Arrange
            var request = new BalanceInfoQuery(1);
            var customer = new Customer(1, "username", CustLevel.Agent, "UUS", 1, true, false);
            customerService.GetCustomerInfo(Arg.Any<int>())
                .Returns(Task.FromResult(customer));

            var balanceInfo = new BalanceInfo(1, 1, 1, 1, 1, 1, 1, 1);
            balanceInfoService.GetBalanceInfo(Arg.Any<UserLevel>(), Arg.Any<int>())
                .Returns(Task.FromResult(balanceInfo));

            var outstanding = new TotalOutstandingModel(1, 2);
            outstandingService.GetTotalOutstanding(Arg.Any<UserLevel>(), Arg.Any<int>())
                .Returns(Task.FromResult(outstanding));

            var expectedResult = new Dictionary<string, object>
            {
                [nameof(customer.CurrencyName)] = customer.CurrencyName,
                [nameof(balanceInfo.CashBalance)] = balanceInfo.CashBalance,
                [nameof(balanceInfo.YesterdayCashBalance)] = balanceInfo.YesterdayCashBalance,
                [nameof(balanceInfo.TotalBalance)] = balanceInfo.TotalBalance,
                [nameof(balanceInfo.YesterdayTotalBalance)] = balanceInfo.YesterdayTotalBalance,
                [nameof(balanceInfo.TodayWinLoss)] = balanceInfo.TodayWinLoss,
                [nameof(balanceInfo.YesterdayWinLoss)] = balanceInfo.YesterdayWinLoss,
                [nameof(balanceInfo.GivenCredit)] = balanceInfo.GivenCredit,
                [customer.CustLevel.GetDirectDownLineLevel().DisplayName + nameof(balanceInfo.TotalCredit)] = balanceInfo.TotalCredit,
                [$"{UserLevel.Member.DisplayName}Outstanding"] = outstanding.TotalOutstanding,
                [$"{customer.CustLevel.DisplayName}Outstanding"] = outstanding.Outstanding
            };

            // Act
            var actualResult = balanceInfoHandler.Handle(request, default(CancellationToken))
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task HandleTopBanlanceInfo_Always_CallsGetBalancesService()
        {
            // Arrange
            var request = new BalanceQuery(1, 2, 50);

            // Act
            await balanceInfoHandler.Handle(request, default(CancellationToken));

            // Assert
            await balanceInfoService
                .Received()
                .GetBalances(Enumeration.FromValue<UserLevel>(request.CustLevel), request.CustId, request.Top);
        }
    }
}