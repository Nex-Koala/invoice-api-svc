using Microsoft.Extensions.DependencyInjection;
using NexKoala.WebApi.Catalog.Domain.Exceptions;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Caching;
using NexKoala.WebApi.Catalog.Domain;
using MediatR;

namespace NexKoala.WebApi.Catalog.Application.Products.Get.v1;
public sealed class GetProductHandler(
    [FromKeyedServices("catalog:products")] IReadRepository<Product> repository,
    ICacheService cache)
    : IRequestHandler<GetProductRequest, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"product:{request.Id}",
            async () =>
            {
                var productItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (productItem == null) throw new ProductNotFoundException(request.Id);
                return new ProductResponse(productItem.Id, productItem.Name, productItem.Description, productItem.Price);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
