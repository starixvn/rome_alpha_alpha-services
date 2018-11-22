namespace Alpha.Services.Tests.Services
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OutstandingServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IRemotingRefService remotingRefService;
        private readonly OutstandingServiceImpl outstandingService;

        public OutstandingServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            remotingRefService = Substitute.For<IRemotingRefService>();
            outstandingService = new OutstandingServiceImpl(dynamicRepository, remotingRefService);
        }

        [Fact]
        public void GetOutstandings_Always_CallDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new OutstandingParam(UserLevel.Agent, 1, new List<int> { 1, 2, 3 });

            // Act
            outstandingService.GetOutstandings(param).ConfigureAwait(false);

            // Asset
            dynamicRepository.Received().FetchAsync<OutstandingModel>(Arg.Is<GetOutstandingCriteria>(
                criteria => criteria.CustId == param.CustId
                        && criteria.CustLevel == param.CustLevel
                        && criteria.ProductIds == param.ProductIds.JoinWithComma()));
        }

        [Fact]
        public void GetDownlineOutstanding_Always_CallDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new OutstandingParam(UserLevel.Agent, 1, new List<int> { 1, 2, 3 });

            // Act
            outstandingService.GetDownlineOutstandings(param).ConfigureAwait(false);

            // Asset
            dynamicRepository.Received().FetchAsync<OutstandingModel>(Arg.Is<GetDownlineOutstandingCriteria>(
                criteria => criteria.CustLevel == param.CustLevel
                        && criteria.ProductIds == param.ProductIds.JoinWithComma()
                        && criteria.DownlineId == param.CustId));
        }

        [Fact]
        public async Task GetDownlineOutstanding_Always_ReturnCorrectProductNames()
        {
            // Arrange
            var param = new OutstandingParam(UserLevel.Agent, 1, new List<int> { 1, 2, 3 });
            var expectedResults = new List<OutstandingModel>
            {
                new OutstandingModel(1, "Product1", 3661775.00M, 715863.95M),
                new OutstandingModel(2, "Product2", 2000.00M, 0),
                new OutstandingModel(3, "Product3", 1000.00M, 0)
            };

            dynamicRepository.FetchAsync<OutstandingModel>(Arg.Is<GetDownlineOutstandingCriteria>(
                criteria => criteria.CustLevel == param.CustLevel
                        && criteria.ProductIds == param.ProductIds.JoinWithComma()
                        && criteria.DownlineId == param.CustId))
                .Returns(expectedResults);

            remotingRefService.GetResourceDataByRefIdAndResourceId("Product", "1", "en-US", "1").Returns("Product1");
            remotingRefService.GetResourceDataByRefIdAndResourceId("Product", "2", "en-US", "2").Returns("Product2");
            remotingRefService.GetResourceDataByRefIdAndResourceId("Product", "3", "en-US", "3").Returns("Product3");

            // Act
            var actualResults = await outstandingService.GetDownlineOutstandings(param).ConfigureAwait(false);

            // Asset
            Assert.Equal(expectedResults, actualResults.Where(outstanding => outstanding.Id > 0).ToList());
        }
    }
}