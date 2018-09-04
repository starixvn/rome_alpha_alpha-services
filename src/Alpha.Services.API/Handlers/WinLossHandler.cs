namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class WinLossHandler : IRequestHandler<WinLossQuery, IDictionary<string, object>>
    {
        private readonly ProductService productService;
        private readonly WinLossService winLossService;

        public WinLossHandler(
            ProductService productService,
            WinLossService winLossService)
        {
            this.productService = productService;
            this.winLossService = winLossService;
        }

        public async Task<IDictionary<string, object>> Handle(WinLossQuery request, CancellationToken cancellationToken)
        {
            var productIds = await productService.GetProductIdsForCustomer(request.CustId, request.MinDate);

            var param = new WinLossParam(request.CustLevel, request.CustId, request.Date, productIds);
            var winLossData = await winLossService.GetWinLoss(param);

            if (winLossData == null || !winLossData.Any())
            {
                return new Dictionary<string, object>();
            }

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

        internal static WinLossModel AccumulateGrandTotal(IEnumerable<WinLossModel> reportData)
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