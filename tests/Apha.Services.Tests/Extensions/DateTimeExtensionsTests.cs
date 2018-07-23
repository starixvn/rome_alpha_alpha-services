namespace Apha.Services.Tests.Extensions
{
    using System;
    using Alpha.Services.Extensions;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(31, 1)]
        [InlineData(1, 6)]
        [InlineData(2, 6)]
        [InlineData(30, 6)]
        [InlineData(1, 12)]
        [InlineData(2, 12)]
        [InlineData(31, 12)]
        public void FirstDayOfMonth_Always_ReturnsFirstDayOfMonth(int day, int month)
        {
            // Arrange
            var dateTime = new DateTime(2016, month, day);

            // Act
            var actualDate = dateTime.FirstDayOfMonth();

            // Assert
            Assert.Equal(month, actualDate.Month);
            Assert.Equal(1, actualDate.Day);
        }
    }
}