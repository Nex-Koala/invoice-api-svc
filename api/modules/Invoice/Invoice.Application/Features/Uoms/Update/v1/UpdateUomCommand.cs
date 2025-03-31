using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Update.v1;

public sealed record UpdateUomCommand(Guid Id, string? Code, string? Description = null)
    : IRequest<Response<Guid>>;
