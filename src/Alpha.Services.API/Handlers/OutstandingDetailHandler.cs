namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Alpha.Services.Utils;
    using MediatR;

    public class OutstandingDetailHandler : IRequestHandler<OutstandingDetailQuery, IEnumerable<OutstandingDetail>>
    {
        private readonly ProductService productService;
        private readonly OutstandingDetailService outstandingDetailService;

        public OutstandingDetailHandler(
            ProductService productService,
            OutstandingDetailService outstandingDetailService)
        {
            this.productService = productService;
            this.outstandingDetailService = outstandingDetailService;
        }

        public async Task<IEnumerable<OutstandingDetail>> Handle(OutstandingDetailQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);

            var param = new OutstandingDetailParam(request.CustLevel, request.CustId, productIds);

            return await outstandingDetailService.GetOutstandingDetails(param);
        }
    }
}