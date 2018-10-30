namespace Alpha.Services.Criteria.Outstanding
{
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetTotalOutstandingCriteria : CriteriaBase
    {
        public GetTotalOutstandingCriteria(UserLevel custLevel, int custId)
        {
        }

        public int CustId { get; }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_CustomerSelStatisticsInfo_Ag"
            : CustLevel.IsMasterLevel()
            ? "Age_CustomerSelStatisticsInfo_MAg"
            : "Age_CustomerSelStatisticsInfo_SAg";

        public override bool IsValid()
            => CustId > 0 && !CustLevel.IsMemberLevel();
    }
}
