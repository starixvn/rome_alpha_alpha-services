namespace Alpha.Services.Params
{
    using System;

    public class MessageParam
    {
        public MessageParam(
            int messageType,
            DateTime fromTime,
            DateTime toTime,
            bool isLocalTest)
        {
            MessageType = messageType;
            FromTime = fromTime;
            ToTime = toTime;
            IsLocalTest = isLocalTest;
        }

        public int MessageType { get; }

        public DateTime FromTime { get; }

        public DateTime ToTime { get; }

        public bool IsLocalTest { get; }
    }
}