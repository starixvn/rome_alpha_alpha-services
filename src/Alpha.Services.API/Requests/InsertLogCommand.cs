namespace Alpha.Services.API.Requests
{
    using System.Collections.Generic;
    using MediatR;

    public class InsertLogCommand : IRequest<bool>
    {
        public int ActorId { get; set; }

        public int CustId { get; set; }

        public int ActionId { get; set; }

        public string SpName { get; set; }

        public string IP { get; set; }

        public IDictionary<string, object> NewValues { get; set; }

        public IDictionary<string, object> OldValues { get; set; }
    }
}