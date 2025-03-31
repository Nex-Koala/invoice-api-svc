using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.GetList.v1;

public record GetClassificationMappingList(Guid UserId, int PageNumber = 1, int PageSize = int.MaxValue)
    : IRequest<PagedResponse<ClassificationMappingResponse>>;
