using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstMaxLengthTests
    {
        [Fact]
        public void AgainstMaxLength_WithNullOrWhitespace_ShouldNotThrow()
        {
            string? input1 = null;
            string? input2 = "";
            string? input3 = "   ";

            var ex1 = Record.Exception(() => Guard.AgainstMaxLength(input1, 5, "طول رشته نباید بیشتر از 5 باشد"));
            var ex2 = Record.Exception(() => Guard.AgainstMaxLength(input2, 5, "طول رشته نباید بیشتر از 5 باشد"));
            var ex3 = Record.Exception(() => Guard.AgainstMaxLength(input3, 5, "طول رشته نباید بیشتر از 5 باشد"));

            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Null(ex3);
        }

        [Theory]
        [InlineData("TooLongValue")] // طول بیشتر از maxLength
        [InlineData("123456")]       // طول دقیقاً بیشتر از 5
        public void AgainstMaxLength_WithValueLongerThanMax_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstMaxLength(input, 5, "طول رشته نباید بیشتر از 5 باشد"));
        }
        [Theory]
        [InlineData("A")]     // کوتاه‌تر از maxLength
        [InlineData("Hello")] // برابر با maxLength
        public void AgainstMaxLength_WithValidLength_ShouldNotThrow(string input)
        {
            var ex = Record.Exception(() => Guard.AgainstMaxLength(input, 5, "طول رشته نباید بیشتر از 5 باشد"));
            Assert.Null(ex);
        }
    }
}
