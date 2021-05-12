namespace Alpha.Services.API.Tests.Controllers
{
    using System;
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Models;
    using MediatR;
    using NSubstitute;
    using Xunit;

    public class ReportControllerTests
    {
        private const int custId = 1;
        private const int DefaultTopDownlinesDisplay = 50;
        private readonly IMediator mediator;
        private readonly ReportController reportController;

        public ReportControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            reportController = new ReportController(mediator);
        }

        [Fact]
        public void GetGetBalanceInfo_Always_CallsMediatorSendBalanceInfoQuery()
        {
            // Act
            reportController.GetBalanceInfo(custId).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<BalanceInfoQuery>(q => q.CustId == custId));
        }

        [Fact]
        public void GetGetBalances_Always_CallsMediatorSendBalanceQuery()
        {
            // Act
            reportController.GetBalances(custId, CustLevel.Agent, DefaultTopDownlinesDisplay).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<BalanceQuery>(
                q => q.CustId == custId && q.CustLevel == UserLevel.Agent.Value && q.Top == 50));
        }

        [Fact]
        public void GetGetWinLoss_Always_CallsMediatorSendWinLossQuery()
        {
            // Arrange
            var date = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var expectedDate = DateTimeOffset.FromUnixTimeMilliseconds(date).ToLocalTime();

            // Act
            reportController.GetWinLoss(custId, CustLevel.Agent, date).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<WinLossQuery>(
                q => q.CustId == custId && q.Date == expectedDate && q.CustLevel == UserLevel.Agent));
        }

        [Fact]
        public void GetOutstanding_Always_CallsMediatorSendOutstandingQuery()
        {
            // Act
            reportController.GetOutstandings(custId, CustLevel.Agent, DefaultTopDownlinesDisplay).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<OutstandingQuery>(
                q => q.CustId == custId && q.CustLevel == UserLevel.Agent));
        }

        [Fact]
        public void GetOutstanding_DownlineIdGreaterThanZero_CallsMediatorSendOutstandingQuery()
        {
            // Act
            reportController.GetDownlineOutstanding(custId, CustLevel.Agent, 1, "member01", "en-US").ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<DownlineOutstandingQuery>(
                q => q.CustId == custId && q.CustLevel == UserLevel.Agent && q.DownlineId == 1));
        }
    }
}