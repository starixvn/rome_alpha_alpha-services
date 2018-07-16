namespace Alpha.Services.Criteria
{
    using System;
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetBalanceInfoCriteria : CriteriaBase
    {
        public GetBalanceInfoCriteria(
            UserLevel custLevel,
            int custId,
            DateTime moveBetsDay)
        {
            CustLevel = custLevel;
            CustId = custId;
            MoveBetsDay = moveBetsDay;
        }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public DateTime MoveBetsDay { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_CustomerSelBalanceInfo_Ag"
            : CustLevel.IsMasterLevel()
            ? "Age_CustomerSelBalanceInfo_MAg"
            : "Age_CustomerSelBalanceInfo_SAg";

        public override bool IsValid() => CustId > 0;
    }
}