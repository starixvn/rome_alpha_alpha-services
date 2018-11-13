namespace Alpha.Services.API.Requests
{
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using MediatR;

    public class OutstandingQuery : IRequest<OutstandingViewModel>
    {
        public OutstandingQuery(int custId, byte custLevel, int top)
        {
            CustId = custId;
            CustLevel = Enumeration.FromValue<UserLevel>(custLevel);
            Top = top;
        }

        public int CustId { get; }

        public UserLevel CustLevel { get; }

        public int Top { get; }
    }
}