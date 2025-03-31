using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Identity.Users.Abstractions;
using NexKoala.Framework.Core.Identity.Users.Features.RegisterUser;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;

public sealed class CreatePartnerCommandHandler(
    ILogger<CreatePartnerCommandHandler> logger,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> repository, IUserService userService
) : IRequestHandler<CreatePartnerCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreatePartnerCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        // create user account
        var userName = Regex.Replace(request.Name, @"[^a-zA-Z0-9]", "");
        var userRegisterResponse = await userService.RegisterAsync(new RegisterUserCommand
        {
            UserName = userName,
            FirstName = request.Name,
            LastName = request.CompanyName,
            Email = request.Email,
            Password = "123Pa$$word!",
        }, "Admin register", cancellationToken);

        var newPartner = Partner.Create(
            userRegisterResponse.UserId,
            request.Name,
            request.CompanyName,
            request.Address1,
            request.Address2,
            request.Address3,
            request.Email,
            request.Phone,
            request.LicenseKey,
            request.Status,
            request.MaxSubmissions
        );
        await repository.AddAsync(newPartner, cancellationToken);
        logger.LogInformation("Partner created {PartnerId}", newPartner.Id);
        return new Response<Guid>(newPartner.Id, "Partner created successfully.");
    }
}
