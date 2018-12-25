namespace Alpha.Services.Criteria.BalanceInfo
{
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetCreditBalanceCriteria : CriteriaBase
    {
        public GetCreditBalanceCriteria(UserLevel custLevel, int custId, bool? closed = false)
            : this(custLevel, custId, string.Empty, null, closed, null, 1, int.MaxValue - 1)
        {
        }

        public GetCreditBalanceCriteria(
            UserLevel custLevel,
            int custId,
            string userName,
            bool? enabled,
            bool? closed,
            bool? suspended,
            int pageIndex,
            int pageSize)
        {
            CustLevel = custLevel;
            CustId = custId;
            UserName = userName ?? string.Empty;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public string UserName { get; }

        public bool? Enabled { get; }

        public bool? Closed { get; }

        public bool? Suspended { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItems { get; set; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_CustomerSelCreditBalanceList_Ag"
            : CustLevel.IsMasterLevel()
            ? "Age_CustomerSelCreditBalanceList_Mag"
            : "Age_CustomerSelCreditBalanceList_Sag";

        public override bool IsValid()
            => CustId > 0;
    }
}
