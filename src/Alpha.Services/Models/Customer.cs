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
            string currencyName)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            CurrencyName = currencyName;
        }

        public int CustId { get; set; }

        public string UserName { get; private set; }

        public byte RoleId { get; private set; }

        public string CurrencyName { get; private set; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}