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

    public class OutstandingDetailHandler : IRequestHandler<OutstandingDetailQuery, OutstandingDetailViewModel>
    {
        private const int topDownlinesDisplay = 50;
        private readonly ProductService productService;
        private readonly OutstandingDetailService outstandingDetailService;

        public OutstandingDetailHandler(
            ProductService productService,
            OutstandingDetailService outstandingDetailService)
        {
            this.productService = productService;
            this.outstandingDetailService = outstandingDetailService;
        }

        public async Task<OutstandingDetailViewModel> Handle(OutstandingDetailQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);
            var param = new OutstandingDetailParam(request.CustLevel, request.CustId, productIds);
            var outstandings = await outstandingDetailService.GetOutstandingDetails(param);

            var total = AccummulateTotal(outstandings);
            var hiddenDownlinesCount = outstandings.Count() - topDownlinesDisplay;
            var topOutstandings = outstandings.OrderByDescending(o => o.MemberOutstanding).Take(topDownlinesDisplay);

            return new OutstandingDetailViewModel(
                request.CustLevel.Value,
                hiddenDownlinesCount,
                total,
                topOutstandings);
        }

        internal static OutstandingDetail AccummulateTotal(IEnumerable<OutstandingDetail> outstandingDetails)
        {
            var total = new OutstandingDetail();

            if (outstandingDetails != null && outstandingDetails.Any())
            {
                foreach (var outstanding in outstandingDetails)
                {
                    total.Add(outstanding);
                }
            }

            return total;
        }
    }
}