namespace Alpha.Services.API.Requests
{
    using MediatR;

    public class GetAccountStatusQuery : IRequest<bool>
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