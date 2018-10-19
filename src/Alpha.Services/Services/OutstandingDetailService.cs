namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.OutstandingDetail;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface OutstandingDetailService
    {
        Task<IEnumerable<OutstandingDetail>> GetOutstandingDetails(OutstandingDetailParam param);
    }

    public class OutstandingDetailServiceImpl : OutstandingDetailService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OutstandingDetailServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<OutstandingDetail>> GetOutstandingDetails(OutstandingDetailParam param)
            => await dynamicRepository.FetchAsync<OutstandingDetail>(
                new GetOutstandingDetailCriteria(param.CustLevel, param.CustId, param.ProductIds))
                .ConfigureAwait(false);
    }
}