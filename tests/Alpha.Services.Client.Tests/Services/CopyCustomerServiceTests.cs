namespace Alpha.Services.Client.Tests.Services
{
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

        //[Fact]
        //public void GetPTWithoutSite_Always_CallsExecuteGet()
        //{
        //    // Arrange
        //    var custId = 33193062;
        //    byte custLevel = 4;

        //    var request = new RestRequest($"customer/{custId}/{custLevel}/pt?site=", Method.GET);

        //    var restResponse = Substitute.For<IRestResponse<CustomerPTInfo>>();
        //    restResponse.IsSuccessful.Returns(true);
        //    restResponse.Data.Returns(new CustomerPTInfo());
        //    restClient.Execute<CustomerPTInfo>(Arg.Any<IRestRequest>())
        //        .Returns(restResponse);

        //    // Act
        //    copyCustomerService.GetPT(custId, custLevel);

        //    // Assert
        //    restClient.Received().Execute<CustomerPTInfo>(Arg.Is<IRestRequest>(
        //        r => r.Resource == request.Resource
        //            && r.Method == request.Method));
        //}

        //[Fact]
        //public void GetPT_Always_CallsExecuteGet()
        //{
        //    // Arrange
        //    var custId = 33193062;
        //    byte custLevel = 4;
        //    var site = "BEST-ODDS";

        //    var request = new RestRequest($"customer/{custId}/{custLevel}/pt?site={site}", Method.GET);

        //    var restResponse = Substitute.For<IRestResponse<CustomerPTInfo>>();
        //    restResponse.IsSuccessful.Returns(true);
        //    restResponse.Data.Returns(new CustomerPTInfo());
        //    restClient.Execute<CustomerPTInfo>(Arg.Any<IRestRequest>())
        //        .Returns(restResponse);

        //    // Act
        //    copyCustomerService.GetPT(custId, custLevel, site);

        //    // Assert
        //    restClient.Received().Execute<CustomerPTInfo>(Arg.Is<IRestRequest>(
        //        r => r.Resource == request.Resource
        //            && r.Method == request.Method));
        //}
    }
}
