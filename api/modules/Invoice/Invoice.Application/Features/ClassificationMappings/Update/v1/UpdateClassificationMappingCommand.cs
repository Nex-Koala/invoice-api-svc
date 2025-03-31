using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Update.v1;

public sealed record UpdateClassificationMappingCommand(Guid Id, string? LhdnClassificationCode, Guid? ClassificationId)
    : IRequest<Response<Guid>>;
