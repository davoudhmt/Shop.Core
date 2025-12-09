using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class ProductFeatureValue : BaseEntity
    {
        /// <summary>
        /// مقدار شناسه محصول در صورتی که ویژگی به صورت مشترک برای محصول تعریف شده باشد
        /// </summary>
        public long? ProductId { get; private set; }

        /// <summary>
        /// مقدار شناسه واریانت محصول در صورتی که ویژگی به صورت اختصاصی برای واریانت تعریف شده باشد
        /// </summary>
        public long? ProductVariantId { get; private set; }

        /// <summary>
        /// مقدار شناسه ویژگی محصول
        /// </summary>
        public long ProductFeatureId { get; private set; }

        /// <summary>
        /// مقدار ویژگی محصول
        /// </summary>
        public string Value { get; private set; } = null!;

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected ProductFeatureValue() : base()
        {
        }

        /// <summary>
        /// این متد سازنده ویژگی محصول است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="productFeatureId"></param>
        /// <param name="value"></param>
        public ProductFeatureValue(int createBy, long? productId, long? productVariantId, long productFeatureId, string value) : base(createBy)
        {
            InitializeProductFeatureValue(productId, productVariantId, productFeatureId, value);
        }

        /// <summary>
        /// این متد مقادیر ویژگی محصول را مقداردهی اولیه می‌کند
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="productFeatureId"></param>
        /// <param name="value"></param>
        private void InitializeProductFeatureValue(long? productId, long? productVariantId, long productFeatureId, string value)
        {
            var normalizedValue = value.Trim();

            if (productId.HasValue && productVariantId.HasValue)
            {
                throw new DomainException("فقط یکی از شناسه محصول یا شناسه واریانت محصول می‌تواند مقداردهی شود.");
            }
            if (!productId.HasValue && !productVariantId.HasValue)
            {
                throw new DomainException("حداقل یکی از شناسه محصول یا شناسه واریانت محصول باید مقداردهی شود.");
            }
            if (productId.HasValue)
            {
                Guard.AgainstInvalidId(productId.Value, "شناسه محصول نامعتبر است.");
            }
            if (productVariantId.HasValue)
            {
                Guard.AgainstInvalidId(productVariantId.Value, "شناسه واریانت محصول نامعتبر است.");
            }
            Guard.AgainstInvalidId(productFeatureId, "شناسه ویژگی محصول نامعتبر است.");
            Guard.AgainstNullOrWhiteSpace(normalizedValue, "مقدار ویژگی محصول نمی‌تواند خالی باشد.");
            Guard.AgainstInvalidLength(normalizedValue, 2, 500, "مقدار ویژگی محصول باید بین 2 تا 500 کاراکتر باشد.");

            if (productId.HasValue)
            {
                ProductId = productId;
            }
            if (productVariantId.HasValue)
            {
                ProductVariantId = productVariantId;
            }
            ProductFeatureId = productFeatureId;
            Value = normalizedValue;
        }

        /// <summary>
        /// در این متد مقدار ویژگی محصول ویرایش می‌شود.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="productFeatureId"></param>
        /// <param name="value"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeProductFeatureValue(long? productId, long? productVariantId, long? productFeatureId, string? value, int updateBy)
        {
            bool hasChange = false;
            if (productId.HasValue && productVariantId.HasValue)
            {
                throw new DomainException("فقط یکی از شناسه محصول یا شناسه واریانت محصول می‌تواند مقداردهی شود.");
            }
            if (!productId.HasValue && !productVariantId.HasValue)
            {
                throw new DomainException("حداقل یکی از شناسه محصول یا شناسه واریانت محصول باید مقداردهی شود.");
            }
            if (productId.HasValue && productId != ProductId)
            {
                Guard.AgainstInvalidId(productId.Value, "شناسه محصول نامعتبر است.");
                ProductId = productId;
                ProductVariantId = null;
                hasChange = true;
            }
            if (productVariantId.HasValue && productVariantId != ProductVariantId)
            {
                Guard.AgainstInvalidId(productVariantId.Value, "شناسه واریانت محصول نامعتبر است.");
                ProductVariantId = productVariantId;
                ProductId = null;
                hasChange = true;
            }
            if (productFeatureId is not null && productFeatureId != ProductFeatureId)
            {
                Guard.AgainstInvalidId(productFeatureId.Value, "شناسه ویژگی محصول نامعتبر است.");
                ProductFeatureId = productFeatureId.Value;
                hasChange = true;
            }
            if (value is not null && !String.Equals(value.Trim(), Value, StringComparison.OrdinalIgnoreCase))
            {
                var normalizedValue = value.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedValue, "مقدار ویژگی محصول نمی‌تواند خالی باشد.");
                Guard.AgainstInvalidLength(normalizedValue, 2, 500, "مقدار ویژگی محصول باید بین 2 تا 500 کاراکتر باشد.");
                Value = normalizedValue;
                hasChange = true;
            }
            if (hasChange)
            {
                ChangeUpdateInfo(updateBy);
            }
        }

        public void AssignToVariant(long productVariantId, int updateBy)
        {
            Guard.AgainstInvalidId(productVariantId, "شناسه واریانت محصول نامعتبر است.");
            ProductVariantId = productVariantId;
            ProductId = null;
            ChangeUpdateInfo(updateBy);
        }

        public void AssignToProduct(long productId, int updateBy)
        {
            Guard.AgainstInvalidId(productId, "شناسه محصول نامعتبر است.");
            ProductId = productId;
            ProductVariantId = null;
            ChangeUpdateInfo(updateBy);
        }
    }
}
