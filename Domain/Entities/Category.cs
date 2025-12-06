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
    public class Category : BaseEntity
    {
        /// <summary>
        /// در این فیلد شناسه دسته‌بندی والد نگهداری می‌شود
        /// </summary>
        public long? ParentCategoryId { get; private set; }

        /// <summary>
        /// فیلد نام دسته‌بندی
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// فیلد کد دسته‌بندی
        /// شامل 3 کاراکتر بزرگ است
        /// </summary>
        public CategoryCode CategoryCode { get; private set; } = null!;

        /// <summary>
        /// فیلد توضیحات دسته‌بندی
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected Category() : base()
        {
        }

        /// <summary>
        /// این متد سازنده دسته‌بندی است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="parentCategoryId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Category(int createBy, long? parentCategoryId, string name, string categoryCode, string? description) : base(createBy)
        {
            InitializeCategory(parentCategoryId, name, categoryCode, description);
        }

        /// <summary>
        /// در این متد مقادیر فیلدهای دسته‌بندی براساس شناسه دسته‌بندی والد مقداردهی اولیه می‌شوند
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        private void InitializeCategory(long? parentCategoryId, string name, string categoryCode, string? description)
        {
            var normalizedName = name.Trim();
            var normalizeDescription = description?.Trim();

            Guard.AgainstInvalidId(parentCategoryId, "شناسه دسته‌بندی والد نامعتبر است");
            Guard.AgainstNullOrWhiteSpace(normalizedName, "نام دسته‌بندی نمی‌تواند خالی باشد");
            Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام دسته‌بندی باید بین 2 تا 100 کاراکتر باشد");
            Guard.AgainstMaxLength(normalizeDescription, 500, "توضیحات دسته‌بندی نمی‌تواند بیشتر از 500 کاراکتر باشد");
            Guard.AgainstSelfReference(Id, parentCategoryId, "دسته‌بندی نمی‌تواند والد خود باشد");

            ParentCategoryId = parentCategoryId;
            Name = normalizedName;
            CategoryCode = new CategoryCode(categoryCode);
            Description = normalizeDescription;
        }

        /// <summary>
        /// این متد برای ویرایش اطلاعات دسته‌بندی استفاده می‌شود
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeCategory(long? parentCategoryId, string? name, string? categoryCode, string? description, EntityStatus? status, int updateBy)
        {
            bool hasChanges = false;
            if (parentCategoryId is not null && parentCategoryId != ParentCategoryId)
            {
                Guard.AgainstInvalidId(parentCategoryId, "شناسه دسته‌بندی والد نامعتبر است");
                Guard.AgainstSelfReference(Id, parentCategoryId, "دسته‌بندی نمی‌تواند والد خود باشد");
                ParentCategoryId = parentCategoryId.Value;
                hasChanges = true;
            }
            if (name is not null && !String.Equals(name.Trim(), Name, StringComparison.Ordinal))
            {
                var normalizedName = name.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedName, "نام دسته بندی نمی تواند خالی باشد.");
                Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام دسته‌بندی باید بین 2 تا 100 کاراکتر باشد");
                Name = normalizedName;
                hasChanges = true;
            }
            if (categoryCode is not null)
            {
                var newCategoryCode = new CategoryCode(categoryCode);
                if (newCategoryCode != CategoryCode)
                {
                    CategoryCode = newCategoryCode;
                    hasChanges = true;
                }
            }
            if (description is not null && !String.Equals(description.Trim(), Description, StringComparison.Ordinal))
            {
                var normalizedDescription = description.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedDescription, "توضیحات دسته بندی نمی تواند خالی باشد");
                Guard.AgainstMaxLength(normalizedDescription, 500, "توضیحات دسته‌بندی نمی‌تواند بیشتر از 500 کاراکتر باشد");
                Description = normalizedDescription;
                hasChanges = true;
            }
            if (status.HasValue && status != Status)
            {
                ChangeStatus(status.Value);
                hasChanges = true;
            }
            if (hasChanges)
            {
                ChangeUpdateInfo(updateBy);
            }
        }
    }
}
