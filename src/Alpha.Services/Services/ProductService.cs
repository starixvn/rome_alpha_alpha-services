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
        Task<IEnumerable<int>> GetOfferedProductIdsForCustomer(int custId, DateTime? minReportDate = null);
    }

    public class ProductServiceImpl : ProductService
    {
        private const string ProductsCacheKey = "Alpha.Services.ProductServiceImpl.OfferedProducts";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;

        public ProductServiceImpl(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheService = cacheService;
        }

        public async Task<IEnumerable<int>> GetOfferedProductIdsForCustomer(int custId, DateTime? minReportDate = null)
        {
            if (custId <= 0)
            {
                return Enumerable.Empty<int>();
            }

            var products = await GetOfferedProductsByCustomer(custId);
            if (products == null)
            {
                return Enumerable.Empty<int>();
            }

            return products
                .Where(product => !product.IsHidden && IsRemainSupportProduct(product, minReportDate))
                .Select(product => product.Id);
        }

        private async Task<IEnumerable<Product>> GetOfferedProductsByCustomer(int custId)
            => await cacheService.GetOrSetAsync(
                $"{ProductsCacheKey}.{custId}",
                () => dynamicRepository.Fetch<Product>(new GetOfferedProductsCriteria(custId)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

        private static bool IsRemainSupportProduct(Product product, DateTime? minReportDate)
            => !minReportDate.HasValue
            || !product.EndOfSupportDate.HasValue
            || minReportDate.Value <= product.EndOfSupportDate.Value;
    }
}