using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstNegativeOrZeroTests
    {
        [Fact]
        public void AgainstNegativeOrZero_WithNull_ShouldNotThrow()
        {
            int? number = null;
            var ex = Record.Exception(() => Guard.AgainstNegativeOrZero(number, "عدد نمی‌تواند خالی باشد"));
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void AgainstNegativeOrZero_WithZeroOrNegative_ShouldThrow(int? number)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstNegativeOrZero(number, "عدد باید بزرگتر از صفر باشد"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(999)]
        public void AgainstNegativeOrZero_WithPositive_ShouldNotThrow(int? number)
        {
            var ex = Record.Exception(() => Guard.AgainstNegativeOrZero(number, "عدد باید بزرگتر از صفر باشد"));
            Assert.Null(ex);
        }
    }
}
