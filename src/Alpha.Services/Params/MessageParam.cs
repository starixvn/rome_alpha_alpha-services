namespace Alpha.Services.Params
{
    using System;

    public class MessageParam
    {
        public MessageParam(
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

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; }
    }
}