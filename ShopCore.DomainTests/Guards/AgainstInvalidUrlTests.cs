using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstInvalidUrlTests
    {
        [Fact]
        public void AgainstInvalidUrl_WithNullOrWhitespace_ShouldNotThrow()
        {
            string? input1 = null;
            string? input2 = "";
            string? input3 = "   ";

            var ex1 = Record.Exception(() => Guard.AgainstInvalidUrl(input1, "آدرس نامعتبر است"));
            var ex2 = Record.Exception(() => Guard.AgainstInvalidUrl(input2, "آدرس نامعتبر است"));
            var ex3 = Record.Exception(() => Guard.AgainstInvalidUrl(input3, "آدرس نامعتبر است"));

            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Null(ex3);
        }
        [Fact]
        public void AgainstInvalidUrl_WithInvalidRelativeUrl_ShouldThrow()
        {
            string input = "http://absolute.com"; // چون UriKind.Relative هست، این نامعتبره
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidUrl(input, "آدرس نامعتبر است"));
        }

        [Fact]
        public void AgainstInvalidUrl_WithRelativeUrlContainingDotDot_ShouldThrow()
        {
            string input = "../images/pic.png";
            var ex = Assert.Throws<DomainException>(() => Guard.AgainstInvalidUrl(input, "آدرس نامعتبر است"));
            Assert.Equal("مسیر نسبی نمی تواند شامل '..' باشد", ex.Message);
        }

        [Fact]
        public void AgainstInvalidUrl_WithValidRelativeUrl_ShouldNotThrow()
        {
            string input = "images/pic.png";
            var ex = Record.Exception(() => Guard.AgainstInvalidUrl(input, "آدرس نامعتبر است"));
            Assert.Null(ex);
        }
    }
}
