using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Update.v1;

public sealed record UpdateUomMappingCommand(Guid Id, string? LhdnUomCode, Guid? UomId)
    : IRequest<Response<Guid>>;
