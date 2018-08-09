namespace Alpha.Services.API.Requests
{
    using System.Collections.Generic;
    using MediatR;

    public class BalanceInfoQuery : IRequest<IDictionary<string, object>>
    {
        public BalanceInfoQuery(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }
    }
}