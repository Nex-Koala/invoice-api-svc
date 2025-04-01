using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetPaymentMethods.v1;

public sealed class GetPaymentMethodsHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetPaymentMethods, Response<List<PaymentMethodResponse>>>
{
    public async Task<Response<List<PaymentMethodResponse>>> Handle(
        GetPaymentMethods request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetPaymentMethodsAsync();

        if (item == null)
            throw new GenericException("Failed to get payment methods.");

        return new Response<List<PaymentMethodResponse>>(item);
    }
}
