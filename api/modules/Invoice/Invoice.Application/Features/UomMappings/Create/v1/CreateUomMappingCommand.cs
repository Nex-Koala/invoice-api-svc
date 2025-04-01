using System.ComponentModel;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Create.v1;

public sealed record CreateUomMappingCommand(
    [property: DefaultValue("Lhdn Code")] string LhdnUomCode,
    [property: DefaultValue("Uom Id")] Guid UomId
) : IRequest<Response<Guid>>;
