using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        /// <summary>
        /// فیلد نام برند
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// فیلد توضیحات برند
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// فیلد آدرس تصویر برند
        /// </summary>
        public string? ImageUrl { get; private set; }

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected Brand() : base()
        {
        }

        /// <summary>
        /// این متد سازنده برند است
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="name"></param>
        public Brand(int createBy, string name, string? description, string? imageUrl) : base(createBy)
        {
            InitializeBrand(name, description, imageUrl);
        }

        /// <summary>
        /// در این متد مقادیر برند مقداردهی اولیه می‌شوند
        /// </summary>
        /// <param name="createBy"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        private void InitializeBrand(string name, string? description, string? imageUrl)
        {
            var normalizedName = name.Trim();
            var normalizedDescription = description?.Trim();
            var normalizedImageUrl = imageUrl?.Trim();

            Guard.AgainstNullOrWhiteSpace(normalizedName, "نام برند نمی تواند خالی باشد");
            Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام برند باید بین 2 تا 100 کاراکتر باشد");
            Guard.AgainstMaxLength(normalizedDescription, 500, "توضیحات برند نمی تواند بیشتر از 500 کاراکتر باشد");
            Guard.AgainstInvalidUrl(normalizedImageUrl, "آدرس تصویر برند معتبر نمی باشد");
            Guard.AgainstInvalidImageExtension(normalizedImageUrl, "آدرس تصویر برند معتبر نمی باشد");
            
            Name = normalizedName;
            Description = normalizedDescription;
            ImageUrl = normalizedImageUrl;
        }

        /// <summary>
        /// در این متد مقادیر برند تغییر می‌کنند
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        public void ChangeBrand(string? name, string? description, string? imageUrl, EntityStatus? status, int updateBy)
        {
            bool hasChanged = false;
            if (name is not null && !String.Equals(name.Trim(), Name, StringComparison.Ordinal))
            {
                var normalizedName = name.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedName, "نام برند نمی تواند خالی باشد");
                Guard.AgainstInvalidLength(normalizedName, 2, 100, "نام برند باید بین 2 تا 100 کاراکتر باشد");
                Name = normalizedName;
                hasChanged = true;
            }
            if (description is not null && !String.Equals(description.Trim(), Description, StringComparison.Ordinal))
            {
                var normalizedDescription = description.Trim();
                Guard.AgainstNullOrWhiteSpace(normalizedDescription, "توضیحات برند نمی تواند خالی باشد.");
                Guard.AgainstMaxLength(normalizedDescription, 500, "توضیحات برند نمی تواند بیشتر از 500 کاراکتر باشد");
                Description = normalizedDescription;
                hasChanged = true;
            }
            if (imageUrl is not null && !String.Equals(imageUrl.Trim(), ImageUrl, StringComparison.Ordinal))
            {
                var normalizedImageUrl = imageUrl.Trim();
                Guard.AgainstInvalidUrl(normalizedImageUrl, "آدرس تصویر برند معتبر نمی باشد");
                Guard.AgainstInvalidImageExtension(normalizedImageUrl, "آدرس تصویر برند معتبر نمی باشد");
                ImageUrl = normalizedImageUrl;
                hasChanged = true;
            }
            if (status.HasValue && status != Status)
            {
                ChangeStatus(status.Value);
                hasChanged = true;
            }
            if (hasChanged)
            {
                ChangeUpdateInfo(updateBy);
            }
        }
    }
}
