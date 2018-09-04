namespace Alpha.Services.API.Requests
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Utils;
    using MediatR;

    public class WinLossQuery : IRequest<IDictionary<string, object>>
    {
        public WinLossQuery(
            int custId,
            byte custLevel,
            DateTime date)
        {
            CustId = custId;
            Date = date;
            CustLevel = Enumeration.FromValue<UserLevel>(custLevel);
        }

        public int CustId { get; }

        public DateTime Date { get; }

        public DateTime MinDate => SystemTime.Today.AddMonths(-3).FirstDayOfMonth();

        public UserLevel CustLevel { get; }
    }
}