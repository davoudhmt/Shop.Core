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
        /// فیلد شناسه محصول
        /// </summary>
        public long ProductID { get; set; }

        /// <summary>
        /// فیلد محصول
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// فیلد آدرس تصویر
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// فیلد مشخص می‌کند که آیا این تصویر، تصویر اصلی محصول است یا خیر
        /// </summary>
        public bool IsMain { get; set; }
    }
}
