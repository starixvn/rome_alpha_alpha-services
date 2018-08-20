namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Infrastructure.Exceptions;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using MediatR;

    public class OutstandingHandler : IRequestHandler<OutstandingQuery, IDictionary<string, object>>
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

        public async Task<IDictionary<string, object>> Handle(OutstandingQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);

            if (customer == null)
            {
                throw new DomainException("Customer does not exist");
            }

            var outstanding = await outstandingService.GetOutstanding(customer.CustLevel, customer.CustId);

            return new Dictionary<string, object>
            {
                { UserLevel.Member.DisplayName, outstanding.TotalOutstanding },
                { customer.CustLevel.DisplayName , outstanding.Outstanding }
            };
        }
    }
}