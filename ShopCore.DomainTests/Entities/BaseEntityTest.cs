using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class BaseEntityTest
    {
        [Fact]
        public void Constructor_WithValidCreateBy_ShouldInitializeProperties()
        {
            var entity = new BaseEntity(1);

            Assert.Equal(1, entity.CreateBy);
            Assert.Equal(EntityStatus.Active, entity.Status);
            Assert.True(entity.CreateDate <= DateTime.UtcNow);
        }

        [Fact]
        public void Constructor_WithInvalidCreateBy_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() => new BaseEntity(0));
        }

        [Fact]
        public void ChangeUpdateInfo_WithValidUpdateBy_ShouldSetProperties()
        {
            var entity = new BaseEntity(1);
            entity.ChangeUpdateInfo(2);

            Assert.Equal(2, entity.UpdateBy);
            Assert.NotNull(entity.UpdateDate);
        }

        [Fact]
        public void ChangeUpdateInfo_WithInvalidUpdateBy_ShouldThrowDomainException()
        {
            var entity = new BaseEntity(1);
            Assert.Throws<DomainException>(() => entity.ChangeUpdateInfo(0));
        }

        [Fact]
        public void ChangeStatus_WithValidStatus_ShouldUpdateStatus()
        {
            var entity = new BaseEntity(1);
            entity.ChangeStatus(EntityStatus.Inactive);

            Assert.Equal(EntityStatus.Inactive, entity.Status);
        }

        [Fact]
        public void ChangeStatus_WithInvalidStatus_ShouldThrowDomainException()
        {
            var entity = new BaseEntity(1);
            Assert.Throws<DomainException>(() => entity.ChangeStatus((EntityStatus)999));
        }
    }
}
