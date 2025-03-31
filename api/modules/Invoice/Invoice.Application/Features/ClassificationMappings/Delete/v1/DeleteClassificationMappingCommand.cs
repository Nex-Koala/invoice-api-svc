using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Delete.v1;

public sealed record DeleteClassificationMappingCommand(Guid Id) : IRequest<Response<Guid>>;
