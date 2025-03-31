using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

public sealed record ClassificationMappingResponse(Guid Id, Guid ClassificationId, string LhdnClassificationCode) { }
