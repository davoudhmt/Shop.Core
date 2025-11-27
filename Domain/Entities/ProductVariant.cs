using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        /// <summary>
        /// مقدار شناسه محصول
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// مقدار قیمت محصول
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// مقدار موجودی محصول
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// مقدار کد SKU محصول
        /// </summary>
        public string SKU { get; set; } = string.Empty;

        /// <summary>
        /// مجموعه مقادیر ویژگی‌های محصول
        /// </summary>
        public ICollection<ProductFeatureValue> VariantFeatures { get; set; } = new List<ProductFeatureValue>();

        /// <summary>
        /// مجموعه تصاویر محصول
        /// </summary>
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
