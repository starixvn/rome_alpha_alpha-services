namespace Alpha.Services.API.Requests
{
    using System.Collections.Generic;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using MediatR;

    public class OutstandingDetailQuery : IRequest<IEnumerable<OutstandingDetail>>
    {
        public OutstandingDetailQuery(int custId, byte custLevel)
        {
            CustId = custId;
            CustLevel = Enumeration.FromValue<UserLevel>(custLevel);
        }

        public int CustId { get; }

        public UserLevel CustLevel { get; }
    }
}