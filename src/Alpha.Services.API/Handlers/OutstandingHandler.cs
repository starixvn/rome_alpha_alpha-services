namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using MediatR;

    public class OutstandingHandler : IRequestHandler<OutstandingQuery, IDictionary<string, object>>
    {
        private readonly OutstandingService outstandingService;

        public OutstandingHandler(OutstandingService outstandingService)
        {
            this.outstandingService = outstandingService;
        }

        public async Task<IDictionary<string, object>> Handle(OutstandingQuery request, CancellationToken cancellationToken)
        {
            var outstanding = await outstandingService.GetOutstanding(request.CustLevel, request.CustId);

            return new Dictionary<string, object>
            {
                { UserLevel.Member.DisplayName, outstanding.TotalOutstanding },
                { request.CustLevel.DisplayName , outstanding.Outstanding }
            };
        }
    }
}