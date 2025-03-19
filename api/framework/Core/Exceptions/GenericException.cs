using System.Net;

namespace NexKoala.Framework.Core.Exceptions;
public class GenericException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public GenericException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public GenericException(string message) : base(message)
    {
        ErrorMessages = new List<string>();
    }
}
