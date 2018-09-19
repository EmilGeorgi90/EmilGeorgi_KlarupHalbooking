using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    [Serializable]
    internal class SerializerException : Exception
    {
        /// <summary>Creates a new object of <see cref="SerializationException"/> with the provided message and exception to wrap.</summary>
        /// <param name="message">The message describing the cause of the communications error.</param>
        /// <param name="innerException">The exception to wrap as an inner exception.</param>
        public SerializerException(string message, Exception innerException) : base(message, innerException)
        {
            // Do not modify.
        }
    }
}
