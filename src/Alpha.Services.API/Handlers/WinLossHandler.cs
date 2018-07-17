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
    using MediatR;

    public class WinLossHandler : IRequestHandler<WinLossQuery, WinLossViewModel>
    {
        private readonly CustomerService customerService;
        private readonly ProductService productService;
        private readonly CustomerDateLimitService customerDateLimitService;
        private readonly WinLossService winLossService;

        public WinLossHandler(
            CustomerService customerService,
            ProductService productService,
            CustomerDateLimitService customerDateLimitService,
            WinLossService winLossService)
        {
            this.customerService = customerService;
            this.productService = productService;
            this.customerDateLimitService = customerDateLimitService;
            this.winLossService = winLossService;
        }

        public async Task<WinLossViewModel> Handle(WinLossQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);
            var productIds = await productService.GetProductIdsForCustomer(request.CustId, request.MinDate);
            var limitedDate = await customerDateLimitService.GetCustomerDateLimit(request.CustId);

            var param = new WinLossParam(customer.CustLevel, customer.CustId, request.Date, limitedDate, productIds);
            var winLossData = await winLossService.GetWinLoss(param);
            var grandTotal = AccumulateGrandTotal(winLossData);

            return new WinLossViewModel(
                grandTotal.Turnover,
                grandTotal.GrossComm,
                grandTotal.MemberNetWinLoss,
                grandTotal.AgentNetWinLoss,
                grandTotal.MasterNetWinLoss,
                grandTotal.SuperNetWinLoss);
        }

        internal WinLossModel AccumulateGrandTotal(IEnumerable<WinLossModel> reportData)
        {
            var grandTotal = new WinLossModel();

            if (reportData != null && reportData.Any())
            {
                foreach (var data in reportData)
                {
                    grandTotal.Add(data);
                }
            }

            return grandTotal;
        }
    }
}