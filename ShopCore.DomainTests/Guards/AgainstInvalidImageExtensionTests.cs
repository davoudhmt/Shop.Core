using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstInvalidImageExtensionTests
    {
        [Fact]
        public void AgainstInvalidImageExtension_WithNullOrWhitespace_ShouldNotThrow()
        {
            string? input1 = null;
            string? input2 = "";
            string? input3 = "   ";

            var ex1 = Record.Exception(() => Guard.AgainstInvalidImageExtension(input1, "پسوند تصویر نامعتبر است"));
            var ex2 = Record.Exception(() => Guard.AgainstInvalidImageExtension(input2, "پسوند تصویر نامعتبر است"));
            var ex3 = Record.Exception(() => Guard.AgainstInvalidImageExtension(input3, "پسوند تصویر نامعتبر است"));

            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Null(ex3);
        }

        [Theory]
        [InlineData("file.txt")]
        [InlineData("document.pdf")]
        [InlineData("image")]
        public void AgainstInvalidImageExtension_WithInvalidExtension_ShouldThrow(string input)
        {
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidImageExtension(input, "پسوند تصویر نامعتبر است"));
        }

        [Theory]
        [InlineData("photo.jpg")]
        [InlineData("picture.JPEG")]
        [InlineData("icon.png")]
        [InlineData("banner.gif")]
        [InlineData("artwork.bmp")]
        [InlineData("vector.svg")]
        [InlineData("compressed.webp")]
        [InlineData("scan.tiff")]
        public void AgainstInvalidImageExtension_WithValidExtension_ShouldNotThrow(string input)
        {
            var ex = Record.Exception(() => Guard.AgainstInvalidImageExtension(input, "پسوند تصویر نامعتبر است"));
            Assert.Null(ex);
        }
    }
}
