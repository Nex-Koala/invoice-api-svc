using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Delete.v1;

public sealed class DeleteUomHandler(
    ILogger<DeleteUomHandler> logger,
    [FromKeyedServices("invoice:uoms")] IRepository<Uom> repository
) : IRequestHandler<DeleteUomCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(DeleteUomCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var uom = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = uom ?? throw new UomNotFoundException(request.Id);
        await repository.DeleteAsync(uom, cancellationToken);
        logger.LogInformation("Uom with id : {UomId} deleted", uom.Id);

        return new Response<Guid>(uom.Id, "UOM deleted successfully.");
    }
}
