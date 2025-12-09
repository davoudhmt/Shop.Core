using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace ShopCore.DomainTests.Entities
{
    public class ProductFeatureTest
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            var feature = new ProductFeature(1, 10, "Color", FeatureMultiplicity.Single);

            Assert.Equal(10, feature.CategoryId);
            Assert.Equal("Color", feature.Name);
            Assert.Equal(FeatureMultiplicity.Single, feature.Multiplicity);
            Assert.Equal(EntityStatus.Active, feature.Status);
        }

        [Fact]
        public void Constructor_WithInvalidCategoryId_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductFeature(1, 0, "Color", FeatureMultiplicity.Single)
            );
        }

        [Fact]
        public void Constructor_WithEmptyName_ShouldThrowDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new ProductFeature(1, 10, "", FeatureMultiplicity.Single)
            );
        }

        [Fact]
        public void ChangeProductFeature_WithValidName_ShouldUpdateNameAndUpdateDate()
        {
            var feature = new ProductFeature(1, 10, "Color", FeatureMultiplicity.Single);
            feature.ChangeProductFeature(null, "Size", null, null, 2);

            Assert.Equal("Size", feature.Name);
            Assert.Equal(2, feature.UpdateBy);
            Assert.NotNull(feature.UpdateDate);
        }

        [Fact]
        public void ChangeProductFeature_WithInvalidName_ShouldThrowDomainException()
        {
            var feature = new ProductFeature(1, 10, "Color", FeatureMultiplicity.Single);

            Assert.Throws<DomainException>(() =>
                feature.ChangeProductFeature(null, "", null, null, 2)
            );
        }

        [Fact]
        public void ChangeProductFeature_WithValidMultiplicity_ShouldUpdateMultiplicity()
        {
            var feature = new ProductFeature(1, 10, "Color", FeatureMultiplicity.Single);
            feature.ChangeProductFeature(null, null, FeatureMultiplicity.Multiple, null, 2);

            Assert.Equal(FeatureMultiplicity.Multiple, feature.Multiplicity);
            Assert.Equal(2, feature.UpdateBy);
            Assert.NotNull(feature.UpdateDate);
        }

        [Fact]
        public void ChangeProductFeature_WithValidStatus_ShouldUpdateStatus()
        {
            var feature = new ProductFeature(1, 10, "Color", FeatureMultiplicity.Single);
            feature.ChangeProductFeature(null, null, null, EntityStatus.Inactive, 2);

            Assert.Equal(EntityStatus.Inactive, feature.Status);
            Assert.Equal(2, feature.UpdateBy);
            Assert.NotNull(feature.UpdateDate);
        }
    }
}
