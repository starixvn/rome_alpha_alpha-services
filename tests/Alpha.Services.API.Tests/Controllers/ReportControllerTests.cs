namespace Alpha.Services.API.Tests.Controllers
{
    using System;
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
    using MediatR;
    using NSubstitute;
    using Xunit;

    public class ReportControllerTests
    {
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
            // Arrange
            var custId = 1;

            // Act
            reportController.GetOutstanding(custId).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<OutstandingQuery>(q => q.CustId == custId));
        }

        [Fact]
        public void GetGetBalanceInfo_Always_CallsMediatorSendBalanceInfoQuery()
        {
            // Arrange
            var custId = 1;

            // Act
            reportController.GetBalanceInfo(custId).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<BalanceInfoQuery>(q => q.CustId == custId));
        }

        [Fact]
        public void GetGetWinLoss_Always_CallsMediatorSendWinLossQuery()
        {
            // Arrange
            var custId = 1;
            var date = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var expectedDate = DateTimeOffset.FromUnixTimeMilliseconds(date).ToLocalTime();

            // Act
            reportController.GetWinLoss(custId, date).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<WinLossQuery>(q => q.CustId == custId && q.Date == expectedDate));
        }
    }
}