namespace Alpha.Services.API.Tests.Controllers
{
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
    using MediatR;
    using NSubstitute;
    using Xunit;

    public class LogControllerTests
    {
        private readonly IMediator mediator;
        private readonly LogController logController;

        public LogControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            logController = new LogController(mediator);
        }

        [Fact]
        public void InsertLog_Always_CallsMediatorSendInsertLogCommand()
        {
            // Arrange & Act
            logController.InsertLog(new InsertLogCommand()).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Any<InsertLogCommand>());
        }
    }
}