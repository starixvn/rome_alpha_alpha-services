namespace Alpha.Services.API.Requests
{
    using Alpha.Services.Models;
    using MediatR;

    public class DownlineInfoQuery : IRequest<DownlineInfo>
    {
        public DownlineInfoQuery(int custId, string downlineUserName)
        {
            CustId = custId;
            DownlineUserName = downlineUserName;
        }

        public int CustId { get; }

        public string DownlineUserName { get; }
    }
}