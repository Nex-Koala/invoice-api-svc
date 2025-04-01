using System.ComponentModel;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Create.v1;

public sealed record CreateClassificationCommand(
    [property: DefaultValue("Classification Code")] string Code,
    [property: DefaultValue("Classification Description")] string Description,
    [property: DefaultValue("User Id")] Guid UserId
) : IRequest<Response<Guid>>;
