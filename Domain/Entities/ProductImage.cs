using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        /// <summary>
        /// مقدار شناسه محصول در صورتی که تصویر به صورت مشترک برای محصول تعریف شده باشد
        /// </summary>
        public long? ProductId { get; set; }

        /// <summary>
        /// مقدار شناسه واریانت محصول در صورتی که تصویر به صورت اختصاصی برای واریانت تعریف شده باشد
        /// </summary>
        public long? ProductVariantId { get; set; }

        /// <summary>
        /// فیلد آدرس تصویر
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// فیلد مشخص می‌کند که آیا این تصویر، تصویر اصلی محصول است یا خیر
        /// اگر مقدار True باشد متعلق به محصول است و اگر False باشد متعلق به واریانت است
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected ProductImage() : base()
        {
        }

        /// <summary>
        /// این متد سازنده تصویر محصول است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="imageUrl"></param>
        /// <param name="isMain"></param>
        public ProductImage(int createBy, long? productId, long? productVariantId, string imageUrl) : base(createBy)
        {
            InitializeProductImage(productId, productVariantId, imageUrl);
        }

        /// <summary>
        /// در این متد مقادیر تصویر محصول مقداردهی اولیه می‌شوند
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="imageUrl"></param>
        /// <param name="isMain"></param>
        /// <exception cref="DomainException"></exception>
        private void InitializeProductImage(long? productId, long? productVariantId, string imageUrl)
        {
            var normalizedImgaeUrl = imageUrl.Trim();

            if(!productId.HasValue && !productVariantId.HasValue)
            {
                throw new DomainException("حداقل یکی از شناسه‌های محصول یا واریانت محصول باید مقداردهی شود.");
            }
            if(productId.HasValue)
                Guard.AgainstInvalidId(productId.Value, "شناسه محصول نامعتبر است.");
            if(productVariantId.HasValue)
                Guard.AgainstInvalidId(productVariantId.Value, "شناسه واریانت محصول نامعتبر است.");

            Guard.AgainstNullOrWhiteSpace(normalizedImgaeUrl, "آدرس تصویر نمی‌تواند خالی باشد.");
            Guard.AgainstInvalidUrl(normalizedImgaeUrl, "آدرس تصویر نامعتبر است.");
            Guard.AgainstInvalidImageExtension(normalizedImgaeUrl, "فرمت تصویر نامعتبر است.");

            ProductId = productId;
            ProductVariantId = productVariantId;
            ImageUrl = normalizedImgaeUrl;
            IsMain = productVariantId.HasValue ? false : true;
        }

        /// <summary>
        /// این متد برای ویرایش اطلاعات تصویر محصول استفاده می‌شود
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="imageUrl"></param>
        /// <param name="isMain"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeProductImage(long? productId, long? productVariantId, string? imageUrl, EntityStatus? status, int updateBy)
        {
            bool hasChange = false;

            if (!productId.HasValue && !productVariantId.HasValue)
            {
                throw new DomainException("حداقل یکی از شناسه‌های محصول یا واریانت محصول باید مقداردهی شود.");
            }
            if(productId is not null && productId != ProductId)
            {
                Guard.AgainstInvalidId(productId.Value, "شناسه محصول نامعتبر است.");
                ProductId = productId;
                IsMain = true;
                hasChange = true;
            }
            if (productVariantId is not null && productVariantId != ProductId)
            {
                Guard.AgainstInvalidId(productVariantId.Value, "شناسه محصول نامعتبر است.");
                ProductVariantId = productVariantId;
                IsMain = false;
                hasChange = true;
            }
            if (imageUrl is not null && !String.Equals(imageUrl.Trim(), ImageUrl, StringComparison.OrdinalIgnoreCase))
            {
                var normalizedImgaeUrl = imageUrl.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedImgaeUrl, "آدرس تصویر نمی‌تواند خالی باشد.");
                Guard.AgainstInvalidUrl(normalizedImgaeUrl, "آدرس تصویر نامعتبر است.");
                Guard.AgainstInvalidImageExtension(normalizedImgaeUrl, "فرمت تصویر نامعتبر است.");
                ImageUrl = normalizedImgaeUrl;
                hasChange = true;
            }
            if (status is not null &&  status != Status)
            {
                ChangeStatus(status.Value);
                hasChange = true;
            }
            if (hasChange)
            {
                ChangeUpdateInfo(updateBy);
            }
        }

        /// <summary>
        /// متد افزودن تصویر به واریانت
        /// </summary>
        /// <param name="productVariantId"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="DomainException"></exception>
        public void AssignToVariant(long productVariantId, int updateBy)
        {
            if (IsMain)
            {
                throw new DomainException("تصویر اصلی نمی‌تواند به واریانت محصول اختصاص یابد.");
            }
            Guard.AgainstInvalidId(productVariantId, "شناسه واریانت محصول نامعتبر است.");
            ProductVariantId = productVariantId;
            ChangeUpdateInfo(updateBy);
        }
    }
}
