using System.Runtime.Serialization;

namespace Luiza.Labs.Domain.Exceptions
{
    [Serializable]
    public class TemplateEmailException : Exception
    {
        public TemplateEmailException()
        {
        }

        public TemplateEmailException(string? message) : base(message)
        {
        }

        public TemplateEmailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TemplateEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
