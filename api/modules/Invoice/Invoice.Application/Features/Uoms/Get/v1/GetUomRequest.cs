using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;

public class GetUomRequest : IRequest<Response<UomResponse>>
{
    public Guid Id { get; set; }

    public GetUomRequest(Guid id) => Id = id;
}
