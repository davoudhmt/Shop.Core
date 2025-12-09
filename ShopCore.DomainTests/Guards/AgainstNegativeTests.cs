using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstNegativeTests
    {
        [Fact]
        public void AgainstNegative_WithNull_ShouldNotThrow()
        {
            int? number = null;
            var ex = Record.Exception(() => Guard.AgainstNegative(number, "عدد نمی‌تواند خالی باشد"));
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void AgainstNegative_WithNegative_ShouldThrow(int? number)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstNegative(number, "عدد نمی‌تواند منفی باشد"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(999)]
        public void AgainstNegative_WithZeroOrPositive_ShouldNotThrow(int? number)
        {
            var ex = Record.Exception(() => Guard.AgainstNegative(number, "عدد نمی‌تواند منفی باشد"));
            Assert.Null(ex);
        }
    }
}
