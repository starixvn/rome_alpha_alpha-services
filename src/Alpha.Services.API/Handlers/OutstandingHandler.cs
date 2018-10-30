[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Alpha.Services.API.Tests")]

namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Alpha.Services.Utils;
    using MediatR;

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
            var outstandings = await GetOutstandings(request.CustLevel, request.CustId);

            var total = AccummulateTotal(outstandings);

            var hiddenDownlinesCount = outstandings.Count() - request.Top;
            var topOutstandings = outstandings.OrderByDescending(o => o.MemberOutstanding).Take(request.Top);

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
            var outstandings = await GetOutstandings(request.CustLevel, request.CustId, request.DownlineId);
            var downlineOutstanding = outstandings.FirstOrDefault();

            return downlineOutstanding == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>
                {
                    ["Username"] = downlineOutstanding.CustName,
                    [UserLevel.Member.DisplayName] = downlineOutstanding.MemberOutstanding,
                    [request.CustLevel.DisplayName] = downlineOutstanding.Outstanding
                };
        }

        private async Task<IEnumerable<OutstandingModel>> GetOutstandings(
            UserLevel custLevel,
            int custId,
            int downlineId = 0)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(custId, SystemTime.Today);
            var param = new OutstandingParam(custLevel, custId, productIds, downlineId);

            return await outstandingService.GetOutstandings(param);
        }
    }
}