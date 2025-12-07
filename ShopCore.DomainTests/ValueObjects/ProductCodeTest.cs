using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.ValueObjects
{
    public class ProductCodeTest
    {
        [Theory]
        [InlineData("abc123", "ABC123")]
        [InlineData(" abc123 ", "ABC123")]
        public void ProductCode_WithValidInput_ShouldBeNormalizeValue(string input, string expected)
        {
            var code = new ProductCode(input);
            Assert.Equal(expected, code.Value);
            Assert.Equal(expected,code.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ProductCode_WithNullOrWhiteSpaceInput_ShouldThrow(string? input)
        {
            Assert.Throws<DomainException>(() => new ProductCode(input!));
        }

        [Theory]
        [InlineData("abc12")]
        [InlineData("abc1234")]
        public void ProductCode_WithInvalidLength_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new ProductCode(input));
        }

        [Theory]
        [InlineData("123ABC")]
        [InlineData("A1BC23")]
        public void ProductCode_WithInvalidStructure_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new ProductCode(input));
        }

        [Fact]
        public void ToString_ShouldReturnNormalizedValue()
        {
            var code = new ProductCode("abc123");
            Assert.Equal("ABC123", code.ToString());
        }

        [Fact]
        public void Equality_WithSameNormalizedValue_ShouldBeEqual()
        {
            var c1 = new ProductCode("abc123");
            var c2 = new ProductCode("ABC123");
            Assert.Equal(c1, c2);
            Assert.True(c1.Equals(c2));
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void Equality_WithDifferentValues_ShouldNotBeEqual()
        {
            var c1 = new ProductCode("ABC123");
            var c2 = new ProductCode("XYZ789");

            Assert.NotEqual(c1, c2);
            Assert.False(c1.Equals(c2));
        }
    }
}
