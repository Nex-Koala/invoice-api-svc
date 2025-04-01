using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Delete.v1;

public sealed record DeleteUomMappingCommand(Guid Id) : IRequest<Response<Guid>>;
