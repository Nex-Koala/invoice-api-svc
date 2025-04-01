using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.GetList.v1;

public record GetUomList(Guid UserId, int PageNumber = 1, int PageSize = int.MaxValue)
    : IRequest<PagedResponse<UomResponse>>;
