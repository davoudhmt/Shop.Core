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
        ///  در این فیلد شناسه دسته‌بندی محصول نگهداری می‌شود
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// در این فیلد شناسه برند محصول نگهداری می‌شود
        /// </summary>
        public long? BrandId { get; set; }

        /// <summary>
        /// این فیلد کد SKU یکتای محصول را نگهداری می‌کند
        /// </summary>
        public string SKU { get; set; } = string.Empty;

        /// <summary>
        /// مجموعه ویژگی‌های مشترک محصول
        /// </summary>
        public ICollection<ProductFeatureValue> SharedFeatures { get; set; } = new List<ProductFeatureValue>();

        /// <summary>
        /// مجموعه واریانت‌های محصول
        /// </summary>
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
