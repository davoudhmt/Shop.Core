using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class BrandTest
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            var brand = new Brand(1, "Nike", "Sport brand", "logo.png");

            Assert.Equal("Nike", brand.Name);
            Assert.Equal("Sport brand", brand.Description);
            Assert.Equal("logo.png", brand.ImageUrl);
            Assert.Equal(EntityStatus.Active, brand.Status);
        }

        [Fact]
        public void Constructor_WithEmptyName_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new Brand(1, "", "Sport brand", "logo.png")
            );
        }

        [Fact]
        public void ChangeBrand_WithValidName_ShouldUpdateNameAndUpdateDate()
        {
            var brand = new Brand(1, "Nike", "Sport brand", "logo.png");
            brand.ChangeBrand("Adidas", null, null, null, 2);

            Assert.Equal("Adidas", brand.Name);
            Assert.Equal(2, brand.UpdateBy);
            Assert.NotNull(brand.UpdateDate);
        }

        [Fact]
        public void ChangeBrand_WithInvalidName_ShouldThrowDomainException()
        {
            var brand = new Brand(1, "Nike", "Sport brand", "logo.png");

            Assert.Throws<DomainException>(() =>
                brand.ChangeBrand("", null, null, null, 2)
            );
        }

        [Fact]
        public void ChangeBrand_WithValidStatus_ShouldUpdateStatus()
        {
            var brand = new Brand(1, "Nike", "Sport brand", "logo.png");
            brand.ChangeBrand(null, null, null, EntityStatus.Inactive, 2);

            Assert.Equal(EntityStatus.Inactive, brand.Status);
            Assert.Equal(2, brand.UpdateBy);
            Assert.NotNull(brand.UpdateDate);
        }
    }
}
