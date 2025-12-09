using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstNullOrWhiteSpaceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void AgainstNullOrWhiteSpace_WithNullOrEmptyOrWhitespace_ShouldThrow(string? input)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(input, "رشته نمی‌تواند خالی یا فقط فاصله باشد"));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("Test")]
        [InlineData("  Valid  ")]
        public void AgainstNullOrWhiteSpace_WithValidString_ShouldNotThrow(string input)
        {
            var ex = Record.Exception(() => Guard.AgainstNullOrWhiteSpace(input, "رشته نمی‌تواند خالی یا فقط فاصله باشد"));
            Assert.Null(ex);
        }
    }
}
