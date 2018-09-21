namespace Alpha.Services.Client.Services
{
    using System.Threading.Tasks;
    using RestSharp;

    public interface CustomerService
    {
        Task<bool> IsAccountClosed(int custId, int subAccountId);
    }

    public class CustomerServiceImpl : BaseService, CustomerService
    {
        public CustomerServiceImpl()
            : this(null)
        {
        }

        internal CustomerServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public async Task<bool> IsAccountClosed(int custId, int subAccountId)
            => await GetAsync<bool>($"customer/{custId}/isaccountclosed/{subAccountId}");
    }
}