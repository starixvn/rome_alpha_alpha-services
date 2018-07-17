namespace Alpha.Services.Criteria
{
    using Fanex.Data.Repository;

    public class GetCustomerDateLimitCriteria : CriteriaBase
    {
        public GetCustomerDateLimitCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_CustomerSelDateLimit";

        public override bool IsValid() => CustId > 0;
    }
}