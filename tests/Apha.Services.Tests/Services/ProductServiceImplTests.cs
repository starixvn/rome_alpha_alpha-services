namespace Apha.Services.Tests.Services
{
    using System;
    using System.Linq;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class ProductServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;
        private readonly ProductServiceImpl productService;

        public ProductServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheService = Substitute.For<ICacheService>();
            productService = new ProductServiceImpl(dynamicRepository, cacheService);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GetOfferedProductIdsForCustomer_InvalidCustId_ReturnsEmptyList(int custId)
        {
            // Arrange
            var expectedResult = Enumerable.Empty<int>();

            // Act
            var actualResult = productService.GetOfferedProductIdsForCustomer(custId, DateTime.Now)
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}