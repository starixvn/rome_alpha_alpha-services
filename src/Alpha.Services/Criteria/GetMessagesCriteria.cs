namespace Alpha.Services.Criteria
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetMessagesCriteria : CriteriaBase
    {
        public GetMessagesCriteria(
            int custId,
            string parentSites,
            int specialType,
            int totalMessage,
            DateTime startDate,
            DateTime endDate,
            bool isTest,
            int messageType)
        {
            CustId = custId;
            ParentSites = parentSites;
            Special = specialType;
            TotalMessasge = totalMessage;
            StartDate = startDate;
            EndDate = endDate;
            IsTest = isTest;
        }

        [SpParam(SqlParamName = "targetId")]
        public int CustId { get; }

        [SpParam(SqlParamName = "targetId2")]
        public string ParentSites { get; }

        public int Special { get; }

        [SpParam(SqlParamName = "record")]
        public int TotalMessasge { get; }

        [SpParam(SqlParamName = "sdate")]
        public DateTime StartDate { get; }

        [SpParam(SqlParamName = "edate")]
        public DateTime EndDate { get; }

        public bool IsTest { get; }

        [SpParam(Ignored = true)]
        public int MessageType { get; }

        public override string GetSettingKey()
            => MessageType == Constants.MessageType.Personal
            ? "Dot_Msg_GetPrivateMessage"
            : MessageType == Constants.MessageType.Normal
            ? "Dot_Msg_GetPublicMessage"
            : "Dot_Msg_GetSpecialMessage";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(ParentSites);
    }
}