namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Models;
    using Fanex.Data.Repository;

    public interface OutstandingService
    {
        Task<OutstandingModel> GetOutstanding(UserLevel custLevel, int custId);
    }

    public class OutstandingServiceImpl : OutstandingService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OutstandingServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<OutstandingModel> GetOutstanding(UserLevel custLevel, int custId)
            => await dynamicRepository.GetAsync<OutstandingModel>(
                new GetOutstandingCriteria(custLevel, custId));
    }
}