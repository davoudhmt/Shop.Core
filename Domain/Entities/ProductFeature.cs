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
        public long CategoryId { get; set; }

        /// <summary>
        /// فیلد کلید ویژگی
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
