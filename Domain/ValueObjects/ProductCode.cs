using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class ProductCode : ValueObject
    {
        public string Value { get; }

        public ProductCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("کد محصول نمی‌تواند خالی باشد.");

            var normalize = value.Trim();

            if (normalize.Length != 6)
                throw new DomainException("کد محصول باید دقیقاً 6 کاراکتر باشد.");

            var categoryPart = normalize[..3]; //value.Substring(0, 3);
            var numericPart = normalize[3..]; //value.Substring(3);

            if (!categoryPart.All(char.IsLetter) || !numericPart.All(char.IsDigit))
            {
                throw new DomainException("کد محصول باید شامل 3 حرف و 3 عدد باشد.");
            }

            Value = normalize.ToUpperInvariant();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString() => Value;
    }
}
