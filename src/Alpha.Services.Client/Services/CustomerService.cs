namespace Alpha.Services.Client.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface CustomerService
    {
        Task<bool> IsAccountClosed(int custId, int subAccountId);

        Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUserName);
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

        public async Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUserName)
            => await GetAsync<DownlineInfo>($"customer/{custId}/downline/{downlineUserName}");
    }
}