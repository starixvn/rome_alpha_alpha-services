namespace Alpha.Services.Criteria
{
    using Fanex.Data.Repository;

    public class GetOfferedProductsCriteria : CriteriaBase
    {
        public GetOfferedProductsCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_GetOfferedProductByCust";

        public override bool IsValid() => CustId > 0;
    }
}