namespace Apha.Services.Tests.Utils
{
    using System;
    using Alpha.Services.Utils;
    using Xunit;

    public class SystemTimeTests
    {
        [Fact]
        public void Today_Always_ReturnsValueSetToTodayProperty()
        {
            // Arrange
            var expectedDate = DateTime.Today;
            SystemTime.Today = expectedDate;

            // Act
            var actualDate = SystemTime.Today;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}