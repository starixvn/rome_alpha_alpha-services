namespace Alpha.Services.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Fanex.Data.Repository;

    public interface CustomerService
    {
        Task<Customer> GetCustomerInfo(int custId);
    }

    public class CustomerServiceImpl : CustomerService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CustomerServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<Customer> GetCustomerInfo(int custId)
            => await dynamicRepository.GetAsync<Customer>(new GetCustomerInfoCriteria(custId));
    }
}