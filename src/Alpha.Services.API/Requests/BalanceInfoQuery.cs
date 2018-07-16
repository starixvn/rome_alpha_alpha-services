namespace Alpha.Services.API.Requests
{
    using Alpha.Services.API.ViewModels;
    using MediatR;

    public class BalanceInfoQuery : IRequest<BalanceInfoViewModel>
    {
        public BalanceInfoQuery(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }
    }
}