namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface OutstandingService
    {
        Task<OutstandingModel> GetOutstanding(OutstandingParam param);
    }

    public class OutstandingServiceImpl : OutstandingService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OutstandingServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<OutstandingModel> GetOutstanding(OutstandingParam param)
            => await dynamicRepository.GetAsync<OutstandingModel>(
                new OutstandingCriteria(param.CustLevel, param.CustId));
    }
}