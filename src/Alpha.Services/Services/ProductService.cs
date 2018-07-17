namespace Alpha.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;

    public interface ProductService
    {
        Task<IEnumerable<int>> GetProductIdsForCustomer(int custId, DateTime? minReportDate = null);
    }

    public class ProductServiceImpl : ProductService
    {
        private const string ProductsCacheKey = "Alpha.Services.ProductServiceImpl.Products";
        private readonly IDynamicRepository dynamicRepository;

        public ProductServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<int>> GetProductIdsForCustomer(int custId, DateTime? minReportDate = null)
        {
            if (custId <= 0)
            {
                return Enumerable.Empty<int>();
            }

            var products = await GetProductsForReport();
            if (products == null)
            {
                return Enumerable.Empty<int>();
            }

            return products
                .Where(product => IsRemainSupportProduct(product, minReportDate))
                .Select(product => product.Id);
        }

        private static bool IsRemainSupportProduct(Product product, DateTime? minReportDate)
            => !minReportDate.HasValue
            || !product.EndOfSupportDate.HasValue
            || minReportDate.Value <= product.EndOfSupportDate.Value;

        private async Task<IEnumerable<Product>> GetProductsForReport()
            => await ConfiguredCacheService.GetOrSetAsync(
                ProductsCacheKey,
                () => dynamicRepository.Fetch<Product>(new GetProductsCriteria()),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
    }
}