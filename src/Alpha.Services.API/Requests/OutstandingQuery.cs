namespace Alpha.Services.API.Requests
{
    using Alpha.Services.API.ViewModels;
    using MediatR;

    public class OutstandingQuery : IRequest<OutstandingViewModel>
    {
        public OutstandingQuery(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }
    }
}