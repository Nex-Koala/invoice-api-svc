using NexKoala.WebApi.Catalog.Application.Products.Get.v1;
using MediatR;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Catalog.Application.Products.Search.v1;

public record SearchProductsCommand(PaginationFilter filter) : IRequest<PagedList<ProductResponse>>;
