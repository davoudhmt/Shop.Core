using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstEmptyTest
    {
        [Fact]
        public void AgainstEmpty_WithEmptyCollection_ShouldThrow()
        {
            var collection = new List<int>();

            Assert.Throws<DomainException>(() => Guard.AgainstEmpty(collection, "کالکشن نمی‌تواند خالی باشد"));
        }

        [Fact]
        public void AgainstEmpty_WithNonEmptyCollection_ShouldNotThrow()
        {
            var collection = new List<int> { 1, 2, 3 };

            var ex = Record.Exception(() => Guard.AgainstEmpty(collection, "کالکشن نمی‌تواند خالی باشد"));
            Assert.Null(ex);
        }
    }
}
