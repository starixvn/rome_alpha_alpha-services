namespace Alpha.Services.Client.Tests.Services
{
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ReportServiceImplTests
    {
        private const int DefaultTopDownlinesDisplay = 50;

        private readonly IRestClient restClient;
        private readonly ReportServiceImpl reportService;

        public ReportServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            reportService = new ReportServiceImpl(restClient);
        }

        #region GetBalanceInfo

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
        public void GetBalances_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var top = 50;

            var request = new RestRequest($"report/{custId}/{custLevel}/balances/{top}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<BalanceTotal>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new BalanceTotal());
            restClient
                .Execute<BalanceTotal>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetBalances(custId, custLevel, top);

            // Assert
            restClient
                .Received()
                .Execute<BalanceTotal>(Arg.Is<IRestRequest>(
                    r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        #endregion GetBalanceInfo

        #region GetWinLoss

        [Fact]
        public void GetWinLoss_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var request = new RestRequest(
                $"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}",
                Method.GET);

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

        #endregion GetWinLoss

        #region GetBalanceInfoAsync

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
        public async Task GetBalancesAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var top = 50;

            var request = new RestRequest($"report/{custId}/{custLevel}/balances/{top}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<BalanceTotal>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new BalanceTotal());
            restClient
                .ExecuteTaskAsync<BalanceTotal>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            await reportService.GetBalancesAsync(custId, custLevel, top);

            // Assert
            await restClient
                .Received().ExecuteTaskAsync<BalanceTotal>(Arg.Is<IRestRequest>(
                    r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        #endregion GetBalanceInfoAsync

        #region GetWinLossAsync

        [Fact]
        public void GetWinLossAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var date = DateTimeOffset.Now;

            var request = new RestRequest(
                $"report/{custId}/{custLevel}/winloss/{date.ToUnixTimeMilliseconds()}",
                Method.GET);

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
            Assert.ThrowsAsync<InvalidOperationException>(
                () => reportService.GetWinLossAsync(custId, custLevel, date));
        }

        #endregion GetWinLossAsync

        #region Outstanding

        [Fact]
        public void GetOutstandings_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var top = 50;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstandings/{top}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.Execute<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetOutstandings(custId, custLevel, DefaultTopDownlinesDisplay);

            // Assert
            restClient.Received().Execute<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstandings_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetOutstandings(custId, custLevel, DefaultTopDownlinesDisplay));
        }

        [Fact]
        public void GetOutstandingsAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var top = 50;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstandings/{top}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.ExecuteTaskAsync<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetOutstandingsAsync(custId, custLevel, DefaultTopDownlinesDisplay).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstandingsAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(
                () => reportService.GetOutstandingsAsync(custId, custLevel, DefaultTopDownlinesDisplay));
        }

        [Fact]
        public void GetDownlineOutstanding_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var downlineId = 3;
            var downlineName = "member01";
            var language = "en-US";

            var request = new RestRequest($"report/{custId}/{custLevel}/outstanding/{downlineId}/{downlineName}/{language}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.Execute<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetDownlineOutstanding(custId, custLevel, downlineId, downlineName, language);

            // Assert
            restClient.Received().Execute<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetDownlineOutstanding_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var downlineId = 3;
            var downlineName = "member01";
            var language = "en-US";

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetDownlineOutstanding(custId, custLevel, downlineId, downlineName, language));
        }

        [Fact]
        public void GetDownlineOutstandingAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var downlineId = 3;
            var downlineName = "member01";
            var language = "en-US";

            var request = new RestRequest($"report/{custId}/{custLevel}/outstanding/{downlineId}/{downlineName}/{language}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.ExecuteTaskAsync<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetDownlineOutstandingAsync(custId, custLevel, downlineId, downlineName, language).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetDownlineOutstandingAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;
            var downlineId = 3;
            var downlineName = "member01";
            var language = "en-US";

            var restResponse = Substitute.For<IRestResponse<Dictionary<string, object>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<Dictionary<string, object>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(
                () => reportService.GetDownlineOutstandingAsync(custId, custLevel, downlineId, downlineName, language));
        }

        #endregion Outstanding
    }
}