using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;

public sealed record UomMappingResponse(Guid Id, Guid UomId, string LhdnUomCode) { }
