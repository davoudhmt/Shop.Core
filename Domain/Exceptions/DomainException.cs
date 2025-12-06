using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        /// <summary>
        /// در این متد سازنده استثناهای دامنه تعریف می‌شود
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message)
        {
        }

        /// <summary>
        /// در این متد سازنده استثناهای دامنه با استثنای داخلی تعریف می‌شود
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
