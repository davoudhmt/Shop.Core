using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        /// <summary>
        /// مقدار شناسه محصول در صورتی که تصویر به صورت مشترک برای محصول تعریف شده باشد
        /// </summary>
        public long? ProductID { get; set; }

        /// <summary>
        /// مقدار شناسه واریانت محصول در صورتی که تصویر به صورت اختصاصی برای واریانت تعریف شده باشد
        /// </summary>
        public long? ProductVariantId { get; set; }

        /// <summary>
        /// فیلد آدرس تصویر
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// فیلد مشخص می‌کند که آیا این تصویر، تصویر اصلی محصول است یا خیر
        /// </summary>
        public bool IsMain { get; set; }
    }
}
