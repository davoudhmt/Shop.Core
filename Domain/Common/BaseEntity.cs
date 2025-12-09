using Domain.Enums;
using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    /// <summary>
    /// این کلاس پایه برای تمام موجودیت‌ها است
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// فیلد شناسه یکتا
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// فیلد تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// فیلد تاریخ آخرین به‌روزرسانی
        /// </summary>
        public DateTime? UpdateDate { get; private set; }

        /// <summary>
        /// فیلد وضعیت
        /// </summary>
        public EntityStatus Status { get; private set; }

        /// <summary>
        /// فیلد شناسه کاربر ایجادکننده
        /// </summary>
        public int CreateBy { get; private set; }

        /// <summary>
        /// فیلد شناسه کاربر آخرین به‌روزرسانی
        /// </summary>
        public int? UpdateBy { get; private set; }

        /// <summary>
        /// فیلد ردیابی نسخه برای کنترل همزمانی
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; private set; }

        /// <summary>
        /// این متد برای استفاده توسط ORM‌ها است
        /// </summary>
        protected BaseEntity()
        {
        }

        public BaseEntity(int createBy)
        {
            InitializeCreation(createBy);
        }

        /// <summary>
        /// در این متد اعتبارسنجی ایجاد انجام می‌شود
        /// </summary>
        /// <param name="createBy"></param>
        /// <exception cref="DomainException"></exception>
        private void InitializeCreation(int createBy)
        {
            Guard.AgainstNegativeOrZero<int>(createBy, "شناسه کاربری نامعتبر است.");
            CreateDate = DateTime.UtcNow;
            Status = EntityStatus.Active;
            CreateBy = createBy;
        }

        /// <summary>
        /// در این متد اطلاعات به‌روزرسانی تغییر می‌کند
        /// </summary>
        /// <param name="updateBy"></param>
        public void ChangeUpdateInfo(int updateBy)
        {
            Guard.AgainstNegativeOrZero<int>(updateBy, "شناسه کاربری نامعتبر است.");
            UpdateBy = updateBy;
            UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// در این متد اعتبارسنجی به‌روزرسانی وضعیت انجام می‌شود
        /// </summary>
        /// <param name="status"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="DomainException"></exception>
        public virtual void ChangeStatus(EntityStatus status)
        {
            Guard.AgainstInvalidEnum<EntityStatus>(status, "وضعیت نامعتبر است.");
            Status = status;
        }
    }
}
