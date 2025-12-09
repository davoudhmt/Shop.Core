using Domain.Entities;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class ProductImageTest
    {
        [Fact]
        public void Constructor_WithValidProductId_ShouldInitializeProperties()
        {
            var image = new ProductImage(1, 10, null, "image.png");

            Assert.Equal(10, image.ProductId);
            Assert.True(image.IsMain);
            Assert.Equal("image.png", image.ImageUrl);
        }

        [Fact]
        public void Constructor_WithValidVariantId_ShouldInitializeProperties()
        {
            var image = new ProductImage(1, null, 20, "variant.png");

            Assert.Equal(20, image.ProductVariantId);
            Assert.False(image.IsMain);
            Assert.Equal("variant.png", image.ImageUrl);
        }

        [Fact]
        public void Constructor_WithoutIds_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductImage(1, null, null, "image.png")
            );
        }

        [Fact]
        public void ChangeProductImage_WithValidProductId_ShouldUpdateProperties()
        {
            var image = new ProductImage(1, 10, null, "image.png");
            image.ChangeProductImage(15, null, null, null, 2);

            Assert.Equal(15, image.ProductId);
            Assert.True(image.IsMain);
            Assert.Equal(2, image.UpdateBy);
            Assert.NotNull(image.UpdateDate);
        }

        [Fact]
        public void ChangeProductImage_WithValidVariantId_ShouldUpdateProperties()
        {
            var image = new ProductImage(1, null, 20, "variant.png");
            image.ChangeProductImage(null, 25, null, null, 2);

            Assert.Equal(25, image.ProductVariantId);
            Assert.False(image.IsMain);
            Assert.Equal(2, image.UpdateBy);
            Assert.NotNull(image.UpdateDate);
        }

        [Fact]
        public void AssignToVariant_WithNonMainImage_ShouldUpdateVariantId()
        {
            var image = new ProductImage(1, null, 20, "variant.png");
            image.AssignToVariant(30, 2);

            Assert.Equal(30, image.ProductVariantId);
            Assert.Equal(2, image.UpdateBy);
            Assert.NotNull(image.UpdateDate);
        }

        [Fact]
        public void AssignToVariant_WithMainImage_ShouldThrowDomainException()
        {
            var image = new ProductImage(1, 10, null, "image.png");

            Assert.Throws<DomainException>(() =>
                image.AssignToVariant(20, 2)
            );
        }
    }
}
