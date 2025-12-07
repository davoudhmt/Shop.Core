using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstOutOfRangeTests
    {
        [Fact]
        public void AgainstOutOfRange_WithNull_ShouldNotThrow()
        {
            int? number = null;
            var ex = Record.Exception(() => Guard.AgainstOutOfRange(number, 1, 10, "عدد باید بین 1 تا 10 باشد"));
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(0)]   // کمتر از min
        [InlineData(11)]  // بیشتر از max
        public void AgainstOutOfRange_WithOutOfRangeValue_ShouldThrow(int? number)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstOutOfRange(number, 1, 10, "عدد باید بین 1 تا 10 باشد"));
        }

        [Theory]
        [InlineData(1)]   // برابر min
        [InlineData(5)]   // بین min و max
        [InlineData(10)]  // برابر max
        public void AgainstOutOfRange_WithValidValue_ShouldNotThrow(int? number)
        {
            var ex = Record.Exception(() => Guard.AgainstOutOfRange(number, 1, 10, "عدد باید بین 1 تا 10 باشد"));
            Assert.Null(ex);
        }

    }
}
