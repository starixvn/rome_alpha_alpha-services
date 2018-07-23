namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetBalanceUpDayAndMoveBetsDayCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnValidSettingKey()
        {
            // Arrange
            var criteria = new GetBalanceUpDayAndMoveBetsDayCriteria();

            // Act & Assert
            Assert.Equal("Age_SelBalanceUpDay", criteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetBalanceUpDayAndMoveBetsDayCriteria();

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}