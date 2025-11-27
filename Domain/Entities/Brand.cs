using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        /// <summary>
        /// فیلد نام برند
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// فیلد توضیحات برند
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// فیلد آدرس تصویر برند
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}
