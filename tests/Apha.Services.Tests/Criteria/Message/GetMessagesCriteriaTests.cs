namespace Alpha.Services.Tests.Criteria.Message
{
    using System;
    using Alpha.Services.Criteria.Message;
    using Xunit;

    public class GetMessagesCriteriaTests
    {
        [Fact]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetNewMessagesCriteria(DateTime.Now, DateTime.Now, 0, true);

            // Act & Assert
            Assert.Equal("Age_GetNewMessage", criteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_FromTimeIsGreaterThanToTime_ReturnsFalse()
        {
            // Arrange
            var fromTime = DateTime.Now;
            var toTime = fromTime.AddHours(-1);
            var criteria = new GetNewMessagesCriteria(fromTime, toTime, 0, true);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetNewMessagesCriteria(DateTime.Now, DateTime.Now, 0, true);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}