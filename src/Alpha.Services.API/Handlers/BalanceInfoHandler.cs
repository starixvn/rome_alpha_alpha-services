namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Infrastructure.Exceptions;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Services;
    using MediatR;

    public class BalanceInfoHandler : IRequestHandler<BalanceInfoQuery, IDictionary<string, object>>
    {
        private readonly CustomerService customerService;
        private readonly BalanceInfoService balanceInfoService;

        public BalanceInfoHandler(
            CustomerService customerService,
            BalanceInfoService balanceInfoService)
        {
            this.customerService = customerService;
            this.balanceInfoService = balanceInfoService;
        }

        public async Task<IDictionary<string, object>> Handle(BalanceInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);

            if (customer == null)
            {
                throw new DomainException("Customer does not exist");
            }

            var balanceInfo = await balanceInfoService.GetBalanceInfo(customer.CustLevel, customer.CustId);
            var directDownlineLevel = customer.CustLevel.GetDirectDownLineLevel();

            return new Dictionary<string, object>
            {
                [nameof(customer.CurrencyName)] = customer.CurrencyName,
                [nameof(balanceInfo.CashBalance)] = balanceInfo.CashBalance,
                [nameof(balanceInfo.YesterdayCashBalance)] = balanceInfo.YesterdayCashBalance,
                [nameof(balanceInfo.TotalBalance)] = balanceInfo.TotalBalance,
                [nameof(balanceInfo.YesterdayTotalBalance)] = balanceInfo.YesterdayTotalBalance,
                [nameof(balanceInfo.TodayWinLoss)] = balanceInfo.TodayWinLoss,
                [nameof(balanceInfo.YesterdayWinLoss)] = balanceInfo.YesterdayWinLoss,
                [nameof(balanceInfo.GivenCredit)] = balanceInfo.GivenCredit,
                [directDownlineLevel.DisplayName + nameof(balanceInfo.TotalCredit)] = balanceInfo.TotalCredit
            };
        }
    }
}