namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
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
            var productIds = await productService.GetOfferedProductIdsForCustomer(request.CustId, request.MinDate);

            var param = new WinLossParam(request.CustLevel, request.CustId, request.Date, productIds);
            var winloss = await winLossService.GetWinLoss(param);

            var result = new Dictionary<string, object>();

            if (winloss != null)
            {
                result.Add(nameof(winloss.Turnover), winloss.Turnover);
                result.Add(nameof(winloss.GrossComm), winloss.GrossComm);

                if (request.CustLevel.IsAgentLevel())
                {
                    result.Add("MemberNetWinLoss.WinLoss", winloss.MemberWinLoss);
                    result.Add("MemberNetWinLoss.Comm", winloss.MemberComm);
                    result.Add("MemberNetWinLoss.Total", winloss.MemberTotal);
                    result.Add("AgentNetWinLoss.WinLoss", winloss.AgentWinLoss);
                    result.Add("AgentNetWinLoss.Comm", winloss.AgentComm);

                    var agentTotal = winloss.AgentWinLoss + winloss.AgentComm;
                    result.Add("AgentNetWinLoss.Total", agentTotal);
                    result.Add("companyWinLoss", -(winloss.MemberTotal + agentTotal));
                }

                if (request.CustLevel.IsMasterLevel())
                {
                    result.Add("AgentNetWinLoss.Total", winloss.AgentTotal);
                    result.Add("MasterNetWinLoss.WinLoss", winloss.MasterWinLoss);
                    result.Add("MasterNetWinLoss.Comm", winloss.MasterComm);

                    var masterTotal = winloss.MasterWinLoss + winloss.MasterComm;
                    result.Add("MasterNetWinLoss.Total", masterTotal);
                    result.Add("companyWinLoss", -(winloss.AgentTotal + masterTotal));
                }

                if (request.CustLevel.IsSuperLevel())
                {
                    result.Add("MasterNetWinLoss.Total", winloss.MasterTotal);
                    result.Add("SuperNetWinLoss.WinLoss", winloss.SuperWinLoss);
                    result.Add("SuperNetWinLoss.Comm", winloss.SuperComm);
                    result.Add("SuperNetWinLoss.Total", winloss.SuperTotal);
                    result.Add("companyWinLoss", -(winloss.MasterTotal + winloss.SuperTotal));
                }
            }

            return result;
        }
    }
}