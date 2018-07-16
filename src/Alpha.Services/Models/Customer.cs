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
            bool isSubpended,
            int currencyId,
            string currencyName)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            IsClosed = isClosed;
            IsSubpended = isSubpended;
            CurrencyId = currencyId;
            CurrencyName = currencyName;
        }

        public int CustId { get; private set; }

        public string UserName { get; private set; }

        public byte RoleId { get; private set; }

        public bool IsClosed { get; private set; }

        public bool IsSubpended { get; private set; }

        public int CurrencyId { get; private set; }

        public string CurrencyName { get; private set; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}