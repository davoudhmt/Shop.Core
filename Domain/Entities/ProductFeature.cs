using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductFeature : BaseEntity
    {
        /// <summary>
        /// فیلد شناسه محصول
        /// </summary>
        public long ProductID { get; set; }

        /// <summary>
        /// فیلد محصول
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// فیلد کلید ویژگی
        /// </summary>
        public required string Key { get; set; }

        /// <summary>
        /// فیلد مقدار ویژگی
        /// </summary>
        public required string Value { get; set; }
    }
}
