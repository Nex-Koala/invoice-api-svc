using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

public class GetClassificationMappingRequest : IRequest<Response<ClassificationMappingResponse>>
{
    public Guid Id { get; set; }

    public GetClassificationMappingRequest(Guid id) => Id = id;
}
