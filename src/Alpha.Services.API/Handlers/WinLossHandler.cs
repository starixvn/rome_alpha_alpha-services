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

    public class WinLossHandler : IRequestHandler<WinLossQuery, IDictionary<string, object>>
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

        public async Task<IDictionary<string, object>> Handle(WinLossQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);
            var productIds = await productService.GetProductIdsForCustomer(request.CustId, request.MinDate);
            var limitedDate = await customerDateLimitService.GetCustomerDateLimit(request.CustId);

            var param = new WinLossParam(customer.CustLevel, customer.CustId, request.Date, limitedDate, productIds);
            var winLossData = await winLossService.GetWinLoss(param);
            var grandTotal = AccumulateGrandTotal(winLossData);
            var companyWinLoss = grandTotal.Company;

            return new Dictionary<string, object>
            {
                { nameof(grandTotal.Turnover) , grandTotal.Turnover },
                { nameof(grandTotal.GrossComm) , grandTotal.GrossComm },

                { "MemberNetWinLoss.WinLoss" , grandTotal.MemberNetWinLoss?.WinLoss },
                { "MemberNetWinLoss.Comm" , grandTotal.MemberNetWinLoss?.Comm },
                { "MemberNetWinLoss.Total" , grandTotal.MemberNetWinLoss?.Total },

                { "AgentNetWinLoss.WinLoss" , grandTotal.AgentNetWinLoss?.WinLoss },
                { "AgentNetWinLoss.Comm" , grandTotal.AgentNetWinLoss?.Comm },
                { "AgentNetWinLoss.Total" , grandTotal.AgentNetWinLoss?.Total },

                { "MasterNetWinLoss.WinLoss" , grandTotal.MasterNetWinLoss?.WinLoss },
                { "MasterNetWinLoss.Comm" , grandTotal.MasterNetWinLoss?.Comm },
                { "MasterNetWinLoss.Total" , grandTotal.MasterNetWinLoss?.Total },

                { "SuperNetWinLoss.WinLoss" , grandTotal.SuperNetWinLoss?.WinLoss },
                { "SuperNetWinLoss.Comm" , grandTotal.SuperNetWinLoss?.Comm },
                { "SuperNetWinLoss.Total" , grandTotal.SuperNetWinLoss?.Total },

                { nameof(companyWinLoss) , companyWinLoss },
            };
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