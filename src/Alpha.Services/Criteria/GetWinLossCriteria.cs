namespace Alpha.Services.Criteria
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetWinLossCriteria : CriteriaBase
    {
        public GetWinLossCriteria(
            UserLevel custLevel,
            int custId,
            DateTime fromDate,
            DateTime toDate,
            DateTime balanceUpDay,
            DateTime moveBetsDay,
            IEnumerable<int> productIds)
        {
            CustLevel = custLevel;
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            BalanceUpDay = balanceUpDay;
            MoveBetsDay = moveBetsDay;
            ProductIds = productIds == null ? string.Empty : productIds.JoinWithComma();
        }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public string ProductIds { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public DateTime BalanceUpDay { get; }

        public DateTime MoveBetsDay { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_Rpt_WinLoss_Member"
            : CustLevel.IsMasterLevel()
            ? "Age_Rpt_WinLoss_Agent"
            : "Age_Rpt_WinLoss_Master";

        public override bool IsValid()
            => CustId > 0
                && FromDate <= ToDate
                && !string.IsNullOrWhiteSpace(ProductIds);
    }
}