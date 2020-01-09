using System;
using System.Runtime.Serialization;

namespace Core.Services
{
    [Serializable]
    internal class DbUpdateException : Exception
    {
        public DbUpdateException()
        {
        }

        public DbUpdateException(string message) : base(message)
        {
        }

        public DbUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}