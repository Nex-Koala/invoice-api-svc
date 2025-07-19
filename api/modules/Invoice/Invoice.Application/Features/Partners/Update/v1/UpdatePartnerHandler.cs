using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Identity.Users.Abstractions;
using NexKoala.Framework.Core.Identity.Users.Features.ToggleUserStatus;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

public sealed class UpdatePartnerHandler(
    ILogger<UpdatePartnerHandler> logger,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> repository,
    IUserService userService
) : IRequestHandler<UpdatePartnerCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        UpdatePartnerCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var partner = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = partner ?? throw new PartnerNotFoundException(request.Id);

        if (request.Status != null && request.Status != partner.Status)
        {
            await userService.ToggleStatusAsync(new ToggleUserStatusCommand {
                ActivateUser = request.Status.Value,
                UserId = partner.UserId
                }, cancellationToken);

            partner.SyncStatus(request.Status.Value);
        }

        var updatedPartner = partner.Update(
            request.Name,
            request.CompanyName,
            request.Address1,
            request.Address2,
            request.Address3,
            request.Email,
            request.Phone,
            request.LicenseKey,
            request.MaxSubmissions
        );

        await repository.UpdateAsync(updatedPartner, cancellationToken);
        logger.LogInformation("Partner with id : {PartnerId} updated.", partner.Id);
        return new Response<Guid>(partner.Id, $"Partner with id : {partner.Id} updated.");
    }
}
