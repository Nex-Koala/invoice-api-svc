using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

public class GetPartnerRequest : IRequest<Response<PartnerResponse>>
{
    public Guid Id { get; set; }

    public GetPartnerRequest(Guid id) => Id = id;
}
