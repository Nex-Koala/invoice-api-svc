using System.Collections.ObjectModel;
using System.Net;

namespace NexKoala.Framework.Core.Exceptions;
public class UnauthorizedException : GenericException
{
    public UnauthorizedException()
        : base("authentication failed", new Collection<string>(), HttpStatusCode.Unauthorized)
    {
    }
    public UnauthorizedException(string message)
       : base(message, new Collection<string>(), HttpStatusCode.Unauthorized)
    {
    }
}
