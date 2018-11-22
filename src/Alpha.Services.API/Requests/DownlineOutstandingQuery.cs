namespace Alpha.Services.API.Requests
{
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using MediatR;

    public class DownlineOutstandingQuery : IRequest<OutstandingViewModel>
    {
        public DownlineOutstandingQuery(int custId, byte custLevel, int downlineId, string downlineName, string language)
        {
            CustId = custId;
            CustLevel = Enumeration.FromValue<UserLevel>(custLevel);
            DownlineId = downlineId;
            DownlineName = downlineName;
            Language = language;
        }

        public int CustId { get; }

        public UserLevel CustLevel { get; }

        public int DownlineId { get; }

        public string DownlineName { get; }

        public string Language { get; }
    }
}
