namespace Alpha.Services.API.Requests
{
    using Alpha.Services.Models;
    using MediatR;

    public class GetCustomerQuery : IRequest<Customer>
    {
        public GetCustomerQuery(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }
    }
}