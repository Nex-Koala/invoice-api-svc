using System.Collections.ObjectModel;
using System.Net;

namespace NexKoala.Framework.Core.Exceptions;
public class NotFoundException : GenericException
{
    public NotFoundException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.NotFound)
    {
    }
}
