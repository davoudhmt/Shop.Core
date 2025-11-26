using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        /// <summary>
        /// نام محصول
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// توضیحات محصول
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// قیمت محصول
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// تعداد محصول در انبار
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// این فیلد ویژگی‌های محصول را نگهداری می‌کند
        /// </summary>
        public ICollection<ProductFeature> Features { get; set; } = new List<ProductFeature>();

        /// <summary>
        /// این فیلد تصاویر محصول را نگهداری می‌کند
        /// </summary>
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        /// <summary>
        ///  در این فیلد شناسه دسته‌بندی محصول نگهداری می‌شود
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// در این فیلد دسته‌بندی محصول نگهداری می‌شود
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// در این فیلد شناسه برند محصول نگهداری می‌شود
        /// </summary>
        public long? BrandId { get; set; }

        /// <summary>
        /// در این فیلد برند محصول نگهداری می‌شود
        /// </summary>
        public Brand? Brand { get; set; }

        /// <summary>
        /// این فیلد کد SKU یکتای محصول را نگهداری می‌کند
        /// </summary>
        public string SKU { get; set; } = string.Empty;
    }
}
