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
        private readonly IMediator mediator;
        private readonly ReportController reportController;

        public ReportControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            reportController = new ReportController(mediator);
        }

        [Fact]
        public void GetOutstanding_Always_CallsMediatorSendOutstandingQuery()
        {
            // Act
            reportController.GetOutstanding(custId, CustLevel.Agent).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<OutstandingQuery>(
                q => q.CustId == custId && q.CustLevel == UserLevel.Agent));
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
        public void GetOutstandingDetail_Always_CallsMediatorSendOutstandingDetailQuery()
        {
            // Act
            reportController.GetOutstandingDetail(custId, CustLevel.Agent).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<OutstandingDetailQuery>(
                q => q.CustId == custId && q.CustLevel == UserLevel.Agent));
        }
    }
}