namespace Alpha.Services.API.Requests
{
    using Alpha.Services.Models;
    using MediatR;

    public class BalanceQuery : IRequest<BalanceTotal>
    {
        public BalanceQuery(int custId, byte custLevel, int top)
        {
            CustId = custId;
            CustLevel = custLevel;
            Top = top;
        }

        public int CustId { get; }

        public byte CustLevel { get; }

        public int Top { get; }
    }
}
