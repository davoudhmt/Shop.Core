using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.ValueObjects
{
    public class SKUTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("  ")]
        public void SKU_WithNullOrWhiteSpace_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new SKU(input!));
        }

        [Theory]
        [InlineData(" abc1234567", "ABC1234567")]
        [InlineData("abc1234567 ", "ABC1234567")]
        [InlineData(" abc1234567  ", "ABC1234567")]
        [InlineData("ABC9999999", "ABC9999999")]
        public void SKU_WithValidInput_ShouldNormalizedValue(string input, string expected)
        {
            var value = new SKU(input);
            Assert.Equal(expected, value.Value);
            Assert.Equal(expected, value.ToString());
        }

        [Theory]
        [InlineData("abc123456")]
        [InlineData("abcd132456")]
        [InlineData("abc12345678")]
        [InlineData("1abc234567")]
        public void SKU_WithInvalidInput_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new SKU(input));
        }

        [Fact]
        public void Equality_WithSameNormalizedValue_ShouldBeEqual()
        {
            var c1 = new SKU("abc1234567");
            var c2 = new SKU("ABC1234567");
            Assert.Equal(c1, c2);
            Assert.True(c1.Equals(c2));
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void Equality_WithDifferentValues_ShouldNotBeEqual()
        {
            var c1 = new SKU("ABC1234567");
            var c2 = new SKU("XYZ7654321");

            Assert.NotEqual(c1, c2);
            Assert.False(c1.Equals(c2));
        }
    }
}
