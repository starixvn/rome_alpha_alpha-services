namespace Alpha.Services.Params
{
    using System;

    public class MessageParam
    {
        public MessageParam(
            int userId,
            int custId,
            bool isAdminUser,
            string parentSites,
            int specialType,
            int totalMessage,
            DateTime startDate,
            DateTime endDate,
            DateTime limitedDate,
            bool isTest,
            int messageType)
        {
            CustId = custId;
            ParentSites = parentSites;
            SpecialType = specialType;
            TotalMessage = totalMessage;
            StartDate = startDate;
            EndDate = endDate;
            LimitedDate = limitedDate;
            IsTest = isTest;
            MessageType = messageType;
            IsAdminUser = isAdminUser;
        }

        public int CustId { get; }

        public bool IsAdminUser { get; }

        public int UserId { get; }

        public string ParentSites { get; }

        public int SpecialType { get; }

        public int TotalMessage { get; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; }

        public DateTime LimitedDate { get; }

        public bool IsTest { get; }

        public int MessageType { get; }
    }
}