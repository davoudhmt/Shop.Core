using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using ShopCore.DomainTests.Helpers;

namespace ShopCore.DomainTests.Entities
{
    public class ProductVariantTest
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            var variant = new ProductVariant(1, 10, 1000, 10, 5, "ABC1234567");

            Assert.Equal(10, variant.ProductId);
            Assert.Equal(1000, variant.Price);
            Assert.Equal(10, variant.DiscountPercent);
            Assert.Equal(5, variant.Stock);
            Assert.Equal("ABC1234567", variant.SKU.Value);
        }

        [Fact]
        public void Constructor_WithInvalidPrice_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductVariant(1, 10, 0, 10, 5, "ABC1234567")
            );
        }

        [Fact]
        public void ChangeProductVariant_WithValidPrice_ShouldUpdatePrice()
        {
            var variant = new ProductVariant(1, 10, 1000, 10, 5, "ABC1234567");
            variant.ChangeProductVariant(1200, null, null, null, null, 2);

            Assert.Equal(1200, variant.Price);
            Assert.Equal(2, variant.UpdateBy);
            Assert.NotNull(variant.UpdateDate);
        }

        [Fact]
        public void AddVariantFeature_WithDuplicate_ShouldThrowDomainException()
        {
            var variant = TestFactory.CreateProductVariantForTest(1, 1, 10, 1000, 10, 5, "ABC1234567");
            var feature1 = new ProductFeatureValue(1, null, 1, 1, "Red");
            var feature2 = new ProductFeatureValue(1, null, 1, 1, "Red");

            variant.AddVariantFeature(feature1, FeatureMultiplicity.Single, 2);

            Assert.Throws<DomainException>(() =>
                variant.AddVariantFeature(feature2, FeatureMultiplicity.Single, 2)
            );
        }

        [Fact]
        public void HasSameFeaturesAs_WithSameFeatures_ShouldThrowDomainException()
        {
            var variant1 = TestFactory.CreateProductVariantForTest(1, 1, 10, 1000, 10, 5, "ABC1234567");
            var variant2 = TestFactory.CreateProductVariantForTest(2, 1, 10, 1000, 10, 5, "XYZ9876543");

            var feature = new ProductFeatureValue(1, null, 1, 1, "Red");
            variant1.AddVariantFeature(feature, FeatureMultiplicity.Single, 2);

            var feature2 = new ProductFeatureValue(1, null, 1, 1, "Red");
            variant2.AddVariantFeature(feature2, FeatureMultiplicity.Single, 2);

            Assert.Throws<DomainException>(() =>
                variant1.HasSameFeaturesAs(variant2)
            );
        }
    }
}
