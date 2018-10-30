namespace Alpha.Services.API.Handlers
{
    using Alpha.Services.API.Infrastructure.Exceptions;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class BalanceInfoHandler : IRequestHandler<BalanceInfoQuery, IDictionary<string, object>>
    {
        private readonly CustomerService customerService;
        private readonly BalanceInfoService balanceInfoService;
        private readonly OutstandingService outstandingService;

        public BalanceInfoHandler(
            CustomerService customerService,
            BalanceInfoService balanceInfoService,
            OutstandingService outstandingService)
        {
            this.customerService = customerService;
            this.balanceInfoService = balanceInfoService;
            this.outstandingService = outstandingService;
        }

        public async Task<IDictionary<string, object>> Handle(BalanceInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);

            if (customer == null)
            {
                throw new DomainException("Customer does not exist");
            }

            var balanceInfo = await balanceInfoService.GetBalanceInfo(customer.CustLevel, request.CustId);
            var directDownlineLevel = customer.CustLevel.GetDirectDownLineLevel();

            var balanceInfoResult = new Dictionary<string, object>
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

            return await AddTotalOutstanding(balanceInfoResult, customer.CustLevel, request.CustId);
        }

        private async Task<IDictionary<string, object>> AddTotalOutstanding(
            IDictionary<string, object> balanceInfo,
            UserLevel custLevel,
            int custId)
        {
            var outstanding = await outstandingService
                .GetTotalOutstanding(custLevel, custId)
                .ConfigureAwait(false);

            balanceInfo.TryAdd(UserLevel.Member.DisplayName, outstanding.TotalOutstanding);
            balanceInfo.TryAdd(custLevel.DisplayName, outstanding.Outstanding);

            return balanceInfo;
        }
    }
}