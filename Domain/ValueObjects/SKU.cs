using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class SKU : ValueObject
    {
        public string Value { get; }

        public SKU(string value) 
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("کد یکت نمی‌تواند خالی باشد.");

            if (value.Length != 10)
                throw new DomainException("کد یکتا باید دقیقاً 10 کاراکتر باشد.");

            var categoryPart = value[..3];
            var numericPart = value[3..];

            if (!categoryPart.All(char.IsLetter) || !numericPart.All(char.IsDigit))
            {
                throw new DomainException("کد محصول باید شامل 3 حرف و 7 عدد باشد.");
            }

            Value = value.Trim().ToUpper();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString() => Value;
    }
}
