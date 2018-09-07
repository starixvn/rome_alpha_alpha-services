namespace Alpha.Services.API.Requests
{
    using System.Collections.Generic;
    using Alpha.Services.API.ViewModels;
    using MediatR;

    public class MessageQuery : IRequest<IEnumerable<MessageViewModel>>
    {
        public int MessageType { get; set; }

        public long FromTime { get; set; }

        public long ToTime { get; set; }

        public IEnumerable<CustomerInfo> Customers { get; set; }
    }
}