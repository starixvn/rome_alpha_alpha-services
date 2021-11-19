namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class CustomerInfoTests
    {
        [Fact]
        public void CustomerInfo_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new CustomerInfo
            {
                CustId = 123,
                UserName = "master1",
                RoleId = 3,
                CurrencyName = "UUS",
                SiteCode = 3001,
                System = "Beta"
            };

            // Assert
            Assert.True(
                123 == model.CustId
                && "master1" == model.UserName
                && 3 == model.RoleId
                && "UUS" == model.CurrencyName
                && 3001 == model.SiteCode
                && "Beta" == model.System);
        }
    }
}
