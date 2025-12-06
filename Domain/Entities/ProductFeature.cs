using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductFeature : BaseEntity
    {
        /// <summary>
        /// فیلد شناسه محصول
        /// </summary>
        public long CategoryId { get; private set; }

        /// <summary>
        /// فیلد کلید ویژگی
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// در این فیلد چندگانگی ویژگی نگهداری می‌شود
        /// </summary>
        public FeatureMultiplicity Multiplicity { get; private set; }

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected ProductFeature() : base()
        {
        }

        /// <summary>
        /// این متد سازنده ویژگی محصول است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        public ProductFeature(int createBy, long categoryId, string name, FeatureMultiplicity multiplicity) : base(createBy)
        {
            InitializeProductFeature(categoryId, name, multiplicity);
        }

        /// <summary>
        /// این متد برای مقداردهی اولیه ویژگی محصول است
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        private void InitializeProductFeature(long categoryId, string name, FeatureMultiplicity multiplicity)
        {
            var normalizedName = name.Trim();

            Guard.AgainstInvalidId(categoryId, "شناسه دسته‌بندی نامعتبر است.");
            Guard.AgainstNullOrWhiteSpace(normalizedName, "نام ویژگی نمی‌تواند خالی باشد.");
            Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام ویژگی باید بین 2 تا 100 کاراکتر باشد.");
            Guard.AgainstInvalidEnum<FeatureMultiplicity>(multiplicity, "مقدار چندگانگی ویژگی نامعتبر است.");

            CategoryId = categoryId;
            Name = normalizedName;
            Multiplicity = multiplicity;
        }

        /// <summary>
        /// این متد برای ویرایش ویژگی محصول است
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeProductFeature(long? categoryId, string? name, FeatureMultiplicity? multiplicity, EntityStatus? status, int updateBy)
        {
            bool hasChange = false;
            if (categoryId is not null && categoryId != CategoryId)
            {
                Guard.AgainstInvalidId(categoryId, "شناسه دسته‌بندی نامعتبر است.");
                CategoryId = categoryId.Value;
                hasChange = true;
            }
            if (name is not null && !String.Equals(name.Trim(), Name, StringComparison.OrdinalIgnoreCase))
            {
                var normalizedName = name.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedName, "نام ویژگی محصول نمی تواند خالی باشد.");
                Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام ویژگی باید بین 2 تا 100 کاراکتر باشد.");
                Name = normalizedName;
                hasChange = true;
            }
            if(multiplicity is not null && multiplicity != Multiplicity)
            {
                Guard.AgainstInvalidEnum<FeatureMultiplicity>(multiplicity, "مقدار چندگانگی ویژگی نامعتبر است.");
                Multiplicity = multiplicity.Value;
                hasChange = true;
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
    }
}
