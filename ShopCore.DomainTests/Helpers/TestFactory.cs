using Domain.Common;
using Domain.Entities;
using System.Reflection;

namespace ShopCore.DomainTests.Helpers
{
    public static class TestFactory
    {
        public static ProductVariant CreateProductVariantForTest(long id, int createBy, long productId, decimal price, decimal? discountPercent, int stock, string sku)
        {
            var variant = new ProductVariant(createBy, productId, price, discountPercent, stock, sku);
            typeof(BaseEntity)
                .GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
                .SetValue(variant, id);
            return variant;
        }
    }
}
