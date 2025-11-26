using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        /// <summary>
        /// فیلد نام دسته‌بندی
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// فیلد توضیحات دسته‌بندی
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// فیلد محصولات مرتبط با این دسته‌بندی
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
