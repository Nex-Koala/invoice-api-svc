using MediatR;

namespace NexKoala.WebApi.Catalog.Application.Products.Update.v1;
public sealed record UpdateProductCommand(
    Guid Id,
    string? Name,
    decimal Price,
    string? Description = null) : IRequest<UpdateProductResponse>;
