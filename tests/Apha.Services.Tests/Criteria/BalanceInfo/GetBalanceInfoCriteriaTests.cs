namespace Alpha.Services.Tests.Criteria.BalanceInfo
{
    using System;
    using Alpha.Services.Criteria.BalanceInfo;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Xunit;

    public class GetBalanceInfoCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange
            var moveBetsDate = new DateTime(2021, 5, 12);

            // Act
            var criteria = new GetBalanceInfoCriteria(UserLevel.Agent, 1, moveBetsDate);

            // Assert
            Assert.True(
                criteria.CustLevel.Value == 2
                && criteria.CustId == 1
                && criteria.MoveBetsDay == moveBetsDate);
        }

        [Theory]
        [InlineData(2, "Age_CustomerSelBalanceInfo_Ag")]
        [InlineData(3, "Age_CustomerSelBalanceInfo_MAg")]
        [InlineData(4, "Age_CustomerSelBalanceInfo_SAg")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetBalanceInfoCriteria(custLevel, 0, DateTime.Now);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetBalanceInfoCriteria(UserLevel.Agent, custId, DateTime.Now);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetBalanceInfoCriteria(UserLevel.Agent, 1, DateTime.Now);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}