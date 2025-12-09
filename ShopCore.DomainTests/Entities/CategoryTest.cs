using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            var category = new Category(1, 2, "Shoes", "SHO", "Sport shoes");

            Assert.Equal("Shoes", category.Name);
            Assert.Equal("SHO", category.CategoryCode.Value);
            Assert.Equal("Sport shoes", category.Description);
            Assert.Equal(2, category.ParentCategoryId);
            Assert.Equal(EntityStatus.Active, category.Status);
        }

        [Fact]
        public void Constructor_WithEmptyName_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new Category(1, 2, "", "SHO", "Sport shoes")
            );
        }

        [Fact]
        public void Constructor_WithInvalidParentId_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new Category(1, 0, "Shoes", "SHO", "Sport shoes")
            );
        }

        [Fact]
        public void ChangeCategory_WithValidName_ShouldUpdateNameAndUpdateDate()
        {
            var category = new Category(1, 2, "Shoes", "SHO", "Sport shoes");
            category.ChangeCategory(null, "Clothes", null, null, null, 2);

            Assert.Equal("Clothes", category.Name);
            Assert.Equal(2, category.UpdateBy);
            Assert.NotNull(category.UpdateDate);
        }

        [Fact]
        public void ChangeCategory_WithInvalidName_ShouldThrowDomainException()
        {
            var category = new Category(1, 2, "Shoes", "SHO", "Sport shoes");

            Assert.Throws<DomainException>(() =>
                category.ChangeCategory(null, "", null, null, null, 2)
            );
        }

        [Fact]
        public void ChangeCategory_WithValidStatus_ShouldUpdateStatus()
        {
            var category = new Category(1, 2, "Shoes", "SHO", "Sport shoes");
            category.ChangeCategory(null, null, null, null, EntityStatus.Inactive, 2);

            Assert.Equal(EntityStatus.Inactive, category.Status);
            Assert.Equal(2, category.UpdateBy);
            Assert.NotNull(category.UpdateDate);
        }
    }
}
