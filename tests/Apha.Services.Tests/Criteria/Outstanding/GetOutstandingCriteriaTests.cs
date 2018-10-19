namespace Alpha.Services.Tests.Criteria.Outstanding
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Xunit;

    public class GetOutstandingCriteriaTests
    {
        [Theory]
        [InlineData(2, "Age_CustomerSelStatisticsInfo_Ag")]
        [InlineData(3, "Age_CustomerSelStatisticsInfo_MAg")]
        [InlineData(4, "Age_CustomerSelStatisticsInfo_SAg")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetOutstandingCriteria(custLevel, 0);

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
            var criteria = new GetOutstandingCriteria(UserLevel.Agent, custId);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_IsMemberLevel_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetOutstandingCriteria(UserLevel.Member, 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetOutstandingCriteria(UserLevel.Agent, 1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}