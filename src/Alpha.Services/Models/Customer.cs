using Alpha.Services.Enumerations;

namespace Alpha.Services.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(
            string userName,
            byte roleId,
            string currencyName)
        {
            UserName = userName;
            RoleId = roleId;
            CurrencyName = currencyName;
        }

        public string UserName { get; private set; }

        public byte RoleId { get; private set; }

        public string CurrencyName { get; private set; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}