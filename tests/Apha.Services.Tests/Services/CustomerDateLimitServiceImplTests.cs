namespace Apha.Services.Tests.Services
{
    using System;
    using Alpha.Services.Criteria;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class CustomerDateLimitServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerDateLimitServiceImpl customerDateLimitService;

        public CustomerDateLimitServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerDateLimitService = new CustomerDateLimitServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetCustomerDateLimit_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;

            // Act
            customerDateLimitService.GetCustomerDateLimit(custId).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().GetAsync<DateTime>(
                Arg.Is<GetCustomerDateLimitCriteria>(c => c.CustId == custId));
        }
    }
}