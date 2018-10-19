namespace Alpha.Services.Params
{
    using System.Collections.Generic;
    using Alpha.Services.Models;

    public class OutstandingDetailParam
    {
        public OutstandingDetailParam(
            UserLevel custLevel,
            int custId,
            IEnumerable<int> productIds)
        {
            CustLevel = custLevel;
            CustId = custId;
            ProductIds = productIds;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public IEnumerable<int> ProductIds { get; }
    }
}