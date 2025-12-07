using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstNullTest
    {
        [Theory]
        [InlineData(null)]
        public void AgainstNull_WithNullInput_ShouldThrow(object input)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstNull(input, "ورودی نمی تواند خالی باشد"));
        }

        [Fact]
        public void AgainstNull_WithValidInput_ShouldNotThrow()
        {
            var ex = Record.Exception(() => Guard.AgainstNull("ABC", "ورودی نمی تواند خالی باشد."));
            Assert.Null(ex);
        }

        [Fact]
        public void AgainstNull_Generic_WithNullNullableStruct_ShouldThrow()
        {
            int? number = null;
            Assert.Throws<DomainException>(() => Guard.AgainstNull(number, "عدد نمی‌تواند خالی باشد"));
        }

        [Fact]
        public void AgainstNull_Generic_WithValidNullableStruct_ShouldNotThrow()
        {
            int? number = 123;
            var ex = Record.Exception(() => Guard.AgainstNull(number, "عدد نمی‌تواند خالی باشد"));
            Assert.Null(ex);
        }
    }
}
