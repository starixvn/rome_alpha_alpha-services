namespace Alpha.Services.Tests.Services
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using System.Collections.Generic;
    using Xunit;

    public class OutstandingServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly OutstandingServiceImpl outstandingService;

        public OutstandingServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            outstandingService = new OutstandingServiceImpl(dynamicRepository);
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
            var param = new OutstandingParam(UserLevel.Agent, 1, new List<int> { 1, 2, 3 }, 2);

            // Act
            outstandingService.GetDownlineOutstanding(param).ConfigureAwait(false);

            // Asset
            dynamicRepository.Received().GetAsync<OutstandingModel>(Arg.Is<GetDownlineOutstandingCriteria>(
                criteria => criteria.CustId == param.CustId
                        && criteria.CustLevel == param.CustLevel
                        && criteria.ProductIds == param.ProductIds.JoinWithComma()
                        && criteria.DownlineId == param.DownlineId));
        }
    }
}