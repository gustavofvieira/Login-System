using System.Runtime.Serialization;

namespace Luiza.Labs.Domain.Exceptions
{
    [Serializable]
    public class RecoverPasswordException : Exception
    {
        public RecoverPasswordException()
        {
        }

        public RecoverPasswordException(string? message) : base(message)
        {
        }

        public RecoverPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecoverPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
