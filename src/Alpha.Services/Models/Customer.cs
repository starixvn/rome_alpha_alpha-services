using Alpha.Services.Enumerations;

namespace Alpha.Services.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(
            int custId,
            string userName,
            byte roleId,
            bool isClosed,
            bool isSubpended)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            IsClosed = isClosed;
            IsSubpended = isSubpended;
        }

        public int CustId { get; private set; }

        public string UserName { get; private set; }

        public byte RoleId { get; private set; }

        public bool IsClosed { get; private set; }

        public bool IsSubpended { get; private set; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}