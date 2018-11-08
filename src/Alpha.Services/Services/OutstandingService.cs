namespace Alpha.Services.Services
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface OutstandingService
    {
        Task<IEnumerable<OutstandingModel>> GetOutstandings(OutstandingParam param);

        Task<IEnumerable<OutstandingModel>> GetDownlineOutstandings(OutstandingParam param);

        Task<TotalOutstandingModel> GetTotalOutstanding(UserLevel custLevel, int custId);
    }

    public class OutstandingServiceImpl : OutstandingService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IRemotingRefService remotingRefService;

        public OutstandingServiceImpl(
            IDynamicRepository dynamicRepository,
            IRemotingRefService remotingRefService)
        {
            this.dynamicRepository = dynamicRepository;
            this.remotingRefService = remotingRefService;
        }

        public async Task<IEnumerable<OutstandingModel>> GetOutstandings(OutstandingParam param)
            => await dynamicRepository.FetchAsync<OutstandingModel>(
                new GetOutstandingCriteria(param.CustLevel, param.CustId, param.ProductIds))
                .ConfigureAwait(false);

        public async Task<IEnumerable<OutstandingModel>> GetDownlineOutstandings(OutstandingParam param)
        {
            var outstandings = await dynamicRepository.FetchAsync<OutstandingModel>(
                new GetDownlineOutstandingCriteria(param.CustLevel, param.ProductIds, param.CustId))
                .ConfigureAwait(false);

            foreach (var outstanding in outstandings)
            {
                outstanding.Name = GetProductName(outstanding.Id, param.Language);
            }

            return outstandings;
        }

        public async Task<TotalOutstandingModel> GetTotalOutstanding(UserLevel custLevel, int custId)
            => await dynamicRepository.GetAsync<TotalOutstandingModel>(
                new GetTotalOutstandingCriteria(custLevel, custId))
                .ConfigureAwait(false);

        private string GetProductName(int productId, string language)
        {
            var resourceId = productId.ToString();

            return remotingRefService.GetResourceDataByRefIdAndResourceId(
                   "Product",
                   resourceId,
                   language,
                   resourceId);
        }
    }
}