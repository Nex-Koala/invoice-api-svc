using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Caching;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

public sealed class GetClassificationMappingHandler(
    [FromKeyedServices("invoice:classificationMappings")] IReadRepository<ClassificationMapping> repository,
    ICacheService cache
) : IRequestHandler<GetClassificationMappingRequest, Response<ClassificationMappingResponse>>
{
    public async Task<Response<ClassificationMappingResponse>> Handle(
        GetClassificationMappingRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"classificationMapping:{request.Id}",
            async () =>
            {
                var classificationMappingItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (classificationMappingItem == null)
                    throw new ClassificationMappingNotFoundException(request.Id);
                return new ClassificationMappingResponse(
                    classificationMappingItem.Id,
                    classificationMappingItem.ClassificationId,
                    classificationMappingItem.LhdnClassificationCode
                );
            },
            cancellationToken: cancellationToken
        );

        if (item == null)
            throw new ClassificationMappingNotFoundException(request.Id);

        return new Response<ClassificationMappingResponse>(item);
    }
}
