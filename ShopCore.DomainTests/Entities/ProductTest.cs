using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using ShopCore.DomainTests.Helpers;

namespace ShopCore.DomainTests.Entities
{
    public class ProductTest
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            var product = new Product(1, "Laptop", new string('a', 30), 10, 5, "ABC123");

            Assert.Equal("Laptop", product.Name);
            Assert.Equal(10, product.CategoryId);
            Assert.Equal(5, product.BrandId);
            Assert.Equal("ABC123", product.ProductCode.Value);
        }

        [Fact]
        public void Constructor_WithInvalidName_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new Product(1, "", new string('a', 30), 10, 5, "ABC123")
            );
        }

        [Fact]
        public void ChangeProduct_WithValidName_ShouldUpdateName()
        {
            var product = new Product(1, "Laptop", new string('a', 30), 10, 5, "ABC123");
            product.ChangeProduct("Tablet", null, null, null, null, null, 2);

            Assert.Equal("Tablet", product.Name);
            Assert.Equal(2, product.UpdateBy);
            Assert.NotNull(product.UpdateDate);
        }

        [Fact]
        public void AddSharedFeature_WithDuplicateValue_ShouldThrowDomainException()
        {
            var product = new Product(1, "Laptop", new string('a', 30), 10, 5, "ABC123");
            var feature1 = new ProductFeatureValue(1, 10, null, 1, "Red");
            var feature2 = new ProductFeatureValue(1, 10, null, 1, "Red");

            product.AddSharedFeature(feature1, 2);

            Assert.Throws<DomainException>(() =>
                product.AddSharedFeature(feature2, 2)
            );
        }

        [Fact]
        public void AddVariant_WithDuplicateFeatures_ShouldThrowDomainException()
        {
            var product = new Product(1, "Laptop", new string('a', 30), 10, 5, "ABC123");

            var variant1 = TestFactory.CreateProductVariantForTest(1, 1, 10, 1000, 10, 5, "SKU1234567");
            var variant2 = TestFactory.CreateProductVariantForTest(2, 1, 10, 1000, 10, 5, "SKU7654321");

            var feature1 = new ProductFeatureValue(1, null, 1, 1, "Red");
            var feature2 = new ProductFeatureValue(1, null, 1, 1, "Red");

            variant1.AddVariantFeature(feature1, FeatureMultiplicity.Single, 2);
            variant2.AddVariantFeature(feature2, FeatureMultiplicity.Single, 2);

            product.AddVariant(variant1, 2);

            Assert.Throws<DomainException>(() =>
                product.AddVariant(variant2, 2)
            );
        }
    }
}
