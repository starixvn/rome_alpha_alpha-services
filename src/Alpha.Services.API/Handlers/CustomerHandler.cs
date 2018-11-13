namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using MediatR;

    public class CustomerHandler :
        IRequestHandler<GetAccountStatusQuery, bool>,
        IRequestHandler<DownlineInfoQuery, DownlineInfo>
    {
        private readonly CustomerService customerService;

        public CustomerHandler(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<bool> Handle(GetAccountStatusQuery request, CancellationToken cancellationToken)
            => await customerService.IsAccountClosed(request.CustId, request.SubAccountId);

        public async Task<DownlineInfo> Handle(DownlineInfoQuery request, CancellationToken cancellationToken)
            => await customerService.GetDownlineInfo(request.CustId, request.DownlineUserName);
    }
}