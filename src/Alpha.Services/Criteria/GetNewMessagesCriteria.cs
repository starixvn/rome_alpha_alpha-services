namespace Alpha.Services.Criteria
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetNewMessagesCriteria : CriteriaBase
    {
        public GetNewMessagesCriteria(
            DateTime startDate,
            DateTime endDate,
            int messageType,
            bool isTest)
        {
            FromTime = startDate;
            ToTime = endDate;
            MessageType = messageType;
            IsTest = isTest;
        }

        public DateTime FromTime { get; }

        public DateTime ToTime { get; }

        [SpParam(SqlParamName = "Type")]
        public int MessageType { get; }

        public bool IsTest { get; }

        public override string GetSettingKey() => "Age_GetNewMessage";

        public override bool IsValid() => ToTime >= FromTime;
    }
}