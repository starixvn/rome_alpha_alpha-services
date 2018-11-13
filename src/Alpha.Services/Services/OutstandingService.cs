namespace Alpha.Services.Services
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface OutstandingService
    {
        Task<IEnumerable<OutstandingModel>> GetOutstandings(OutstandingParam param);

        Task<OutstandingModel> GetDownlineOutstanding(OutstandingParam param);

        Task<TotalOutstandingModel> GetTotalOutstanding(UserLevel custLevel, int custId);
    }

    public class OutstandingServiceImpl : OutstandingService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OutstandingServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<OutstandingModel>> GetOutstandings(OutstandingParam param)
            => await dynamicRepository.FetchAsync<OutstandingModel>(
                new GetOutstandingCriteria(param.CustLevel, param.CustId, param.ProductIds))
                .ConfigureAwait(false);

        public async Task<OutstandingModel> GetDownlineOutstanding(OutstandingParam param)
            => await dynamicRepository.GetAsync<OutstandingModel>(
                new GetDownlineOutstandingCriteria(param.CustLevel, param.CustId, param.ProductIds, param.DownlineId))
                .ConfigureAwait(false);

        public async Task<TotalOutstandingModel> GetTotalOutstanding(UserLevel custLevel, int custId)
            => await dynamicRepository.GetAsync<TotalOutstandingModel>(
                new GetTotalOutstandingCriteria(custLevel, custId))
                .ConfigureAwait(false);
    }
}