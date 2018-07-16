namespace Alpha.Services.Criteria
{
    using Fanex.Data.Repository;

    public class CustomerInfoCriteria : CriteriaBase
    {
        public CustomerInfoCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_CustomerSelUserInfo";

        public override bool IsValid() => CustId > 0;
    }
}