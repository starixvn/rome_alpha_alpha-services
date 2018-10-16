namespace Alpha.Services.Client.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class LogServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly LogServiceImpl logService;

        public LogServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            logService = new LogServiceImpl(restClient);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WriteLog_InvalidActorId_ThrowArgumentNullException(int actorId)
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(actorId);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WriteLog_InvalidCustId_ThrowArgumentNullException(int custId)
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(custId);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WriteLog_InvalidActionId_ThrowArgumentNullException(int actionId)
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(actionId);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WriteLog_InvalidSPName_ThrowArgumentNullException(string spName)
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns(spName);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WriteLog_InvalidIP_ThrowArgumentNullException(string ip)
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns("SPName");
            logData.IP.Returns(ip);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Fact]
        public void WriteLog_NewValuesIsNull_ThrowArgumentNullException()
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns("SPName");
            logData.IP.Returns("127.0.0.1");

            IDictionary<string, object> newValues = null;
            logData.NewValues.Returns(newValues);

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Fact]
        public void WriteLog_NewValuesIsEmpty_ThrowArgumentNullException()
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns("SPName");
            logData.IP.Returns("127.0.0.1");
            logData.NewValues.Returns(new Dictionary<string, object>());

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => logService.WriteLog(logData));
        }

        [Fact]
        public void WriteLog_ValidParams_CallsExecuteTaskAsync()
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns("SPName");
            logData.IP.Returns("127.0.0.1");
            var newValues = new Dictionary<string, object>
            {
                ["username"] = "username"
            };
            logData.NewValues.Returns(newValues);

            var response = Substitute.For<IRestResponse>();
            response.IsSuccessful.Returns(true);
            restClient.ExecuteTaskAsync(Arg.Any<IRestRequest>()).Returns(Task.FromResult(response));

            // Act
            logService.WriteLog(logData).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync(
                Arg.Is<IRestRequest>(
                    request => request.Resource.Equals("Log")
                            && request.Method == Method.POST));
        }

        [Fact]
        public void WriteLog_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var logData = Substitute.For<ILogData>();
            logData.ActorId.Returns(1);
            logData.CustId.Returns(2);
            logData.ActionId.Returns(3);
            logData.SPName.Returns("SPName");
            logData.IP.Returns("127.0.0.1");
            var newValues = new Dictionary<string, object>
            {
                ["username"] = "username"
            };
            logData.NewValues.Returns(newValues);

            var response = Substitute.For<IRestResponse>();
            response.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync(Arg.Any<IRestRequest>()).Returns(Task.FromResult(response));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => logService.WriteLog(logData));
        }
    }
}