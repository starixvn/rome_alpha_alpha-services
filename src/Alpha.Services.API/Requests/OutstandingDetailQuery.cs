namespace Alpha.Services.API.Requests
{
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using MediatR;

    public class OutstandingDetailQuery : IRequest<OutstandingDetailViewModel>
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