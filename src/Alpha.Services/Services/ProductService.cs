namespace Alpha.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.Services.Configurations;
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
        private const int CacheExpirationTimeInMinute = 5;
        private const string ProductsCacheKey = "Alpha.Services.ProductServiceImpl.OfferedProducts";
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheService cacheService;
        private readonly IAppSettings appSettings;

        public ProductServiceImpl(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService,
            IAppSettings appSettings)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheService = cacheService;
            this.appSettings = appSettings;
        }

        public async Task<IEnumerable<int>> GetOfferedProductIdsForCustomer(int custId, DateTime? minReportDate = null)
        {
            if (custId <= 0)
            {
                return Enumerable.Empty<int>();
            }

            var products = await GetOfferedProductsByCustomer(custId, this.appSettings.SettingStatus);
            if (products == null)
            {
                return Enumerable.Empty<int>();
            }

            return products
                .Where(product => !product.IsHidden && IsRemainSupportProduct(product, minReportDate))
                .Select(product => product.Id);
        }

        private async Task<IEnumerable<Product>> GetOfferedProductsByCustomer(int custId, byte settingStatus)
            => await cacheService.GetOrSetAsync(
                $"{ProductsCacheKey}.{custId}",
                () => dynamicRepository.Fetch<Product>(new GetOfferedProductsCriteria(custId, settingStatus)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationTimeInMinute)));

        private static bool IsRemainSupportProduct(Product product, DateTime? minReportDate)
            => !minReportDate.HasValue
            || !product.EndOfSupportDate.HasValue
            || minReportDate.Value <= product.EndOfSupportDate.Value;
    }
}