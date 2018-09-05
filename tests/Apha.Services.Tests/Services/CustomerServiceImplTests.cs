namespace Apha.Services.Tests.Services
{
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class CustomerServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerServiceImpl customerService;
        private readonly ICacheService cacheService;

        public CustomerServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheService = Substitute.For<ICacheService>();
            customerService = new CustomerServiceImpl(dynamicRepository, cacheService);
        }

        [Fact]
        public void GetCustomerInfo_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;

            // Act
            customerService.GetCustomerInfo(custId).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.GetAsync<Customer>(
                Arg.Is<GetCustomerInfoCriteria>(c => c.CustId == custId));
        }
    }
}