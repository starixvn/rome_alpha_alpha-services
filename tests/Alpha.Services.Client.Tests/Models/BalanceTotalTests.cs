namespace Alpha.Services.Client.Tests.Models
{
    using System.Linq;
    using Alpha.Services.Client.Models;
    using Xunit;

    public class BalanceTotalTests
    {
        [Fact]
        public void BalanceTotal_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new BalanceTotal
            {
                CustLevel = 4,
                HiddenDownlinesCount = 10,
                Total = null,
                Balances = Enumerable.Empty<BalanceItem>()
            };

            // Assert
            Assert.True(
                4 == model.CustLevel
                && 10 == model.HiddenDownlinesCount
                && null == model.Total
                && false == model.Balances.Any());
        }
    }
}
