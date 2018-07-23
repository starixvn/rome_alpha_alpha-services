namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetProductsCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnValidSettingKey()
        {
            // Arrange
            var criteria = new GetProductsCriteria();

            // Act & Assert
            Assert.Equal("Age_Rpt_GetProductList", criteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetProductsCriteria();

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}