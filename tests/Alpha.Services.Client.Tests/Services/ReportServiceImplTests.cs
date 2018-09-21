namespace Alpha.Services.Client.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class ReportServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly ReportServiceImpl reportService;

        public ReportServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            reportService = new ReportServiceImpl(restClient);
        }

        [Fact]
        public void GetOutstanding_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstanding", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetOutstanding(custId, custLevel);

            // Assert
            restClient.Received().Execute<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstanding_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetOutstanding(custId, custLevel));
        }

        [Fact]
        public void GetBalanceInfo_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;

            var request = new RestRequest($"report/{custId}/balanceinfo", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetBalanceInfo(custId);

            // Assert
            restClient.Received().Execute<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetBalanceInfo_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetBalanceInfo(custId));
        }

        [Fact]
        public void GetWinLoss_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var request = new RestRequest($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetWinLoss(custId, custLevel, date);

            // Assert
            restClient.Received().Execute<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetWinLoss_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetWinLoss(custId, custLevel, date));
        }

        [Fact]
        public void GetOutstandingAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstanding", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetOutstandingAsync(custId, custLevel).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstandingAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => reportService.GetOutstandingAsync(custId, custLevel));
        }

        [Fact]
        public void GetBalanceInfoAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;

            var request = new RestRequest($"report/{custId}/balanceinfo", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetBalanceInfoAsync(custId).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetBalanceInfoAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => reportService.GetBalanceInfoAsync(custId));
        }

        [Fact]
        public void GetWinLossAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var request = new RestRequest($"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Dictionary<string, object>());
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetWinLossAsync(custId, custLevel, date).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<Dictionary<string, object>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetWinLossAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => reportService.GetWinLossAsync(custId, custLevel, date));
        }
    }
}