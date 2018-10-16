namespace Alpha.Services.API.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class LogHandlerTests
    {
        private readonly LogService logService;
        private readonly LogHandler logHandler;

        public LogHandlerTests()
        {
            logService = Substitute.For<LogService>();
            logHandler = new LogHandler(logService);
        }

        [Fact]
        public void Handle_Always_CallsWriteLog()
        {
            // Arrange
            var request = new InsertLogCommand
            {
                ActorId = 1,
                CustId = 2,
                ActionId = 3,
                SpName = "SPName",
                IP = "127.0.0.1",
                NewValues = new Dictionary<string, object>(),
                OldValues = new Dictionary<string, object>()
            };

            // Act
            logHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            logService.Received().WriteLog(
                Arg.Is<LogParam>(
                    p => p.ActorId == request.ActorId
                        && p.CustId == request.CustId
                        && p.ActionId == request.ActionId
                        && p.SpName == request.SpName
                        && p.IP == request.IP
                        && p.NewValues == request.NewValues
                        && p.OldValues == request.OldValues));
        }
    }
}