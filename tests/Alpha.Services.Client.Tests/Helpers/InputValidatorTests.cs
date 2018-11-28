namespace Alpha.Services.Client.Tests.Helpers
{
    using System;
    using System.Linq;
    using Alpha.Services.Client.Helpers;
    using Xunit;

    public class InputValidatorTests
    {
        [Fact]
        public void ValidateInputParam_InvalidInt_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => InputValidator.ValidateInputParam("custId", -123));
        }

        [Fact]
        public void ValidateInputParam_StringIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => InputValidator.ValidateInputParam("custId", null));
        }

        [Fact]
        public void ValidateInputParam_StringIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => InputValidator.ValidateInputParam("custId", string.Empty));
        }

        [Fact]
        public void ValidateInputParam_StringIsAllWhiteSpace_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => InputValidator.ValidateInputParam("custId", "     "));
        }

        [Fact]
        public void ValidateInputParam_EnumerableIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => InputValidator.ValidateInputParam<string>("custId", null));
        }

        [Fact]
        public void ValidateInputParam_EnumerableIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InputValidator.ValidateInputParam<string>("custId", Enumerable.Empty<string>()));
        }
    }
}
