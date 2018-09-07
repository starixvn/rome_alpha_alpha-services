using Alpha.Services.Enumerations;
using Alpha.Services.Models;

namespace Alpha.Services.API.Requests
{
    public class CustomerInfo
    {
        public int CustId { get; set; }

        public byte RoleId { get; set; }

        public string Language { get; set; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);
    }
}