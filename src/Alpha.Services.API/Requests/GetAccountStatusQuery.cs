namespace Alpha.Services.API.Requests
{
    using Alpha.Services.Models;
    using MediatR;

    public class GetAccountStatusQuery : IRequest<AccountStatus>
    {
        public GetAccountStatusQuery(int custId, int subAccountId)
        {
            CustId = custId;
            SubAccountId = subAccountId;
        }

        public int CustId { get; }

        public int SubAccountId { get; }
    }
}