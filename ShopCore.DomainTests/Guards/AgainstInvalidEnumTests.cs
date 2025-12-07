using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.DomainTests.Guards
{
    public class AgainstInvalidEnumTests
    {
        [Fact]
        public void AgainstInvalidEnum_WithNull_ShouldNotThrow()
        {
            EntityStatus? status = null;
            var ex = Record.Exception(() => Guard.AgainstInvalidEnum(status, "وضعیت محصول معتبر نمی‌باشد"));
            Assert.Null(ex);
        }

        [Fact]
        public void AgainstInvalidEnum_WithValidEnumValue_ShouldNotThrow()
        {
            EntityStatus? status = EntityStatus.Active;
            var ex = Record.Exception(() => Guard.AgainstInvalidEnum(status, "وضعیت محصول معتبر نمی‌باشد"));
            Assert.Null(ex);
        }

        [Fact]
        public void AgainstInvalidEnum_WithInvalidEnumValue_ShouldThrow()
        {
            // مقدار 99 در ProductStatus تعریف نشده
            EntityStatus? status = (EntityStatus)99;
            Assert.Throws<DomainException>(() => Guard.AgainstInvalidEnum(status, "وضعیت محصول معتبر نمی‌باشد"));
        }
    }
}
