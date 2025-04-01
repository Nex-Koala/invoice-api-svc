using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Delete.v1;

public sealed record DeleteClassificationCommand(Guid Id) : IRequest<Response<Guid>>;
