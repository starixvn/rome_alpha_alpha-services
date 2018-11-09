[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Alpha.Services.API.Tests")]

namespace Alpha.Services.API.Handlers
{
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Alpha.Services.Utils;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class OutstandingHandler :
        IRequestHandler<OutstandingQuery, OutstandingViewModel>,
        IRequestHandler<DownlineOutstandingQuery, IDictionary<string, object>>
    {
        private readonly ProductService productService;
        private readonly OutstandingService outstandingService;

        public OutstandingHandler(
            ProductService productService,
            OutstandingService outstandingService)
        {
            this.productService = productService;
            this.outstandingService = outstandingService;
        }

        public async Task<OutstandingViewModel> Handle(OutstandingQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);

            var param = new OutstandingParam(request.CustLevel, request.CustId, productIds);
            var outstandings = await outstandingService.GetOutstandings(param);
            var top = request.Top < 0 ? 0 : request.Top;

            var total = AccummulateTotal(outstandings);
            var hiddenDownlinesCount = outstandings.Count() - top;
            var topOutstandings = outstandings.OrderByDescending(o => o.MemberOutstanding).Take(top);

            return new OutstandingViewModel(
                request.CustLevel.Value,
                hiddenDownlinesCount,
                total,
                topOutstandings);
        }

        internal static OutstandingModel AccummulateTotal(IEnumerable<OutstandingModel> outstandingDetails)
        {
            var total = new OutstandingModel();

            if (outstandingDetails != null && outstandingDetails.Any())
            {
                foreach (var outstanding in outstandingDetails)
                {
                    total.Add(outstanding);
                }
            }

            return total;
        }

        public async Task<IDictionary<string, object>> Handle(DownlineOutstandingQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);

            var param = new OutstandingParam(request.CustLevel, request.CustId, productIds, request.DownlineId);
            var outstanding = await outstandingService.GetDownlineOutstanding(param);

            return outstanding == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>
                {
                    ["Username"] = outstanding.CustName,
                    [UserLevel.Member.DisplayName] = outstanding.MemberOutstanding,
                    [request.CustLevel.DisplayName] = outstanding.Outstanding
                };
        }
    }
}