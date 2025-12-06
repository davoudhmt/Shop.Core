using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public abstract class ValueObject
    {
        /// <summary>
        /// کلاس پایه برای تمام Value Object ها در دامنه.
        /// این کلاس منطق مقایسه و تولید HashCode را پیاده‌سازی می‌کند.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// این متد باید توسط کلاس‌های فرزند پیاده‌سازی شود
        /// و اجزای اصلی که برای مقایسه برابری استفاده می‌شوند را برگرداند.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            //اگر شیء ورودی تهی باشد یا نوع آن با نوع فعلی متفاوت باشد، برابر نیستند
            if (obj == null || obj.GetType() != GetType())
                return false;

            //مقایسه اجزای مساوی بودن
            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// تولید HashCode برای شیء فعلی بر اساس اجزای مقایسه‌ای.
        /// این متد برای استفاده در مجموعه‌ها و دیکشنری‌ها ضروری است.
        /// </summary>
        /// <returns>عدد Hash یکتا برای شیء فعلی</returns>
        public override int GetHashCode()
        {
            //محاسبه HashCode با استفاده از اجزای مقایسه‌ای
            return GetEqualityComponents().Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
        }
    }
}
