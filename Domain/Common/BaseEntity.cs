using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        /// <summary>
        /// فیلد شناسه یکتا
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// فیلد تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// فیلد تاریخ آخرین به‌روزرسانی
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// فیلد وضعیت
        /// </summary>
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        /// <summary>
        /// فیلد شناسه کاربر ایجادکننده
        /// </summary>
        public int CreateBy { get; set; }

        /// <summary>
        /// فیلد شناسه کاربر آخرین به‌روزرسانی
        /// </summary>
        public int? UpdateBy { get; set; }

        /// <summary>
        /// فیلد ردیابی نسخه برای کنترل همزمانی
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
