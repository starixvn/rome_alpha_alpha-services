namespace Alpha.Services.API.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class OutstandingHandlerTests
    {
        private readonly OutstandingService outstandingService;
        private readonly OutstandingHandler outstandingHandler;

        public OutstandingHandlerTests()
        {
            outstandingService = Substitute.For<OutstandingService>();
            outstandingHandler = new OutstandingHandler(outstandingService);
        }

        [Fact]
        public void Handle_Always_CallsOutstandingServiceGetOutstanding()
        {
            // Arrange
            var request = new OutstandingQuery(1, CustLevel.Agent);

            // Act
            outstandingHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            outstandingService.Received()
                .GetOutstanding(Arg.Is(request.CustLevel), Arg.Is(request.CustId));
        }

        [Fact]
        public void Handle_Always_ReturnsCorrectResult()
        {
            // Arrange
            var request = new OutstandingQuery(1, CustLevel.Agent);
            var outstanding = new OutstandingModel(1, 1);
            outstandingService.GetOutstanding(Arg.Any<UserLevel>(), Arg.Any<int>())
                .Returns(Task.FromResult(outstanding));
            var expectedResult = new Dictionary<string, object>
            {
                [UserLevel.Member.DisplayName] = outstanding.TotalOutstanding,
                [request.CustLevel.DisplayName] = outstanding.Outstanding
            };

            // Act
            var actualResult = outstandingHandler.Handle(request, default(CancellationToken))
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}