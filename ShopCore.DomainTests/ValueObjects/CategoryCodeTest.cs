using Domain.Exceptions;
using Domain.ValueObjects;

namespace ShopCore.DomainTests.ValueObjects
{
    public class CategoryCodeTest
    {
        [Theory]
        [InlineData("abc", "ABC")]
        [InlineData(" Abc ", "ABC")]
        [InlineData("XYZ", "XYZ")]
        public void CreateCategoryCode_WithValidLetters_ShouldNormalize(string input, string expected)
        {
            var code = new CategoryCode(input);
            Assert.Equal(expected, code.Value);
            Assert.Equal(expected, code.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void CreateCategoryCode_WithEmptyOrWhitespace_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new CategoryCode(input));
        }

        [Theory]
        [InlineData("AB")]
        [InlineData("ABCD")]
        [InlineData("ABCDEF")]
        public void CreateCategoryCode_WithInvalidLength_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new CategoryCode(input));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("!@#")]
        [InlineData("a12")]
        public void CreateCategoryCode_WithInvalidInput_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => new CategoryCode(input));
        }

        [Fact]
        public void ToString_ShouldReturnNormalizedValue()
        {
            var code = new CategoryCode("abc");
            Assert.Equal("ABC", code.ToString());
        }

        [Fact]
        public void Equality_WithSameNormalizedValue_ShouldBeEqual()
        {
            var c1 = new CategoryCode("abc");
            var c2 = new CategoryCode("abc");
            Assert.Equal(c1, c2);
            Assert.True(c1.Equals(c2));
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void Equality_WithDifferentValues_ShouldNotBeEqual()
        {
            var c1 = new CategoryCode("ABC");
            var c2 = new CategoryCode("XYZ");

            Assert.NotEqual(c1, c2);
            Assert.False(c1.Equals(c2));
        }

    }
}
