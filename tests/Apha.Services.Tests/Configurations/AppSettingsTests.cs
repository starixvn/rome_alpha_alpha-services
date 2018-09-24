namespace Apha.Services.Tests.Configurations
{
    using System;
    using Alpha.Services.Configurations;
    using Microsoft.Extensions.Configuration;
    using NSubstitute;
    using Xunit;

    public class AppSettingsTests
    {
        private readonly IConfiguration configuration;
        private AppSettings appSettings;

        public AppSettingsTests()
        {
            configuration = Substitute.For<IConfiguration>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsLocalTest_HasConfig_ReturnsCorrectValue(bool expectedValue)
        {
            // Arrange
            configuration.GetSection("IsLocalTest").Value
                .Returns(expectedValue.ToString());

            // Act
            appSettings = new AppSettings(configuration);

            // Assert
            Assert.Equal(expectedValue, appSettings.IsLocalTest);
        }

        [Fact]
        public void GetParentSites_HasNotConfig_ThrowsInvalidOperationException()
        {
            // Arrange
            appSettings = new AppSettings(configuration);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => appSettings.GetParentSites("Agent", "IBCBet"));
        }
    }
}