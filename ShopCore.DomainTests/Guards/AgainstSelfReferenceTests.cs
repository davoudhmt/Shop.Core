using Domain.Common;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstSelfReferenceTests
    {
        [Fact]
        public void AgainstSelfReference_WithNullValues_ShouldNotThrow()
        {
            long? id = null;
            long? parentId = null;

            var ex = Record.Exception(() => Guard.AgainstSelfReference(id, parentId, "شناسه نمی‌تواند به خودش ارجاع دهد"));
            Assert.Null(ex);
        }

        [Fact]
        public void AgainstSelfReference_WithEqualValues_ShouldThrow()
        {
            long? id = 10;
            long? parentId = 10;

            Assert.Throws<DomainException>(() => Guard.AgainstSelfReference(id, parentId, "شناسه نمی‌تواند به خودش ارجاع دهد"));
        }

        [Fact]
        public void AgainstSelfReference_WithDifferentValues_ShouldNotThrow()
        {
            long? id = 10;
            long? parentId = 20;

            var ex = Record.Exception(() => Guard.AgainstSelfReference(id, parentId, "شناسه نمی‌تواند به خودش ارجاع دهد"));
            Assert.Null(ex);
        }
    }
}
