using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Update.v1;

public sealed record UpdateClassificationCommand(Guid Id, string? Code, string? Description = null)
    : IRequest<Response<Guid>>;
