namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using MediatR;

    public class CustomerHandler :
        IRequestHandler<GetCustomerQuery, Customer>,
        IRequestHandler<GetAccountStatusQuery, AccountStatus>,
        IRequestHandler<DownlineInfoQuery, DownlineInfo>
    {
        private readonly CustomerService customerService;

        public CustomerHandler(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            => await customerService.GetCustomerInfo(request.CustId);

        public async Task<AccountStatus> Handle(GetAccountStatusQuery request, CancellationToken cancellationToken)
            => await customerService.GetAccountStatus(request.CustId, request.SubAccountId);

        public async Task<DownlineInfo> Handle(DownlineInfoQuery request, CancellationToken cancellationToken)
            => await customerService.GetDownlineInfo(request.CustId, request.DownlineUserName);
    }
}