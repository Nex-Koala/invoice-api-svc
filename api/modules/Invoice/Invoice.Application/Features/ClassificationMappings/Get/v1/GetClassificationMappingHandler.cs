using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

public sealed class GetClassificationMappingHandler(
    [FromKeyedServices("invoice:classificationMappings")] IReadRepository<ClassificationMapping> repository
) : IRequestHandler<GetClassificationMappingRequest, Response<ClassificationMappingResponse>>
{
    public async Task<Response<ClassificationMappingResponse>> Handle(
        GetClassificationMappingRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var classificationMappingItem = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (classificationMappingItem == null)
            throw new ClassificationMappingNotFoundException(request.Id);

        var response = new ClassificationMappingResponse(
            classificationMappingItem.Id,
            classificationMappingItem.ClassificationId,
            classificationMappingItem.LhdnClassificationCode
        );

        return new Response<ClassificationMappingResponse>(response);
    }
}
