using System.ComponentModel;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Create.v1;

public sealed record CreateUomCommand(
    [property: DefaultValue("Uom Code")] string Code,
    [property: DefaultValue("Uom Description")] string Description,
    [property: DefaultValue("User Id")] Guid UserId
) : IRequest<Response<Guid>>;
