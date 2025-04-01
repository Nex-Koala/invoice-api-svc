using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.GetList.v1;

public record GetClassificationList(Guid UserId, int PageNumber = 1, int PageSize = int.MaxValue)
    : IRequest<PagedResponse<ClassificationResponse>>;
