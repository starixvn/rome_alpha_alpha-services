namespace Alpha.Services.API.Requests
{
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using MediatR;
    using System.Collections.Generic;

    public class DownlineOutstandingQuery : IRequest<IDictionary<string, object>>
    {
        public DownlineOutstandingQuery(int custId, byte custLevel, int downlineId)
        {
            CustId = custId;
            CustLevel = Enumeration.FromValue<UserLevel>(custLevel);
            DownlineId = downlineId;
        }

        public int CustId { get; }

        public UserLevel CustLevel { get; }

        public int DownlineId { get; }
    }
}
