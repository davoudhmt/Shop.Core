using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstDuplicateTests
    {
        [Fact]
        public void AgainstDuplicate_WithDuplicateValue_ShouldThrow()
        {
            var collection = new List<int> { 1, 2, 3 };
            int newValue = 2;

            Assert.Throws<DomainException>(() => Guard.AgainstDuplicate(collection, newValue, "مقدار تکراری مجاز نیست"));
        }

        [Fact]
        public void AgainstDuplicate_WithUniqueValue_ShouldNotThrow()
        {
            var collection = new List<int> { 1, 2, 3 };
            int newValue = 4;

            var ex = Record.Exception(() => Guard.AgainstDuplicate(collection, newValue, "مقدار تکراری مجاز نیست"));
            Assert.Null(ex);
        }
    }
}
