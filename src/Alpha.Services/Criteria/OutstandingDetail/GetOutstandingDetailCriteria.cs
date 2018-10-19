namespace Alpha.Services.Criteria.OutstandingDetail
{
    using System.Collections.Generic;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetOutstandingDetailCriteria : CriteriaBase
    {
        public GetOutstandingDetailCriteria(
            UserLevel custLevel,
            int custId,
            IEnumerable<int> productIds)
        {
            CustId = custId;
            CustLevel = custLevel;
            ProductIds = productIds == null ? string.Empty : productIds.JoinWithComma();
        }

        public int CustId { get; }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public string ProductIds { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
            ? "Age_Rpt_Outstanding_Member"
            : CustLevel.IsMasterLevel()
            ? "Age_Rpt_Outstanding_Agent"
            : "Age_Rpt_Outstanding_Master";

        public override bool IsValid() => CustId > 0 && !CustLevel.IsMemberLevel();
    }
}