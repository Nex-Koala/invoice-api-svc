using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.GetByEmail.v1;

public record GetPartnerByEmailRequest(string Email) : IRequest<Response<PartnerResponse>>;
