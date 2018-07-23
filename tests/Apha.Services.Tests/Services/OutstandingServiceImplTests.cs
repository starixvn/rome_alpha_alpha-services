namespace Apha.Services.Tests.Services
{
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
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
        public void GetOutstanding_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;
            var custLevel = UserLevel.Agent;

            // Act
            outstandingService.GetOutstanding(custLevel, custId).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().GetAsync<OutstandingModel>(
                Arg.Is<GetOutstandingCriteria>(c => c.CustLevel == custLevel && c.CustId == custId));
        }
    }
}