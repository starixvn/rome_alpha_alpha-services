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

    public class ReportServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly ReportServiceImpl reportService;

        public ReportServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            reportService = new ReportServiceImpl(restClient);
        }

        #region GetOutstanding

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

        #endregion GetOutstanding

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

        #region GetOutstandingAsync

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

        #endregion GetOutstandingAsync

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

        #region GetOutstandingDetail

        [Fact]
        public void GetOutstandingDetail_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstandingdetail", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.Execute<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            reportService.GetOutstandingDetail(custId, custLevel);

            // Assert
            restClient.Received().Execute<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstandingDetail_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => reportService.GetOutstandingDetail(custId, custLevel));
        }

        #endregion GetOutstandingDetail

        #region GetOutstandingDetailAsync

        [Fact]
        public void GetOutstandingDetailAsync_Always_CallsExecuteGet()
        {
            // Arrange
            var custId = 1;
            byte custLevel = 2;

            var request = new RestRequest($"report/{custId}/{custLevel}/outstandingdetail", Method.GET);

            var restResponse = Substitute.For<IRestResponse<OutstandingDetail>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new OutstandingDetail());
            restClient.ExecuteTaskAsync<OutstandingDetail>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            reportService.GetOutstandingDetailAsync(custId, custLevel).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<OutstandingDetail>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetOutstandingDetailAsync_ResponseIsFailed_ThrowsInvalidOperationException()
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
                () => reportService.GetOutstandingDetailAsync(custId, custLevel));
        }

        #endregion GetOutstandingDetailAsync
    }
}