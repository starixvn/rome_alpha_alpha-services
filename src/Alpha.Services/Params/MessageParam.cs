namespace Alpha.Services.Params
{
    using System;
    using Alpha.Services.Models;

    public class MessageParam
    {
        public MessageParam(
            UserLevel custLevel,
            int custId,
            int messageType,
            string language,
            DateTime fromTime,
            DateTime toTime)
        {
            CustLevel = custLevel;
            CustId = custId;
            MessageType = messageType;
            Language = language;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public int MessageType { get; }

        public string Language { get; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; }
    }
}