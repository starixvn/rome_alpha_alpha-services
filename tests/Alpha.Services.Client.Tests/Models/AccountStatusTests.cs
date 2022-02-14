namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class AccountStatusTests
    {
        [Fact]
        public void AccountStatus_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new AccountStatus
            {
                IsActive = true,
                Locked = 2,
                IsProtectedMode = true
            };

            // Assert
            Assert.True(
                true == model.IsActive
                && 2 == model.Locked
                && true == model.IsProtectedMode
                && true == model.IsLocked);
        }
    }
}
