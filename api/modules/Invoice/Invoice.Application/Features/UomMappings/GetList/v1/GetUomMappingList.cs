using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.GetList.v1;

public record GetUomMappingList(Guid UserId, int PageNumber = 1, int PageSize = int.MaxValue)
    : IRequest<PagedResponse<UomMappingResponse>>;
