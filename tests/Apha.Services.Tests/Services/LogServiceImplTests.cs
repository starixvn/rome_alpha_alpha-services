namespace Apha.Services.Tests.Services
{
    using System.Collections.Generic;
    using Alpha.Services.Criteria;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class LogServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly LogServiceImpl logService;

        public LogServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            logService = new LogServiceImpl(dynamicRepository);
        }

        [Fact]
        public void WriteLog_Always_CallsExecuteAsyncInsertUserLogCommand()
        {
            // Arrange
            var param = new LogParam(
                actorId: 1,
                custId: 1,
                actionId: 1,
                spName: "spName",
                ip: "127.0.0.1",
                newValues: new Dictionary<string, object>
                {
                    ["username"] = "username"
                },
                oldValues: null);

            // Act
            logService.WriteLog(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().ExecuteAsync(Arg.Any<InsertUserLogCommand>());
        }
    }
}