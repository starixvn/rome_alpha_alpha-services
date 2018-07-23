namespace Alpha.Services.Criteria
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Constants;
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetMessagesCriteria : CriteriaBase
    {
        private static readonly IDictionary<int, int> specialTypes = new Dictionary<int, int>
        {
            [MessageTypeConstants.Normal] = 0,
            [MessageTypeConstants.Special] = 1,
            [MessageTypeConstants.Personal] = 2
        };

        public GetMessagesCriteria(
            int custId,
            string parentSites,
            int totalMessage,
            DateTime startDate,
            DateTime endDate,
            bool isTest,
            int messageType)
        {
            CustId = custId;
            ParentSites = parentSites;
            TotalMessasge = totalMessage;
            StartDate = startDate;
            EndDate = endDate;
            IsTest = isTest;
            MessageType = messageType;
        }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        [SpParam(SqlParamName = "targetId")]
        public int CustId { get; }

        [SpParam(SqlParamName = "targetId2")]
        public string ParentSites { get; }

        public int Special => specialTypes[MessageType];

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
            => MessageType == MessageTypeConstants.Personal
            ? "Dot_Msg_GetPrivateMessage"
            : MessageType == MessageTypeConstants.Normal
            ? "Dot_Msg_GetPublicMessage"
            : "Dot_Msg_GetSpecialMessage";

        public override bool IsValid()
            => CustId > 0
                && !string.IsNullOrWhiteSpace(ParentSites)
                && EndDate >= StartDate;
    }
}