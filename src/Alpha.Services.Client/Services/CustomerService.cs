namespace Alpha.Services.Client.Services
{
    using System.Threading.Tasks;

    public interface CustomerService
    {
        Task<bool> IsAccountClosed(int custId, int subAccountId);
    }

    public class CustomerServiceImpl : BaseService, CustomerService
    {
        public async Task<bool> IsAccountClosed(int custId, int subAccountId)
            => await GetAsync<bool>($"customer/{custId}/isaccountclosed/{subAccountId}");
    }
}