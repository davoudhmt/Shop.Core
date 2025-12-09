using Domain.Entities;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class ProductFeatureValueTest
    {
        [Fact]
        public void Constructor_WithValidProductId_ShouldInitializeProperties()
        {
            var featureValue = new ProductFeatureValue(1, 10, null, 5, "Red");

            Assert.Equal(10, featureValue.ProductId);
            Assert.Null(featureValue.ProductVariantId);
            Assert.Equal(5, featureValue.ProductFeatureId);
            Assert.Equal("Red", featureValue.Value);
        }

        [Fact]
        public void Constructor_WithBothIds_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductFeatureValue(1, 10, 20, 5, "Red")
            );
        }

        [Fact]
        public void Constructor_WithoutIds_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductFeatureValue(1, null, null, 5, "Red")
            );
        }

        [Fact]
        public void ChangeProductFeatureValue_WithValidVariantId_ShouldUpdateProperties()
        {
            var featureValue = new ProductFeatureValue(1, 10, null, 5, "Red");
            featureValue.ChangeProductFeatureValue(null, 20, null, null, 2);

            Assert.Equal(20, featureValue.ProductVariantId);
            Assert.Null(featureValue.ProductId);
            Assert.Equal(2, featureValue.UpdateBy);
            Assert.NotNull(featureValue.UpdateDate);
        }

        [Fact]
        public void ChangeProductFeatureValue_WithInvalidValue_ShouldThrowDomainException()
        {
            var featureValue = new ProductFeatureValue(1, 10, null, 5, "Red");

            Assert.Throws<DomainException>(() =>
                featureValue.ChangeProductFeatureValue(null, null, null, "", 2)
            );
        }

        [Fact]
        public void AssignToVariant_ShouldUpdateVariantIdAndClearProductId()
        {
            var featureValue = new ProductFeatureValue(1, 10, null, 5, "Red");
            featureValue.AssignToVariant(20, 2);

            Assert.Equal(20, featureValue.ProductVariantId);
            Assert.Null(featureValue.ProductId);
            Assert.Equal(2, featureValue.UpdateBy);
            Assert.NotNull(featureValue.UpdateDate);
        }

        [Fact]
        public void AssignToProduct_ShouldUpdateProductIdAndClearVariantId()
        {
            var featureValue = new ProductFeatureValue(1, null, 20, 5, "Red");
            featureValue.AssignToProduct(10, 2);

            Assert.Equal(10, featureValue.ProductId);
            Assert.Null(featureValue.ProductVariantId);
            Assert.Equal(2, featureValue.UpdateBy);
            Assert.NotNull(featureValue.UpdateDate);
        }
    }
}
