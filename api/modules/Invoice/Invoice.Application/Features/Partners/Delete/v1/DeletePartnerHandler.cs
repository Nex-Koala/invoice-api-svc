using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Identity.Users.Abstractions;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Delete.v1;

public sealed class DeletePartnerHandler(
    ILogger<DeletePartnerHandler> logger,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> repository, IUserService userService
) : IRequestHandler<DeletePartnerCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var partner = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = partner ?? throw new PartnerNotFoundException(request.Id);

        await userService.DeleteAsync(partner.UserId);

        await repository.DeleteAsync(partner, cancellationToken);
        logger.LogInformation("Partner with id : {PartnerId} deleted", partner.Id);

        return new Response<Guid>(partner.Id, "Partner deleted successfully.");
    }
}
