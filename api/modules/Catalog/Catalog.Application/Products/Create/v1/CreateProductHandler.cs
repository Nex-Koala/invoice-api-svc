﻿using NexKoala.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;

namespace NexKoala.WebApi.Catalog.Application.Products.Create.v1;
public sealed class CreateProductHandler(
    ILogger<CreateProductHandler> logger,
    [FromKeyedServices("catalog:products")] IRepository<Product> repository)
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var product = Product.Create(request.Name!, request.Description, request.Price);
        await repository.AddAsync(product, cancellationToken);
        logger.LogInformation("product created {ProductId}", product.Id);
        return new CreateProductResponse(product.Id);
    }
}
