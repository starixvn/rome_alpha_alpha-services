namespace Alpha.Services.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class ProductServiceImplTests
    {
        private const string ProductsCacheKey = "Alpha.Services.ProductServiceImpl.OfferedProducts";

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
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetOfferedProductIdsForCustomer_ProductsIsNull_ReturnsEmptyList()
        {
            // Arrange
            var expectedResult = Enumerable.Empty<int>();

            var custId = 1;
            IEnumerable<Product> products = null;
            cacheService.GetOrSetAsync(
                $"{ProductsCacheKey}.{custId}",
                Arg.Any<Func<IEnumerable<Product>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(products));

            // Act
            var actualResult = productService.GetOfferedProductIdsForCustomer(custId, DateTime.Now)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetOfferedProductIdsForCustomer_ProductsIsNotNull_ReturnsCorrectResult()
        {
            // Arrange
            var custId = 1;
            IEnumerable<Product> products = new List<Product>
            {
                new Product(1, false, null),
                new Product(2, false, null),
                new Product(3, true, null)
            };
            cacheService.GetOrSetAsync(
                $"{ProductsCacheKey}.{custId}",
                Arg.Any<Func<IEnumerable<Product>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult(products));

            var expectedResult = new List<int> { 1, 2 };

            // Act
            var actualResult = productService.GetOfferedProductIdsForCustomer(custId, DateTime.Now)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}