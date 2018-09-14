namespace Alpha.Services.API.Tests.Handlers
{
    using System.Threading;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
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
    }
}