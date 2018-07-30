namespace Apha.Services.Tests.Services
{
    using System;
    using System.Linq;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class ProductServiceImplTests
    {
        private const string ProductsCacheKey = "Alpha.Services.ProductServiceImpl.Products";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ProductServiceImpl productService;

        public ProductServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            productService = new ProductServiceImpl(dynamicRepository);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GetProductIdsForCustomer_InvalidCustId_ReturnsEmptyList(int custId)
        {
            // Arrange
            var expectedResult = Enumerable.Empty<int>();

            // Act
            var actualResult = productService.GetProductIdsForCustomer(custId, DateTime.Now).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}