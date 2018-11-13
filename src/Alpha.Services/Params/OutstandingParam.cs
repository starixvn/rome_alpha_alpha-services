namespace Alpha.Services.Params
{
    using Alpha.Services.Models;
    using System.Collections.Generic;

    public class OutstandingParam
    {
        public OutstandingParam(
            UserLevel custLevel,
            int custId,
            IEnumerable<int> productIds,
            int downlineId = 0)
        {
            CustLevel = custLevel;
            CustId = custId;
            ProductIds = productIds;
            DownlineId = downlineId;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public IEnumerable<int> ProductIds { get; }

        public int DownlineId { get; }
    }
}