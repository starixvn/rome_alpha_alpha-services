namespace Alpha.Services.Client.Tests.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class CustomerServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly CustomerServiceImpl customerService;

        public CustomerServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            customerService = new CustomerServiceImpl(restClient);
        }

        [Fact]
        public void IsAccountClosed_Always_CallsExecuteTaskAsync()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;

            var request = new RestRequest($"customer/{custId}/isaccountclosed/{subAccountId}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<bool>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(false);
            restClient.ExecuteTaskAsync<bool>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            customerService.IsAccountClosed(custId, subAccountId).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<bool>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void IsAccountClosed_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;

            var restResponse = Substitute.For<IRestResponse<bool>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<bool>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => customerService.IsAccountClosed(custId, subAccountId));
        }
    }
}