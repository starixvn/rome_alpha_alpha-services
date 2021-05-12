namespace Alpha.Services.Tests.Criteria.BalanceInfo
{
    using Alpha.Services.Criteria.BalanceInfo;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Xunit;

    public class GetCreditBalanceCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange & Act
            var criteria = new GetCreditBalanceCriteria(
                Enumeration.FromValue<UserLevel>(4),
                8,
                "super",
                true,
                false,
                false,
                1,
                500)
            {
                TotalItems = 61
            };

            // Assert
            Assert.True(
                criteria.CustLevel.Value == 4
                && criteria.CustId == 8
                && criteria.UserName == "super"
                && criteria.Enabled == true
                && criteria.Closed == false
                && criteria.Suspended == false
                && criteria.PageIndex == 1
                && criteria.PageSize == 500
                && criteria.TotalItems == 61);
        }

        [Fact]
        public void Constructor_UsernameIsNull_AssignsEmpty()
        {
            // Arrange & Act
            var criteria = new GetCreditBalanceCriteria(
                Enumeration.FromValue<UserLevel>(4),
                8,
                null,
                true,
                false,
                false,
                1,
                500);

            // Assert
            Assert.Empty(criteria.UserName);
        }

        [Theory]
        [InlineData(2, "Age_CustomerSelCreditBalanceList_Ag")]
        [InlineData(3, "Age_CustomerSelCreditBalanceList_Mag")]
        [InlineData(4, "Age_CustomerSelCreditBalanceList_Sag")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetCreditBalanceCriteria(custLevel, 8);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(int.MinValue, false)]
        [InlineData(8, true)]
        public void IsValid_CustId_ReturnsExpectedValues(int custId, bool expectedValid)
        {
            // Arrange
            var criteria = new GetCreditBalanceCriteria(Enumeration.FromValue<UserLevel>(4), custId);

            // Act
            var actualValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValid, actualValid);
        }
    }
}
