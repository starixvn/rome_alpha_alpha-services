namespace Alpha.Services.Models
{
    using System.Collections.Generic;
    using Alpha.Services.Enumerations;

    public class Customer
    {
        private readonly IDictionary<int, string> systems = new Dictionary<int, string>
        {
            [1] = "IBCBet",
            [7] = "789Y",
            [8] = "EU88",
            [9] = "Y987",
        };

        public Customer()
        {
        }

        public Customer(
            int custId,
            string userName,
            byte roleId,
            string currencyName,
            int siteCode)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            CurrencyName = currencyName;
            SiteCode = siteCode;
        }

        public int CustId { get; }

        public string UserName { get; }

        public byte RoleId { get; }

        public string CurrencyName { get; }

        public int SiteCode { get; }

        public string System => systems[SiteCode];

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}