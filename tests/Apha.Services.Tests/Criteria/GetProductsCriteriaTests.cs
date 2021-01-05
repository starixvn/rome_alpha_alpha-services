namespace Alpha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetProductsCriteriaTests
    {
        private readonly byte settingStatus = 0;

        [Fact]
        public void GetSettingKey_Always_ReturnValidSettingKey()
        {
            // Arrange
            var criteria = new GetOfferedProductsCriteria(1, settingStatus);

            // Act & Assert
            Assert.Equal("Age_GetOfferedProductByCust", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetOfferedProductsCriteria(custId, settingStatus);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCustId_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetOfferedProductsCriteria(1, settingStatus);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}