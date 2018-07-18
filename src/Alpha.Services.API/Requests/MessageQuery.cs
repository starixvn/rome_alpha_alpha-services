namespace Alpha.Services.API.Requests
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.API.ViewModels;
    using MediatR;

    public class MessageQuery : IRequest<IEnumerable<MessageViewModel>>
    {
        public MessageQuery(
            int custId,
            int messageType,
            string language,
            DateTime fromTime,
            DateTime toTime)
        {
            CustId = custId;
            MessageType = messageType;
            Language = language;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public int CustId { get; }

        public int MessageType { get; }

        public string Language { get; }

        public DateTime FromTime { get; }

        public DateTime ToTime { get; }
    }
}