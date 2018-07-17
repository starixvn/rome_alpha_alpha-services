using System;
using System.Collections.Generic;
using Alpha.Services.Models;

namespace Alpha.Services.Params
{
    public class WinLossParam
    {
        public WinLossParam(
            UserLevel custLevel,
            int custId,
            DateTime date,
            DateTime limitedDate,
            IEnumerable<int> productIds)
        {
            CustLevel = custLevel;
            CustId = custId;
            Date = date;
            LimitedDate = limitedDate;
            ProductIds = productIds;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public DateTime Date { get; }

        public DateTime LimitedDate { get; }

        public IEnumerable<int> ProductIds { get; }
    }
}