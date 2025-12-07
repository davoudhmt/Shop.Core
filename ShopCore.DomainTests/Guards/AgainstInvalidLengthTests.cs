using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstInvalidLengthTests
    {
        [Fact]
        public void AgainstInvalidLength_WithNullOrWhitespace_ShouldNotThrow()
        {
            string? input1 = null;
            string? input2 = "";
            string? input3 = "   ";

            var ex1 = Record.Exception(() => Guard.AgainstInvalidLength(input1, 2, 5, "طول نامعتبر است"));
            var ex2 = Record.Exception(() => Guard.AgainstInvalidLength(input2, 2, 5, "طول نامعتبر است"));
            var ex3 = Record.Exception(() => Guard.AgainstInvalidLength(input3, 2, 5, "طول نامعتبر است"));

            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Null(ex3);
        }

        [Theory]
        [InlineData("A")]       // کوتاه‌تر از minLength
        [InlineData("TooLongValue")] // بلندتر از maxLength
        public void AgainstInvalidLength_WithOutOfRangeLength_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidLength(input, 2, 5, "طول نامعتبر است"));
        }

        [Theory]
        [InlineData("AB")]   // برابر minLength
        [InlineData("Test")] // بین min و max
        [InlineData("Hello")] // برابر maxLength
        public void AgainstInvalidLength_WithValidLength_ShouldNotThrow(string input)
        {
            var ex = Record.Exception(() => Guard.AgainstInvalidLength(input, 2, 5, "طول نامعتبر است"));
            Assert.Null(ex);
        }
    }
}
