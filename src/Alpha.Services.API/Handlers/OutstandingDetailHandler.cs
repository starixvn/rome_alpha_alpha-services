namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Alpha.Services.Utils;
    using MediatR;

    public class OutstandingDetailHandler : IRequestHandler<OutstandingDetailQuery, OutstandingDetailViewModel>
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

        public async Task<OutstandingDetailViewModel> Handle(OutstandingDetailQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);
            var param = new OutstandingDetailParam(request.CustLevel, request.CustId, productIds);
            var outstandings = await outstandingDetailService.GetOutstandingDetails(param);

            return new OutstandingDetailViewModel(request.CustLevel.Value, outstandings);
        }
    }
}