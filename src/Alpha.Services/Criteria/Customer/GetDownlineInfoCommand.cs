namespace Alpha.Services.Criteria.Customer
{
    using Fanex.Data.Repository;

    public class GetDownlineInfoCommand : NonQueryCommand
    {
        public GetDownlineInfoCommand(int uplineId, string userName, int subAccountId)
        {
            UplineId = uplineId;
            UserName = userName;
            SubAccountId = subAccountId;
        }

        public int UplineId { get; }

        public string UserName { get; }

        public int SubAccountId { get; }

        public bool IsActive { get; set; }

        public int CustId { get; set; }

        public bool IsSubAcc { get; set; }

        public byte RoleId { get; set; }

        public override string GetSettingKey() => "Age_CustomerSelCheckBelonging_ByUsername";

        public override bool IsValid() => UplineId > 0 && !string.IsNullOrWhiteSpace(UserName);
    }
}