namespace Alpha.Services.Criteria.Outstanding
{
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class GetDownlineOutstandingCriteria : CriteriaBase
    {
        public GetDownlineOutstandingCriteria(
            UserLevel custLevel,
            IEnumerable<int> productIds,
            int downlineId)
        {
            CustLevel = custLevel;
            ProductIds = productIds == null ? string.Empty : productIds.JoinWithComma();
            DownlineId = downlineId;
        }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public string ProductIds { get; }

        public int DownlineId { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_Rpt_DownlineOutstanding_Member"
            : CustLevel.IsMasterLevel()
            ? "Age_Rpt_DownlineOutstanding_Agent"
            : "Age_Rpt_DownlineOutstanding_Master";

        public override bool IsValid() => !CustLevel.IsMemberLevel() && DownlineId > 0;
    }
}
