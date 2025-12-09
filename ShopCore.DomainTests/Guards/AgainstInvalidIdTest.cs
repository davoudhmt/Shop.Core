using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstInvalidIdTest
    {
        [Fact]
        public void AgainstInvalidId_WithInValidInput_ShouldThrow()
        {
            var input1 = -1;
            var input2 = 0;
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidId(input1, "عدد منفی نمی تواند باشد"));
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidId(input2, "عدد 0 نمی تواند باشد"));
        }

        [Fact]
        public void AgainstInvalidId_WithValidInput_ShouldNotThrow()
        {
            var input1 = 1;
            var input2 = 99999;
            var ex1 = Record.Exception(() => Guard.AgainstInvalidId(input1, "ورودی باید عدد مثبت باشد"));
            Assert.Null(ex1);
            var ex2 = Record.Exception(() => Guard.AgainstInvalidId(input2, "ورودی باید عدد مثبت باشد"));
            Assert.Null(ex2);
        }
    }
}
