using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Delete.v1;

public sealed record DeleteUomCommand(Guid Id) : IRequest<Response<Guid>>;
