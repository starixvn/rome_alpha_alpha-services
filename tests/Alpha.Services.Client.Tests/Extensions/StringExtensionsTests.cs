namespace Alpha.Services.Client.Tests.Extensions
{
    using Alpha.Services.Client.Extensions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void StripHTML_Always_ReturnsCorrectValue()
        {
            // Arrange
            var html = "<p>There was a <b>.NET</b> programmer";
            var expectedValue = "There was a .NET programmer";

            // Act
            var actualResult = html.StripHTML();

            // Assert
            Assert.Equal(expectedValue, actualResult);
        }
    }
}