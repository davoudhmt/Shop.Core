using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    /// <summary>
    /// CategoryCode یک Value Object است که کد دسته‌بندی محصول را نگهداری می‌کند.
    /// این کلاس مسئول اعتبارسنجی مقدار ورودی و تضمین درستی آن است.
    /// </summary>
    public class CategoryCode : ValueObject
    {
        /// <summary>
        /// مقدار نهایی و معتبر کد دسته‌بندی
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// سازنده کلاس که قوانین اعتبارسنجی را اعمال می‌کند.
        /// </summary>
        /// <param name="value">کد دسته‌بندی ورودی</param>
        /// <exception cref="DomainException">اگر مقدار نامعتبر باشد خطا پرتاب می‌شود</exception>
        public CategoryCode(string value)
        {
            var normalize = value.Trim();

            if (string.IsNullOrWhiteSpace(normalize))
                throw new DomainException("کد دسته‌بندی نمی‌تواند خالی باشد.");

            if (normalize.Length != 3)
                throw new DomainException("کد دسته‌بندی باید دقیقاً ۳ کاراکتر باشد.");

            if (!normalize.All(char.IsLetter))
                throw new DomainException("کد دسته‌بندی باید فقط شامل حروف باشد.");

            Value = normalize.ToUpperInvariant();
        }

        /// <summary>
        /// اجزای مقایسه‌ای برای بررسی برابری دو CategoryCode
        /// </summary>
        /// <returns>لیست اجزای مقایسه‌ای</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// نمایش رشته‌ای مقدار کد دسته‌بندی
        /// </summary>
        /// <returns>کد دسته‌بندی به صورت رشته</returns>
        public override string ToString() => Value;
    }
}
