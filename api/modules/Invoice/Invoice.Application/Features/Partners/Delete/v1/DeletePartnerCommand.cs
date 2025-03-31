using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Delete.v1;

public sealed record DeletePartnerCommand(Guid Id) : IRequest<Response<Guid>>;
