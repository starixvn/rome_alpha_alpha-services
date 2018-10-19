namespace Alpha.Services.Tests.Services
{
    using System.Collections.Generic;
    using Alpha.Services.Criteria.OutstandingDetail;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class OutstandingDetailServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly OutstandingDetailServiceImpl outstandingDetailService;

        public OutstandingDetailServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            outstandingDetailService = new OutstandingDetailServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetOutstandingDetails_Always_CallDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new OutstandingDetailParam(UserLevel.Agent, 1, new List<int> { 1, 2, 3 });

            // Act
            outstandingDetailService.GetOutstandingDetails(param).ConfigureAwait(false);

            // Asset
            dynamicRepository.Received().FetchAsync<OutstandingDetail>(Arg.Is<GetOutstandingDetailCriteria>(
                criteria => criteria.CustId == param.CustId
                        && criteria.CustLevel == param.CustLevel
                        && criteria.ProductIds == param.ProductIds.JoinWithComma()));
        }
    }
}