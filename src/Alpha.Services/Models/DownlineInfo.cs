namespace Alpha.Services.Models
{
    public class DownlineInfo
    {
        public DownlineInfo(
            int custId,
            bool isSubAccount,
            bool isActive,
            byte roleId)
        {
            CustId = custId;
            IsSubAccount = isSubAccount;
            IsActive = isActive;
            RoleId = roleId;
        }

        public int CustId { get; }

        public bool IsSubAccount { get; }

        public bool IsActive { get; }

        public byte RoleId { get; }
    }
}