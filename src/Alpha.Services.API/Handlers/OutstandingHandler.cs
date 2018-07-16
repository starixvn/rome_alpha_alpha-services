namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class OutstandingHandler : IRequestHandler<OutstandingQuery, OutstandingViewModel>
    {
        private readonly CustomerService customerService;
        private readonly OutstandingService outstandingService;

        public OutstandingHandler(
            CustomerService customerService,
            OutstandingService outstandingService)
        {
            this.customerService = customerService;
            this.outstandingService = outstandingService;
        }

        public async Task<OutstandingViewModel> Handle(OutstandingQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);
            var param = new OutstandingParam(customer.CustLevel, customer.CustId);
            var outstanding = await outstandingService.GetOutstanding(param);

            return new OutstandingViewModel(outstanding.Outstanding, outstanding.TotalOutstanding);
        }
    }
}