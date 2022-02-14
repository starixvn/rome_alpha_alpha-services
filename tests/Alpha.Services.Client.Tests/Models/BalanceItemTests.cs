namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class BalanceItemTests
    {
        [Fact]
        public void BalanceItem_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new BalanceItem
            {
                Username = "agent1",
                Balance = 101,
                YesterdayBalance = 109
            };

            // Assert
            Assert.True(
                "agent1" == model.Username
                && 101 == model.Balance
                && 109 == model.YesterdayBalance);
        }
    }
}
