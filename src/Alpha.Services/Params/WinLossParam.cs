namespace Alpha.Services.Params
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Models;

    public class WinLossParam
    {
        public WinLossParam(
            UserLevel custLevel,
            int custId,
            DateTime date,
            IEnumerable<int> productIds)
        {
            CustLevel = custLevel;
            CustId = custId;
            Date = date;
            ProductIds = productIds;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public DateTime Date { get; }

        public IEnumerable<int> ProductIds { get; }
    }
}