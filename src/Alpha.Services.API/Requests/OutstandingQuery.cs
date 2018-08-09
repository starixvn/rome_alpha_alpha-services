namespace Alpha.Services.API.Requests
{
    using System.Collections.Generic;
    using Alpha.Services.API.ViewModels;
    using MediatR;

    public class OutstandingQuery : IRequest<IDictionary<string, object>>
    {
        public OutstandingQuery(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }
    }
}