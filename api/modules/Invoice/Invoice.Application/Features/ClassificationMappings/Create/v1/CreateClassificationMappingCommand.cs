using System.ComponentModel;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Create.v1;

public sealed record CreateClassificationMappingCommand(
    [property: DefaultValue("Lhdn Code")] string LhdnClassificationCode,
    [property: DefaultValue("Classification Id")] Guid ClassificationId
) : IRequest<Response<Guid>>;
