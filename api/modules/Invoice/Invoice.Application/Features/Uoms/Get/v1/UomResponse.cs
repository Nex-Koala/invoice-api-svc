using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;

public sealed record UomResponse(Guid Id, Guid UserId, string Code, string? Description);
