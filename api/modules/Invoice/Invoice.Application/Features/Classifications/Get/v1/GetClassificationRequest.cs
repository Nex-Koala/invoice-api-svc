using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;

public class GetClassificationRequest : IRequest<Response<ClassificationResponse>>
{
    public Guid Id { get; set; }

    public GetClassificationRequest(Guid id) => Id = id;
}
