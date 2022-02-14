namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class DownlineInfoTests
    {
        [Fact]
        public void DownlineInfo_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new DownlineInfo
            {
                CustId = 123,
                IsSubAccount = true,
                IsActive = true,
                RoleId = 3
            };

            // Assert
            Assert.True(
                123 == model.CustId
                && true == model.IsSubAccount
                && true == model.IsActive
                && 3 == model.RoleId);
        }
    }
}
