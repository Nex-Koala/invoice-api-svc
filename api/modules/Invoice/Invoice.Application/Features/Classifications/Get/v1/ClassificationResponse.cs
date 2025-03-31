using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;

public sealed record ClassificationResponse(Guid Id, Guid UserId, string Code, string? Description);
