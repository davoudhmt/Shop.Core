using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
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
        public long ProductId { get; private set; }

        /// <summary>
        /// مقدار قیمت محصول
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// مقدار درصد تخفیف
        /// </summary>
        public decimal? DiscountPercent { get; private set; }

        /// <summary>
        /// مقدار موجودی محصول
        /// </summary>
        public int Stock { get; private set; }

        /// <summary>
        /// مقدار کد SKU محصول
        /// شامل 10 کاراکتر است
        /// </summary>
        public SKU SKU { get; private set; } = null!;

        /// <summary>
        /// مجموعه مقادیر ویژگی‌های محصول
        /// </summary>
        public ICollection<ProductFeatureValue> VariantFeatures { get; private set; } = new List<ProductFeatureValue>();

        /// <summary>
        /// مجموعه تصاویر محصول
        /// </summary>
        public ICollection<ProductImage> ProductImages { get; private set; } = new List<ProductImage>();

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected ProductVariant() : base()
        {
        }

        /// <summary>
        /// متد سازنده واریانت محصول
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="sku"></param>
        public ProductVariant(int createBy, long productId, decimal price, decimal? discountPercent, int stock, string sku) : base(createBy)
        {
            InitializeProductVariant(productId, price, discountPercent, stock, sku);
        }

        /// <summary>
        /// متد کمکی برای مقداردهی اولیه واریانت محصول
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="sku"></param>
        private void InitializeProductVariant(long productId, decimal price, decimal? discountPercent, int stock, string sku)
        {

            Guard.AgainstInvalidId(productId, "شناسه محصول نامعتبر است.");
            Guard.AgainstNegativeOrZero<decimal>(price, "قیمت محصول نمی تواند منفی یا صفر باشد.");
            Guard.AgainstOutOfRange(discountPercent, 0, 100, "مقدار درصد تخفیف باید بین 0 تا 100 باشد.");
            Guard.AgainstNegative<int>(stock, "موجودی محصول نمی تواند منفی باشد.");

            ProductId = productId;
            Price = price;
            Stock = stock;
            SKU = new SKU(sku);
        }


        public void ChangeProductVariant(decimal? price, decimal? discountPercent, int? stock, string? sku, EntityStatus? status, int updateBy)
        {
            bool hasChange = false;
            if (price is not null && price != Price)
            {
                Guard.AgainstNegativeOrZero<decimal>(price.Value, "قیمت محصول نمی تواند منفی یا صفر باشد.");
                Price = price.Value;
                hasChange = true;
            }
            if (discountPercent is not null && discountPercent != DiscountPercent)
            {
                Guard.AgainstOutOfRange(discountPercent, 0, 100, "مقدار درصد تخفیف باید بین 0 تا 100 باشد.");
                DiscountPercent = discountPercent.Value;
                hasChange = true;
            }
            if (stock is not null && stock != Stock)
            {
                Guard.AgainstNegative<int>(stock.Value, "موجودی محصول نمی تواند منفی باشد.");
                Stock = stock.Value;
                hasChange = true;
            }
            if (sku is not null)
            {
                var newSKU = new SKU(sku);
                if (newSKU != SKU)
                {
                    SKU = newSKU;
                    hasChange = true;
                }
            }
            if (status.HasValue && status != Status)
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
        /// این متد یک مقدار ویژگی جدید به واریانت محصول اضافه می‌کند
        /// </summary>
        /// <param name="variantfeature"></param>
        /// <param name="multiplicity"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="DomainException"></exception>
        public void AddVariantFeature(ProductFeatureValue variantfeature, FeatureMultiplicity multiplicity, int updateBy)
        {
            Guard.AgainstNull(variantfeature, "مقدار ویژگی محصول نمی تواند خالی باشد.");
            Guard.AgainstInvalidId(variantfeature.ProductFeatureId, "شناسه ویژگی محصول نامعتبر است.");
            Guard.AgainstNullOrWhiteSpace(variantfeature.Value, "مقدار ویژگی محصول نمی تواند خالی باشد.");
            Guard.AgainstInvalidEnum<FeatureMultiplicity>(multiplicity, "نوع چندگانگی ویژگی محصول نامعتبر است.");

            bool isDuplicate = VariantFeatures.Any(vf => vf.ProductFeatureId == variantfeature.ProductFeatureId && vf.Value == variantfeature.Value);
            if (isDuplicate)
            {
                throw new DomainException("مقدار ویژگی محصول تکراری است.");
            }
            if (multiplicity == FeatureMultiplicity.Single)
            {
                bool alreadyExists = VariantFeatures.Any(vf => vf.ProductFeatureId == variantfeature.ProductFeatureId);
                if (alreadyExists)
                {
                    throw new DomainException("این ویژگی محصول تنها یک مقدار می تواند داشته باشد.");
                }
            }
            variantfeature.AssignToVariant(this.Id, updateBy);
            VariantFeatures.Add(variantfeature);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantFeatureId"></param>
        /// <param name="updateBy"></param>
        public void RemoveVariantFeature(long variantFeatureId, int updateBy)
        {
            Guard.AgainstInvalidId(variantFeatureId, "شناسه ویژگی محصول نامعتبر است.");
            var featureToRemove = VariantFeatures.FirstOrDefault(vf => vf.Id == variantFeatureId);
            Guard.AgainstNull(featureToRemove, "ویژگی محصول مورد نظر یافت نشد.");
            VariantFeatures.Remove(featureToRemove!);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// در این متد یک تصویر جدید به واریانت محصول اضافه می‌شود
        /// </summary>
        /// <param name="productImage"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="DomainException"></exception>
        public void AddProductImage(ProductImage productImage, int updateBy)
        {
            var normalizedUrl = productImage.ImageUrl.Trim().ToLower();
            bool isDuplicate = ProductImages.Any(pi => pi.ImageUrl.Trim().ToLower() == normalizedUrl);
            if (isDuplicate)
            {
                throw new DomainException("تصویر محصول تکراری است.");
            }

            productImage.AssignToVariant(this.Id, updateBy);
            ProductImages.Add(productImage);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// در این متد یک تصویر از واریانت محصول حذف می‌شود
        /// </summary>
        /// <param name="productImageId"></param>
        /// <param name="updateBy"></param>
        public void RemoveProductImage(long productImageId, int updateBy)
        {
            Guard.AgainstInvalidId(productImageId, "شناسه تصویر محصول نامعتبر است.");
            var imageToRemove = ProductImages.FirstOrDefault(pi => pi.Id == productImageId);
            Guard.AgainstNull(imageToRemove, "تصویر محصول مورد نظر یافت نشد.");
            ProductImages.Remove(imageToRemove!);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// این متد بررسی میکند که آیا واریانت محصول فعلی دارای ویژگی‌های مشابه با واریانت محصول دیگر است یا خیر
        /// </summary>
        /// <param name="otherVariant"></param>
        /// <exception cref="DomainException"></exception>
        public void HasSameFeaturesAs(ProductVariant otherVariant)
        {
            Guard.AgainstNull(otherVariant, "واریانت محصول مورد نظر نمی تواند خالی باشد.");
            Guard.AgainstSelfReference(this.Id, otherVariant.Id, "واریانت محصول نمی تواند با خودش مقایسه شود.");

            var thisFeatures = this.VariantFeatures.Select(vf => new {vf.ProductFeatureId, vf.Value }).ToHashSet();
            var otherFeatures = otherVariant.VariantFeatures.Select(vf => new { vf.ProductFeatureId, vf.Value }).ToHashSet();
            if (thisFeatures.SetEquals(otherFeatures))
            {
                throw new DomainException("واریانت محصول با ویژگی‌های مشابه وجود دارد.");
            }
        }
    }
}
