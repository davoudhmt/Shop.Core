using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;
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
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// توضیحات محصول
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        /// <summary>
        ///  در این فیلد شناسه دسته‌بندی محصول نگهداری می‌شود
        /// </summary>
        public long CategoryId { get; private set; }

        /// <summary>
        /// در این فیلد شناسه برند محصول نگهداری می‌شود
        /// </summary>
        public long? BrandId { get; private set; }

        /// <summary>
        /// این فیلد کد ProductCode یکتای محصول را نگهداری می‌کند
        /// شامل 6 کاراکتر است
        /// </summary>
        public ProductCode ProductCode { get; private set; } = null!;

        /// <summary>
        /// مجموعه ویژگی‌های مشترک محصول
        /// </summary>
        public ICollection<ProductFeatureValue> SharedFeatures { get; private set; } = new List<ProductFeatureValue>();

        /// <summary>
        /// مجموعه واریانت‌های محصول
        /// </summary>
        public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected Product() : base()
        {
        }

        /// <summary>
        /// این متد سازنده محصول است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="categoryId"></param>
        /// <param name="brandId"></param>
        /// <param name="sku"></param>
        public Product(int createBy, string name, string description, long categoryId, long? brandId, string productCode) : base(createBy)
        {
            InitializeProduct(name, description, categoryId, brandId, productCode);
        }

        /// <summary>
        /// در این متد مقادیر محصول مقداردهی اولیه می‌شوند
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="categoryId"></param>
        /// <param name="brandId"></param>
        /// <param name="sku"></param>
        private void InitializeProduct(string name, string description, long categoryId, long? brandId, string productCode)
        {
            var normalizedName = name.Trim();
            var normalizedDescription = description.Trim();

            Guard.AgainstNullOrWhiteSpace(normalizedName, "نام محصول نمی‌تواند خالی باشد.");
            Guard.AgainstInvalidLength(normalizedName, 2, 200, "نام محصول باید بین 2 تا 200 کاراکتر باشد.");
            Guard.AgainstNullOrWhiteSpace(normalizedDescription, "توضیحات محصول نمی‌تواند خالی باشد.");
            Guard.AgainstInvalidLength(normalizedDescription, 20, 1000, "توضیحات محصول باید بین 20 تا 1000 کاراکتر باشد.");
            Guard.AgainstInvalidId(categoryId, "شناسه دسته‌بندی محصول معتبر نمی‌باشد.");
            if(brandId is not null)
            {
                Guard.AgainstInvalidId(brandId.Value, "شناسه برند محصول معتبر نمی‌باشد.");
            }

            Name = normalizedName;
            Description = normalizedDescription;
            CategoryId = categoryId;
            BrandId = brandId;
            ProductCode = new ProductCode(productCode);
        }

        /// <summary>
        /// این متد برای ویرایش اطلاعات محصول استفاده می‌شود
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="categoryId"></param>
        /// <param name="brandId"></param>
        /// <param name="sku"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeProduct(string? name, string? description, long? categoryId, long? brandId, string? productCode, EntityStatus? status, int updateBy)
        {
            bool hasChange = false;
            if (name is not null && !String.Equals(name.Trim(), Name, StringComparison.Ordinal))
            {
                var normalizedName = name.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedName, "نام محصول نمی‌تواند خالی باشد.");
                Guard.AgainstInvalidLength(normalizedName, 2, 200, "نام محصول باید بین 2 تا 200 کاراکتر باشد.");
                Name = normalizedName;
                hasChange = true;
            }
            if (description is not null && !String.Equals(description.Trim(), Description, StringComparison.Ordinal))
            {
                var normalizedDescription = description.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedDescription, "توضیحات محصول نمی‌تواند خالی باشد.");
                Guard.AgainstInvalidLength(normalizedDescription, 20, 1000, "توضیحات محصول باید بین 20 تا 1000 کاراکتر باشد.");
                Description = normalizedDescription;
                hasChange = true;
            }
            if (categoryId is not null && categoryId != CategoryId)
            {
                Guard.AgainstInvalidId(categoryId.Value, "شناسه دسته‌بندی محصول معتبر نمی‌باشد.");
                CategoryId = categoryId.Value;
                hasChange = true;
            }
            if (brandId is not null && brandId != BrandId)
            {
                Guard.AgainstInvalidId(brandId.Value, "شناسه برند محصول معتبر نمی‌باشد.");
                BrandId = brandId.Value;
                hasChange = true;
            }
            if (productCode is not null)
            {
                var newProductCode = new ProductCode(productCode);
                if (newProductCode != ProductCode)
                {
                    ProductCode = newProductCode;
                    hasChange = true;
                }
            }
            if (status is not null && status != Status)
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
        /// این متد برای افزودن یک ویژگی مشترک به محصول استفاده می‌شود
        /// </summary>
        /// <param name="featureValue"></param>
        public void AddSharedFeature(ProductFeatureValue featureValue, int updateBy)
        {
            Guard.AgainstNull(featureValue, "ویژگی محصول نمی‌تواند خالی باشد.");
            Guard.AgainstEmpty(featureValue.Value, "مقدار ویژگی محصول نمی‌تواند خالی باشد.");
            Guard.AgainstDuplicate(SharedFeatures.Select(f => f.Value), featureValue.Value, "مقدار ویژگی تکراری است.");
            SharedFeatures.Add(featureValue);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// متد حذف یک ویژگی مشترک از محصول
        /// </summary>
        /// <param name="productFeatureId"></param>
        /// <returns></returns>
        public void RemoveSharedFeature(long productFeatureId, int updateBy)
        {
            Guard.AgainstInvalidId(productFeatureId, "شناسه ویژگی محصول معتبر نمی‌باشد.");
            var feature = SharedFeatures.FirstOrDefault(f => f.ProductFeatureId == productFeatureId);
            Guard.AgainstNull(feature, "ویژگی محصول یافت نشد.");
            SharedFeatures.Remove(feature!);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// این متد برای افزودن یک واریانت به محصول استفاده می‌شود
        /// </summary>
        /// <param name="variant"></param>
        public void AddVariant(ProductVariant variant, int updateBy)
        {
            Guard.AgainstNull(variant, "واریانت محصول نمی‌تواند خالی باشد.");
            Guard.AgainstEmpty(variant.VariantFeatures, "مقدار ویژگی محصول نمی‌تواند خالی باشد.");
            foreach (var existingVariant in Variants)
            {
                existingVariant.HasSameFeaturesAs(variant);
            }
            Variants.Add(variant);
            ChangeUpdateInfo(updateBy);
        }

        /// <summary>
        /// متد حذف یک واریانت از محصول
        /// </summary>
        /// <param name="productVariantId"></param>
        /// <returns></returns>
        public void RemoveVariant(long productVariantId, int updateBy)
        {
            Guard.AgainstInvalidId(productVariantId, "شناسه واریانت محصول معتبر نمی‌باشد.");
            var variant = Variants.FirstOrDefault(v => v.Id == productVariantId);
            Guard.AgainstNull(variant, "واریانت محصول یافت نشد.");
            Variants.Remove(variant!);
            ChangeUpdateInfo(updateBy);
        }
    }
}
