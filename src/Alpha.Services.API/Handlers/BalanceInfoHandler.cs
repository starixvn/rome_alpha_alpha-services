namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Services;
    using MediatR;

    public class BalanceInfoHandler : IRequestHandler<BalanceInfoQuery, BalanceInfoViewModel>
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

        public async Task<BalanceInfoViewModel> Handle(BalanceInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);
            var balanceInfo = await balanceInfoService.GetBalanceInfo(customer.CustLevel, customer.CustId);

            return new BalanceInfoViewModel(
                customer.CurrencyName,
                balanceInfo.CashBalance,
                balanceInfo.YesterdayCashBalance,
                balanceInfo.TotalBalance,
                balanceInfo.YesterdayTotalBalance,
                balanceInfo.TodayWinLoss,
                balanceInfo.YesterdayWinLoss,
                balanceInfo.GivenCredit,
                balanceInfo.MemberCredit);
        }
    }
}