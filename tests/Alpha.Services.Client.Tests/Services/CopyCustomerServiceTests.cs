namespace Alpha.Services.Client.Tests.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class CopyCustomerServiceTests
    {
        private readonly IRestClient restClient;
        private readonly CopyCustomerService copyCustomerService;

        public CopyCustomerServiceTests()
        {
            restClient = Substitute.For<IRestClient>();
            copyCustomerService = new CopyCustomerServiceImpl(restClient);
        }

        [Fact]
        public async Task ValidateMaxNewMember_Always_CallsExecuteGetAsync()
        {
            // Arrange
            var agentId = 33193062;
            var language = "en-US";

            var request = new RestRequest($"customer/{agentId}/ValidateMaxNewMember?language={language}", Method.GET);

            var restResponse = Substitute.For<IRestResponse<Result>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Result());
            restClient
                .ExecuteTaskAsync<Result>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            await copyCustomerService.ValidateMaxNewMember(agentId, language);

            // Assert
            await restClient
                .Received()
                .ExecuteTaskAsync<Result>(Arg.Is<IRestRequest>(
                    r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetPT_Always_CallsExecuteGetAsync()
        {
            // Arrange
            var custId = 33193062;
            byte custLevel = 4;

            var request = new RestRequest($"customer/{custId}/{custLevel}/pt", Method.GET);

            var restResponse = Substitute.For<IRestResponse<CustomerPTInfo>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new CustomerPTInfo());
            restClient.ExecuteTaskAsync<CustomerPTInfo>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            copyCustomerService.GetPT(custId, custLevel).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<CustomerPTInfo>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void Copy_Always_CallsExecuteTaskAsync()
        {
            // Arrange
            var copyData = Substitute.For<ICopyCustomerData>();
            copyData.ActorId.Returns(33193110);
            copyData.ActorUserLevel.Returns((byte)4);
            copyData.UserName.Returns("leemaster00");
            copyData.Credit.Returns(10);
            copyData.SourceCustId.Returns(33193111);
            copyData.SourceUsername.Returns("sourceUsername");
            copyData.Language.Returns("en-US");
            copyData.IP.Returns(string.Empty);

            var restResponse = Substitute.For<IRestResponse<CopyCustomerResult>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new CopyCustomerResult());
            restClient.ExecuteTaskAsync<CopyCustomerResult>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            copyCustomerService.Copy(copyData).ConfigureAwait(false);

            // Assert
            restClient
                .Received()
                .ExecuteTaskAsync<CopyCustomerResult>(Arg.Is<IRestRequest>(
                    r => r.Resource == "customer/copy"
                    && r.Method == Method.POST));
        }

        [Fact]
        public void CheckReachLimitDownline_Always_CallsExecuteTaskAsync()
        {
            // Arrange

            var restResponse = Substitute.For<IRestResponse<Result>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new Result());
            restClient.ExecuteTaskAsync<Result>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            copyCustomerService.CheckReachLimitDownline(1, 2, "username", "en-US").ConfigureAwait(false);

            // Assert
            restClient
                .Received()
                .ExecuteTaskAsync<Result>(Arg.Is<IRestRequest>(
                    r => r.Resource == "customer/CheckReachLimitDownline"
                        && r.Method == Method.POST));
        }
    }
}