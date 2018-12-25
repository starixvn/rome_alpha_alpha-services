namespace Alpha.Services.Tests.Criteria.Outstanding
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Xunit;

    public class GetTotalOutstandingCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange & Act
            var criteria = new GetTotalOutstandingCriteria(
                Enumeration.FromValue<UserLevel>(4),
                8);

            // Assert
            Assert.True(
                criteria.CustLevel.Value == 4
                && criteria.CustId == 8);
        }

        [Theory]
        [InlineData(2, "Age_CustomerSelStatisticsInfo_Ag")]
        [InlineData(3, "Age_CustomerSelStatisticsInfo_MAg")]
        [InlineData(4, "Age_CustomerSelStatisticsInfo_SAg")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevel, string expectedSettingKey)
        {
            // Arrange
            var criteria = new GetTotalOutstandingCriteria(
                Enumeration.FromValue<UserLevel>(custLevel),
                8);

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
            var criteria = new GetTotalOutstandingCriteria(Enumeration.FromValue<UserLevel>(4), custId);

            // Act
            var actualValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValid, actualValid);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        public void IsValid_CustLevel_ReturnsExpectedValues(byte custLevel, bool expectedValid)
        {
            // Arrange
            var criteria = new GetTotalOutstandingCriteria(Enumeration.FromValue<UserLevel>(custLevel), 8);

            // Act
            var actualValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValid, actualValid);
        }
    }
}
