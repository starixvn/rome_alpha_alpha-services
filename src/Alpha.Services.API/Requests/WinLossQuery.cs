namespace Alpha.Services.API.Requests
{
    using System;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Extensions;
    using Alpha.Services.Utils;
    using MediatR;

    public class WinLossQuery : IRequest<WinLossViewModel>
    {
        public WinLossQuery(
            int custId,
            DateTime date)
        {
            CustId = custId;
            Date = date;
        }

        public int CustId { get; }

        public DateTime Date { get; }

        public DateTime MinDate => SystemTime.Today.AddMonths(-3).FirstDayOfMonth();
    }
}