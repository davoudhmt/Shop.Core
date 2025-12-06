using Domain.Enums;
using Domain.Exceptions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class Guard
    {
        /// <summary>
        /// در این متد بررسی می‌شود که آیا مقدار ورودی null است یا خیر
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstNull(object? value, string message)
        {
            if (value == null)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// متد بررسی می‌کند که آیا مقدار Nullable ورودی null است یا خیر
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstNull<T>(T? value, string message) where T : struct
        {
            if (!value.HasValue)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// این متد بررسی می‌شود که آیا شناسه ورودی معتبر است یا خیر
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstInvalidId(long? id, string message)
        {
            if (id.HasValue && id.Value <= 0)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا مقدار ورودی از نوع Enum معتبر است یا خیر
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstInvalidEnum<TEnum>(TEnum? value, string message) where TEnum : struct, Enum
        {
            if (value.HasValue)
            {
                if (!Enum.IsDefined(typeof(TEnum), value.Value))
                {
                    throw new DomainException(message);
                }
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا رشته ورودی تهی یا null است یا خیر
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstNullOrWhiteSpace(string? value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا مقدار عددی ورودی منفی یا صفر است یا خیر
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstNegativeOrZero<T>(T? value, string message) where T : struct, IComparable<T>
        {
            if (value.HasValue && value.Value.CompareTo(default(T)) <= 0)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// این متد بررسی می‌شود که آیا مقدار عددی ورودی منفی است یا خیر
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstNegative<T>(T? value, string message) where T : struct, IComparable<T>
        {
            if (value.HasValue && value.Value.CompareTo(default(T)) < 0)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا مقدار عددی ورودی در بازه مشخص شده قرار دارد یا خیر
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstOutOfRange<T>(T? value, T min, T max, string message) where T : struct, IComparable<T>
        {
            if (value.HasValue && (value.Value.CompareTo(min) < 0 || value.Value.CompareTo(max) > 0))
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که رشته ورودی تهی یا null نباشد و آیا طول رشته ورودی در بازه مشخص شده قرار دارد یا خیر
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstInvalidLength(string? value, int minLength, int maxLength, string message)
        {
            if (!string.IsNullOrWhiteSpace(value) && (value.Length < minLength || value.Length > maxLength))
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا طول رشته ورودی از حداکثر طول مجاز بیشتر است یا خیر
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstMaxLength(string? value, int maxLength, string message)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا مجموعه ورودی شامل مقادیر تکراری است یا خیر
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstDuplicate<T>(IEnumerable<T> collection, T newValue, string message)
        {
            if (collection.Contains(newValue))
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا مجموعه ورودی خالی است یا خیر
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstEmpty<T>(IEnumerable<T> collection, string message)
        {
            if (!collection.Any())
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا رشته ورودی یک URL معتبر است یا خیر
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstInvalidUrl(string? url, string message)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            if (!Uri.TryCreate(url, UriKind.Relative, out _))
                throw new DomainException(message);

            if (url.Contains(".."))
                throw new DomainException("مسیر نسبی نمی تواند شامل '..' باشد");
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا رشته ورودی یک URL معتبر برای تصویر است یا خیر
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstInvalidImageExtension(string? url, string message)
        {
            if (string.IsNullOrWhiteSpace(url))
                return; // اجازه می‌دهیم URL خالی باشد

            var extension = Path.GetExtension(url);

            // استخراج پسوند فایل
            var validExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff"
            };

            if (string.IsNullOrWhiteSpace(extension) || !validExtensions.Contains(extension))
                throw new DomainException(message);
        }

        /// <summary>
        /// در این متد بررسی می‌شود که آیا یک مرجع به خود ایجاد نشده است یا خیر
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AgainstSelfReference(long? id, long? parentId, string message)
        {
            if (id.HasValue && parentId.HasValue && id.Value == parentId.Value)
            {
                throw new DomainException(message);
            }
        }
    }
}
