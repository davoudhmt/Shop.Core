using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductFeatureValue : BaseEntity
    {
        /// <summary>
        /// مقدار شناسه محصول
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// مقدار شناسه ویژگی محصول
        /// </summary>
        public long ProductFeatureId { get; set; }

        /// <summary>
        /// مقدار ویژگی محصول
        /// </summary>
        public string Value { get; set; } = string.Empty;
    }
}
